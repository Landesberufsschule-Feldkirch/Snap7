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
        private static readonly string[] Array = {
            "LinearTween",
            "EaseInQuad",
            "EaseOutQuad",
            "EaseInOutQuad",
            "EaseInCubic",
            "EaseOutCubic",
            "EaseInOutCubic",
            "EaseInQuart",
            "EaseOutQuart",
            "EaseInOutQuart",
            "EaseInQuint",
            "EaseOutQuint",
            "EaseInOutQuint",
            "EaseInSine",
            "EaseOutSine",
            "EaseInOutSine",
            "EaseInExpo",
            "EaseOutExpo",
            "EaseInOutExpo",
            "EaseInCirc",
            "EaseOutCirc",
            "EaseInOutCirc"
        };

        // ReSharper disable once UnusedMember.Global
        public string[] TypeArray = Array;

        private double _motionValue;
        private string _type = "";

        public TweenMotion()
        {
            if (TickInterval <= 0) { TickInterval = 10; }
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
                case "LinearTween":
                    _motionValue = _endPos * current / _duration + _starPos;
                    break;

                case "EaseInQuad":
                    current /= _duration;
                    _motionValue = _endPos * current * current + _starPos;
                    break;

                case "EaseOutQuad":
                    current /= _duration;
                    _motionValue = -_endPos * current * (current - 2) + _starPos;
                    break;

                case "EaseInOutQuad":
                    current /= _duration / 2;
                    if (current < 1) { _motionValue = (double)_endPos / 2 * current * current + _starPos; }
                    else
                    {
                        current--;
                        _motionValue = -(double)_endPos / 2 * (current * (current - 2) - 1) + _starPos;
                    }
                    break;

                case "EaseInCubic":
                    current /= _duration;
                    _motionValue = _endPos * current * current * current + _starPos;
                    break;

                case "EaseOutCubic":
                    current /= _duration;
                    current--;
                    _motionValue = _endPos * (current * current * current + 1) + _starPos;
                    break;

                case "EaseInOutCubic":
                    current /= _duration / 2;
                    if (current < 1) { _motionValue = (double)_endPos / 2 * current * current * current + _starPos; }
                    else
                    {
                        current -= 2;
                        _motionValue = (double)_endPos / 2 * (current * current * current + 2) + _starPos;
                    }
                    break;

                case "EaseInQuart":
                    current /= _duration;
                    _motionValue = _endPos * current * current * current * current + _starPos;
                    break;

                case "EaseOutQuart":
                    current /= _duration;
                    current--;
                    _motionValue = -_endPos * (current * current * current * current - 1) + _starPos;
                    break;

                case "EaseInOutQuart":
                    current /= _duration / 2;
                    if (current < 1) { _motionValue = (double)_endPos / 2 * current * current * current * current + _starPos; }
                    else
                    {
                        current -= 2;
                        _motionValue = -(double)_endPos / 2 * (current * current * current * current - 2) + _starPos;
                    }
                    break;

                case "EaseInQuint":
                    current /= _duration;
                    _motionValue = _endPos * current * current * current * current * current + _starPos;
                    break;

                case "EaseOutQuint":
                    current /= _duration;
                    current--;
                    _motionValue = _endPos * (current * current * current * current * current + 1) + _starPos;
                    break;

                case "EaseInOutQuint":
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

                case "EaseInSine":
                    _motionValue = -_endPos * Math.Cos(current / _duration * (Math.PI / 2)) + _endPos + _starPos;
                    break;

                case "EaseOutSine":
                    _motionValue = _endPos * Math.Sin(current / _duration * (Math.PI / 2)) + _starPos;
                    break;

                case "EaseInOutSine":
                    _motionValue = -(double)_endPos / 2 * (Math.Cos(Math.PI * current / _duration) - 1) + _starPos;
                    break;

                case "EaseInExpo":
                    _motionValue = _endPos * Math.Pow(2, 10 * (current / _duration - 1)) + _starPos;
                    break;

                case "EaseOutExpo":
                    _motionValue = _endPos * (-Math.Pow(2, -10 * current / _duration) + 1) + _starPos;
                    break;

                case "EaseInOutExpo":
                    current /= _duration / 2;
                    if (current < 1) { _motionValue = (double)_endPos / 2 * Math.Pow(2, 10 * (current - 1)) + _starPos; }
                    else
                    {
                        current--;
                        _motionValue = (double)_endPos / 2 * (-Math.Pow(2, -10 * current) + 2) + _starPos;
                    }
                    break;

                case "EaseInCirc":
                    current /= _duration;
                    _motionValue = -(double)_endPos * (Math.Sqrt(1 - current * current) - 1) + _starPos;
                    break;

                case "EaseOutCirc":
                    current /= _duration;
                    current--;
                    _motionValue = _endPos * Math.Sqrt(1 - current * current) + _starPos;
                    break;

                case "EaseInOutCirc":
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