//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using Keystone.Goap.Agent;

namespace Keystone.Goap
{
    public class WaitThenCompleteActionRunState : ActionRunState
    {
        private float _time;
        private readonly bool _mayResolve;

        public WaitThenCompleteActionRunState(float time, bool mayResolve)
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
            return false;
        }

        public override bool IsCompleted(IAgent agent)
        {
            return _time <= 0f;
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