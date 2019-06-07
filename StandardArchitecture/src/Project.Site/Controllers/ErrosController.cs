using Architecture.Domain.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Project.Site.Controllers
{
    public class ErrosController : Controller
    {
        private readonly IUser _user;

        public ErrosController(IUser user)
        {
            user = _user;
        }

        [Route("/erro-de-aplicacao")]
        [Route("/erro-de-aplicacao/{id}")]
        public IActionResult Erros(string id)
        {
            switch (id)
            {
                case "404":
                    return View("NotFound");

                case "403":
                case "401":
                    if (!_user.IsAuthenticated()) return RedirectToAction("Login", "Account");
                    return View("AccessDenied");
            }
            return View("Error");
        }
    }

}
