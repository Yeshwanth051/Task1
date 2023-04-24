using System;
using System.Collections.Generic;
using AutoMapper;
using Bogus;
using Bogus.DataSets;
using Newtonsoft.Json;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set up the AutoMapper configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Person, PersonDto>();
                cfg.CreateMap<Address, AddressDto>();
            });

            // Create an IMapper instance
            var mapper = config.CreateMapper();

            // Generate a list of Person objects using Bogus
            var people = new Faker<Person>()
                .RuleFor(p => p.Id, f => f.UniqueIndex)
                .RuleFor(p => p.FirstName, f => f.Name.FirstName())
                .RuleFor(p => p.LastName, f => f.Name.LastName())
                .RuleFor(p => p.Age, f => f.Random.Number(18, 65))
                .RuleFor(p => p.Address, f => new Address
                {
                    Street = f.Address.StreetName(),
                    City = f.Address.City(),
                    State = f.Address.State(),
                    Pin = f.Address.ZipCode()
                })
                .Generate(10);

            // Map the Person objects to PersonDto objects
            var dtos = mapper.Map<List<Person>, List<PersonDto>>(people);

            //// Serialize the list of PersonDto objects to JSON
            //var json = JsonConvert.SerializeObject(dtos, Formatting.Indented);

            //// Output the JSON to the console
            //Console.WriteLine(json);
            string jsonReport = JsonConvert.SerializeObject(dtos, Formatting.Indented);

            // write the JSON to a file
            File.WriteAllText("mapped.json", jsonReport);
        }
    }
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Address Address { get; set; }
    }

    public class PersonDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public AddressDto Address { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pin { get; set; }
    }

    public class AddressDto
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Pin { get; set; }
    }
}

