//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Linq;

namespace ZeroFramework.Goap
{
    public class ActionEffectsValidator : IValidator<IAgentTypeConfig>
    {
        public void Validate(IAgentTypeConfig agentTypeConfig, IValidationResults results)
        {
            var missing = agentTypeConfig.Actions.Where(x => !x.Effects.Any()).ToArray();

            if (!missing.Any())
                return;

            results.AddWarning($"Actions without Effects: {string.Join(", ", missing.Select(x => x.Name))}");
        }
    }
}
