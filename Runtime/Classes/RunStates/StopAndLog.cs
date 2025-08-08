//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using Keystone.Goap.Agent;

namespace Keystone.Goap
{
    public class StopAndLog : ActionRunState
    {
        private readonly string _message;

        public StopAndLog(string message)
        {
            _message = message;
        }

        public override void Update(IAgent agent, IActionContext context)
        {
        }

        public override bool ShouldStop(IAgent agent)
        {
            agent.Logger.Log(_message);
            return true;
        }

        public override bool ShouldPerform(IAgent agent)
        {
            return false;
        }

        public override bool IsCompleted(IAgent agent)
        {
            return false;
        }

        public override bool MayResolve(IAgent agent)
        {
            return false;
        }

        public override bool IsRunning()
        {
            return false;
        }
    }
}