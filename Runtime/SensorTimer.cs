//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using Keystone.Goap.Agent;

namespace Keystone.Goap
{
    public static class SensorTimer
    {
        public static AlwaysSensorTimer Always { get; } = new();
        public static OnceSensorTimer Once { get; } = new();
        public static IntervalSensorTimer Interval(float interval) => new(interval);
    }

    public class AlwaysSensorTimer : ISensorTimer
    {
        public bool ShouldSense(ITimer timer)
        {
            return true;
        }
    }

    public class OnceSensorTimer : ISensorTimer
    {
        public bool ShouldSense(ITimer timer)
        {
            if (timer == null)
                return true;

            return false;
        }
    }

    public class IntervalSensorTimer : ISensorTimer
    {
        private readonly float _interval;

        public IntervalSensorTimer(float interval)
        {
            _interval = interval;
        }

        public bool ShouldSense(ITimer timer)
        {
            if (timer == null)
                return true;

            if (timer.GetElapsed() >= _interval)
                return true;

            return false;
        }
    }
}