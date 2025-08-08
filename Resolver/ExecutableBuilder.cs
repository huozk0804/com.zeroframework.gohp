//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace Keystone.Goap
{
    public class ExecutableBuilder : IExecutableBuilder
    {
        private readonly List<IConnectable> _actionIndexList;
        private readonly bool[] _executableList;

        public ExecutableBuilder(List<IConnectable> actionIndexList)
        {
            _actionIndexList = actionIndexList;
            _executableList = _actionIndexList.Select(x => false).ToArray();
        }

        public IExecutableBuilder SetExecutable(IConnectable action, bool executable)
        {
            var index = GetIndex(action);

            if (index == -1)
                return this;

            _executableList[index] = executable;

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
            for (var i = 0; i < _executableList.Length; i++)
            {
                _executableList[i] = false;
            }
        }

        public bool[] Build()
        {
            return _executableList;
        }
    }
}