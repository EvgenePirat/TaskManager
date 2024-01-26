using AutoMapper;
using BusinessLayer.Models.Contact.Request;
using TaskManager.Dto.Contact.Request;

namespace TaskManager.Mappers
{
    /// <summary>
    /// Mapper for presentation and business layer for contact model
    /// </summary>
    public class ContactDtoProfile : Profile
    {
        public ContactDtoProfile()
        {
            CreateMap<ContactFormDto, ContactFormModel>();
        }
    }
}
