//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Linq;

namespace Keystone.Goap
{
    public class ActionTargetValidator : IValidator<IAgentTypeConfig>
    {
        public void Validate(IAgentTypeConfig agentTypeConfig, IValidationResults results)
        {
            var missing = agentTypeConfig.Actions.Where(x => x.RequiresTarget && x.Target == null).ToArray();

            if (!missing.Any())
                return;

            results.AddWarning($"Actions without Target: {string.Join(", ", missing.Select(x => x.Name))}");
        }
    }
}
