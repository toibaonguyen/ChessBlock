using Chess.Data.Common.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Chess.Data.Common.Repositories
{
    public interface IBlockchainRepository<TEntity> : IDisposable
        where TEntity : class, IBlockEntity
    {
       Task AddAsync(TEntity entity);
       Task UpdateAsync(TEntity entity);
    }
}
