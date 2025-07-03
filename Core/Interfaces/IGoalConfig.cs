//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;

namespace ZeroFramework.Goap
{
    public interface IGoalConfig : IClassConfig
    {
        float BaseCost { get; set; }
        List<ICondition> Conditions { get; }
    }
}