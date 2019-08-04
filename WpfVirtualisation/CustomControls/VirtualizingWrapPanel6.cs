using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Hdd.CustomControls
{
    /// <summary>
    ///     Sixth step to implementing a VirtualizingWrapPanel
    ///     This example:
    ///     - has virtualization
    ///     - has scrollbars
    ///     - has dynamic number of columns based on number of items that can fit in available space (minimum 1) - _itemsPerRow
    ///     - has a dynamic item size calculated from the first item - _itemSize
    ///     - DOES NOT handle orientation (vertical or horizontal)
    ///     - DOES NOT handle {Horizontal, Vertical}ContentAlignment dependency property to fine tune alignment of last row if it contains less than _itemsPerRow number of items
    /// </summary>
    public class VirtualizingWrapPanel6 : VirtualizingPanel, IScrollInfo
    {
        private Size _itemSize;
        private int _itemsPerRow;
        private int _rowCount;
        private Size _viewport = new Size(0.0, 0.0);
        private Size _extent = new Size(0.0, 0.0);
        private Point _offset = new Point(0.0, 0.0);

        private ItemsControl ItemsControl => ItemsControl.GetItemsOwner(this);
        private ReadOnlyCollection<object> Items => ((ItemContainerGenerator)ItemContainerGenerator).Items;

        private IRecyclingItemContainerGenerator RecyclingItemContainerGenerator =>
            (IRecyclingItemContainerGenerator)ItemContainerGenerator;

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

        private (int startIndex, int endIndex) UpdateItemRange()
        {
            if (!GetIsVirtualizing(ItemsControl))
            {
                return (startIndex: 0, endIndex: Items.Count - 1);
            }

            var viewportStartHeight = _offset.Y;
            var viewportEndHeight = _offset.Y + _viewport.Height;

            if (GetCacheLengthUnit(ItemsControl) == VirtualizationCacheLengthUnit.Pixel)
            {
                viewportStartHeight = Math.Max(viewportStartHeight - GetCacheLength(ItemsControl).CacheBeforeViewport,
                    0.0);
                viewportEndHeight = Math.Min(viewportEndHeight + GetCacheLength(ItemsControl).CacheAfterViewport,
                    _extent.Height);
            }

            var itemStartIndex = (int)Math.Floor(viewportStartHeight / _itemSize.Height) * _itemsPerRow;
            var itemEndIndex = Math.Min(Items.Count,
                (int)Math.Ceiling(viewportEndHeight / _itemSize.Height) * _itemsPerRow) - 1;

            if (GetCacheLengthUnit(ItemsControl) == VirtualizationCacheLengthUnit.Page)
            {
                var visibleRows = (int)(_viewport.Height / _itemSize.Height) * _itemsPerRow;
                itemStartIndex =
                    Math.Max(itemStartIndex - (int)GetCacheLength(ItemsControl).CacheBeforeViewport * visibleRows, 0);
                itemEndIndex = Math.Min(
                    itemEndIndex + (int)GetCacheLength(ItemsControl).CacheAfterViewport * visibleRows,
                    Items.Count - 1);
            }
            else if (GetCacheLengthUnit(ItemsControl) == VirtualizationCacheLengthUnit.Item)
            {
                itemStartIndex = Math.Max(itemStartIndex - (int)GetCacheLength(ItemsControl).CacheBeforeViewport, 0);
                itemEndIndex = Math.Min(itemEndIndex + (int)GetCacheLength(ItemsControl).CacheAfterViewport,
                    Items.Count - 1);
            }

            return (startIndex: itemStartIndex, endIndex: itemEndIndex);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            CalculateItemProperties(availableSize);
            var (startIndex, endIndex) = UpdateItemRange();
            VirtualizeItems(startIndex, endIndex);
            CleanupItems(startIndex, endIndex);
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

                var childItemRect = new Rect(
                    itemX - _offset.X,
                    itemY - _offset.Y,
                    _itemSize.Width,
                    _itemSize.Height);

                internalChild.Arrange(childItemRect);
            }

            return finalSize;
        }

        private void VirtualizeItems(int startIndex, int endIndex)
        {
            var position = ItemContainerGenerator.GeneratorPositionFromIndex(startIndex);
            var index = position.Offset == 0 ? position.Index : position.Index + 1;
            using (ItemContainerGenerator.StartAt(position, GeneratorDirection.Forward, true))
            {
                while (true)
                {
                    if (startIndex <= endIndex)
                    {
                        var nextItem = (UIElement)ItemContainerGenerator.GenerateNext(out var isNewlyRealized);
                        if (isNewlyRealized || !InternalChildren.Contains(nextItem))
                        {
                            if (index >= InternalChildren.Count)
                            {
                                AddInternalChild(nextItem);
                            }
                            else
                            {
                                InsertInternalChild(index, nextItem);
                            }

                            ItemContainerGenerator.PrepareItemContainer(nextItem);
                            nextItem.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                        }

                        ++startIndex;
                        ++index;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private void CleanupItems(int startIndex, int endIndex)
        {
            for (var index = InternalChildren.Count - 1; index >= 0; --index)
            {
                var positionFromChildIndex = new GeneratorPosition(index, 0);
                var indexFromGeneratorPosition =
                    ItemContainerGenerator.IndexFromGeneratorPosition(positionFromChildIndex);

                if (startIndex >= indexFromGeneratorPosition &&
                    endIndex <= indexFromGeneratorPosition)
                {
                    if (GetVirtualizationMode(ItemsControl) == VirtualizationMode.Recycling)
                    {
                        RecyclingItemContainerGenerator.Recycle(positionFromChildIndex, 1);
                    }
                    else
                    {
                        ItemContainerGenerator.Remove(positionFromChildIndex, 1);
                    }

                    RemoveInternalChildRange(index, 1);
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

            if (ViewportHeight != 0.0 &&
                VerticalOffset != 0.0 &&
                VerticalOffset + ViewportHeight + 1.0 >= ExtentHeight)
            {
                _offset = new Point(_offset.X, extent.Height - availableSize.Height);
                ScrollOwner?.InvalidateScrollInfo();
            }

            if (ViewportWidth != 0.0 &&
                HorizontalOffset != 0.0 &&
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
