//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace Keystone.Goap.Agent
{
    public interface IAgentEvents
    {
        // Actions
        event ActionDelegate OnActionStart;
        void ActionStart(IAction action);

        event ActionDelegate OnActionEnd;
        void ActionEnd(IAction action);

        event ActionDelegate OnActionStop;
        void ActionStop(IAction action);

        event ActionDelegate OnActionComplete;
        void ActionComplete(IAction action);

        // Targets
        event TargetDelegate OnTargetInRange;
        void TargetInRange(ITarget target);

        event TargetDelegate OnTargetNotInRange;
        void TargetNotInRange(ITarget target);

        event TargetRangeDelegate OnTargetChanged;
        void TargetChanged(ITarget target, bool inRange);

        event EmptyDelegate OnTargetLost;
        void TargetLost();

        event TargetDelegate OnMove;
        void Move(ITarget target);

        event EmptyDelegate OnPause;
        void Pause();

        event EmptyDelegate OnResume;
        void Resume();
    }
}
