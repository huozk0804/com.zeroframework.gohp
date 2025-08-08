using System;
using Keystone.Goap;

namespace Keystone.Editor.Package
{
    public class NodeEffect
    {
        public IEffect Effect { get; set; }
        public Guid[] Connections { get; set; } = {};
    }
}