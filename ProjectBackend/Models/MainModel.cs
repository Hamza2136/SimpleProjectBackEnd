namespace ProjectBackend.Models
{
    public class MainModel
    {
        public int SerialNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string UploadLink { get; set; } = Guid.NewGuid().ToString();

        public MainModel()
        {
            ExpiryDate = DateTime.UtcNow.AddHours(60);
        }
    }
}
