using System.Collections;
using System.Linq;

namespace TestFixtureDataPresentation.Implementation
{
    interface ISession
    {
        IQueryable<T> Query<T>();
        void Save(object o);
    }

    class InMemorySession : ISession
    {
        readonly ArrayList _database = new ArrayList();

        public IQueryable<T> Query<T>()
        {
            return _database.OfType<T>().AsQueryable();
        }

        public void Save(object o)
        {
            _database.Add(o);
        }
    }
}
