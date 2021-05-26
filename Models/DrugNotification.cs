namespace BackgroundQueue.Models
{
    public class DrugNotification
    {
        public string Message { get; set; }

        public DrugNotification(string message)
        {
            Message = message;
        }
    }
}