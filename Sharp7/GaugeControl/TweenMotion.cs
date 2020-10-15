// by Antonis Ntit (antonis68)
// email: spanomarias68@gmail.com
using System;
using System.Threading;
using System.Timers;

namespace TweenMotionLib
{
    /// <summary>
    /// 
    /// </summary>
    public class TweenMotion
    {
        private System.Timers.Timer timer = new System.Timers.Timer();
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
        private double timerValue = 0;
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
            timerValue = (double)TickInterval;
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
        public bool isEnable
        {
            get
            {
                return timer.Enabled;
            }
        }


        private void Timer_Tick(object sender, EventArgs e)
        {

            timerValue += TickInterval;
            double current = timerValue;
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

        public static string linearTween { get { return "linearTween"; } }
        public static string easeInQuad { get { return "easeInQuad"; } }
        public static string easeOutQuad { get { return "easeOutQuad"; } }
        public static string easeInOutQuad { get { return "easeInOutQuad"; } }
        public static string easeInCubic { get { return "easeInCubic"; } }
        public static string easeOutCubic { get { return "easeOutCubic"; } }
        public static string easeInOutCubic { get { return "easeInOutCubic"; } }
        public static string easeInQuart { get { return "easeInQuart"; } }
        public static string easeOutQuart { get { return "easeOutQuart"; } }
        public static string easeInOutQuart { get { return "easeInOutQuart"; } }
        public static string easeInQuint { get { return "easeInQuint"; } }
        public static string easeOutQuint { get { return "easeOutQuint"; } }
        public static string easeInOutQuint { get { return "easeInOutQuint"; } }
        public static string easeInSine { get { return "easeInSine"; } }
        public static string easeOutSine { get { return "easeOutSine"; } }
        public static string easeInOutSine { get { return "easeInOutSine"; } }
        public static string easeInExpo { get { return "easeInExpo"; } }
        public static string easeOutExpo { get { return "easeOutExpo"; } }
        public static string easeInOutExpo { get { return "easeInOutExpo"; } }
        public static string easeInCirc { get { return "easeInCirc"; } }
        public static string easeOutCirc { get { return "easeOutCirc"; } }
        public static string easeInOutCirc { get { return "easeInOutCirc"; } }
    }



}
