using UnityEditor;
using ZeroFramework.Goap;

namespace ZeroFramework.Editor.Package
{
    [CustomPropertyDrawer(typeof(GoalClassAttribute))]
    public class GoalClassDrawer : ClassDrawerBase<IGoal>
    {
    }
}