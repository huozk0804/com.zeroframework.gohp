using UnityEngine.UIElements;
using ZeroFramework.Goap;

namespace ZeroFramework.Editor.Package
{
    public class CapabilitySensorElement : VisualElement, IFoldable
    {
        public Foldout Foldout { get; set; }
        public ClassRefField SensorField { get; set; }
        public ClassRefField KeyField { get; set; }

        public Label LabelField { get; set; }
        
        public CapabilitySensorElement(CapabilityConfigScriptable scriptable, GeneratorScriptable generator, CapabilitySensor sensor)
        {
            Foldout = new Foldout
            {
                value = false,
            };
            Add(Foldout);
            
            Foldout.Add(new Card((card) =>
            {
                var sensorLabel = new LabeledField<ClassRefField>("Sensor");
                SensorField = sensorLabel.Field;
                card.Add(sensorLabel);

                if (sensor is CapabilityMultiSensor)
                {
                    var sensorsLabel = new LabeledField<Label>("Keys");
                    LabelField = sensorsLabel.Field;
                    card.Add(sensorsLabel);
                    return;
                }
            
                var keyLabel = new LabeledField<ClassRefField>("Key");
                KeyField = keyLabel.Field;
                card.Add(keyLabel);
            }));
        }
    }
}