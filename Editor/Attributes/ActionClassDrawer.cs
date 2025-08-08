using UnityEditor;
using Keystone.Goap;
using Keystone.Goap.Agent;

namespace Keystone.Editor.Package
{
    [CustomPropertyDrawer(typeof(ActionClassAttribute))]
    public class ActionClassDrawer : ClassDrawerBase<IAction>
    {
    }
}