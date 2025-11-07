using AutoMapper;
using UserService.Model.Request;
using UserService.Repository;
using UserService.Repository.Entity;

namespace UserService.Services
{
    public class ContactService : IContactService
    {
        private readonly IMapper _mapper;
        private readonly IContactRepository _contactRepository;
        
        public ContactService(IMapper mapper, IContactRepository contactRepository)
        {
            _mapper = mapper;
            _contactRepository = contactRepository;
        }
        public async Task<bool> SubmitContactAsync(ContactRequest req)
        {
            var contact = _mapper.Map<ContactMessage>(req);
            if (string.IsNullOrWhiteSpace(contact.Name) ||
                string.IsNullOrWhiteSpace(contact.Email) ||
                string.IsNullOrWhiteSpace(contact.Message))
            {
                return false;
            }

            contact.CreatedOn = DateTime.UtcNow;
            contact.CreatedBy = "system";
            await _contactRepository.AddAsync(contact);
            return true;
        }
    }
}
