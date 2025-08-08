using UnityEditor;
using Keystone.Goap;

namespace Keystone.Editor.Package
{
    [CustomPropertyDrawer(typeof(TargetSensorAttribute))]
    public class TargetSensorDrawer : ClassDrawerBase<ITargetSensor>
    {
    }
}