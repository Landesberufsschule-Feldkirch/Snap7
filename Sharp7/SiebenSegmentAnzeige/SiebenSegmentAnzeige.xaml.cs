using System.Windows;

namespace SiebenSegmentAnzeige
{
    public partial class UserControl
    {

        public bool LedA { get; set; }
        public bool LedB { get; set; }
        public bool LedC { get; set; }
        public bool LedD { get; set; }
        public bool LedE { get; set; }
        public bool LedF { get; set; }
        public bool LedG { get; set; }
        public bool LedDp { get; set; }

        public UserControl()
        {
            var viewModel = new ViewModel.ViewModel(this);

            LedA = true;
            LedB = true;
            LedC = true;
            LedD = true;
            LedE = true;
            LedF = true;
            LedG = true;
            LedDp = true;

            InitializeComponent();
            DataContext = viewModel;
        }


        private bool _visibilitySegmentA;
        public bool VisibilitySegmentA
        {
            get => _visibilitySegmentA;
            set
            {
                _visibilitySegmentA = value;
                SetValue(VisibilitySegmentAProperty, _visibilitySegmentA);
            }
        }

        public static readonly DependencyProperty VisibilitySegmentAProperty =
            DependencyProperty.Register("VisSegA", typeof(bool), typeof(SiebenSegmentAnzeige),
                new PropertyMetadata(OnVisibilitySegmentAChanged));

        private static void OnVisibilitySegmentAChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //
        }


    }
}