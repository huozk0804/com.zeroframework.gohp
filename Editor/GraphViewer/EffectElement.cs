using UnityEngine.UIElements;
using Keystone.Goap;

namespace Keystone.Editor.Package
{
    public class EffectElement : VisualElement
    {
        public INodeEffect GraphEffect { get; }

        public EffectElement(INodeEffect graphEffect)
        {
            GraphEffect = graphEffect;
            AddToClassList("effect");
            
            Label = new Label(GetText(graphEffect.Effect));
            Add(Label);
        }
        
        private string GetText(IEffect effect)
        {
            var suffix = effect.Increase ? "++" : "--";

            return $"{effect.WorldKey.Name}{suffix}";
        }

        public Label Label { get; set; }
    }
}