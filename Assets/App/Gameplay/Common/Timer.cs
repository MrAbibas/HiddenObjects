using System;
using UnityEngine;
using VContainer;

namespace App.Gameplay.Common
{
    public class Timer
    {
        public event Action<float> OnTimeUpdated;
        public event Action OnTimerCompleted;
        public float Duration { get; private set; }
        public float ElapsedTime { get; private set; }
        public float TimeLeft => Duration - ElapsedTime;
        private bool _timerCompleted;

        [Inject]
        public Timer()
        {
            Duration = 0;
            ElapsedTime = 0;
            _timerCompleted = false;
        }

        public void Start(float duration)
        {
            Duration = duration;
            ElapsedTime = 0;
            _timerCompleted = false;
        }

        public void Tick(float deltaTime)
        {
            if(_timerCompleted) return;
            
            ElapsedTime = Mathf.Clamp(ElapsedTime + deltaTime, 0, Duration);
            OnTimeUpdated?.Invoke(ElapsedTime);
            if (ElapsedTime >= Duration)
            {
                _timerCompleted = true;
                OnTimerCompleted?.Invoke();
            }
        }
    }
}