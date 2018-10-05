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

```
longLivedViewModel.EventOnLongLivedViewModel += LongLivedViewModel_EventOnLongLivedViewModel
```

This can be difficult to get right, and could rely on the implementer to make their class `IDiposable` with the overhead that brings, or seek alternative solutions such as using weak events.

## Weak events

Weak events can be used to avoid the overhead of managing event subscription when the lifetimes of the event source is different to that of the event listener.

```
WeakEventManager<LongLivedViewModel, EventArgs>.AddHandler(longLivedViewModel, nameof(LongLivedViewModel.EventOnLongLivedViewModel), LongLivedViewModel_EventOnLongLivedViewModel);
```
