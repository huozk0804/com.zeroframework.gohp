//------------------------------------------------------------
// Zero Framework
// Copyright © 2025-2026 All rights reserved.
// Feedback: https://github.com/huozk0804/ZeroFramework
//------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Keystone.Goap
{
    public class PositionBuilder : IPositionBuilder
    {
        private readonly List<IConnectable> _actionIndexList;
        private readonly float3[] _executableList;

        public PositionBuilder(List<IConnectable> actionIndexList)
        {
            _actionIndexList = actionIndexList;
            _executableList = _actionIndexList.Select(x => GraphResolverJob.InvalidPosition).ToArray();
        }

        public IPositionBuilder SetPosition(IConnectable action, Vector3? position)
        {
            var index = GetIndex(action);

            if (index == -1)
                return this;

            _executableList[index] = position ?? GraphResolverJob.InvalidPosition;

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

        public float3[] Build()
        {
            return _executableList;
        }

        public void Clear()
        {
            for (var i = 0; i < _executableList.Length; i++)
            {
                _executableList[i] = GraphResolverJob.InvalidPosition;
            }
        }
    }
}