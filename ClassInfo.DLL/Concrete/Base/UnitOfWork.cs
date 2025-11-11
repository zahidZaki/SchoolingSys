using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassInfo.DLL.Db_Context;
using ClassInfo.DLL.Interfaces.IBase;
using Microsoft.Extensions.Logging;

namespace ClassInfo.DLL.Concrete.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ILogger<UnitOfWork> _logger;

        public ClassesDbContext ClassesDbContext { get; }

        public UnitOfWork(ClassesDbContext dbContext, ILogger<UnitOfWork> logger)
        {
            ClassesDbContext = dbContext;
            this._logger = logger;
        }

        public UnitOfWork()
        {
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        #region IUnitofWork Members

        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            await ClassesDbContext.Database.BeginTransactionAsync(cancellationToken);
        }
        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            await ClassesDbContext.Database.RollbackTransactionAsync(cancellationToken);
        }
        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            await ClassesDbContext.Database.CommitTransactionAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await ClassesDbContext.SaveChangesAsync(cancellationToken);
        }

        #endregion


        #region Disposing logic

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    ClassesDbContext.Dispose();
                }
            }

            this.disposed = true;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
