using UnityEditor;
using ZeroFramework.Goap;
using ZeroFramework.Goap.Agent;

namespace ZeroFramework.Editor.Package
{
    [CustomPropertyDrawer(typeof(ActionClassAttribute))]
    public class ActionClassDrawer : ClassDrawerBase<IAction>
    {
    }
}