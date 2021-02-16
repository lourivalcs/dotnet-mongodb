using MongoDB.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace MongoDB.Domain.ModelDTO
{
    public class UserDto : BaseDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Email { get; set; }

        public AddressDto Address { get; set; }

        public UserDto() { }
        public UserDto(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Age = user.Age;
            Gender = user.Gender;
            Email = user.Email;
            Address = new AddressDto(user?.Address);
        }
    }

    public class AddressDto
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public AddressDto() { }
        public AddressDto(Address addressEntity)
        {
            if (addressEntity == null)
                return;

            StreetAddress = addressEntity.StreetAddress;
            City = addressEntity.City;
            State = addressEntity.State;
        }
    }
}
