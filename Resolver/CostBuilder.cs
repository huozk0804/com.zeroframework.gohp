//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace ZeroFramework.Goap
{
    public class CostBuilder : ICostBuilder
    {
        private readonly List<IConnectable> _actionIndexList;
        private readonly float[] _costList;

        public CostBuilder(List<IConnectable> actionIndexList)
        {
            _actionIndexList = actionIndexList;
            _costList = _actionIndexList.Select(x => 1f).ToArray();
        }

        public ICostBuilder SetCost(IConnectable action, float cost)
        {
            var index = GetIndex(action);

            if (index == -1)
                return this;

            _costList[index] = cost;
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

        public float[] Build()
        {
            return _costList;
        }

        public void Clear()
        {
            for (var i = 0; i < _costList.Length; i++)
            {
                _costList[i] = 1f;
            }
        }
    }
}