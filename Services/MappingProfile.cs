using AutoMapper;
using petder.Models.DataModels;
using petder.Models.ViewModels;
using petder.Models.ViewModels.Chats;
using petder.Models.ViewModels.Requests;

namespace petder.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Authentication
            CreateMap<User, RegisterViewModel>().ForMember(x => x.Username, opt => opt.MapFrom(x => x.username))
                                                .ForMember(x => x.Password, opt => opt.MapFrom(x => x.password))
                                                .ForMember(x => x.Address, opt => opt.Ignore())
                                                .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(x => x.phone_number))
                                                .ReverseMap();

            CreateMap<RegisterViewModel, User>().ForMember(x => x.username, opt => opt.MapFrom(x => x.Username))
                                                .ForMember(x => x.password, opt => opt.MapFrom(x => x.Password))
                                                .ForMember(x => x.phone_number, opt => opt.MapFrom(x => x.PhoneNumber))
                                                .ForMember(x => x.Address, opt => opt.Ignore());

            #endregion

            #region Profile
            CreateMap<Pet, PetProfileViewModel>().ForMember(x => x.Breed, opt => opt.MapFrom(x => x.Breed.name))
                                                 .ForMember(x => x.Name, opt => opt.MapFrom(x => x.name))
                                                 .ForMember(x => x.PetId, opt => opt.MapFrom(x => x.pet_id));

            CreateMap<Pet, CreatePetViewModel>().ForMember(x => x.Gender, opt => opt.MapFrom(x => x.gender))
                                                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.description))
                                                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.name))
                                                .ForMember(x => x.BreedId, opt => opt.MapFrom(x => x.breed_id))
                                                .ForMember(x => x.PetImages, opt => opt.Ignore())
                                                .ForMember(x => x.BirthDatetime, opt => opt.Ignore())
                                                .ReverseMap();

            CreateMap<CreatePetViewModel, Pet>().ForMember(x => x.gender, opt => opt.MapFrom(x => x.Gender))
                                                .ForMember(x => x.description, opt => opt.MapFrom(x => x.Description))
                                                .ForMember(x => x.name, opt => opt.MapFrom(x => x.Name))
                                                .ForMember(x => x.breed_id, opt => opt.MapFrom(x => x.BreedId))
                                                .ForMember(x => x.birth_datetime, opt => opt.Ignore())
                                                .ForMember(x => x.PetImages, opt => opt.Ignore())
                                                .ReverseMap();
            
            CreateMap<Pet, EditProfileViewModel>().ForMember(x => x.PetId, opt => opt.MapFrom(x => x.pet_id))
                                                  .ForMember(x => x.BreedId, opt => opt.MapFrom(x => x.breed_id))
                                                  .ForMember(x => x.Name, opt => opt.MapFrom(x => x.name))
                                                  .ForMember(x => x.Gender, opt => opt.MapFrom(x => x.gender))
                                                  .ForMember(x => x.BirthDateTime, opt => opt.MapFrom(x => x.birth_datetime))
                                                  .ForMember(x => x.Description, opt => opt.MapFrom(x => x.description))
                                                  .ForMember(x => x.IsCurrent, opt => opt.MapFrom(x => x.is_current))
                                                  .ForMember(x => x.PetImages, opt => opt.Ignore())
                                                  .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.user_id));
    
            #endregion

            #region Request
            CreateMap<BlockViewModel, BlockList>().ForMember(x => x.blocked_pet_id, opt => opt.MapFrom(x => x.BlockedPetId))
                                                  .ForMember(x => x.pet_id, opt => opt.MapFrom(x => x.BlockerPetId));

            CreateMap<RequestViewModel, RequestList>().ForMember(x => x.requested_pet_id, opt => opt.MapFrom(x => x.RequestedPetId))
                                                      .ForMember(x => x.pet_id, opt => opt.MapFrom(x => x.RequesterPetId));

            CreateMap<AcceptRequestViewModel, Session>().ForMember(x => x.request_id, opt => opt.MapFrom(x => x.RequestListId));

            #endregion

            #region Chat
            CreateMap<MessageViewModel, Message>().ForMember(x => x.sender_pet_id, opt => opt.MapFrom(x => x.SenderId))
                                                  .ForMember(x => x.message, opt => opt.MapFrom(x => x.Message))
                                                  .ForMember(x => x.is_system_message, opt => opt.MapFrom(x => x.IsSystemMessage))
                                                  .ForMember(x => x.session_id, opt => opt.MapFrom(x => x.SessionId));
                                                                                        
            #endregion
        }
    }
}