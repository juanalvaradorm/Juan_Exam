using System;
using System.Collections.Generic;
using JMAR.SYSTEM.DOMAIN.Repositories;
using System.Threading;
using System.Threading.Tasks;
using JMAR.SYSTEM.DOMAIN.Utils;
using JMAR.SYSTEM.DOMAIN.Filters;
using System.Reflection;
using System.Linq;
using JMAR.SYSTEM.DOMAIN.Extensions;
using JMAR.SYSTEM.DOMAIN.Converters;

namespace JMAR.SYSTEM.DOMAIN.Manager
{
    public abstract class BaseManager<T, U> : IBaseManager<T, U> where T : class where U : class
    {

        public readonly IBaseRepository<T> _repository;

        public BaseManager(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<T> AddAsync(U viewModel, CancellationToken ct = default(CancellationToken))
        {
            var entity = this.PrepareAddData(viewModel);
            entity = await _repository.AddAsync(entity, ct);

            return entity;
        }

        public virtual async Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken))
        {

            return await _repository.DeleteAsync(id, ct);
        }

        public virtual async Task<Tuple<List<U>, PagedResult<T>>> GetAllAsync(QueryParameter pagingParameter, IFilter filter, CancellationToken ct = default(CancellationToken))
        {
            await Task.Delay(1);
            var source = _repository.GetAllAsync(ct).GetPaged(pagingParameter, filter);

            var resources = this.GetConverter().ConvertList(source.Results);

            source.Results = null;

            return new Tuple<List<U>, PagedResult<T>>(resources, source);

        }

        public virtual async Task<U> GetByIdAsync(int? id, CancellationToken ct = default(CancellationToken))
        {
            var entity = await _repository.GetByIdAsync(id, ct);
            if (entity == null) { return null; }

            var entityVM = this.GetConverter().Convert(entity);

            return entityVM;
        }

        public virtual async Task<bool> UpdateAsync(U viewModel, int? Id, CancellationToken ct = default(CancellationToken))
        {
            var entity = await _repository.GetByIdAsync(Id, ct);
            entity = PrepareUpdateData(entity, viewModel);
            return await _repository.UpdateAsync(entity, ct);
        }

        public async Task<bool> UpdatePatchAsync(U viewModel, int id, CancellationToken ct = default(CancellationToken))
        {
            var entity = await _repository.GetByIdAsync(id, ct);

            var properties = entity.GetType().GetProperties();

            foreach (var property in properties)
            {
                var key = property.Name;
                PropertyInfo propertyInfo = viewModel.GetType().GetProperty(key);
                if (propertyInfo == null) continue;
                var value = property.GetValue(entity);
                if (value != null)
                {
                    var propertyInterfaces = value.GetType().GetInterfaces();
                    if (propertyInterfaces.Any(x => x.Name.Contains("IEntity"))) continue;
                    if (property.Name == "Budget") continue;
                    if (property.Name == "ElementSuccessPictures") continue;
                    if (property.Name == "WidgetsLayoutAsign") continue;
                }

                if (property.Name == "ElementSuccessPictures") continue;

                var update = propertyInfo.GetValue(viewModel);

                if (update != null)
                {
                    property.SetValue(entity, update);
                }
            }

            entity = await PreparePatchData(entity, viewModel);


            return await _repository.UpdateAsync(entity, ct);
        }

        protected abstract T PrepareAddData(U viewModel);


        protected abstract T PrepareUpdateData(T entity, U viewModel);

        protected abstract IConverter<T, U> GetConverter();

        protected virtual async Task<T> PreparePatchData(T entity, U viewModel)
        {
            await Task.Delay(1);
            return entity;
        }

        public List<string> getRelatedEntities()
        {
            var entitiesName = new List<string>();
            var entities = _repository.getRelatedEntities();
            foreach (var entity in entities)
            {
                var name = entity.Name.Split('.').Last();

                entitiesName.Add(name);
            }

            return entitiesName;
        }

        public List<string> getRelatedFields(string entity)
        {
            var entities = _repository.getRelatedEntities();
            var ent = entities.Where(e => e.ToString().Split(':')[1].Trim().Equals(entity)).FirstOrDefault();
            if (ent == null) { return new List<string>(); }
            return ent.GetProperties().Select(e => e.Name).ToList();
        }

    }
}
