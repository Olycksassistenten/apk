using System;
using System.IO;
using System.Linq;
using Olycksassistenten.Logic;
using SQLite;

namespace Olycksassistenten.Data
{
    public abstract class BaseRepository
    {
        #region Constructors

        protected BaseRepository()
        {
            InitDatabase();
        }

        #endregion

        #region Properties

        public SQLiteConnection Db
        {
            get
            {
                return new SQLiteConnection(Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal), "olycksassistenten.db3"));
            }
        }

        #endregion

        #region Methods

        private void InitDatabase()
        {
            using (Db)
            {
                //skapa upp tabellerna, om de inte redan existerar
                Db.CreateTable<InsuranceCompany>();
                Db.CreateTable<User>();

                //skapa upp testdata
                if (!Db.Table<User>().Any())
                {
                    Db.Insert(new User
                    {
                        Username = "cl", 
                        Password = "cl", 
                        FullName = "Caroline Larsson", 
                        Address = "Björklövsvägen 6A", 
                        Ssn = 8304055646, 
                        Email = "caroline.larsson@mail.com", 
                        Phone = "07308003128"
                    });
                }

                if (!Db.Table<InsuranceCompany>().Any())
                {
                    Db.Insert(new InsuranceCompany
                    {
                        Id = 1, 
                        CompanyName = "Volvia", 
                        Email = "caroline.larsson@mail.com"
                    });

                    Db.Insert(new InsuranceCompany 
                    {
                        Id = 2, 
                        CompanyName = "Länsförsäkringar", 
                        Email = "caroline.larsson@mail.com" 
                    });

                    Db.Insert(new InsuranceCompany
                    {
                        Id = 3, 
                        CompanyName = "If", 
                        Email = "caroline.larsson@mail.com"
                    });

                    Db.Insert(new InsuranceCompany
                    {
                        Id = 4, 
                        CompanyName = "Dina försäkringar", 
                        Email = "caroline.larsson@mail.com"
                    });
                }
            }
        }

        #endregion
    }
}