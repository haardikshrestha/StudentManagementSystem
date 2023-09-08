namespace StudentManagementSystem.Models
{
    public class Signup
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? PhoneNo { get; set;}
        public DateTime CreatedDate { get; set; }
    }
}
