using System;
using Keystone.Goap;

namespace Keystone.Editor.Package
{
    public class Node
    {
        public Guid Guid => Action.Guid;
        
        public IConnectable Action { get; set; }

        public NodeEffect[] Effects { get; set; } = {};
        public NodeCondition[] Conditions { get; set; } = {};
    }
}