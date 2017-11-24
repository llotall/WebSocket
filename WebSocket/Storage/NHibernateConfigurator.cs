using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using NHibernate;
using Storage.Mappings;

namespace Storage
{
    public class NHibernateConfigurator
    {
        public interface ISessionFactory
        {
            /// <summary>
            /// Сессия
            /// </summary>
            ISession Session { get; }
        }

        /// <summary>
        /// The nhibernate configurator.
        /// </summary>
        public class NHibernateConfiguration : ISessionFactory
        {

            /// <summary>
            /// Фабрика сессий
            /// </summary>
            private readonly NHibernate.ISessionFactory _sessionFactory;

            /// <summary>
            /// Конструктор, инициализирует объект <see cref="NHibernateConfiguration"/> класса
            /// </summary>
            public NHibernateConfiguration(IConfiguration appConfig)
            {
                var connectionString = @"Server=localhost;Port=3306;Uid=root;Pwd=rootpass;Database=crmparserdb;";

                var hibernateConfig = Fluently.Configure().Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>())
                    .Database(MySQLConfiguration.Standard.ConnectionString(connectionString).ShowSql()).BuildConfiguration();

                _sessionFactory = hibernateConfig.BuildSessionFactory();
            }

            /// <summary>
            /// Получить сессию
            /// </summary>
            public ISession Session => _sessionFactory.OpenSession();
        }
    }
}
