// by Antonis Ntit (antonis68)
// email: spanomarias68@gmail.com

using System;

namespace GaugeControl
{
    public class TweenMotion
    {
        private readonly System.Timers.Timer _timer = new System.Timers.Timer();

        public int TickInterval { get; set; }

        public delegate void OnMotion(double value, GaugeControl.MotionType type);

        public event OnMotion MyMotion;
        private double _timerValue;
        private int _starPos;
        private int _endPos;
        private double _duration;
        private double _motionValue;
        private GaugeControl.MotionType _motionType = GaugeControl.MotionType.LinearTween;

        public TweenMotion()
        {
            if (TickInterval <= 0) { TickInterval = 10; }
            _timerValue = TickInterval;
            _timer.Interval = TickInterval;
            _timer.Elapsed += Timer_Tick;
        }

        public void Start(GaugeControl.MotionType motionType, int starPos, int endPos, double duration)
        {
            Stop();
            _timerValue = 0;
            _starPos = starPos;
            _endPos = endPos - starPos;
            _duration = duration * 1000;
            _motionType = motionType;
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

            switch (_motionType)
            {
                case GaugeControl.MotionType.LinearTween:
                    _motionValue = _endPos * current / _duration + _starPos;
                    break;

                case GaugeControl.MotionType.EaseInQuad:
                    current /= _duration;
                    _motionValue = _endPos * current * current + _starPos;
                    break;

                case GaugeControl.MotionType.EaseOutQuad:
                    current /= _duration;
                    _motionValue = -_endPos * current * (current - 2) + _starPos;
                    break;

                case GaugeControl.MotionType.EaseInOutQuad:
                    current /= _duration / 2;
                    if (current < 1) { _motionValue = (double)_endPos / 2 * current * current + _starPos; }
                    else
                    {
                        current--;
                        _motionValue = -(double)_endPos / 2 * (current * (current - 2) - 1) + _starPos;
                    }
                    break;

                case GaugeControl.MotionType.EaseInCubic:
                    current /= _duration;
                    _motionValue = _endPos * current * current * current + _starPos;
                    break;

                case GaugeControl.MotionType.EaseOutCubic:
                    current /= _duration;
                    current--;
                    _motionValue = _endPos * (current * current * current + 1) + _starPos;
                    break;

                case GaugeControl.MotionType.EaseInOutCubic:
                    current /= _duration / 2;
                    if (current < 1) { _motionValue = (double)_endPos / 2 * current * current * current + _starPos; }
                    else
                    {
                        current -= 2;
                        _motionValue = (double)_endPos / 2 * (current * current * current + 2) + _starPos;
                    }
                    break;

                case GaugeControl.MotionType.EaseInQuart:
                    current /= _duration;
                    _motionValue = _endPos * current * current * current * current + _starPos;
                    break;

                case GaugeControl.MotionType.EaseOutQuart:
                    current /= _duration;
                    current--;
                    _motionValue = -_endPos * (current * current * current * current - 1) + _starPos;
                    break;

                case GaugeControl.MotionType.EaseInOutQuart:
                    current /= _duration / 2;
                    if (current < 1) { _motionValue = (double)_endPos / 2 * current * current * current * current + _starPos; }
                    else
                    {
                        current -= 2;
                        _motionValue = -(double)_endPos / 2 * (current * current * current * current - 2) + _starPos;
                    }
                    break;

                case GaugeControl.MotionType.EaseInQuint:
                    current /= _duration;
                    _motionValue = _endPos * current * current * current * current * current + _starPos;
                    break;

                case GaugeControl.MotionType.EaseOutQuint:
                    current /= _duration;
                    current--;
                    _motionValue = _endPos * (current * current * current * current * current + 1) + _starPos;
                    break;

                case GaugeControl.MotionType.EaseInOutQuint:
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

                case GaugeControl.MotionType.EaseInSine:
                    _motionValue = -_endPos * Math.Cos(current / _duration * (Math.PI / 2)) + _endPos + _starPos;
                    break;

                case GaugeControl.MotionType.EaseOutSine:
                    _motionValue = _endPos * Math.Sin(current / _duration * (Math.PI / 2)) + _starPos;
                    break;

                case GaugeControl.MotionType.EaseInOutSine:
                    _motionValue = -(double)_endPos / 2 * (Math.Cos(Math.PI * current / _duration) - 1) + _starPos;
                    break;

                case GaugeControl.MotionType.EaseInExpo:
                    _motionValue = _endPos * Math.Pow(2, 10 * (current / _duration - 1)) + _starPos;
                    break;

                case GaugeControl.MotionType.EaseOutExpo:
                    _motionValue = _endPos * (-Math.Pow(2, -10 * current / _duration) + 1) + _starPos;
                    break;

                case GaugeControl.MotionType.EaseInOutExpo:
                    current /= _duration / 2;
                    if (current < 1) { _motionValue = (double)_endPos / 2 * Math.Pow(2, 10 * (current - 1)) + _starPos; }
                    else
                    {
                        current--;
                        _motionValue = (double)_endPos / 2 * (-Math.Pow(2, -10 * current) + 2) + _starPos;
                    }
                    break;

                case GaugeControl.MotionType.EaseInCirc:
                    current /= _duration;
                    _motionValue = -(double)_endPos * (Math.Sqrt(1 - current * current) - 1) + _starPos;
                    break;

                case GaugeControl.MotionType.EaseOutCirc:
                    current /= _duration;
                    current--;
                    _motionValue = _endPos * Math.Sqrt(1 - current * current) + _starPos;
                    break;

                case GaugeControl.MotionType.EaseInOutCirc:
                    current /= _duration / 2;
                    if (current < 1) { _motionValue = -(double)_endPos / 2 * (Math.Sqrt(1 - current * current) - 1) + _starPos; }
                    else
                    {
                        current -= 2;
                        _motionValue = (double)_endPos / 2 * (Math.Sqrt(1 - current * current) + 1) + _starPos;
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            MyMotion?.Invoke(_motionValue, _motionType);
        }
    }
}