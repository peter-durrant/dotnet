using System.Windows;
using System.Windows.Controls;

namespace Hdd.CustomControls
{
    public static class VirtualizingWrapPanelExtensions
    {
        public static double Height(this Size size, Orientation orientation)
        {
            return orientation == Orientation.Horizontal ? size.Width : size.Height;
        }

        public static double Width(this Size size, Orientation orientation)
        {
            return orientation == Orientation.Horizontal ? size.Height : size.Width;
        }

        public static Size Size(this Orientation orientation, double width, double height)
        {
            return orientation == Orientation.Horizontal ? new Size(height, width) : new Size(width, height);
        }

        public static double X(this Point point, Orientation orientation)
        {
            return orientation == Orientation.Horizontal ? point.Y : point.X;
        }

        public static double Y(this Point point, Orientation orientation)
        {
            return orientation == Orientation.Horizontal ? point.X : point.Y;
        }

        public static Rect Rect(this Orientation orientation, double x, double y, double width, double height)
        {
            return orientation == Orientation.Horizontal
                ? new Rect(y, x, width, height)
                : new Rect(x, y, width, height);
        }
    }
}
