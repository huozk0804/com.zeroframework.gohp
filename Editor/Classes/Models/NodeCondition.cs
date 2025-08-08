using System;

namespace Keystone.Editor.Package
{
    public class NodeCondition
    {
        public string Condition { get; set; }
        
        public Guid[] Connections { get; set; } = {};
    }
}