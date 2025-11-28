using System;
using VContainer;

namespace App.Gameplay.Common
{
    public class Timer
    {
        public event Action<float> OnTimeUpdated;
        public float Duration { get; private set; }
        public float ElapsedTime { get; private set; }
        public float TimeLeft => Duration - ElapsedTime;

        [Inject]
        public Timer()
        {
            Duration = 0;
            ElapsedTime = 0;
        }
        
        public Timer(float duration)
        {
            Duration = duration;
            ElapsedTime = 0;
        }

        public void Init(float duration)
        {
            Duration = duration;
            ElapsedTime = 0;
        }
    }
}