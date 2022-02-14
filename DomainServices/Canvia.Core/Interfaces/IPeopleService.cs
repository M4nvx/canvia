using Canvia.Model;
using Canvia.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Canvia.Core.Interfaces
{
    public interface IPeopleService
    {
        Task<Person> GetById(int id);

        Task<int> Save(Person value);

        Task<int> Delete(int id);

        IEnumerable<Person> GetAll();

        Task<int> Update(int id, Person person);

        Task<int> DeleteTable(int id);
    }
}
