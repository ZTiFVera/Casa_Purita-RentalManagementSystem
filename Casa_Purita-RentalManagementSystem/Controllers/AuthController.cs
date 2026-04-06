using Microsoft.AspNetCore.Mvc;
using Casa_Purita_RentalManagementSystem.Data;
using Casa_Purita_RentalManagementSystem.Models;

namespace Casa_Purita_RentalManagementSystem.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Auth/Register
        public IActionResult Register() => View();

        // POST: /Auth/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(string fullName, string email, string password)
        {
            if (_context.Users.Any(u => u.Email == email))
            {
                ViewBag.Error = "Email already exists.";
                return View();
            }

            var user = new User
            {
                FullName = fullName,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["Success"] = "Registration successful. Please log in.";
            return RedirectToAction("Login");
        }

        // GET: /Auth/Login
        public IActionResult Login() => View();

        // POST: /Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                ViewBag.Error = "Invalid email or password.";
                return View();
            }

            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserName", user.FullName);
            HttpContext.Session.SetString("UserRole", user.Role);

            return RedirectToAction("Index", "Dashboard");
        }

        // GET: /Auth/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}