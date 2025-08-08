//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using Keystone.Goap.Agent;

namespace Keystone.Goap
{
    public interface IMultiSensor : IHasConfig<IMultiSensorConfig>, ILocalSensor, IGlobalSensor
    {
        string[] GetSensors();
        Dictionary<Type, ILocalSensor> LocalSensors { get; }
        Dictionary<Type, IGlobalSensor> GlobalSensors { get; }
    }

    public interface ISensor
    {
        public void Created();
        public Type[] GetKeys();
    }

    public interface ILocalSensor : ISensor
    {
        public void Update();
        public void Sense(IWorldData data, IActionReceiver agent, IComponentReference references);
    }

    public interface IGlobalSensor : ISensor
    {
        public void Sense(IWorldData data);
    }
}