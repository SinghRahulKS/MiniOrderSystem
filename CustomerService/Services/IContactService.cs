using UserService.Model.Request;

namespace UserService.Services
{
        public interface IContactService
        {
            Task<bool> SubmitContactAsync(ContactRequest contact);
        }
}
