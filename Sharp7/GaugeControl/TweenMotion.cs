// by Antonis Ntit (antonis68)
// email: spanomarias68@gmail.com
using System;

namespace TweenMotionLib
{
    /// <summary>
    /// 
    /// </summary>
    public class TweenMotion
    {
        private readonly System.Timers.Timer timer = new System.Timers.Timer();
        /// <summary>
        /// Set tick interval. Default is 20
        /// </summary>
        public int TickInterval { get; set; }
        /// <summary>
        ///        
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        public delegate void OnMotion(double value, string type);
        /// <summary>
        /// This event is activated when called Start function 
        /// </summary>
        public event OnMotion onMotion;
        private double timerValue;
        private int starPos;
        private int endPos;
        private double duration;
       
        /// <summary>
        /// Array string of type motion
        /// </summary>
        public string[] typeArray = {"linearTween","easeInQuad","easeOutQuad","easeInOutQuad",
                                     "easeInCubic","easeOutCubic","easeInOutCubic","easeInQuart",
                                     "easeOutQuart","easeInOutQuart","easeInQuint","easeOutQuint",
                                     "easeInOutQuint","easeInSine","easeOutSine","easeInOutSine",
                                     "easeInExpo","easeOutExpo","easeInOutExpo","easeInCirc",
                                     "easeOutCirc","easeInOutCirc"
                                     };
        private double motionValue = 0;
        private string type = "";
        /// <summary>
        /// 
        /// </summary>
        public TweenMotion()
        {
            if (TickInterval <= 0) { TickInterval = 20; }
            timerValue = TickInterval;
            timer.Interval = TickInterval;
            timer.Elapsed += Timer_Tick;           
        }
        /// <summary>
        /// Start the motion
        /// </summary>
        /// <param name="type">Is the type of motion in string. Example "easeInSine" or TweenMotionLib.typeMotion.easeInSine</param>
        /// <param name="starPos">Start position </param>
        /// <param name="endPos">End position </param>
        /// <param name="duration">Duration in seconds</param>
        public void Start(string type, int starPos, int endPos, double duration)
        {
            Stop();     
            timerValue = 0;
            this.starPos = starPos;
            this.endPos = endPos - starPos;
            this.duration = duration * 1000;
            this.type = type;
            timer.Start();          
        }
        /// <summary>
        /// Stop motion
        /// </summary>
        public void Stop()
        {
            timer.Stop();
         }
        /// <summary>
        /// return boolean if timer is enabled
        /// </summary>
        public bool isEnable => timer.Enabled;


