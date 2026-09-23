using System.Linq;
using FlaUI.Core.AutomationElements;
using Newtonsoft.Json;

namespace WeChatAuto.Models
{
    public class MessageCheckpointItem
    {

        public AutomationElement Element { get; set; }
        public int CurrentSnapTop { get; set; }
        public int[] RunTimeId { get; set; }

        public bool IsChange()
        {
            if (this.Element == null || !this.Element.IsAvailable)
                return true;
            if (this.Element.BoundingRectangle.Y != CurrentSnapTop ||
                !this.Element.Properties.RuntimeId.Value.SequenceEqual(this.RunTimeId))
                return true;

            return false;
        }
    }
}