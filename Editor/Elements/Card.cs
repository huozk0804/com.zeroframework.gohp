using System;
using UnityEngine.UIElements;

namespace Keystone.Editor.Package
{
    public class Card : VisualElement
    {
        public Card(Action<Card> callback)
        {
            name = "card";
            callback(this);
        }
    }
}