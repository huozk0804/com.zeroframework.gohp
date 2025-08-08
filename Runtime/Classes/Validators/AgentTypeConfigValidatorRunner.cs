//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;

namespace Keystone.Goap
{
    public class AgentTypeConfigValidatorRunner : IAgentTypeConfigValidatorRunner
    {
        private readonly List<IValidator<IAgentTypeConfig>> _validators = new()
        {
            new WorldKeySensorsValidator(),
            new TargetKeySensorsValidator(),
            new ActionClassTypeValidator(),
            new GoalClassTypeValidator(),
            new TargetSensorClassTypeValidator(),
            new WorldSensorClassTypeValidator(),
            new ActionEffectsValidator(),
            new GoalConditionsValidator(),
            new ActionTargetValidator(),
            new ActionEffectKeyValidator(),
            new ActionConditionKeyValidator(),
            new GoalConditionsValidator(),
            new GoalConditionKeyValidator(),
            new WorldSensorKeyValidator(),
            new TargetSensorKeyValidator(),
            new DuplicateTargetSensorValidator(),
            new DuplicateWorldSensorValidator(),
        };

        public IValidationResults Validate(IAgentTypeConfig agentTypeConfig)
        {
            var results = new ValidationResults(agentTypeConfig.Name);

            foreach (var validator in _validators)
            {
                validator.Validate(agentTypeConfig, results);
            }

            return results;
        }
    }
}
