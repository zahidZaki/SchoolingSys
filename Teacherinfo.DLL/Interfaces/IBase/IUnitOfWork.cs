using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teacherinfo.DLL.Db_Context;

namespace Teacherinfo.DLL.Interfaces.IBase
{
    public interface IUnitOfWork : IDisposable
    {
        public TeachterDbContext TeachterDbContext { get; }
        public Task BeginTransactionAsync(CancellationToken cancellationToken);
        public Task RollbackTransactionAsync(CancellationToken cancellationToken);
        public Task CommitTransactionAsync(CancellationToken cancellationToken);
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
