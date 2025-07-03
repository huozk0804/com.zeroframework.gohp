//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace ZeroFramework.Goap
{
    public class EnabledBuilder : IEnabledBuilder
    {
        private readonly List<IConnectable> _actionIndexList;
        private readonly bool[] _enabledList;

        public EnabledBuilder(List<IConnectable> actionIndexList)
        {
            _actionIndexList = actionIndexList;
            _enabledList = _actionIndexList.Select(x => true).ToArray();
        }

        public IEnabledBuilder SetEnabled(IConnectable action, bool executable)
        {
            var index = GetIndex(action);

            if (index == -1)
                return this;

            _enabledList[index] = executable;

            return this;
        }

        private int GetIndex(IConnectable condition)
        {
            for (var i = 0; i < _actionIndexList.Count; i++)
            {
                if (_actionIndexList[i] == condition)
                    return i;
            }

            return -1;
        }

        public void Clear()
        {
            for (var i = 0; i < _enabledList.Length; i++)
            {
                _enabledList[i] = true;
            }
        }

        public bool[] Build()
        {
            return _enabledList;
        }
    }
}