using NTestDataBuilder;
using TestFixtureDataPresentation.Implementation.Models;

namespace TestFixtureDataPresentation.Tests._03_TestDataBuilder
{
    class DemographicBuilder : TestDataBuilder<Demographic, DemographicBuilder>
    {
        public DemographicBuilder()
        {
            Set(x => x.State, null);
            Set(x => x.MinimumAge, null);
            Set(x => x.MaximumAge, null);
        }

        protected override Demographic BuildObject()
        {
            return new Demographic(Get(x => x.State), Get(x => x.MinimumAge), Get(x => x.MaximumAge));
        }
    }
}
