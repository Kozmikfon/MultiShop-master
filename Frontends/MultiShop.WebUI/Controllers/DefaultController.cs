using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
    /// <summary>
    /// Anasayfa artık React uygulamasında (http://localhost:5173). Bu adrese yönlendirir.
    /// </summary>
    public class DefaultController : Controller
    {
        private readonly IConfiguration _configuration;

        public DefaultController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var reactAppUrl = _configuration["ReactAppUrl"] ?? "http://localhost:5173";
            return Redirect(reactAppUrl);
        }
    }
}
