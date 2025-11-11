using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ClassInfo.DLL.Interfaces.IBase;
using Microsoft.Extensions.Logging;

namespace ClassInfo.DLL.Concrete.Base
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GenericRepository<T>> _logger;
        public GenericRepository(IUnitOfWork unitOfWork, ILogger<GenericRepository<T>> logger)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
        }
        public async Task Add(T entity)
        {
            try
            {
                this._logger.LogInformation("Adding:" + entity);
                var savedEntity = this._unitOfWork.ClassesDbContext.Set<T>().Add(entity);
                await Task.FromResult(savedEntity.Entity);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message, ex.InnerException, ex.InnerException != null ? ex.InnerException.Message : string.Empty);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Method for deleting.
        /// </summary>
        /// <param name="entity">is an entity model.</param>
        /// <returns>Returns result.</returns>
        public async Task Delete(T entity)
        {
            try
            {
                this._logger.LogInformation("Deleting:" + entity);
                var savedEntity = this._unitOfWork.ClassesDbContext.Set<T>().Remove(entity);
                await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message, ex.InnerException, ex.InnerException != null ? ex.InnerException.Message : string.Empty);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Method for getting.
        /// </summary>
        /// <returns>Returns result.</returns>
        public async Task<IEnumerable<T>> Get()
        {
            try
            {
                this._logger.LogInformation("Getting list.");
                var entityList = this._unitOfWork.ClassesDbContext.Set<T>().ToList();
                return await Task.FromResult(entityList);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message, ex.InnerException, ex.InnerException != null ? ex.InnerException.Message : string.Empty);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Method for getting single item.
        /// </summary>
        /// <param name="predicate">param check weather.</param>
        /// <returns>Returns result.</returns>
        public async Task<T> GetSingle(Expression<Func<T, bool>> predicate)
        {
            try
            {
                this._logger.LogInformation("Getting single:" + predicate);
                var entity = this._unitOfWork.ClassesDbContext.Set<T>().FirstOrDefault(predicate);
                return await Task.FromResult(entity);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message, ex.InnerException, ex.InnerException != null ? ex.InnerException.Message : string.Empty);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Method for getting.
        /// </summary>
        /// <param name="predicate">param check weather.</param>
        /// <returns>Returns result.</returns>
        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate)
        {
            try
            {
                this._logger.LogInformation("Getting:" + predicate);
                var entityList = this._unitOfWork.ClassesDbContext.Set<T>().Where(predicate).ToList();
                return await Task.FromResult(entityList);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message, ex.InnerException, ex.InnerException != null ? ex.InnerException.Message : string.Empty);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Method for updating.
        /// </summary>
        /// <param name="entity">is an entity model.</param>
        /// <returns>Returns result.</returns>
        public async Task Update(T entity)
        {
            try
            {
                this._logger.LogInformation("Updating:" + entity);
                var savedEntity = this._unitOfWork.ClassesDbContext.Set<T>().Update(entity);
                await Task.FromResult(savedEntity.Entity);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message, ex.InnerException, ex.InnerException != null ? ex.InnerException.Message : string.Empty);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Method for adding list.
        /// </summary>
        /// <param name="entities">is a list of entity model.</param>
        /// <returns>Returns result.</returns>
        public async Task Add(IEnumerable<T> entities)
        {
            try
            {
                this._logger.LogInformation("Adding list." + entities);
                foreach (var entity in entities)
                {
                    this._unitOfWork.ClassesDbContext.Set<T>().Add(entity);
                }

                await Task.FromResult(entities);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message, ex.InnerException, ex.InnerException != null ? ex.InnerException.Message : string.Empty);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Method for deleting list.
        /// </summary>
        /// <param name="entities">is a list of entity model.</param>
        /// <returns>Returns result.</returns>
        public async Task Delete(IEnumerable<T> entities)
        {
            try
            {
                this._logger.LogInformation("Deleting list." + entities);
                foreach (var entity in entities)
                {
                    this._unitOfWork.ClassesDbContext.Set<T>().Remove(entity);
                }

                await Task.FromResult(entities);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message, ex.InnerException, ex.InnerException != null ? ex.InnerException.Message : string.Empty);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// Method for updating list.
        /// </summary>
        /// <param name="entities">is an entity model.</param>
        /// <returns>Returns result.</returns>
        public async Task Update(IEnumerable<T> entities)
        {
            try
            {
                this._logger.LogInformation("Updating list." + entities);
                foreach (var entity in entities)
                {
                    this._unitOfWork.ClassesDbContext.Set<T>().Update(entity);
                }

                await Task.FromResult(entities);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message, ex.InnerException, ex.InnerException != null ? ex.InnerException.Message : string.Empty);
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
