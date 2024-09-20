namespace StudentRecordPortal.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetCurrentCollegeId()
        {
            var user = _httpContextAccessor.HttpContext.User;
            var collegeIdClaim = user.FindFirst("Id");

            if (collegeIdClaim != null && int.TryParse(collegeIdClaim.Value, out int collegeId))
            {
                return collegeId;
            }

            throw new UnauthorizedAccessException("College ID not found in claims.");
        }
    }
}
