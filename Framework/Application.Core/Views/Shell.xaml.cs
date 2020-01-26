﻿using System;
using System.Windows;
using Hdd.Application.Core.Regions;
using Prism.Regions;

namespace Hdd.Application.Core.Views
{
    public partial class Shell : Window
    {
        public Shell(IRegionManager regionManager)
        {
            InitializeComponent();

            _ = regionManager ?? throw new ArgumentNullException(nameof(regionManager));

            regionManager.RegisterViewWithRegion(RegionNames.TabRegion, typeof(DefaultView));
            regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(DefaultView));
            regionManager.RegisterViewWithRegion(RegionNames.ConfigurationRegion, typeof(DefaultView));
            regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, typeof(DefaultView));
            regionManager.RegisterViewWithRegion(RegionNames.NotificationRegion, typeof(DefaultView));
        }
    }
}