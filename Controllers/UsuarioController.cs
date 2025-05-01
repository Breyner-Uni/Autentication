using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SampleMvcApp.Service;
using SampleMvcApp.ViewModels;
using System;
using System.Threading.Tasks;

namespace SampleMvcApp.Controllers
{
    public class UsuarioController : Controller
    {

        private IProfileService profile;

        public UsuarioController(IProfileService profi)
        {
            profile = profi;
        }



        [HttpGet]
        public IActionResult Index()
        {
            var perfil = new UserProfile(); // o cargar datos reales si quieres
            return View(perfil);
        }

        [HttpPost]
        public async Task<IActionResult> updateprofile(UserProfile user)
        {
            var userid= "auth0|6813743ec7dacf733071e656";
            string url = $"https://dev-kw0pbr6x4dqrvm5g.us.auth0.com/api/v2/users/{userid}";

            var response = await profile.UpdateProfile(url, user);
            if (!response.Error)
            {
                ViewBag.Success = true;
                throw new Exception("Se ha actualizado");
            }
            else
            
                ViewBag.Error = await response.GetErrorMessage();
                var message=await response.GetErrorMessage();
                throw new Exception($"Error al actualizar {message}");
            

            

        }



    }
}
