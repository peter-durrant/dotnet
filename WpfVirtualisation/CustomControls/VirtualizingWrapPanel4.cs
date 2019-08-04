using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Hdd.CustomControls
{
    /// <summary>
    ///     Fourth step to implementing a VirtualizingWrapPanel
    ///     Based on example code at:
    ///     https://blogs.msdn.microsoft.com/dancre/2006/02/13/implementing-a-virtualizingpanel-part-2-iitemcontainergenerator/
    ///     https://blogs.msdn.microsoft.com/bencon/2006/01/06/iscrollinfo-in-avalon-part-i/
    ///     This example:
    ///     - DOES NOT virtualize - it shows all items
    ///     - has dynamic number of columns based on number of items that can fit in available space (minimum 1) - _itemsPerRow
    ///     - has a dynamic item size calculated from the first item - _itemSize
    ///     - implements IScrollInfo
    ///     - DOES NOT scroll
    /// </summary>
    public class VirtualizingWrapPanel4 : VirtualizingPanel, IScrollInfo
    {
        private Size _itemSize;
        private int _itemsPerRow;
        private ReadOnlyCollection<object> Items => ((ItemContainerGenerator)ItemContainerGenerator).Items;

        private void CalculateItemProperties(Size availableSize)
        {
            _itemSize = InternalChildren.Count == 0
                ? CalculateItemSize(availableSize)
                : InternalChildren[0].DesiredSize;
            _itemsPerRow = !double.IsInfinity(availableSize.Width)
                ? Math.Max(1, (int)Math.Floor(availableSize.Width / _itemSize.Width))
                : Items.Count;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            CalculateItemProperties(availableSize);

            var startIndex = 0;
            var position = ItemContainerGenerator.GeneratorPositionFromIndex(startIndex);
            using (ItemContainerGenerator.StartAt(position, GeneratorDirection.Forward, true))
            {
                for (var i = 0; i < Items.Count; i++)
                {
                    var nextItem = (UIElement)ItemContainerGenerator.GenerateNext(out var isNewlyRealized);
                    if (isNewlyRealized)
                    {
                        AddInternalChild(nextItem);
                        ItemContainerGenerator.PrepareItemContainer(nextItem);
                        nextItem.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    }
                }
            }

            return new Size(double.PositiveInfinity, double.PositiveInfinity);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            for (var childIndex = 0; childIndex < InternalChildren.Count; ++childIndex)
            {
                var internalChild = InternalChildren[childIndex];

                var indexFromChildIndex =
                    ItemContainerGenerator.IndexFromGeneratorPosition(new GeneratorPosition(childIndex, 0));
                var columnIndex = indexFromChildIndex % _itemsPerRow;
                var rowIndex = indexFromChildIndex / _itemsPerRow;

                var itemX = columnIndex * _itemSize.Width;
                var itemY = rowIndex * _itemSize.Height;

                var childItemRect = new Rect(itemX, itemY, _itemSize.Width, _itemSize.Height);

                internalChild.Arrange(childItemRect);
            }

            return finalSize;
        }

        private Size CalculateItemSize(Size availableSize)
        {
            if (Items.Count == 0)
            {
                return new Size(0.0, 0.0);
            }

            using (ItemContainerGenerator.StartAt(ItemContainerGenerator.GeneratorPositionFromIndex(0),
                GeneratorDirection.Forward, true))
            {
                var nextItem = (UIElement)ItemContainerGenerator.GenerateNext();
                AddInternalChild(nextItem);
                ItemContainerGenerator.PrepareItemContainer(nextItem);
                nextItem.Measure(new Size(availableSize.Width, double.PositiveInfinity));

                return nextItem.DesiredSize;
            }
        }

        #region IScrollInfo

        public void LineUp()
        {
            // todo implement
        }

        public void LineDown()
        {
            // todo implement
        }

        public void LineLeft()
        {
            // todo implement
        }

        public void LineRight()
        {
            // todo implement
        }

        public void PageUp()
        {
            // todo implement
        }

        public void PageDown()
        {
            // todo implement
        }

        public void PageLeft()
        {
            // todo implement
        }

        public void PageRight()
        {
            // todo implement
        }

        public void MouseWheelUp()
        {
            // todo implement
        }

        public void MouseWheelDown()
        {
            // todo implement
        }

        public void MouseWheelLeft()
        {
            // todo implement
        }

        public void MouseWheelRight()
        {
            // todo implement
        }

        public void SetHorizontalOffset(double offset)
        {
            // todo implement
        }

        public void SetVerticalOffset(double offset)
        {
            // todo implement
        }

        public Rect MakeVisible(Visual visual, Rect rectangle)
        {
            // todo implement
            return new Rect();
        }

        public bool CanVerticallyScroll { get; set; }
        public bool CanHorizontallyScroll { get; set; }
        public double ExtentWidth { get; }
        public double ExtentHeight { get; }
        public double ViewportWidth { get; }
        public double ViewportHeight { get; }
        public double HorizontalOffset { get; }
        public double VerticalOffset { get; }
        public ScrollViewer ScrollOwner { get; set; }

        #endregion
    }
}
