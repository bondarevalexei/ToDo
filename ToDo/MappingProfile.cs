using AutoMapper;
using Entities.Models;
using Shared.DTO;

namespace ToDo
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>();

            CreateMap<ToDoTask, ToDoTaskDto>();

            CreateMap<AccountForCreationDto, Account>();

            CreateMap<ToDoTaskForCreationDto, ToDoTask>();

            CreateMap<ToDoTaskForUpdateDto, ToDoTask>().ReverseMap();

            CreateMap<AccountForUpdateDto, Account>();

            CreateMap<UserForRegistrationDto, User>();
        }
    }
}
