//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using ZeroFramework.Goap.Agent;

namespace ZeroFramework.Goap
{
    public interface IActionConfig : IClassConfig
    {
        float BaseCost { get; }
        ITargetKey Target { get; }
        float StoppingDistance { get; }
        bool ValidateTarget { get; }
        bool RequiresTarget { get; }
        bool ValidateConditions { get; }
        ICondition[] Conditions { get; }
        IEffect[] Effects { get; }
        public ActionMoveMode MoveMode { get; }
        public IActionProperties Properties { get; }
    }

    public interface IClassCallbackConfig
    {
        public Action<object> Callback { get; }
    }
}
