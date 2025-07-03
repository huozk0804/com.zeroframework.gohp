using UnityEditor;
using ZeroFramework.Goap;

namespace ZeroFramework.Editor.Package
{
    [CustomPropertyDrawer(typeof(TargetSensorAttribute))]
    public class TargetSensorDrawer : ClassDrawerBase<ITargetSensor>
    {
    }
}