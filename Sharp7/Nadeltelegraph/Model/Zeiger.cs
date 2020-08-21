namespace Nadeltelegraph.Model
{
    public class Zeiger
    {
        private int _winkel;

        private int _breiteUpLeft;
        private int _breiteUpRight;
        private int _breiteDownLeft;
        private int _breiteDownRight;

        private const int WinkelNadel = 35;
        private const int BreiteBreit = 10;
        private const int BreiteSchmal = 1;

        public Zeiger()
        {
            _winkel = 0;
            _breiteUpLeft = BreiteSchmal;
            _breiteUpRight = BreiteSchmal;
            _breiteDownLeft = BreiteSchmal;
            _breiteDownRight = BreiteSchmal;
        }

        internal void SetPosition(bool rechts, bool links)
        {
            _winkel = 0;
            if (rechts)
            {
                _winkel = WinkelNadel;
                _breiteUpLeft = BreiteSchmal;
                _breiteUpRight = BreiteBreit;
                _breiteDownLeft = BreiteBreit;
                _breiteDownRight = BreiteSchmal;
            }

            if (links)
            {
                _winkel = -WinkelNadel;
                _breiteUpLeft = BreiteBreit;
                _breiteUpRight = BreiteSchmal;
                _breiteDownLeft = BreiteSchmal;
                _breiteDownRight = BreiteBreit;
            }
        }

        internal int GetWinkel() => _winkel;
        internal int GetBreiteUpLeft() => _breiteUpLeft;          // 11 ... 15
        internal int GetBreiteUpRight() => _breiteUpRight;        // 21 ... 25
        internal int GetBreiteDownLeft() => _breiteDownLeft;      // 31 ... 35
        internal int GetBreiteDownRight() => _breiteDownRight;    // 41 ... 45
    }
}
