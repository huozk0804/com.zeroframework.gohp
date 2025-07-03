//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace ZeroFramework.Goap
{
    public class TargetKey : ITargetKey
    {
        public TargetKey(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
