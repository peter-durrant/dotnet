# Child Window

This project uses [MVVM Dialogs](https://github.com/FantasticFiasco/mvvm-dialogs) to create WPF dialogs.

The specific problem to solve is to create a dialog (child window) that is proportional in size to the parent window.

The height and width of the window is set by the parent (or owner) window. The `ChildWindow` binding for `Height` and `Width` would ideally be set from the window `Owner`.

```xml
<Window x:Class="Hdd.View.ChildWindow"
        Height="{Binding Owner.ActualHeight,
                         RelativeSource={RelativeSource Self},
                         Converter={converters:ScaleConverter},
                         ConverterParameter=0.9}"
        Width="{Binding Owner.ActualWidth,
                        RelativeSource={RelativeSource Self},
                        Converter={converters:ScaleConverter},
                        ConverterParameter=0.9}"
```

On creation the `Owner` of the window is `null` so the binding has no effect. The particular solution identified here sets the `Owner` early, so that the binding can be evaluated.

## Setting the owner on the dialog window

The constructor to the `ChildWindow` (dialog) takes a `Window owner`. This allows the `Owner` property of the window to be set before the call to `InitializeComponent()`. This ensures that the WPF binding can be evaluated. See [ChildWindow.cs](./View/ChildWindow.xaml.cs).

```c#
public partial class ChildWindow : Window
{
    public ChildWindow(Window owner, ChildViewModel viewModel)
    {
        Owner = owner;
        DataContext = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

        InitializeComponent();
```

## DialogService

The `MvvmDialogs.DialogService` can use a default `DialogFactory` (set `dialogFactory` to `null` on the constructor). This will use the `MvvmDialogs.ReflectionDialogFactory` to create `Window` objects.

In order to provide custom behaviour when creating windows - in this case to set the `Owner` - a new `DialogFactory` is required.

## DialogFactory

The new `DialogFactory` is responsible for creating `Window` objects using either the default behaviour using the `ReflectionDialogFactory` or custom behaviour when requiring this additional functionality.

The [`DialogFactory`](./View/DialogFactory.cs) class continues to use the `ReflectionDialogFactory` for all `Window` objects, except for those recognised as requiring the `Owner` to be set.

Since the `DialogFactory` can set the `Owner`, this must be initialised using the parent window, in this case in [App.xaml.cs](./WpfApp/App.xaml.cs).

```c#
var dialogFactory = new DialogFactory();
var dialogService = new DialogService(dialogFactory, new DialogTypeLocator());
var mainViewModel = new MainViewModel(dialogService);
MainWindow = new MainWindow(mainViewModel);
dialogFactory.Owner = MainWindow;
MainWindow.Show();
```

## DialogTypeLocator

The [`DialogTypeLocator`](./View/DialogTypeLocator.cs) class is required to map view model types to view types.

## ScaleConverter

If needed, the [`ScaleConverter`](./View/Converters/ScaleConverter.cs) is used to convert the `ActualHeight` or `ActualWidth` from the `Owner` window and scale the child window (dialog) by a factor. In this example `0.9` is used to represent 90% of the original size.

```xml
```xml
<Window x:Class="Hdd.View.ChildWindow"
        Height="{Binding Owner.ActualHeight,
                         RelativeSource={RelativeSource Self},
                         Converter={converters:ScaleConverter},
                         ConverterParameter=0.9}"
```
