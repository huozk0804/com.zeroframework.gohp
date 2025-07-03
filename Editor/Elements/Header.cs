using UnityEngine.UIElements;

namespace ZeroFramework.Editor.Package
{
    public class Header : VisualElement
    {
        public Header(string text)
        {
            name = "header";
            Add(new Label(text));
        }
    }
}