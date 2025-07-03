//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;

namespace ZeroFramework.Goap
{
    public class GlobalSensor : IGlobalSensor
    {
        public Action<IWorldData> SenseMethod;
        public Type Key { get; set; }

        public void Created() { }

        public Type[] GetKeys() => new[] { Key };

        public void Sense(IWorldData data) => SenseMethod(data);
    }
}
