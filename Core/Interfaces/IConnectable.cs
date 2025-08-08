//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;

namespace Keystone.Goap
{
    public interface IConnectable
    {
        Guid Guid { get; }
        IEffect[] Effects { get; }
        ICondition[] Conditions { get; }
    }
}