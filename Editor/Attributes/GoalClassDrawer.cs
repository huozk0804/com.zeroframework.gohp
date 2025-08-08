using UnityEditor;
using Keystone.Goap;

namespace Keystone.Editor.Package
{
    [CustomPropertyDrawer(typeof(GoalClassAttribute))]
    public class GoalClassDrawer : ClassDrawerBase<IGoal>
    {
    }
}