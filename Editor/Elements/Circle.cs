using UnityEngine;
using UnityEngine.UIElements;

namespace ZeroFramework.Editor.Package
{
    public class Circle : VisualElement
    {
        public Circle(Color color, float size)
        {
            // Set the size of the circle
            style.width = size;
            style.height = size;
            
            style.maxWidth = size;
            style.maxHeight = size;

            // Set border radius to half of the size to make it a circle
            style.borderBottomLeftRadius = size / 2;
            style.borderBottomRightRadius = size / 2;
            style.borderTopLeftRadius = size / 2;
            style.borderTopRightRadius = size / 2;
            
            SetColor(color);
        }

        public void SetColor(Color color)
        {
            // Set the background color
            style.backgroundColor = color; // Replace with your desired color
        }
    }
}