namespace StudentManagementSystem.Models
{
    public class AttendanceDetails
    {
        public int Id { get; set; }
        public int AttendanceId { get; set; }
        public int studentId { get; set; }
        public byte AbsentPresentStatus { get; set;}

        public Attendance? Attendance { get; set; }
    }
}
