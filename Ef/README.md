# Entity Framework Core 3.0 and WPF Core

This project demonstrates the use of Entity Framework (EF) coupled with WPF:
1. automatic creation of database schema.
2. rapid display and navigation through the data.

The database used for storing the entities is SQLite.

## SQLite

The [DatabaseContext](./EfData/DatabaseContext.cs) class derives from `DbContext` (from EF).

Override `OnConfiguring` to set up the connection string. In a basic form it is sufficient to just define the path to the database file.

```c#
protected override void OnConfiguring(DbContextOptionsBuilder options)
{
    options.UseSqlite($"Data Source={_path}");
}
```

## Schema

The database schema is defined using the EF fluent API. Override `OnModelCreating` to define the schema (see [DatabaseContext](./EfData/DatabaseContext.cs)).

```c#
protected override void OnModelCreating(ModelBuilder modelBuilder)
```

There are a number of collections that are defined in the demo:

```c#
public DbSet<PartType> PartTypes { get; set; }
public DbSet<Part> Parts { get; set; }
public DbSet<Feature> Features { get; set; }
```

Each collection contains entities:
1. `PartType`
2. `Part`
3. `Feature`

[PartType](./EfData/PartType.cs) defines the types of parts stored in the database.

[Part](./EfData/Part.cs) defines the parts that can be stored in the database. Each `Part` has a type of [PartType](./EfData/PartType.cs). Each `Part` contains `Features` - a collection of [Feature](./EfData/Feature.cs).

```c#
public class Part
{
    public long Id { get; set; }
    public PartType PartType { get; set; }
    public string Name { get; set; }
    public Status Status { get; set; }
    public ICollection<Feature> Features { get; set; }
}
```

[Feature](./EfData/Feature.cs) defines the characteristics of features on the part.

### Keys

By default EF treats fields on entities called `Id` as primary keys. Use `HasKey` to deviate from the pattern or to define explicitly.

```c#
modelBuilder.Entity<Part>().HasKey(part => part.Id);
modelBuilder.Entity<PartType>().HasKey(partType => partType.Id);
modelBuilder.Entity<Feature>().HasKey(feature => feature.Id);
```

### Required fields

Required fields are described using the fluent API in the following way.

```c#
modelBuilder.Entity<Part>().Property(part => part.Id).IsRequired();
```

### Required fields from other entities

[Part](./EfData/Part.cs) has a property that is an entity in another table `PartType`.

```c#
public PartType PartType { get; set; }
```

It is not possible to define the required field in the same way.

```c#
// will not work
modelBuilder.Entity<Part>().Property(part => part.PartType).IsRequired();
```

Instead, use the relationship fluent API to define a reference navigation property mapping from `Part.PartType` to the `PartType` entities using `HasOne` (one-to-one). Then define the reverse mapping, which in
this case is done with `WithMany` (many-to-one) since there are an entity of `PartType` will map to many `Part` entities (i.e. there can be many parts made of a particular part type). Finally, call
`IsRequired()` to indicate that `Part.PartType` must not be left undefined when it is written to the database.

```c#
modelBuilder.Entity<Part>().HasOne(part => part.PartType).WithMany().IsRequired();
```

Note, the mapping could have been defined in the opposite direction instead.

```c#
modelBuilder.Entity<PartType>().HasMany<Part>().WithOne(x => x.PartType).IsRequired();
```

## Migration

Always assume that migration will be used, which allows the database to go through upgrades as the product evolves over each release.

[DatabaseManager](./EfData/DatabaseManager.cs) defines the policy of migration.

If the database does not exist then the tables need to be created, otherwise migrate the existing database.

```c#
if (!databaseCreator.Exists())
{
    databaseCreator.CreateTables();
}
else
{
    context.Database.Migrate();
}
```

Note, avoid `databaseCreator.EnsureCreated();` as this will not allow migration.

## Data Model

The data model uses the database context to return data needed for the application.

The `PartCollection` function in [DataModel](./Ef/Model/DataModel.cs) returns all parts from the database.

```c#
public IQueryable<Part> PartCollection()
{
    return _context.Parts.Include(part => part.PartType).AsNoTracking();
}
```

### Read Only Entities - No Tracking

Use `AsNoTracking` to indicate that no changes will be written back to the database. This speeds up queries as entities will not need to be tracked and updated.

### Load Associated Entities - Eager Loading

Use the `Include()` API to load related entities. Without `Include(part => part.PartType)` in the `PartCollection()` function, only the `Part` would be loaded, and the reference `PartType` would be `null`.
