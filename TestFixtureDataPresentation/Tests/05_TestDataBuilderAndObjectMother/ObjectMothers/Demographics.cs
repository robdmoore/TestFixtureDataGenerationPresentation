using TestFixtureDataPresentation.Tests._05_TestDataBuilderAndObjectMother.Builders;

namespace TestFixtureDataPresentation.Tests._05_TestDataBuilderAndObjectMother.ObjectMothers
{
    static partial class ObjectMother
    {
        public static class Demographics
        {
            public static DemographicBuilder Blank
            {
                get { return new DemographicBuilder(); }
            }

            public static DemographicBuilder AllMembers
            {
                get { return new DemographicBuilder().ForAllMembers(); }
            }
        }
    }
}
