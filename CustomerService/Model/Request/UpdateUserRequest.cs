using System.ComponentModel.DataAnnotations;

namespace UserService.Model.Request
{
    public class UpdateUserRequest
    {
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNo { get; set; }
    }
}
