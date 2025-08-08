//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Linq;

namespace Keystone.Goap
{
    public class TargetSensorClassTypeValidator : IValidator<IAgentTypeConfig>
    {
        public void Validate(IAgentTypeConfig agentTypeConfig, IValidationResults results)
        {
            var empty = agentTypeConfig.TargetSensors.Where(x => string.IsNullOrEmpty(x.ClassType)).ToArray();

            if (!empty.Any())
                return;

            results.AddError($"Target Sensors without ClassType: {string.Join(", ", empty.Select(x => x.Name))}");
        }
    }
}
