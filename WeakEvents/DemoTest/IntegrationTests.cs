using System;
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
            var shortLivedViewModel = new ShortLivedViewModel(longLivedViewModel, false);

            // act
            longLivedViewModel.RaiseEvent();

            Assert.AreEqual("ShortLivedViewModel - handled event", longLivedViewModel.Log);
        }

        [Test]
        public void ShortLivedViewModel_WeakEventManagerReferenceToLongLivedViewModel_HandlesEventRaisedOnLongLivedViewModel()
        {
            var longLivedViewModel = new LongLivedViewModel();
            var shortLivedViewModel = new ShortLivedViewModel(longLivedViewModel, true);

            // act
            longLivedViewModel.RaiseEvent();

            Assert.AreEqual("ShortLivedViewModel - handled event", longLivedViewModel.Log);
        }

        [Test]
        public void
            ShortLivedViewModel_StrongReferenceToLongLivedViewModel_ShortLivedViewModelUnreferenced_AttemptGarbageCollection_ShortLivedViewModelKeptAliveByStrongReference()
        {
            var longLivedViewModel = new LongLivedViewModel();
            var shortLivedViewModel = new ShortLivedViewModel(longLivedViewModel, false);
            var weakReferenceShortLivedViewModel = new WeakReference(shortLivedViewModel);
            shortLivedViewModel = null;

            // act
            GC.Collect();

            Assert.IsFalse(weakReferenceShortLivedViewModel.IsAlive);
        }

        [Test]
        public void
            ShortLivedViewModel_WeakEventManagerReferenceToLongLivedViewModel_ShortLivedViewModelUnreferenced_AttemptGarbageCollection_ShortLivedViewModelGarbageCollected()
        {
            var longLivedViewModel = new LongLivedViewModel();
            var shortLivedViewModel = new ShortLivedViewModel(longLivedViewModel, true);
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
            var shortLivedViewModel = new ShortLivedViewModel(longLivedViewModel, false);
            shortLivedViewModel = null;
            GC.Collect();

            // act
            longLivedViewModel.RaiseEvent();

            Assert.AreEqual("ShortLivedViewModel - handled event", longLivedViewModel.Log);
        }

        [Test]
        public void
            ShortLivedViewModel_WeakEventManagerReferenceToLongLivedViewModel_ShortLivedViewModelUnreferenced_AttemptGarbageCollection_ShortLivedViewModelGarbageCollected_DoesNotHandleEventRaisedOnLongLivedViewModel()
        {
            var longLivedViewModel = new LongLivedViewModel();
            var shortLivedViewModel = new ShortLivedViewModel(longLivedViewModel, true);
            shortLivedViewModel = null;
            GC.Collect();

            // act
            longLivedViewModel.RaiseEvent();

            Assert.IsEmpty(longLivedViewModel.Log);
        }
    }
}