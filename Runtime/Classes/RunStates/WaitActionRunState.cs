//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using ZeroFramework.Goap.Agent;

namespace ZeroFramework.Goap
{
    public class WaitActionRunState : ActionRunState
    {
        private readonly bool _mayResolve;
        private float _time;

        public WaitActionRunState(float time, bool mayResolve)
        {
            _time = time;
            _mayResolve = mayResolve;
        }

        public override void Update(IAgent agent, IActionContext context)
        {
            _time -= context.DeltaTime;
        }

        public override bool ShouldStop(IAgent agent)
        {
            return false;
        }

        public override bool ShouldPerform(IAgent agent)
        {
            return _time <= 0f;
        }

        public override bool IsCompleted(IAgent agent)
        {
            return false;
        }

        public override bool MayResolve(IAgent agent)
        {
            return _mayResolve;
        }

        public override bool IsRunning()
        {
            return _time > 0f;
        }
    }
}