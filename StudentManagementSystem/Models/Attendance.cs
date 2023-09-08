namespace StudentManagementSystem.Models
{
    public class Attendance
    {
        public Attendance()
        {
              AttendanceDetails = new List<AttendanceDetails>();
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int courseId { get; set; }
        public int levelId { get; set; }
        public int groupId { get; set; }

        public IEnumerable<AttendanceDetails> AttendanceDetails { get; set; }
    }
}
