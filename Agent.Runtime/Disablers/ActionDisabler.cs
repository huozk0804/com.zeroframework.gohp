//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

namespace ZeroFramework.Goap.Agent
{
    public static class ActionDisabler
    {
        public static IActionDisabler Forever => new ForeverActionDisabler();
        public static IActionDisabler ForTime(float time) => new ForTimeActionDisabler(time);
    }
}
