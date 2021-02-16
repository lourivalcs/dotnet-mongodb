using MongoDB.Domain.ModelDTO;

namespace MongoDB.Domain.Model
{
    public class User : BaseModelEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }

        public User() { }
        public User(UserDto userDto)
        {
            Id = userDto.Id;
            Name = userDto.Name;
            Age = userDto.Age;
            Gender = userDto.Gender;
            Email = userDto.Email;
            Address = new Address(userDto?.Address);
        }
    }

    public class Address
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public Address() { }

        public Address(AddressDto addressDto)
        {
            if (addressDto == null)
                return;

            StreetAddress = addressDto.StreetAddress;
            City = addressDto.City;
            State = addressDto.State;
        }
    }
}
