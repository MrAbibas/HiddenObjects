using System;

namespace App.Gameplay.Common
{
    public class Timer
    {
        public event Action<float> OnTimeUpdated;
        public float Duration { get; private set; }
        public float ElapsedTime { get; private set; }
        public float TimeLeft => Duration - ElapsedTime;

        public Timer(float duration)
        {
            Duration = duration;
            ElapsedTime = 0;
        }
    }
}