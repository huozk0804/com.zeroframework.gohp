using UnityEngine;
using UnityEngine.UIElements;

namespace ZeroFramework.Editor.Package
{
    public class HorizontalSplitView : VisualElement
    {
        private VisualElement leftElement;
        private VisualElement rightElement;
        private float leftWidthPercentage;

        public HorizontalSplitView(VisualElement left, VisualElement right, float percentage)
        {
            // Ensure the percentage is between 0 and 100
            leftWidthPercentage = Mathf.Clamp(percentage, 0f, 100f);

            leftElement = left;
            rightElement = right;

            // Style the HorizontalSplitView
            style.flexDirection = FlexDirection.Row;

            // Add and style children
            Add(leftElement);
            Add(rightElement);

            UpdateLayout();
        }

        private void UpdateLayout()
        {
            leftElement.style.flexGrow = 0;
            leftElement.style.flexBasis = Length.Percent(leftWidthPercentage);

            rightElement.style.flexGrow = 1;
            rightElement.style.flexBasis = Length.Percent(100 - leftWidthPercentage);
        }

        public void SetLeftWidthPercentage(float percentage)
        {
            leftWidthPercentage = Mathf.Clamp(percentage, 0f, 100f);
            UpdateLayout();
        }

        public void ReplaceLeftElement(VisualElement newLeftElement)
        {
            leftElement = newLeftElement;
            Clear();
            Add(leftElement);
            Add(rightElement);
            UpdateLayout();
        }
    }
}