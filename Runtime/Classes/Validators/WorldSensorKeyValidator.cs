//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Linq;

namespace Keystone.Goap
{
    public class WorldSensorKeyValidator : IValidator<IAgentTypeConfig>
    {
        public void Validate(IAgentTypeConfig agentTypeConfig, IValidationResults results)
        {
            var missing = agentTypeConfig.WorldSensors.Where(x => x.Key == null).ToArray();

            if (!missing.Any())
                return;

            results.AddError($"WorldSensors without Key: {string.Join(", ", missing.Select(x => x.Name))}");
        }
    }
}
