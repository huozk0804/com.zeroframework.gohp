using UnityEditor;
using Keystone.Goap;

namespace Keystone.Editor.Package
{
    [CustomPropertyDrawer(typeof(WorldSensorAttribute))]
    public class WorldSensorDrawer : ClassDrawerBase<IWorldSensor>
    {
    }
}