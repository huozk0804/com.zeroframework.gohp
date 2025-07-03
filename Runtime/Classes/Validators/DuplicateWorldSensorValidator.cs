//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Linq;

namespace ZeroFramework.Goap
{
    public class DuplicateWorldSensorValidator : IValidator<IAgentTypeConfig>
    {
        public void Validate(IAgentTypeConfig config, IValidationResults results)
        {
            var provided = GetWorldSensorKeys(config).Concat(GetMultiSensorKeys(config));
            var duplicates = provided
                .GroupBy(x => x)
                .Where(x => x.Count() > 1)
                .Select(x => x.Key)
                .ToArray();

            if (!duplicates.Any())
                return;

            results.AddWarning($"Duplicate world sensor keys: {string.Join(", ", duplicates)}");
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
            var temp = new ClassResolver().Load<IMultiSensor, IMultiSensorConfig>(agentTypeConfig.MultiSensors);

            return temp
                .SelectMany(x => x.GetKeys())
                .Select(x => x.GetGenericTypeName())
                .Distinct()
                .ToArray();
        }
    }
}