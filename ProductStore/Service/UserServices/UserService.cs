using System.Security.Claims;

namespace ProductStore.Service.UserServices {
    public class UserService : IUserService {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor HttpContextAccessor) {
            _httpContextAccessor = HttpContextAccessor;
        }

        public string GetUserName() {
            var returnResult = string.Empty;
            if(_httpContextAccessor != null) {
                returnResult = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return returnResult;
        }
    }
}
