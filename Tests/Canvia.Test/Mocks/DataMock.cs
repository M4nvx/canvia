using Canvia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvia.Test.Mocks
{
    public static class DataMock
    {
        public static List<Person> GetAll()
        {
            return new List<Person>()
            {
                new Person(){Id = 1, Name = "Julian", Lastname = "Alcantara", Birthdate = new DateTime(1980,01,01), IsAvailable = true}
            };
        }
    }
}
