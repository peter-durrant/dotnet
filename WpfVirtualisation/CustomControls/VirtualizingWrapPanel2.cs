using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Hdd.CustomControls
{
    /// <summary>
    ///     Second step to implementing a VirtualizingWrapPanel
    ///     Based on example code at:
    ///     https://blogs.msdn.microsoft.com/dancre/2006/02/13/implementing-a-virtualizingpanel-part-2-iitemcontainergenerator/
    ///     https://blogs.msdn.microsoft.com/bencon/2006/01/06/iscrollinfo-in-avalon-part-i/
    ///     This example:
    ///     - DOES NOT virtualize - it shows all items
    ///     - has 3 columns - _itemsPerRow
    ///     - has a fixed item size of 300(W) x 200(H) - _itemSize
    ///     - implements IScrollInfo
    ///     - DOES NOT scroll
    /// </summary>
    public class VirtualizingWrapPanel2 : VirtualizingPanel, IScrollInfo
    {
        private readonly Size _itemSize = new Size(300, 200);
        private readonly int _itemsPerRow = 3;
        private ReadOnlyCollection<object> Items => ((ItemContainerGenerator)ItemContainerGenerator).Items;

        protected override Size MeasureOverride(Size availableSize)
        {
            // Before we can use the generator, we must access the InternalChildren property as follows. Until you do, the generator is null. 
            // https://blogs.msdn.microsoft.com/dancre/2006/02/13/implementing-a-virtualizingpanel-part-2-iitemcontainergenerator/
            var internalChildren = InternalChildren;

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
