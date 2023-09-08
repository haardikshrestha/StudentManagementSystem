using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;
using StudentManagementSystem.ViewModel;

namespace StudentManagementSystem.Controllers
{
    public class StudentRegistrationsController : Controller
    {
        private readonly MyDbContext _context;

        public StudentRegistrationsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: StudentRegistrations
        public async Task<IActionResult> Index()
        {
            var modellist = new List<StudentRegistrationViewModel>();
            try
            {
                var studentData = await _context.StudentRegistrations.ToListAsync();
                foreach (var d in studentData)
                {
                    var levelData = await _context.Levels.FirstOrDefaultAsync(x => x.levelId == d.LevelId);
                    var groupData = await _context.Groups.FirstOrDefaultAsync(x => x.Id == d.GroupId);
                    var courseData = await _context.Course.FirstOrDefaultAsync(x => x.courseId == d.CourseId);

                    modellist.Add(new StudentRegistrationViewModel
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Address= d.Address,
                        PhoneNo = d.PhoneNo,
                        LevelId = d.LevelId,
                        groupName = groupData != null ? groupData.Name : null,
                        courseName=courseData != null ? courseData.courseName : null,
                        levelName = levelData != null ? levelData.levelName : null,

                    });
                }
            }

            catch (Exception ex)
            {

            }

            return View(modellist); 
        }

        // GET: StudentRegistrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StudentRegistrations == null)
            {
                return NotFound();
            }

            var studentRegistration = await _context.StudentRegistrations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentRegistration == null)
            {
                return NotFound();
            }

            return View(studentRegistration);
        }

        // GET: StudentRegistrations/Create
        public async Task<ActionResult> Create()
        {
            var model = new StudentRegistrationViewModel();
            try
            {
                var groupList = new List<SelectListItem>();
                var levelList = new List<SelectListItem>();
                var courseList = new List<SelectListItem>();
                var getGroupData = await _context.Groups.ToListAsync();
                var getLevelData = await _context.Levels.ToListAsync();
                var getCourseData = await _context.Course.ToListAsync();
                foreach (var group in getGroupData)
                {
                    model.groupData.Add(new SelectListItem
                    { Text = group.Name, Value = group.Id.ToString() });
                }

                foreach (var level in getLevelData)
                {
                    model.levelData.Add(new SelectListItem
                    { Text = level.levelName, Value = level.levelId.ToString() });
                }

                foreach (var course in getCourseData)
                {
                    model.courseData.Add(new SelectListItem
                    { Text = course.courseName, Value = course.courseId.ToString() });
                }

            }
            catch (Exception ex)
            {

            }
            return View(model);
        }

        // POST: StudentRegistrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupId,LevelId,CourseId,Id,Name,Address,Email,PhoneNo")] StudentRegistration studentRegistration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentRegistration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentRegistration);
        }

        // GET: StudentRegistrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var model = new StudentRegistrationViewModel();
            try
            {
                var groupList = new List<SelectListItem>();
                var levelList = new List<SelectListItem>();
                var courseList = new List<SelectListItem>();
                var getGroupData = await _context.Groups.ToListAsync();
                var getLevelData = await _context.Levels.ToListAsync();
                var getCourseData = await _context.Course.ToListAsync();
                foreach (var group in getGroupData)
                {
                    model.groupData.Add(new SelectListItem
                    { Text = group.Name, Value = group.Id.ToString() });
                }

                foreach (var level in getLevelData)
                {
                    model.levelData.Add(new SelectListItem
                    { Text = level.levelName, Value = level.levelId.ToString() });
                }

                foreach (var course in getCourseData)
                {
                    model.courseData.Add(new SelectListItem
                    { Text = course.courseName, Value = course.courseId.ToString() });
                }
                var studentRegistration = await _context.StudentRegistrations.FindAsync(id);
                model.Name = studentRegistration.Name;
                model.Address = studentRegistration.Address;
                model.PhoneNo = studentRegistration.PhoneNo;
                model.Email = studentRegistration.Email;
                model.GroupId = studentRegistration.GroupId;
                model.LevelId = studentRegistration.LevelId;
                model.CourseId = studentRegistration.CourseId;
            }
            catch (Exception ex)
            {

            }
            //if (id == null || _context.StudentRegistrations == null)
            //{
            //    return NotFound();
            //}

            //var studentRegistration = await _context.StudentRegistrations.FindAsync(id);
            //if (studentRegistration == null)
            //{
            //    return NotFound();
            //}
            return View(model);
        }

        // POST: StudentRegistrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupId,LevelId,CourseId,Id,Name,Address,Email,PhoneNo")] StudentRegistration studentRegistration)
        {
            if (id != studentRegistration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentRegistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentRegistrationExists(studentRegistration.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(studentRegistration);
        }

        // GET: StudentRegistrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StudentRegistrations == null)
            {
                return NotFound();
            }

            var studentRegistration = await _context.StudentRegistrations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentRegistration == null)
            {
                return NotFound();
            }

            return View(studentRegistration);
        }

        // POST: StudentRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StudentRegistrations == null)
            {
                return Problem("Entity set 'MyDbContext.StudentRegistrations'  is null.");
            }
            var studentRegistration = await _context.StudentRegistrations.FindAsync(id);
            if (studentRegistration != null)
            {
                _context.StudentRegistrations.Remove(studentRegistration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentRegistrationExists(int id)
        {
            return (_context.StudentRegistrations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
