# WPF

WPF extension library.

## NuGet Package Generation

The rules for generating **Hdd.Presentation.Core** NuGet package are defined in [Package.nuspec](./Presentation.Core/Package.nuspec).

The **Hdd.Presentation.Core** nuget package was created by running the `nuget.exe` command

```bat
nuget pack Presentation.Core.csproj -IncludeReferencedProjects
```

If NuGet is not installed then it can be downloaded from [https://www.nuget.org/downloads](https://www.nuget.org/downloads).

The output package can be copied to a personal NuGet server or uploaded to a public server. Ensure NuGet is configured to read any repositories.
