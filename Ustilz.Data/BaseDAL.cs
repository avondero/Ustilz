namespace Ustilz.Data
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query;

    using Ustilz.Data.Interfaces;

    #endregion

    /// <summary>The totem dao.</summary>
    /// <typeparam name="TContext">Type du contexte</typeparam>
    /// <typeparam name="TModel">Type du model</typeparam>
    /// <typeparam name="TIdentity">Type de la clé primaire du model</typeparam>
    [PublicAPI]
    internal abstract class BaseDAL<TContext, TModel, TIdentity>
        : IBaseDAL<TModel, TIdentity> where TModel : class, IDto<TIdentity>
                                      where TIdentity : IComparable<TIdentity>
                                      where TContext : DbContext
    {
        #region Constructeurs et destructeurs

        /// <summary>Initializes a new instance of the <see cref="BaseDAL{TContext,TModel,TIdentity}" /> class.</summary>
        /// <param name="context">The context.</param>
        protected BaseDAL(TContext context) => this.Context = context;

        #endregion

        #region Propriétés et indexeurs

        /// <summary>Gets or sets the context.</summary>
        protected TContext Context { get; set; }

        #endregion

        #region Méthodes publiques

        /// <summary>The create.</summary>
        /// <param name="model">The model.</param>
        /// <returns>The <see cref="Task" />.</returns>
        public async Task<int> Add(TModel model)
        {
            await this.Context.Set<TModel>().AddAsync(model);
            return await this.Context.SaveChangesAsync();
        }

        /// <summary>
        ///     Méthode d'ajout d'une liste d'éléments.
        /// </summary>
        /// <param name="models">La liste d'éléments à ajouter.</param>
        /// <returns>Retourne le nombre de ligne impactées.</returns>
        public async Task<int> AddRange(IEnumerable<TModel> models)
        {
            await this.Context.Set<TModel>().AddRangeAsync(models);
            return await this.Context.SaveChangesAsync();
        }

        /// <summary>The exists.</summary>
        /// <param name="id">The id.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public bool Exists(TIdentity id) => this.Context.Set<TModel>().Any(e => e.Id.CompareTo(id) == 0);

        /// <summary>The get all.</summary>
        /// <param name="includes">The includes.</param>
        /// <returns>The <see cref="Task" />.</returns>
        public async Task<List<TModel>> GetAll(params Expression<Func<TModel, object>>[] includes)
        {
            var set = this.Context.Set<TModel>();
            var includeSet = includes.Aggregate<Expression<Func<TModel, object>>, IIncludableQueryable<TModel, object>>(null,
                                                                                                                        (current, include)
                                                                                                                            => current == null
                                                                                                                                   ? set.Include(include)
                                                                                                                                   : current.Include(include));
            return includeSet == null ? await set.ToListAsync() : await includeSet.ToListAsync();
        }

        /// <summary>The get all.</summary>
        /// <returns>The <see cref="Task" />.</returns>
        public async Task<List<TModel>> GetAll() => await this.Context.Set<TModel>().ToListAsync();

        /// <summary>The get details.</summary>
        /// <param name="id">The id.</param>
        /// <param name="includes">The includes.</param>
        /// <returns>The <see cref="Task" />.</returns>
        public async Task<TModel> GetDetails(TIdentity id, params Expression<Func<TModel, object>>[] includes)
        {
            var set = this.Context.Set<TModel>();
            var includeSet = includes.Aggregate<Expression<Func<TModel, object>>, IIncludableQueryable<TModel, object>>(null,
                                                                                                                        (current, include)
                                                                                                                            => current == null
                                                                                                                                   ? set.Include(include)
                                                                                                                                   : current.Include(include));
            return includeSet == null
                       ? await set.SingleOrDefaultAsync(model => model.Id.CompareTo(id) == 0)
                       : await includeSet.SingleOrDefaultAsync(model => model.Id.CompareTo(id) == 0);
        }

        /// <summary>The get details.</summary>
        /// <param name="id">The id.</param>
        /// <returns>The <see cref="Task" />.</returns>
        public async Task<TModel> GetDetails(TIdentity id) => await this.Context.Set<TModel>().SingleOrDefaultAsync(model => model.Id.CompareTo(id) == 0);

        /// <summary>The remove.</summary>
        /// <param name="model">The model.</param>
        /// <returns>The <see cref="Task" />.</returns>
        public async Task<int> Remove(TModel model)
        {
            this.Context.Set<TModel>().Remove(model);
            return await this.Context.SaveChangesAsync();
        }

        /// <summary>
        ///     Méthode suppression des données de la table.
        /// </summary>
        /// <returns>Retourne le nombre de ligné impactées.</returns>
        public async Task<int> RemoveAll()
        {
            this.Context.Set<TModel>().RemoveRange(await this.GetAll());
            return await this.Context.SaveChangesAsync();
        }

        /// <summary>The update.</summary>
        /// <param name="model">The model.</param>
        /// <returns>The <see cref="Task" />.</returns>
        public async Task<int> Update(TModel model)
        {
            this.Context.Set<TModel>().Update(model);
            return await this.Context.SaveChangesAsync();
        }

        #endregion
    }
}
