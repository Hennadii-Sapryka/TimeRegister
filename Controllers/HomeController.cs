using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TimeRegisterApp.Context;
using TimeRegisterApp.Models;
using TimeRegisterApp.Services;

namespace TimeRegisterApp.Controllers
{
    public class HomeController : Controller
    {
        readonly TimeCounterService timeCounter;
        private ApplicationContext db;

        public HomeController(ApplicationContext context, TimeCounterService tcs)
        {
            db = context;
            timeCounter = tcs;

        }

        public IActionResult Index(int? id)
        {
            var getProjectId = db.Checkpoints.Where(c => c.ProjectId == id);
            var timeIncludeZero = db.Checkpoints.Where(u => EF.Functions. Like(u.MarkedTime, "00:00:00.00"));
            var withOutZero = db.Checkpoints.Where(u => EF.Functions.Like(u.MarkedTime, "%:%:%.%")).Except(timeIncludeZero).Where(c=>c.ProjectId==id);

            if (getProjectId.Any())
            {
                return View(withOutZero);
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddProject() => View();

        public IActionResult Count(string button, int id)
        {
            string getTime = timeCounter.TimeCounter(button);
            var firstProject = db.Projects.Where(c=>c.Id==id).FirstOrDefault();
            var spentTime = db.SpentTimes.Where(c=>c.Id==id).FirstOrDefault();

            firstProject.Checkpoints.Add(new Checkpoint { ProjectId = id, MarkedTime = getTime });

            spentTime.Duration = getTime;
            db.SaveChanges();

            return RedirectToAction( "Index", new {id});
        }
        [HttpPost]
        public IActionResult AddProject(Project project)
        {
            var NewProject = new Project();
            var spentTime = new SpentTime();

            NewProject.Name = project.Name;
            NewProject.Checkpoints.Add(new Checkpoint { ProjectId = NewProject.Id , MarkedTime = "--:--:--.--" });

            spentTime.Duration = "--:--:--.--";
            NewProject.SpentTime = spentTime;

            db.Projects.Add(NewProject);
            db.SaveChanges();

            return RedirectToAction("GetProject");
        }

        public IActionResult GetProject()
        {
            if (db.Projects.Select(c => c.SpentTime.Duration).Any())
            {
                var list = db.Projects.Include(c => c.SpentTime);
                return View(list);
            }
            return RedirectToAction("Error");
        }

        public IActionResult Error() => View();
    }
}
