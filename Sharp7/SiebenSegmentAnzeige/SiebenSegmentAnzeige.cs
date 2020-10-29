using System.Windows;
using System.Windows.Controls;

namespace SiebenSegmentAnzeige
{
    public class SiebenSegmentAnzeige : Control
    {
        static SiebenSegmentAnzeige()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SiebenSegmentAnzeige), new FrameworkPropertyMetadata(typeof(SiebenSegmentAnzeige)));
        }
    }
}