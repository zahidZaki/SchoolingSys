using SchoolingSysApiGateway.Interfaces.IBase;

namespace SchoolingSysApiGateway.Concrete.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ILogger<UnitOfWork> _logger;


        public UnitOfWork(ILogger<UnitOfWork> logger)
        {
            //TeleHelpDeskTasksBoardDbContext = dbContext;
            _logger = logger;
        }

        public UnitOfWork()
        {
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        //#region IUnitofWork Members

        //public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        //{
        //    await TeleHelpDeskTasksBoardDbContext.Database.BeginTransactionAsync(cancellationToken);
        //}
        //public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        //{
        //    await TeleHelpDeskTasksBoardDbContext.Database.RollbackTransactionAsync(cancellationToken);
        //}
        //public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        //{
        //    await TeleHelpDeskTasksBoardDbContext.Database.CommitTransactionAsync(cancellationToken);
        //}

        //public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        //{
        //    return await TeleHelpDeskTasksBoardDbContext.SaveChangesAsync(cancellationToken);
        //}

        //#endregion


        #region Disposing logic

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {

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
