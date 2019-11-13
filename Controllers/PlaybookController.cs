using Microsoft.AspNetCore.Mvc;

namespace Site.Controllers
{
    public class PlaybooksController : Controller
    {
        
        public IActionResult Playbooks() 
        {
            return View();
        }
        
        
        public IActionResult ProductDescription() {
            return View();
        }

        public IActionResult TechnicalDueDiligence() {
            return View();
        }

        public IActionResult StandardQuestionere() {
            return View();
        }

        public IActionResult BuildingAndRunningTeams() {
            return View();
        }
        public IActionResult CAIQ() {
            return View();
        }

        public  IActionResult OmniChannelMarketing() {
            return View();
        }

        public IActionResult NewBusinessChecklist() {
            return View();
        }
    }
}