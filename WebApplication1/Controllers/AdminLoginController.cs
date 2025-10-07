using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Ecommerce_website.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly JwtServices _jwt;
        private readonly IPasswordHasher<Users> _passwordHasher;

        public UserController(AppDbContext db, JwtServices jwt,IPasswordHasher<Users> passwordHasher)
        {
            _db = db;
            _jwt = jwt;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(SignUpDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.email == dto.email);
            if (existingUser != null)
                return BadRequest("Email is already registered.");

            var user = new Users
            {
                full_name = dto.full_name,
                email = dto.email,
                phone_number = dto.phone_number,
                address = dto.address,
                password = _passwordHasher.HashPassword(null, dto.password)
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return Ok(new { success = true, message = "User registered successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid login data.");

            var user = await _db.Users.FirstOrDefaultAsync(u => u.email == dto.email);
            if (user == null)
                return Unauthorized("Invalid email or password.");

            var result = _passwordHasher.VerifyHashedPassword(user, user.password, dto.password);
            if (result != PasswordVerificationResult.Success)
                return Unauthorized("Invalid email or password.");

            var token = _jwt.GenerateToken(user);
            return Ok(new { token });
        }
    }
}
