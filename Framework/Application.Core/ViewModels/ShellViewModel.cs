using System;
using Hdd.Application.Core.Regions;
using Hdd.Application.Core.Views;
using Prism.Mvvm;
using Prism.Regions;

namespace Hdd.Application.Core.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        public ShellViewModel(IRegionManager regionManager)
        {
            _ = regionManager ?? throw new ArgumentNullException(nameof(regionManager));

            regionManager.RegisterViewWithRegion(RegionNames.TabRegion, typeof(DefaultView));
            regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(DefaultView));
            regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, typeof(DefaultView));
            regionManager.RegisterViewWithRegion(RegionNames.NotificationRegion, typeof(DefaultView));
        }
    }
}
