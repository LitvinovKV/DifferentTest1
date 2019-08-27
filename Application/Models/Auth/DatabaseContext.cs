using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNet.Identity;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System.Configuration;

namespace Application.Models.Auth
{
    public class DatabaseContext
    {
        private readonly ISessionFactory sessionFactory;

        public DatabaseContext()
        {
            // Получить строку подклчюения к БД из файла web.config
            string connectionString = ConfigurationManager.ConnectionStrings["connectionDB"].ConnectionString;
            this.sessionFactory = Fluently.Configure()
                // Настройка подключения к БД
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connectionString))
                // Настройка маппинга. Из-за AddFromAssemblyOf NHibernate будет пытаться 
                // маппить каждый класс в этой сброке (assembly)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DatabaseContext>())
                // ShemaUpdate позволяет создавать/обновлять таблицы и поля в БД
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
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