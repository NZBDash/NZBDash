//using System;
//using System;
//using System.Data.Entity;
//using System.Collections.Generic;
//using System.Data.Entity.Validation;
//using System.Data.Entity.Infrastructure;

//namespace NZBDash.DataAccess
//{
//    public class EfRepository<T> where T : Entity
//    {
//        //TODO finish this
////		http://codereview.stackexchange.com/questions/33109/repository-service-design-pattern
////			private readonly IDbContext _context;
////			private IDbSet<T> _entities;
////
////			public EfRepository(IDbContext context)
////			{
////				_context = context;
////			}
////
////			private IDbSet<T> Entities
////			{
////				get { return _entities ?? (_entities = _context.Set<T>()); }
////			}
////		public TEntity GetById(object id)
////		{
////			return Entities.Find(id);
////		}
////
////		public void Insert(T entity)
////		{
////			Entities.Add(entity);
////			_context.SaveChanges();
////		}
////
////		public void Update(TEntity entity)
////		{
////			_context.SaveChanges();
////		}
////
////		public void Delete(int id)
////		{
////			// Create a new instance of an entity (BaseEntity) and add the id.
////			var entity = new T
////			{
////				ID = id
////			};
////
////			// Attach the entity to the context and call the delete method.
////			Entities.Attach(entity);
////			Delete(entity);
////		}
////
////		public void Delete(TEntity entity)
////		{
////			Entities.Remove(entity);
////			_context.SaveChanges();
////		}
////
////		public IList<T> Table
////		{
////			get { return Entities.ToList(); }
////		}

//        }
//}

