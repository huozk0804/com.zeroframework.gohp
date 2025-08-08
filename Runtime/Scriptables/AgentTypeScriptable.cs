//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Keystone.Goap
{
    [CreateAssetMenu(menuName = "Goap/AgentTypeConfig")]
    public class AgentTypeScriptable : ScriptableObject
    {
        [FormerlySerializedAs("capabilityFactories")]
        public List<ScriptableCapabilityFactoryBase> capabilities = new();

        public string Name => name;

        public IAgentTypeConfig Create()
        {
            var configs = capabilities
                .Select(behaviour => behaviour.Create())
                .ToList();

            return new AgentTypeConfig(name)
            {
                Goals = configs.SelectMany(x => x.Goals).ToList(),
                Actions = configs.SelectMany(x => x.Actions).ToList(),
                WorldSensors = configs.SelectMany(x => x.WorldSensors).ToList(),
                TargetSensors = configs.SelectMany(x => x.TargetSensors).ToList(),
                MultiSensors = configs.SelectMany(x => x.MultiSensors).ToList(),
            };
        }
    }
}
