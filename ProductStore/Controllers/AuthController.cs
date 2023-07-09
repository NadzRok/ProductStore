

using ProductStore.Models;

namespace ProductStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly ProductDbContext _productDbContext;
        private IConfiguration _config;
        private IUserService _userService;

        public AuthController(IConfiguration config, IUserService userService, ProductDbContext productDbContext) {
            _config = config;
            _userService = userService;
            _productDbContext = productDbContext;
        }

        [HttpGet, Authorize]
        public ActionResult<string> GetUserName() {
            var userName = _userService.GetUserName();
            return Ok(userName);
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(UserDto RegisterDetails) {
            if (RegisterDetails == null) {
                return BadRequest("No data.");
            }
            CreatePasswordHash(RegisterDetails.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User() {
                //Id = Guid.NewGuid(),
                Username = RegisterDetails.Username,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash
            };
            await _productDbContext.AddAsync(user);
            await _productDbContext.SaveChangesAsync();
            return Ok("Successful");
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto Login) {
            if(Login == null) {
                return BadRequest("No data.");
            }
            var user = await _productDbContext.Users.FirstOrDefaultAsync(u => u.Username == Login.Username);
            if (user == null) {
                return BadRequest("Username/Password incorrect.");
            }
            if(!VerifyPasswordHash(Login.Password, user.PasswordHash, user.PasswordSalt)) {
                return BadRequest("Username/Password incorrect.");
            }
            string token = CreateToken(user);
            return Ok(token);
        }

        private void CreatePasswordHash(string Password, out byte[] passwordHash, out byte[] passwordSalt) {
            using(var hmac = new HMACSHA512()) {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));
            }
        }

        private bool VerifyPasswordHash(string Password, byte[] PasswordHash, byte[] PasswordSalt) {
            using(var hmac = new HMACSHA512(PasswordSalt)) {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));
                return computeHash.SequenceEqual(PasswordHash);
            }
        }

        private string CreateToken(User user) {
            List<Claim> claims = new List<Claim>(){
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:TokenKey").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
