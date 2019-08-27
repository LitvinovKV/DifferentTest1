using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNet.Identity;
using NHibernate;
using System.Configuration;

namespace Application.Models.Auth
{
    public class DatabaseContext
    {
        private readonly ISessionFactory sessionFactory;

        public DatabaseContext()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionDB"].ConnectionString;
            this.sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DatabaseContext>())
                .BuildSessionFactory();
        }

        public ISession OpeSession()
        {
            return this.sessionFactory.OpenSession();
        }

        public IUserStore<User, int> GenerateUserStore()
        {
            return new IdentityStore(OpeSession());
        }
    }
}