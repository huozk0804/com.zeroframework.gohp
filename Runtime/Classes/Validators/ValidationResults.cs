//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace Keystone.Goap
{
    public class ValidationResults : IValidationResults
    {
        private readonly string _name;
        private readonly List<string> _errors = new();
        private readonly List<string> _warnings = new();

        public ValidationResults(string name)
        {
            _name = name;
        }

        public void AddError(string error)
        {
            _errors.Add($"[{_name}] {error}");
        }

        public void AddWarning(string warning)
        {
            _warnings.Add($"[{_name}] {warning}");
        }

        public List<string> GetErrors()
        {
            return _errors;
        }

        public List<string> GetWarnings()
        {
            return _warnings;
        }

        public bool HasErrors()
        {
            return _errors.Any();
        }

        public bool HasWarnings()
        {
            return _warnings.Any();
        }
    }
}