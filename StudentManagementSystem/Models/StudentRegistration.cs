namespace StudentManagementSystem.Models
{
    public class StudentRegistration
    {
        public int GroupId { get; set; }
        public int LevelId { get; set; }
        public int CourseId { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNo { get; set; }
    }
}
