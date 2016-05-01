using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Olycksassistenten.Logic;

namespace Olycksassistenten.Data
{
    public class InsuranceCompanyRepository : BaseRepository
    {
        #region Methods

        public InsuranceCompany Get(int id)
        {
            InsuranceCompany entity;
            using (Db)
            {
                entity = Db.Get<InsuranceCompany>(id);
            }
            return entity;
        }

        public List<InsuranceCompany> Find(Expression<Func<InsuranceCompany, bool>> expression)
        {
            List<InsuranceCompany> entities;
            using (Db)
            {
                entities = Db.Table<InsuranceCompany>().Where(expression).ToList();
            }
            return entities;
        }

        public List<InsuranceCompany> GetAll()
        {
            List<InsuranceCompany> entities;
            using (Db)
            {
                entities = Db.Table<InsuranceCompany>().ToList();
            }
            return entities;
        }

        public void InsertOrUpdate(InsuranceCompany entity)
        {
            using (Db)
            {
                if (entity.Id > 0)
                    Db.Update(entity);
                else
                    Db.Insert(entity);
            }
        }

        public void Delete(InsuranceCompany entity)
        {
            using (Db)
            {
                Db.Delete<InsuranceCompany>(entity.Id);
            }
        }

        #endregion
    }
}