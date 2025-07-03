//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using UnityEngine;

namespace ZeroFramework.Goap.Agent
{
    public class Timer : ITimer
    {
        private float _lastTouch = 0;

        public void Touch()
        {
            _lastTouch = Time.realtimeSinceStartup;
        }

        public float GetElapsed()
        {
            return Time.realtimeSinceStartup - _lastTouch;
        }

        public bool IsRunningFor(float time)
        {
            return GetElapsed() >= time;
        }
    }
}
