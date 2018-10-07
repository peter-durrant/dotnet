# WeakEventHandler

This project demonstrates the difference between using strongly referenced events and weak events using either the generic `WeakEventHandler` or a custom `WeakEventHandler` [EventOnLongLivedViewModelWeakEventManager](./Demo/EventOnLongLivedViewModelWeakEventManager).

This project has two main classes:
1. `LongLivedViewModel`
2. `ShortLivedViewModel`

`LongLivedViewModel` is an object that is long-lived and has an event called `EventOnLongLivedViewModel` that is raised if `RaiseEvent()` is called on the `LongLivedViewModel` object.

`ShortLivedViewModel` subscribes to the event `EventOnLongLivedViewModel` but makes no attempt to unsubscribe. It subscribes using one of three mechanisms:
1. Strongly referenced events
2. Weak events using generic WeakEventManager
3. Weak events using a custome WeakEventManager `EventOnLongLivedViewModelWeakEventManager`

Depending on the subscription technique used, a `ShortLivedViewModel` object may leak.

Subscription is shown in the constructor of [`ShortLivedViewModel`](./Demo/ViewModel/ShortLivedViewModel.cs).

## Strongly referenced events

When using event subscription of this type, it is important to unsubscribe if the source object (in this case `longLivedViewModel`) has a longer lifespan than the subscriber.

```c#
longLivedViewModel.EventOnLongLivedViewModel += LongLivedViewModel_EventOnLongLivedViewModel
```

This can be difficult to get right, and could rely on the implementer to make their class `IDiposable` with the overhead that brings, or seek alternative solutions such as using weak events.

## Weak events

Weak events can be used to avoid the overhead of managing event subscription when the lifetimes of the event source is different to that of the event listener.

### Generic WeakEventManager

```c#
WeakEventManager<LongLivedViewModel, EventArgs>.AddHandler(
    longLivedViewModel,
    nameof(LongLivedViewModel.EventOnLongLivedViewModel),
    LongLivedViewModel_EventOnLongLivedViewModel);
```

### Custom WeakEventManager

```c#
EventOnLongLivedViewModelWeakEventManager.AddHandler(
    longLivedViewModel,
    LongLivedViewModel_EventOnLongLivedViewModel);
```

## Demonstration - expected output

The output is generated in the method `TestWeakEventManager()` in [MainWindow.xaml.cs](./Demo/MainWindow.xaml.cs) and the event handler `LongLivedViewModel_EventOnLongLivedViewModel` in [`ShortLivedViewModel`](./Demo/ViewModel/ShortLivedViewModel.cs).

The application provides a UI that toggles the event subscription mechanisms in the constructor of `ShortLivedViewModel` in [ShortLivedViewModel.cs](./Demo/ViewModel/ShortLivedViewModel.cs).

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

### Strongly referenced events - output

Note that the strong reference via subscription in `ShortLivedViewModel` to `LongLivedViewModel` has prevented the `ShortLivedViewModel` from being garbage collected. Hence `ShortLivedViewModel.IsAlive = True`.

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

### Weak events - output

Note that the weak reference via subscription in `ShortLivedViewModel` to `LongLivedViewModel` has enabled the `ShortLivedViewModel` to be garbage collected.
Hence `ShortLivedViewModel.IsAlive = False` in the log indicates that the object has been garbage collected and cannot handle the event raised by `LongLivedViewModel`.
This is also confirmed by the absence of `ShortLivedViewModel - handled event` in the log.

```
Using generic WeakEventManager
Raise event on LongLivedViewModel
ShortLivedViewModel - handled event
Clear reference to ShortLivedViewModel
Force garbage collection
ShortLivedViewModel.IsAlive = False
Raise event on LongLivedViewModel
```
or
```
Using custom WeakEventManager
Raise event on LongLivedViewModel
ShortLivedViewModel - handled event
Clear reference to ShortLivedViewModel
Force garbage collection
ShortLivedViewModel.IsAlive = False
Raise event on LongLivedViewModel
```
