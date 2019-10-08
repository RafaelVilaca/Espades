using Espades.Common.Enumerators;

namespace Espades.Common.Containers
{
    public class Message
    {
        public Message(string message)
        {
            Text = message;
        }

        public Message(string message, StatusResult status, int time)
        {
            Text = message;
            Status = status;
            Time = time;
        }

        public string Text { get; set; }
        public StatusResult? Status { get; set; } = null;
        public int? Time { get; set; } = null;
    }
}
