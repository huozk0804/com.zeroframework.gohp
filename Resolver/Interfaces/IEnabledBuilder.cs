//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace Keystone.Goap
{
    public interface IEnabledBuilder
    {
        IEnabledBuilder SetEnabled(IConnectable action, bool executable);
        void Clear();
        bool[] Build();
    }
}