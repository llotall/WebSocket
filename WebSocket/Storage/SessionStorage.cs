﻿using NHibernate;
using Storage.Interfaces;

namespace Storage
{
    public class SessionStorage : ISessionStorage
    {
        private readonly NHibernateConfigurator.ISessionFactory _sessionFactory;

        private ISession _session;

        public SessionStorage(NHibernateConfigurator.ISessionFactory sf)
        {
            _sessionFactory = sf;
        }

        public ISession Session
        {
            get
            {
                if (_session != null) return _session;

                lock (this)
                {
                    _session = _session ?? _sessionFactory.Session;
                }

                return _session;
            }
        }
    }
}
