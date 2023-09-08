using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentManagementSystem.ViewModel
{
    public class StudentRegistrationViewModel
    {

        public StudentRegistrationViewModel()
        {
            groupData = new List<SelectListItem>(); //initializing the list as it was nullable property before
            levelData = new List<SelectListItem>();
            courseData = new List<SelectListItem>();
        }
        public int GroupId { get; set; }
        public string? groupName { get; set; }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNo { get; set; }
        public int LevelId { get; set; }
        public string? levelName { get; set; }
        public int CourseId { get; set; }
        public string? courseName { get; set; }

        public List<SelectListItem> groupData { get; set; }
        public List<SelectListItem> levelData { get; set; }
        public List<SelectListItem> courseData { get; set; }
    }
}
