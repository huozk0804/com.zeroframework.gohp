//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace Keystone.Goap
{
    public class WorldKey : IWorldKey
    {
        public WorldKey(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
