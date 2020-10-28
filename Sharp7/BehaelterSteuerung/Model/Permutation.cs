namespace BehaelterSteuerung.Model
{
    public class Permutation
    {
        private readonly int _ziffer1;
        private readonly int _ziffer2;
        private readonly int _ziffer3;
        private readonly int _ziffer4;

        public Permutation(int i1, int i2, int i3, int i4)
        {
            _ziffer1 = i1;
            _ziffer2 = i2;
            _ziffer3 = i3;
            _ziffer4 = i4;
        }

        internal string GetText() => _ziffer1 + _ziffer2.ToString() + _ziffer3 + _ziffer4;
    }
}