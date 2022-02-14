using Canvia.Core.Interfaces;
using Canvia.Core.Respositories;
using Canvia.Model;
using Canvia.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvia.Core
{
    public class PeopleService : IPeopleService
    {
        public async Task<Person> GetById(int id)
        {
            using (var context = new CanviaRepositoryManager(new CanviaConnect()))
            {
                return await context.PersonRepository.GetById(id);
            }
        }

        public async Task<int> Save(Person value)
        {
            using (var context = new CanviaRepositoryManager(new CanviaConnect()))
            {
                return await context.PersonRepository.Add(value);
            }
        }

        public async Task<int> Delete(int id)
        {
            using (var context = new CanviaRepositoryManager(new CanviaConnect()))
            {
                var entity = await this.GetById(id);
                if (entity != null)
                {
                    entity.IsAvailable = false;
                }
                return entity != null ? await context.PersonRepository.Update(entity) : 0;
            }
        }

        public IEnumerable<Person> GetAll()
        {
            using (var context = new CanviaConnect())
            {
                return context.GetAll().ToList();
            }
        }

        public async Task<int> Update(int id, Person person)
        {
            using (var context = new CanviaRepositoryManager(new CanviaConnect()))
            {
                var entity = await this.GetById(id);
                if (entity != null)
                {
                    var query = "UpdatePerson";

                    var parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@lastName", person.Lastname),
                        new SqlParameter("@id", id)
                    };

                    var result = await context.PersonRepository.ExecuteQuery<object>(query, parameters);
                }


                return entity != null ? 0 : 0;
            }
        }

        public async Task<int> DeleteTable(int id)
        {
            using (var context = new CanviaRepositoryManager(new CanviaConnect()))
            {
                var entity = await this.GetById(id);
                return entity != null ? await context.PersonRepository.DeleteAndReturn(entity) : 0;
            }
        }
    }
}
