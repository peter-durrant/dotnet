using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Hdd.CustomControls
{
    /// <summary>
    ///     Fifth step to implementing a VirtualizingWrapPanel
    ///     This example:
    ///     - DOES NOT virtualize - it shows all items
    ///     - has dynamic number of columns based on number of items that can fit in available space (minimum 1) - _itemsPerRow
    ///     - has a dynamic item size calculated from the first item - _itemSize
    ///     - scrollbars calculated
    /// </summary>
    public class VirtualizingWrapPanel5 : VirtualizingPanel, IScrollInfo
    {
        private Size _itemSize;
        private int _itemsPerRow;
        private int _rowCount;
        private Size _viewport = new Size(0.0, 0.0);
        private Size _extent = new Size(0.0, 0.0);
        private Point _offset = new Point(0.0, 0.0);

        private ReadOnlyCollection<object> Items => ((ItemContainerGenerator)ItemContainerGenerator).Items;

        private double LineScrollAmount => GetScrollUnit(this) == ScrollUnit.Pixel ? 16 : _itemSize.Height;
        private double MouseWheelScrollAmount => GetScrollUnit(this) == ScrollUnit.Pixel ? 48 : _itemSize.Height;
        private double PageScrollAmount => GetScrollUnit(this) == ScrollUnit.Pixel ? ViewportHeight : _viewport.Height;

        private void CalculateItemProperties(Size availableSize)
        {
            _itemSize = InternalChildren.Count == 0
                ? CalculateItemSize(availableSize)
                : InternalChildren[0].DesiredSize;
            _itemsPerRow = !double.IsInfinity(availableSize.Width)
                ? Math.Max(1, (int)Math.Floor(availableSize.Width / _itemSize.Width))
                : Items.Count;
            _rowCount = (int)Math.Ceiling(Items.Count / (double)_itemsPerRow);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            CalculateItemProperties(availableSize);
            VirtualizeItems();
            return UpdateScrollInfo(availableSize);
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

        private void VirtualizeItems()
        {
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
        }

        private Size UpdateScrollInfo(Size availableSize)
        {
            var extent = new Size(
                _itemSize.Width * _itemsPerRow,
                _itemSize.Height * _rowCount);

            availableSize = new Size(
                Math.Min(availableSize.Width, extent.Width),
                Math.Min(availableSize.Height, extent.Height));

            if (ViewportHeight != 0.0 && VerticalOffset != 0.0 && VerticalOffset + ViewportHeight + 1.0 >= ExtentHeight)
            {
                _offset = new Point(_offset.X, extent.Height - availableSize.Height);
                ScrollOwner?.InvalidateScrollInfo();
            }

            if (ViewportWidth != 0.0 && HorizontalOffset != 0.0 &&
                HorizontalOffset + ViewportWidth + 1.0 >= ExtentWidth)
            {
                _offset = new Point(extent.Width - availableSize.Width, _offset.Y);
                ScrollOwner?.InvalidateScrollInfo();
            }

            if (availableSize != _viewport)
            {
                _viewport = availableSize;
                ScrollOwner?.InvalidateScrollInfo();
            }

            if (extent != _extent)
            {
                _extent = extent;
                ScrollOwner?.InvalidateScrollInfo();
            }

            return availableSize;
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
            SetVerticalOffset(VerticalOffset - LineScrollAmount);
        }

        public void LineDown()
        {
            SetVerticalOffset(VerticalOffset + LineScrollAmount);
        }

        public void LineLeft()
        {
            SetHorizontalOffset(HorizontalOffset - LineScrollAmount);
        }

        public void LineRight()
        {
            SetHorizontalOffset(HorizontalOffset + LineScrollAmount);
        }

        public void PageUp()
        {
            SetVerticalOffset(VerticalOffset - PageScrollAmount);
        }

        public void PageDown()
        {
            SetVerticalOffset(VerticalOffset + PageScrollAmount);
        }

        public void PageLeft()
        {
            SetHorizontalOffset(HorizontalOffset - PageScrollAmount);
        }

        public void PageRight()
        {
            SetHorizontalOffset(HorizontalOffset + PageScrollAmount);
        }

        public void MouseWheelUp()
        {
            SetVerticalOffset(VerticalOffset - MouseWheelScrollAmount);
        }

        public void MouseWheelDown()
        {
            SetVerticalOffset(VerticalOffset + MouseWheelScrollAmount);
        }

        public void MouseWheelLeft()
        {
            SetHorizontalOffset(HorizontalOffset - MouseWheelScrollAmount);
        }

        public void MouseWheelRight()
        {
            SetHorizontalOffset(HorizontalOffset + MouseWheelScrollAmount);
        }

        public void SetHorizontalOffset(double offset)
        {
            if (offset < 0.0 || _viewport.Width >= _extent.Width)
            {
                offset = 0.0;
            }
            else if (offset + _viewport.Width >= _extent.Width)
            {
                offset = _extent.Width - _viewport.Width;
            }

            _offset = new Point(offset, _offset.Y);
            ScrollOwner?.InvalidateScrollInfo();
            InvalidateMeasure();
        }

        public void SetVerticalOffset(double offset)
        {
            if (offset < 0.0 || _viewport.Height >= _extent.Height)
            {
                offset = 0.0;
            }
            else if (offset + _viewport.Height >= _extent.Height)
            {
                offset = _extent.Height - _viewport.Height;
            }

            _offset = new Point(_offset.X, offset);
            ScrollOwner?.InvalidateScrollInfo();
            InvalidateMeasure();
        }

        public Rect MakeVisible(Visual visual, Rect rectangle)
        {
            var point = visual.TransformToAncestor(this).Transform(_offset);
            var x = 0.0;
            var y = 0.0;
            if (point.X < _offset.X)
            {
                x = point.X - _offset.X;
            }
            else if (point.X + rectangle.Width > _offset.X + _viewport.Width)
            {
                x = point.X + rectangle.Width - _offset.X + _viewport.Width;
            }

            if (point.Y < _offset.Y)
            {
                y = point.Y - _offset.Y;
            }
            else if (point.Y + rectangle.Height > _offset.Y + _viewport.Height)
            {
                y = point.Y + rectangle.Height - _offset.Y + _viewport.Height;
            }

            SetHorizontalOffset(_offset.X + x);
            SetVerticalOffset(_offset.Y + y);

            var width = Math.Min(rectangle.Width, _viewport.Width);
            var height = Math.Min(rectangle.Height, _viewport.Height);

            return new Rect(x, y, width, height);
        }

        public bool CanVerticallyScroll { get; set; }
        public bool CanHorizontallyScroll { get; set; }
        public double ExtentWidth => _extent.Width;
        public double ExtentHeight => _extent.Height;
        public double ViewportWidth => _viewport.Width;
        public double ViewportHeight => _viewport.Height;
        public double HorizontalOffset => _offset.X;
        public double VerticalOffset => _offset.Y;
        public ScrollViewer ScrollOwner { get; set; }

        #endregion
    }
}
