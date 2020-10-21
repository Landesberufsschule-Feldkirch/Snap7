// by Antonis Ntit (antonis68)
// email: spanomarias68@gmail.com

using System;

namespace GaugeControl
{

    public class TweenMotion
    {
        private readonly System.Timers.Timer _timer = new System.Timers.Timer();


        public int TickInterval { get; set; }

        public delegate void OnMotion(double value, string type);

        public event OnMotion onMotion;
        private double _timerValue;
        private int _starPos;
        private int _endPos;
        private double _duration;

        public string[] TypeArray = {"linearTween","easeInQuad","easeOutQuad","easeInOutQuad",
                                     "easeInCubic","easeOutCubic","easeInOutCubic","easeInQuart",
                                     "easeOutQuart","easeInOutQuart","easeInQuint","easeOutQuint",
                                     "easeInOutQuint","easeInSine","easeOutSine","easeInOutSine",
                                     "easeInExpo","easeOutExpo","easeInOutExpo","easeInCirc",
                                     "easeOutCirc","easeInOutCirc"
                                     };

        private double _motionValue;
        private string _type = "";

        public TweenMotion()
        {
            if (TickInterval <= 0) { TickInterval = 20; }
            _timerValue = TickInterval;
            _timer.Interval = TickInterval;
            _timer.Elapsed += Timer_Tick;
        }

        public void Start(string motionType, int starPos, int endPos, double duration)
        {
            Stop();
            _timerValue = 0;
            _starPos = starPos;
            _endPos = endPos - starPos;
            _duration = duration * 1000;
            _type = motionType;
            _timer.Start();
        }

        public void Stop() => _timer.Stop();

        // ReSharper disable once UnusedMember.Global
        public bool IsEnable => _timer.Enabled;

        private void Timer_Tick(object sender, EventArgs e)
        {
            _timerValue += TickInterval;
            var current = _timerValue;
            if (_timerValue >= _duration) { _timer.Stop(); }

            switch (_type)
            {
                case "linearTween":
                    _motionValue = _endPos * current / _duration + _starPos;
                    break;

                case "easeInQuad":
                    current /= _duration;
                    _motionValue = _endPos * current * current + _starPos;
                    break;

                case "easeOutQuad":
                    current /= _duration;
                    _motionValue = -_endPos * current * (current - 2) + _starPos;
                    break;

                case "easeInOutQuad":
                    current /= _duration / 2;
                    if (current < 1) { _motionValue = (double)_endPos / 2 * current * current + _starPos; }
                    else
                    {
                        current--;
                        _motionValue = -(double)_endPos / 2 * (current * (current - 2) - 1) + _starPos;
                    }
                    break;

                case "easeInCubic":
                    current /= _duration;
                    _motionValue = _endPos * current * current * current + _starPos;
                    break;
                case "easeOutCubic":
                    current /= _duration;
                    current--;
                    _motionValue = _endPos * (current * current * current + 1) + _starPos;
                    break;
                case "easeInOutCubic":
                    current /= _duration / 2;
                    if (current < 1) { _motionValue = (double)_endPos / 2 * current * current * current + _starPos; }
                    else
                    {
                        current -= 2;
                        _motionValue = (double)_endPos / 2 * (current * current * current + 2) + _starPos;
                    }
                    break;
                case "easeInQuart":
                    current /= _duration;
                    _motionValue = _endPos * current * current * current * current + _starPos;
                    break;


                case "easeOutQuart":
                    current /= _duration;
                    current--;
                    _motionValue = -_endPos * (current * current * current * current - 1) + _starPos;
                    break;
                case "easeInOutQuart":
                    current /= _duration / 2;
                    if (current < 1) { _motionValue = (double)_endPos / 2 * current * current * current * current + _starPos; }
                    else
                    {
                        current -= 2;
                        _motionValue = -(double)_endPos / 2 * (current * current * current * current - 2) + _starPos;
                    }
                    break;

                case "easeInQuint":
                    current /= _duration;
                    _motionValue = _endPos * current * current * current * current * current + _starPos;
                    break;


                case "easeOutQuint":
                    current /= _duration;
                    current--;
                    _motionValue = _endPos * (current * current * current * current * current + 1) + _starPos;
                    break;

                case "easeInOutQuint":
                    current /= _duration / 2;
                    if (current < 1)
                    {
                        _motionValue = (double)_endPos / 2 * current * current * current * current *
                     current + _starPos;
                    }
                    else
                    {
                        current -= 2;
                        _motionValue = (double)_endPos / 2 * (current * current * current * current * current + 2) + _starPos;
                    }
                    break;
                case "easeInSine":
                    _motionValue = -_endPos * Math.Cos(current / _duration * (Math.PI / 2)) + _endPos + _starPos;
                    break;


                case "easeOutSine":
                    _motionValue = _endPos * Math.Sin(current / _duration * (Math.PI / 2)) + _starPos;
                    break;

                case "easeInOutSine":
                    _motionValue = -(double)_endPos / 2 * (Math.Cos(Math.PI * current / _duration) - 1) + _starPos;
                    break;

                case "easeInExpo":
                    _motionValue = _endPos * Math.Pow(2, 10 * (current / _duration - 1)) + _starPos;
                    break;

                case "easeOutExpo":
                    _motionValue = _endPos * (-Math.Pow(2, -10 * current / _duration) + 1) + _starPos;
                    break;

                case "easeInOutExpo":
                    current /= _duration / 2;
                    if (current < 1) { _motionValue = (double)_endPos / 2 * Math.Pow(2, 10 * (current - 1)) + _starPos; }
                    else
                    {
                        current--;
                        _motionValue = (double)_endPos / 2 * (-Math.Pow(2, -10 * current) + 2) + _starPos;
                    }
                    break;
                case "easeInCirc":
                    current /= _duration;
                    _motionValue = -(double)_endPos * (Math.Sqrt(1 - current * current) - 1) + _starPos;
                    break;

                case "easeOutCirc":
                    current /= _duration;
                    current--;
                    _motionValue = _endPos * Math.Sqrt(1 - current * current) + _starPos;
                    break;

                case "easeInOutCirc":
                    current /= _duration / 2;
                    if (current < 1) { _motionValue = -(double)_endPos / 2 * (Math.Sqrt(1 - current * current) - 1) + _starPos; }
                    else
                    {
                        current -= 2;
                        _motionValue = (double)_endPos / 2 * (Math.Sqrt(1 - current * current) + 1) + _starPos;
                    }
                    break;
            }
            onMotion?.Invoke(_motionValue, _type);
        }
    }
}