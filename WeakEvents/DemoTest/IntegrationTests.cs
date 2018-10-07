using System;
using System.Diagnostics;
using Hdd.WeakEvents.Demo;
using Hdd.WeakEvents.Demo.ViewModel;
using NUnit.Framework;

namespace Hdd.WeakEvents.DemoTest
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public void ShortLivedViewModel_StrongReferenceToLongLivedViewModel_HandlesEventRaisedOnLongLivedViewModel()
        {
            var longLivedViewModel = new LongLivedViewModel();
            var shortLivedViewModel = new ShortLivedViewModel(longLivedViewModel, EventPattern.StrongReference);

            // act
            longLivedViewModel.RaiseEvent();

            Assert.AreEqual("ShortLivedViewModel - handled event", longLivedViewModel.Log);
        }

        [Test]
        public void ShortLivedViewModel_GenericWeakEventManagerReferenceToLongLivedViewModel_HandlesEventRaisedOnLongLivedViewModel()
        {
            var longLivedViewModel = new LongLivedViewModel();
            var shortLivedViewModel = new ShortLivedViewModel(longLivedViewModel, EventPattern.GenericWeakEventManager);

            // act
            longLivedViewModel.RaiseEvent();

            Assert.AreEqual("ShortLivedViewModel - handled event", longLivedViewModel.Log);
        }

        [Test]
        public void ShortLivedViewModel_CustomWeakEventManagerReferenceToLongLivedViewModel_HandlesEventRaisedOnLongLivedViewModel()
        {
            var longLivedViewModel = new LongLivedViewModel();
            var shortLivedViewModel = new ShortLivedViewModel(longLivedViewModel, EventPattern.CustomEventManager);

            // act
            longLivedViewModel.RaiseEvent();

            Assert.AreEqual("ShortLivedViewModel - handled event", longLivedViewModel.Log);
        }

        [Test]
        public void
            ShortLivedViewModel_StrongReferenceToLongLivedViewModel_ShortLivedViewModelUnreferenced_AttemptGarbageCollection_ShortLivedViewModelKeptAliveByStrongReference()
        {
            var longLivedViewModel = new LongLivedViewModel();
            var shortLivedViewModel = new ShortLivedViewModel(longLivedViewModel, EventPattern.StrongReference);
            var weakReferenceShortLivedViewModel = new WeakReference(shortLivedViewModel);
            shortLivedViewModel = null;

            // act
            GC.Collect();

            Assert.IsTrue(weakReferenceShortLivedViewModel.IsAlive);
        }

        [Test]
        public void
            ShortLivedViewModel_GenericWeakEventManagerReferenceToLongLivedViewModel_ShortLivedViewModelUnreferenced_AttemptGarbageCollection_ShortLivedViewModelGarbageCollected()
        {
            var longLivedViewModel = new LongLivedViewModel();
            var shortLivedViewModel = new ShortLivedViewModel(longLivedViewModel, EventPattern.GenericWeakEventManager);
            var weakReferenceShortLivedViewModel = new WeakReference(shortLivedViewModel);
            shortLivedViewModel = null;

            // act
            GC.Collect();

            Assert.IsFalse(weakReferenceShortLivedViewModel.IsAlive);
        }

        [Test]
        public void
            ShortLivedViewModel_CustomWeakEventManagerReferenceToLongLivedViewModel_ShortLivedViewModelUnreferenced_AttemptGarbageCollection_ShortLivedViewModelGarbageCollected()
        {
            var longLivedViewModel = new LongLivedViewModel();
            var shortLivedViewModel = new ShortLivedViewModel(longLivedViewModel, EventPattern.CustomEventManager);
            var weakReferenceShortLivedViewModel = new WeakReference(shortLivedViewModel);
            shortLivedViewModel = null;

            // act
            GC.Collect();

            Assert.IsFalse(weakReferenceShortLivedViewModel.IsAlive);
        }

        [Test]
        public void
            ShortLivedViewModel_StrongReferenceToLongLivedViewModel_ShortLivedViewModelUnreferenced_AttemptGarbageCollection_ShortLivedViewModelKeptAliveByStrongReference_HandlesEventRaisedOnLongLivedViewModel()
        {
            var longLivedViewModel = new LongLivedViewModel();
            var shortLivedViewModel = new ShortLivedViewModel(longLivedViewModel, EventPattern.StrongReference);
            shortLivedViewModel = null;
            GC.Collect();

            // act
            longLivedViewModel.RaiseEvent();

            Assert.AreEqual("ShortLivedViewModel - handled event", longLivedViewModel.Log);
        }

        [Test]
        public void
            ShortLivedViewModel_GenericWeakEventManagerReferenceToLongLivedViewModel_ShortLivedViewModelUnreferenced_AttemptGarbageCollection_ShortLivedViewModelGarbageCollected_DoesNotHandleEventRaisedOnLongLivedViewModel()
        {
            var longLivedViewModel = new LongLivedViewModel();
            var shortLivedViewModel = new ShortLivedViewModel(longLivedViewModel, EventPattern.GenericWeakEventManager);
            shortLivedViewModel = null;
            GC.Collect();

            // act
            longLivedViewModel.RaiseEvent();

            Assert.IsEmpty(longLivedViewModel.Log);
        }

        [Test]
        public void
            ShortLivedViewModel_CustomWeakEventManagerReferenceToLongLivedViewModel_ShortLivedViewModelUnreferenced_AttemptGarbageCollection_ShortLivedViewModelGarbageCollected_DoesNotHandleEventRaisedOnLongLivedViewModel()
        {
            var longLivedViewModel = new LongLivedViewModel();
            var shortLivedViewModel = new ShortLivedViewModel(longLivedViewModel, EventPattern.CustomEventManager);
            shortLivedViewModel = null;
            GC.Collect();

            // act
            longLivedViewModel.RaiseEvent();

            Assert.IsEmpty(longLivedViewModel.Log);
        }
    }
}