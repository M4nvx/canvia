using Canvia.Core.Infraestructure;
using Canvia.Core.Infraestructure.Respositories;
using Canvia.Core.Infraestructure.Respositories.Interfaces;
using Canvia.Model;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Canvia.Core.Respositories
{
    public class CanviaRepositoryManager : IAsyncRepositoryManager
    {
        #region Fields

        private readonly CanviaConnect Context;

        #endregion

        #region Repositories

        public IAsyncRepository<Person> PersonRepository { get; private set; }

        public IAsyncRepository<Relationships> RelationshipsRepository { get; private set; }

        #endregion

        #region Constructor

        public CanviaRepositoryManager(DbContext context)
        {
            this.Context = context as CanviaConnect;

            this.PersonRepository = new AsyncRespository<Person>(this.Context);

            this.RelationshipsRepository = new AsyncRespository<Relationships>(this.Context);
        }

        #endregion

        public void Dispose()
        {
            this.Context.Dispose();
        }

        public async Task<int> SaveChanges()
        {
            return await this.Context.SaveChangesAsync();
        }
    }
}
