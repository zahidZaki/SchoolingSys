using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Teacherinfo.DLL.Db_Context;
using Teacherinfo.DLL.Interfaces.IBase;

namespace Teacherinfo.DLL.Concrete.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ILogger<UnitOfWork> _logger;

        public TeachterDbContext TeachterDbContext { get; }

        public UnitOfWork(TeachterDbContext dbContext, ILogger<UnitOfWork> logger)
        {
            TeachterDbContext = dbContext;
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
            await TeachterDbContext.Database.BeginTransactionAsync(cancellationToken);
        }
        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            await TeachterDbContext.Database.RollbackTransactionAsync(cancellationToken);
        }
        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            await TeachterDbContext.Database.CommitTransactionAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await TeachterDbContext.SaveChangesAsync(cancellationToken);
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
                    TeachterDbContext.Dispose();
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

