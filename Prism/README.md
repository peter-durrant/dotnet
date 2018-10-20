# Prism WPF Application

This project demonstrates the use of Prism to create a modular application.

## Dependencies

The main dependencies to get from NuGet are:
* Unity
* Prism.Unity
* Prism.Wpf

Find the full list of packages in [packages.config](./src/MyPrismApplication/packages.config).

## Conventions

For the prism `ViewModelLocator` the view and view model code must be placed in the following folders:
* Views - place in a `Views` folder (namespace)
* View models - place in a `ViewModels` folder (namespace)

## Application

The application should be a `PrismApplication` - see the containing tag in [App.xaml](./src/MyPrismApplication/App.xaml). This will require `override` methods to 
be defined in [App.xaml.cs](./src/MyPrismApplication/App.xaml.cs).

Remove the `StartupUri` attribute from [App.xaml](./src/MyPrismApplication/App.xaml) and use `CreateShell` in the [App.xaml.cs](./src/MyPrismApplication/App.xaml.cs) to load the correct view.
