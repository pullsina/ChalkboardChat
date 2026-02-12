namespace ChalkboardChat.UI.ViewModels
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string Text { get; set; } = "";
        public DateTime Date { get; set; }
        public bool IsMine { get; set; }
        public bool IsDeleted { get; set; }
    }
}
