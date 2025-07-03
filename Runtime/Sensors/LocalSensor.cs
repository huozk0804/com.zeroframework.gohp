//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using ZeroFramework.Goap.Agent;

namespace ZeroFramework.Goap
{
    public class LocalSensor : ILocalSensor
    {
        public Action<IWorldData, IActionReceiver, IComponentReference> SenseMethod;
        public Type Key { get; set; }

        public Type[] GetKeys() => new[] { Key };

        public void Created()
        {
        }

        public void Update()
        {
        }

        public void Sense(IWorldData data, IActionReceiver agent, IComponentReference references) =>
            SenseMethod(data, agent, references);
    }
}