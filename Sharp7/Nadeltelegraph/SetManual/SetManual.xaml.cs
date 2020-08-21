namespace Nadeltelegraph.SetManual
{
    public partial class SetManual
    {
        private readonly View.View _vm;


        public SetManual(View.View viewManual)
        {
            _vm = viewManual;
            InitializeComponent();
            DataContext = _vm;


        }
    }
}