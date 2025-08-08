//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;

namespace Keystone.Goap
{
    public class ConditionBuilder : IConditionBuilder
    {
        private readonly List<ICondition> _conditionIndexList;
        private readonly bool[] _conditionsMetList;

        public ConditionBuilder(List<ICondition> conditionIndexList)
        {
            _conditionIndexList = conditionIndexList;
            _conditionsMetList = new bool[_conditionIndexList.Count];
        }

        public IConditionBuilder SetConditionMet(ICondition condition, bool met)
        {
            var index = GetIndex(condition);

            if (index == -1)
                return this;

            _conditionsMetList[index] = met;

            return this;
        }

        private int GetIndex(ICondition condition)
        {
            for (var i = 0; i < _conditionIndexList.Count; i++)
            {
                if (_conditionIndexList[i] == condition)
                    return i;
            }

            return -1;
        }

        public bool[] Build()
        {
            return _conditionsMetList;
        }

        public void Clear()
        {
            for (var i = 0; i < _conditionsMetList.Length; i++)
            {
                _conditionsMetList[i] = false;
            }
        }
    }
}