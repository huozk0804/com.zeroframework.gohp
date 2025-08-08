using System.Linq;
using UnityEngine.UIElements;
using Keystone.Goap.Agent;

namespace Keystone.Editor.Package
{
    public class LogDrawer : VisualElement
    {
        private readonly ILogger logger;
        private Label text;

        public LogDrawer(ILogger logger)
        {
            this.logger = logger;
            var card = new Card((card) =>
            {
                card.Add(new Label("Logs:"));
                
                text = new Label();
                card.Add(text);
            });
            
            Add(card);
            
            schedule.Execute(() =>
            {
                Update();
            }).Every(33);
        }

        private void Update()
        {
            var logs = logger.Logs.ToArray().Reverse();
            
            text.text = string.Join("\n", logs);
        }
    }
}