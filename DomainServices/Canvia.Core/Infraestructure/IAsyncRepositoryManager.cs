using System;
using System.Threading.Tasks;

namespace Canvia.Core.Infraestructure
{
    public interface IAsyncRepositoryManager : IDisposable
    {
        Task<int> SaveChanges();
    }
}
