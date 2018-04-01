# Fields

This code demonstrates a structure of fields. The root fields are a `FieldGroup` that can contain zero or more fields of type `Field` and/or zero or more fields
of type `FieldGroup`. This structure allows a tree of fields to be defined that could represent structured values such as application settings or configuration
of application behaviour.

`FieldGroup` is defined in [FieldGroup.cs](./Model/FieldGroup.cs).

## Field Structure

A `Field` has a number of properties.

|Field|Type|Description|
|---|---|---|
|`Id`|`string`|Identifier for a field|
|`RawValue`|`string`|Input value (from UI or text serialised data)|
|`Value`|`T`|Generic type `T` that represents the strongly-typed value|
|`HasValue`|`bool`|Value indicates whether the `RawValue` can be successfully converted to a `Value`|

The field interface is split into two components:
* `IField` - defines the common type-independent properties - `Id`, `RawValue` and `HasValue`.
* `IField<T>` - defines the type-specific property - `Value`.

Both interfaces are defined in [IField.cs](./Model/IField.cs) with `IField<T>` derived from `IField`.

The advantage of this split is that `FieldGroup` can have a list of `IField` fields in one container with differing `Value` types. WPF will also allow binding to the
`Value` property of an `IField` even though the property is defined in `IField<T>`.

## View Model

There is a view model [ViewModel.cs](./WpfApp/ViewModel.cs) that defines the root `FieldGroup`, and it also defines a `Field` as a demonstration.

## View

There is a view [MainWindow.xaml](./WpfApp/MainWindow.xaml) that binds to the view model.

The view implements a number of data templates for each variation of `Field` that could be found in the `FieldGroup` list of fields (`Field`).

Typical numeric or string data templates may share a generic template such as the `DefaultFieldTemplate`. This shows field values in text boxes.

```xml
<DataTemplate x:Key="DefaultFieldTemplate" DataType="{x:Type model:IField}">
    <Grid Background="Aqua">
        <StackPanel>
            <Label x:Name="FieldName" Content="{Binding Id}" />
            <TextBox x:Name="FieldRawValue" Text="{Binding RawValue, UpdateSourceTrigger=PropertyChanged}" />
            <TextBox x:Name="FieldValue" Text="{Binding Value}" IsEnabled="False" />
            <TextBox x:Name="FieldHasValue" Text="{Binding HasValue, Mode=OneWay}" IsEnabled="False" />
        </StackPanel>
    </Grid>
</DataTemplate>
```

A more specific version can be used for boolean fields so that values can be displayed or set using a checkbox.

```xml
<DataTemplate x:Key="BoolFieldTemplate" DataType="{x:Type model:IField}">
    <Grid Background="Aqua">
        <StackPanel>
            <Label x:Name="FieldName" Content="{Binding Id}" />
            <TextBox x:Name="FieldRawValue" Text="{Binding RawValue, UpdateSourceTrigger=PropertyChanged}" />
            <CheckBox x:Name="FieldValue" IsChecked="{Binding Value}" />
            <TextBox x:Name="FieldHasValue" Text="{Binding HasValue, Mode=OneWay}" IsEnabled="False" />
        </StackPanel>
    </Grid>
</DataTemplate>
```

### Defining the Data Template

The available data templates need a selector [FieldDataTemplateSelector.cs](./WpfApp/FieldDataTemplateSelector.cs) that is derived from `DataTemplateSelector`. This provides the logic
to choose the correct data template based on the fields in the `FieldGroup` object in the view model.

The data template selector defines as many `DataTemplate` objects as required. The correspond the templates in the XAML [MainWindow.xaml](./WpfApp/MainWindow.xaml).

```c#
public DataTemplate DefaultFieldTemplate { get; set; }
public DataTemplate BoolFieldTemplate { get; set; }
```

The selector requires a method to choose the best template by overriding the `SelectTemplate` method. The logic here is quite simple; if the field is a boolean field the the `BoolFieldTemplate`
is used, otherwise the `DefaultFieldTemplate` is used (e.g. compatible with `int` and `double` fields).

```c#
public override DataTemplate SelectTemplate(object item, DependencyObject container)
{
    if (item == null)
    {
        throw new ArgumentNullException(nameof(item));
    }

    if (!(item is IField field))
    {
        throw new InvalidOperationException($"Unrecognised type {item.GetType().Name} expected {nameof(IField)}");
    }

    if (field is IField<bool>)
    {
        return BoolFieldTemplate;
    }

    return DefaultFieldTemplate;
}
}
```

### Selecting Data Template

The way to choose the appropriate template based on the field type is using the `FieldDataTemplateSelector`. In the XAML [MainWindow.xaml](./WpfApp/MainWindow.xaml) the templates are listed with
reference to the data template.

```xml
<wpfApp:FieldDataTemplateSelector x:Key="FieldDataTemplateSelector"
                                  DefaultFieldTemplate="{StaticResource DefaultFieldTemplate}"
                                  BoolFieldTemplate="{StaticResource BoolFieldTemplate}" />
```
