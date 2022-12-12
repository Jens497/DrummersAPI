using MusicBackend.Data;
using MusicBackend.Dtos;
using MusicBackend.Models;
using System.Runtime.CompilerServices;

namespace MusicBackend
{
    public static class DtoMapper
    {
        public static ExcerciseDto ConvertToExcerciseDto(this tblExcercise excercise)
        {
            return new ExcerciseDto
            {
                Id = excercise.Id,
                Name = excercise.Name,
                Filename = excercise.Filename,
            };
        }
        //Mapper for objects that needs to converted from one object to another IE. DTO - data transfer objects.
        public static UserDto CovertToDtoUser(this UserRequest user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
            };
        }

        public static tblUser CovertToDtoUser(this tblUser user)
        {
            return new tblUser
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
            };
        }

        public static UserRequest ConvertUsertblToUserRequest(this tblUser user)
        {
            return new UserRequest
            {
                Id = Convert.ToInt32(user.Id),
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
            };
        }
    }
}
