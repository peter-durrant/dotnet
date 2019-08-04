using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Hdd.CustomControls
{
    /// <summary>
    ///     First step to implementing a VirtualizingWrapPanel
    ///     Based on example code at
    ///     https://blogs.msdn.microsoft.com/dancre/2006/02/13/implementing-a-virtualizingpanel-part-2-iitemcontainergenerator/
    ///     This example:
    ///     - DOES NOT virtualize - it shows all items
    ///     - has 3 columns - _itemsPerRow
    ///     - has a fixed item size of 300(W) x 200(H) - _itemSize
    ///     - has a fixed size container 1000(W) x 1000(H) - _controlSize
    ///     - CANNOT scroll more than 5 rows as control is constrained to 1000(H) - 5 rows with _itemSize Height 200
    /// </summary>
    public class VirtualizingWrapPanel1 : VirtualizingPanel
    {
        private readonly Size _controlSize = new Size(1000, 1000);
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

            return _controlSize;
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
    }
}
