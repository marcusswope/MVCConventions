using System;
using System.Web.Mvc;
using MVCConventions.Attributes;

namespace MVCConventions.Example.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new ExampleViewModel());
        }
    }

    public class ExampleViewModel
    {
        [Required]
        public string EmailAddress { get; set; }
        [Required, MinLength(8)]
        public string Password { get; set; }
        public string SocialSecurityNumber { get; set; }
        public bool SignUpForNewsletter { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}