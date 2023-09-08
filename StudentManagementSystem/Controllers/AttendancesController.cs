using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;
using StudentManagementSystem.ViewModel;

namespace StudentManagementSystem.Controllers
{
    [Route("attendance")]
    public class AttendancesController : Controller
    {
        private readonly MyDbContext _context;

        public AttendancesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Attendances
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            return _context.Attendances != null ?
                        View(await _context.Attendances.ToListAsync()) :
                        Problem("Entity set 'MyDbContext.Attendances'  is null.");
        }

        // GET: Attendances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // GET: Attendances/Create
        [Route("createget")]
        public async Task<IActionResult>  Create()
        {
            var model = new AttendanceViewModel();
            try
            {
                var groupData =  _context.Groups.Where(x => x.Name != null)
                    .Select(x => new Group
                    {
                        Id = x.Id,
                        Name = x.Name,
                    }).ToList();
                model.GroupData = groupData;
                var levelData = await _context.Levels.ToListAsync();
                model.LevelData = levelData;
                var courseData = await _context.Course.ToListAsync();   
                model.CourseData = courseData;
                model.Date = DateTime.Now.Date;
            }catch (Exception ex)
            {

            }
            ViewData["AttendanceDataList"] = model;
            return View(model);
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,courseId,levelId,groupId")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attendance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(attendance);
        }

        // GET: Attendances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,courseId,levelId,groupId")] Attendance attendance)
        {
            if (id != attendance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(attendance.Id))
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
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Attendances == null)
            {
                return Problem("Entity set 'MyDbContext.Attendances'  is null.");
            }
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance != null)
            {
                _context.Attendances.Remove(attendance);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(int id)
        {
          return (_context.Attendances?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        [HttpPost]
        [Route("getStudentData")]
        public async Task<IActionResult> getStudentData([FromBody] AttendanceFilterModel model)
        {
            var studentData = new List<StudentDataModel>();
            try
            {
                var dataExist = await _context.Attendances.FirstOrDefaultAsync(x => x.Date.Date == model.Date.Date && 
                x.courseId == model.CourseId && x.levelId == model.LevelId && x.groupId == model.GroupId);

                if (dataExist == null)
                {
                    studentData = _context.StudentRegistrations.Where(x => x.LevelId == model.LevelId &&
                    x.CourseId == model.CourseId && x.GroupId == model.GroupId).
                    Select(x => new StudentDataModel
					{
                        Name = x.Name, 
                        Id = x.Id,
						CourseId = x.CourseId,
                        LevelId = x.LevelId, 
                        GroupId = x.GroupId,
                        Status = 0,
                        //Sn = i
                    }).ToList();
                }
            }
            catch (Exception ex) 
            { 

            }
            return Json(studentData);
        }
    }
}
