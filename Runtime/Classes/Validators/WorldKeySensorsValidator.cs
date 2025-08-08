//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Linq;

namespace Keystone.Goap
{
    public class WorldKeySensorsValidator : IValidator<IAgentTypeConfig>
    {
        public void Validate(IAgentTypeConfig agentTypeConfig, IValidationResults results)
        {
            var required = agentTypeConfig.GetWorldKeys().Select(x => x.Name).Distinct();
            var provided = GetWorldSensorKeys(agentTypeConfig).Concat(GetMultiSensorKeys(agentTypeConfig));

            var missing = required.Except(provided).ToHashSet();

            if (!missing.Any())
                return;

            results.AddWarning($"WorldKeys without sensors: {string.Join(", ", missing)}");
        }

        private string[] GetWorldSensorKeys(IAgentTypeConfig agentTypeConfig)
        {
            return agentTypeConfig.WorldSensors
                .Where(x => x.Key != null)
                .Select(x => x.Key.Name)
                .Distinct()
                .ToArray();
        }

        private string[] GetMultiSensorKeys(IAgentTypeConfig agentTypeConfig)
        {
            var sensors = new ClassResolver().Load<IMultiSensor, IMultiSensorConfig>(agentTypeConfig.MultiSensors);

            return sensors
                .SelectMany(x => x.GetKeys())
                .Select(x => x.GetGenericTypeName())
                .Distinct()
                .ToArray();
        }
    }
}