        private void Timer_Tick(object sender, EventArgs e)
        {

            timerValue += TickInterval;
            var current = timerValue;
            if (timerValue >= duration) { timer.Stop(); }

            switch (type)
            {
                case "linearTween":
                    motionValue = endPos * current / duration + starPos;
                    break;

                case "easeInQuad":
                    current /= duration;
                    motionValue = endPos * current * current + starPos;
                    break;

                case "easeOutQuad":
                    current /= duration;
                    motionValue = -endPos * current * (current - 2) + starPos;
                    break;

                case "easeInOutQuad":
                    current /= duration / 2;
                    if (current < 1) { motionValue = endPos / 2 * current * current + starPos; }
                    else
                    {
                        current--;
                        motionValue = -endPos / 2 * (current * (current - 2) - 1) + starPos;
                    }
                    break;

                case "easeInCubic":
                    current /= duration;
                    motionValue = endPos * current * current * current + starPos;
                    break;
                case "easeOutCubic":
                    current /= duration;
                    current--;
                    motionValue = endPos * (current * current * current + 1) + starPos;
                    break;
                case "easeInOutCubic":
                    current /= duration / 2;
                    if (current < 1) { motionValue = endPos / 2 * current * current * current + starPos; }
                    else
                    {
                        current -= 2;
                        motionValue = endPos / 2 * (current * current * current + 2) + starPos;
                    }
                    break;
                case "easeInQuart":
                    current /= duration;
                    motionValue = endPos * current * current * current * current + starPos;
                    break;


                case "easeOutQuart":
                    current /= duration;
                    current--;
                    motionValue = -endPos * (current * current * current * current - 1) + starPos;
                    break;
                case "easeInOutQuart":
                    current /= duration / 2;
                    if (current < 1) { motionValue = endPos / 2 * current * current * current * current + starPos; }
                    else
                    {
                        current -= 2;
                        motionValue = -endPos / 2 * (current * current * current * current - 2) + starPos;
                    }
                    break;

                case "easeInQuint":
                    current /= duration;
                    motionValue = endPos * current * current * current * current * current + starPos;
                    break;


                case "easeOutQuint":
                    current /= duration;
                    current--;
                    motionValue = endPos * (current * current * current * current * current + 1) + starPos;
                    break;

                case "easeInOutQuint":
                    current /= duration / 2;
                    if (current < 1)
                    {
                        motionValue = endPos / 2 * current * current * current * current *
                     current + starPos;
                    }
                    else
                    {
                        current -= 2;
                        motionValue = endPos / 2 * (current * current * current * current * current + 2) + starPos;
                    }
                    break;
                case "easeInSine":
                    motionValue = -endPos * Math.Cos(current / duration * (Math.PI / 2)) + endPos + starPos;
                    break;


                case "easeOutSine":
                    motionValue = endPos * Math.Sin(current / duration * (Math.PI / 2)) + starPos;
                    break;

                case "easeInOutSine":
                    motionValue = -endPos / 2 * (Math.Cos(Math.PI * current / duration) - 1) + starPos;

                    break;

                case "easeInExpo":
                    motionValue = endPos * Math.Pow(2, 10 * (current / duration - 1)) + starPos;
                    break;

                case "easeOutExpo":
                    motionValue = endPos * (-Math.Pow(2, -10 * current / duration) + 1) + starPos;
                    break;

                case "easeInOutExpo":
                    current /= duration / 2;
                    if (current < 1) { motionValue = endPos / 2 * Math.Pow(2, 10 * (current - 1)) + starPos; }
                    else
                    {
                        current--;
                        motionValue = endPos / 2 * (-Math.Pow(2, -10 * current) + 2) + starPos;
                    }
                    break;
                case "easeInCirc":
                    current /= duration;
                    motionValue = -endPos * (Math.Sqrt(1 - current * current) - 1) + starPos;
                    break;

                case "easeOutCirc":
                    current /= duration;
                    current--;
                    motionValue = endPos * Math.Sqrt(1 - current * current) + starPos;
                    break;

                case "easeInOutCirc":
                    current /= duration / 2;
                    if (current < 1) { motionValue = -endPos / 2 * (Math.Sqrt(1 - current * current) - 1) + starPos; }
                    else
                    {
                        current -= 2;
                        motionValue = endPos / 2 * (Math.Sqrt(1 - current * current) + 1) + starPos;
                    }
                    break;
            }
            onMotion(motionValue, type);


        }

    }
    /// <summary>
    /// Types of motion
    /// </summary>
    public class typeMotion
    {

        public static string linearTween => "linearTween";
        public static string easeInQuad => "easeInQuad";
        public static string easeOutQuad => "easeOutQuad";
        public static string easeInOutQuad => "easeInOutQuad";
        public static string easeInCubic => "easeInCubic";
        public static string easeOutCubic => "easeOutCubic";
        public static string easeInOutCubic => "easeInOutCubic";
        public static string easeInQuart => "easeInQuart";
        public static string easeOutQuart => "easeOutQuart";
        public static string easeInOutQuart => "easeInOutQuart";
        public static string easeInQuint => "easeInQuint";
        public static string easeOutQuint => "easeOutQuint";
        public static string easeInOutQuint => "easeInOutQuint";
        public static string easeInSine => "easeInSine";
        public static string easeOutSine => "easeOutSine";
        public static string easeInOutSine => "easeInOutSine";
        public static string easeInExpo => "easeInExpo";
        public static string easeOutExpo => "easeOutExpo";
        public static string easeInOutExpo => "easeInOutExpo";
        public static string easeInCirc => "easeInCirc";
        public static string easeOutCirc => "easeOutCirc";
        public static string easeInOutCirc => "easeInOutCirc";
    }



}
