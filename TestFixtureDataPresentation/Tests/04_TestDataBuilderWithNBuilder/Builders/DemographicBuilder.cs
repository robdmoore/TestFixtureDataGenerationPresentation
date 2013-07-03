using NTestDataBuilder;
using TestFixtureDataPresentation.Implementation.Models;

namespace TestFixtureDataPresentation.Tests._04_TestDataBuilder.BuildersWithNBuilder
{
    class DemographicBuilder : TestDataBuilder<Demographic, DemographicBuilder>
    {
        public DemographicBuilder()
        {
            ForAllMembers();
        }

        public DemographicBuilder ForAllMembers()
        {
            Set(x => x.State, null);
            Set(x => x.MinimumAge, null);
            Set(x => x.MaximumAge, null);
            return this;
        }

        public DemographicBuilder ForState(State state)
        {
            Set(x => x.State, state);
            return this;
        }

        public DemographicBuilder WithMinimumAge(int minimumAge)
        {
            Set(x => x.MinimumAge, minimumAge);
            return this;
        }

        public DemographicBuilder WithMaximumAge(int maximumAge)
        {
            Set(x => x.MaximumAge, maximumAge);
            return this;
        }

        protected override Demographic BuildObject()
        {
            return new Demographic(Get(x => x.State), Get(x => x.MinimumAge), Get(x => x.MaximumAge));
        }
    }
}
