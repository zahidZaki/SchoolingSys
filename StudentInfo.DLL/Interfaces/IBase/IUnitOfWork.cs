using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInfo.DLL.Db_Context;

namespace StudentInfo.DLL.Interfaces.IBase
{
    public interface IUnitOfWork : IDisposable
    {
        public StudentDbContext StudentDbContext { get; }
        public Task BeginTransactionAsync(CancellationToken cancellationToken);
        public Task RollbackTransactionAsync(CancellationToken cancellationToken);
        public Task CommitTransactionAsync(CancellationToken cancellationToken);
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
