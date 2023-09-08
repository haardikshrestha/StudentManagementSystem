using StudentManagementSystem.Models;
using System.Text.RegularExpressions;

namespace StudentManagementSystem.ViewModel
{
    public class AttendanceViewModel
    {
        public AttendanceViewModel() 
        {
            GroupData = new List<Models.Group>();
            CourseData = new List<Models.Course>();
            LevelData = new List<Models.Level>();
        }
        public List<Models.Group> GroupData { get; set; }
        public List<Course> CourseData { get; set; }
        public List<Level> LevelData { get; set; }
        public DateTime Date { get; set; }
        public int courseId { get; set; }
        public int levelId { get; set; }
        public int groupId { get; set; }
        public int Id { get; set; }
    }
}
