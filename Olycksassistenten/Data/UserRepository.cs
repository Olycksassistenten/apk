using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Olycksassistenten.Logic;

namespace Olycksassistenten.Data
{
    public class UserRepository : BaseRepository
    {
        #region Methods

        public User Get(int id)
        {
            User entity;
            using (Db)
            {
                entity = Db.Get<User>(id);
            }
            return entity;
        }

        public List<User> Find(Expression<Func<User, bool>> expression)
        {
            List<User> entities;
            using (Db)
            {
                entities = Db.Table<User>().Where(expression).ToList();
            }
            return entities;
        }

        public List<User> GetAll()
        {
            List<User> entities;
            using (Db)
            {
                entities = Db.Table<User>().ToList();
            }
            return entities;
        }

        public void InsertOrUpdate(User entity)
        {
            using (Db)
            {
                if (entity.Id > 0)
                    Db.Update(entity);
                else
                    Db.Insert(entity);
            }
        }

        public void Delete(User entity)
        {
            using (Db)
            {
                Db.Delete<User>(entity.Id);
            }
        }

        #endregion
    }
}