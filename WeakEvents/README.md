# WeakEventHandler

This project demonstrates the difference between using strongly referenced events and weak events using a `WeakEventHandler`.

This project has two main classes:
1. `LongLivedViewModel`
2. `ShortLivedViewModel`

`LongLivedViewModel` is an object that is long-lived and has an event called `EventOnLongLivedViewModel` that is raised if `RaiseEvent()` is called on the `LongLivedViewModel` object.

`ShortLivedViewModel` subscribes to the event `EventOnLongLivedViewModel` but makes no attempt to unsubscribe. It subscribes using one of two mechanisms:
1. Strongly referenced events
2. Weak events

Depending on the subscription technique used, a `ShortLivedViewModel` object may leak.

## Strongly referenced events

When using event subscription of this type, it is important to unsubscribe if the source object (in this case `longLivedViewModel`) has a longer lifespan than the subscriber.

```c#
longLivedViewModel.EventOnLongLivedViewModel += LongLivedViewModel_EventOnLongLivedViewModel
```

This can be difficult to get right, and could rely on the implementer to make their class `IDiposable` with the overhead that brings, or seek alternative solutions such as using weak events.

## Weak events

Weak events can be used to avoid the overhead of managing event subscription when the lifetimes of the event source is different to that of the event listener.

```c#
WeakEventManager<LongLivedViewModel, EventArgs>.
    AddHandler(
    	longLivedViewModel,
    	nameof(LongLivedViewModel.EventOnLongLivedViewModel),
    	LongLivedViewModel_EventOnLongLivedViewModel);
```

## Demonstration - expected output

The output is generated in the method `TestWeakEventManager()` in [MainWindow.xaml.cs](./Demo/MainWindow.xaml.cs) and the event handler `LongLivedViewModel_EventOnLongLivedViewModel` in [`ShortLivedViewModel`](./Demo/ViewModel/ShortLivedViewModel.cs).

The application provides a UI that toggles the event subscription mechanism in the constructor of `ShortLivedViewModel` in [ShortLivedViewModel.cs](./Demo/ViewModel/ShortLivedViewModel.cs).

In the method `TestWeakEventManager` a `WeakReference` is taken on `shortLivedViewModel` to identify if garbage collection was acheived after the reference to `shortLivedViewModel` was set to `null` and garbage collection was forced using `GC.Collect()`. If garbage collection was successful at cleaning up `shortLivedViewModel` then `weakReferenceShortLivedViewModel.IsAlive` is `false`, otherwise `true`.

Finally, if `shortLivedViewModel` is garbage collected, then on the final `longLivedViewModel.RaiseEvent()` the `shortLivedViewModel` should not have handled the event (demonstrated by `ShortLivedViewModel - handled event` not present in the output).

```c#
private void TestWeakEventManager(LongLivedViewModel longLivedViewModel)
{
    longLivedViewModel.Log = _testWeakEventManager ? "Using WeakEventManager" : "Using strong reference";
    var shortLivedViewModel = new ShortLivedViewModel(longLivedViewModel, _testWeakEventManager);
    var weakReferenceShortLivedViewModel = new WeakReference(shortLivedViewModel);

    longLivedViewModel.Log = "Raise event on LongLivedViewModel";
    // expect shortLivedViewModel to handle event
    longLivedViewModel.RaiseEvent();

    longLivedViewModel.Log = "Clear reference to ShortLivedViewModel";

    shortLivedViewModel = null;

    longLivedViewModel.Log = "Force garbage collection";
    GC.Collect();

    longLivedViewModel.Log = $"ShortLivedViewModel.IsAlive = {weakReferenceShortLivedViewModel.IsAlive}";

    longLivedViewModel.Log = "Raise event on LongLivedViewModel";
    // shortLivedViewModel will only handle the event if not using the WeakEventManager
    longLivedViewModel.RaiseEvent();
}
```

### Strongly referenced events

```
Using strong reference
Raise event on LongLivedViewModel
ShortLivedViewModel - handled event
Clear reference to ShortLivedViewModel
Force garbage collection
ShortLivedViewModel.IsAlive = True
Raise event on LongLivedViewModel
ShortLivedViewModel - handled event
```

### Weak events

```
Using WeakEventManager
Raise event on LongLivedViewModel
ShortLivedViewModel - handled event
Clear reference to ShortLivedViewModel
Force garbage collection
ShortLivedViewModel.IsAlive = False
Raise event on LongLivedViewModel
```