using System.Linq;

namespace TestFixtureDataPresentation.Implementation
{
    interface IQuery<in TIn, out TOut>
    {
        TOut Query(IQueryable<TIn> source);
    }
}
