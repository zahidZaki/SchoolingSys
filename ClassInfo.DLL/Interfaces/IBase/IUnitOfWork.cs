using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassInfo.DLL.Db_Context;

namespace ClassInfo.DLL.Interfaces.IBase
{
    public interface IUnitOfWork : IDisposable
    {
        public ClassesDbContext ClassesDbContext { get; }
        public Task BeginTransactionAsync(CancellationToken cancellationToken);
        public Task RollbackTransactionAsync(CancellationToken cancellationToken);
        public Task CommitTransactionAsync(CancellationToken cancellationToken);
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
