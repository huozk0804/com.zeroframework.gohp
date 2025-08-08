//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace Keystone.Goap.Agent
{
    public abstract class ActionProviderBase : MonoBehaviour, IActionProvider
    {
        private readonly Dictionary<IAction, IActionDisabler> _disables = new();

        public abstract IActionReceiver Receiver { get; set; }
        public abstract void ResolveAction();

        public bool IsDisabled(IAction action)
        {
            if (!this._disables.TryGetValue(action, out var disabler))
                return false;

            if (this.Receiver is not IMonoAgent agent)
                return false;

            if (disabler.IsDisabled(agent))
                return true;

            this.Enable(action);
            return false;
        }

        public void Enable(IAction action)
        {
            this._disables.Remove(action);
        }

        public void Disable(IAction action, IActionDisabler disabler)
        {
            this._disables[action] = disabler;
        }
    }
}
