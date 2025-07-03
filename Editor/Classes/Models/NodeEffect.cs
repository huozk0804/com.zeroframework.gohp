using System;
using ZeroFramework.Goap;

namespace ZeroFramework.Editor.Package
{
    public class NodeEffect
    {
        public IEffect Effect { get; set; }
        public Guid[] Connections { get; set; } = {};
    }
}