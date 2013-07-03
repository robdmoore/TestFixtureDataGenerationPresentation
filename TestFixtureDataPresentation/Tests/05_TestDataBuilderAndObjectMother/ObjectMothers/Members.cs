using TestFixtureDataPresentation.Tests._05_TestDataBuilderAndObjectMother.Builders;

namespace TestFixtureDataPresentation.Tests._05_TestDataBuilderAndObjectMother.ObjectMothers
{
    static partial class ObjectMother
    {
        public static class Members
        {
            public static MemberBuilder Fred
            {
                get { return new MemberBuilder(); }
            }
        }
    }
}
