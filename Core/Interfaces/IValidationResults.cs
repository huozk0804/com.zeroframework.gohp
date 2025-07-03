//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;

namespace ZeroFramework.Goap
{
    public interface IValidationResults
    {
        void AddError(string error);
        void AddWarning(string warning);
        List<string> GetErrors();
        List<string> GetWarnings();
        bool HasErrors();
        bool HasWarnings();
    }
}