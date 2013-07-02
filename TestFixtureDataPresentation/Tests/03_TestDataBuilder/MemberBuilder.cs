using System;
using NTestDataBuilder;
using TestFixtureDataPresentation.Implementation.Models;

namespace TestFixtureDataPresentation.Tests._03_TestDataBuilder
{
    class MemberBuilder : TestDataBuilder<Member, MemberBuilder>
    {
        public MemberBuilder()
        {
            Set(x => x.Name, "Fred");
            Set(x => x.State, State.Wa);
            Set(x => x.Dob, new DateTime(1970, 1, 1));
        }

        protected override Member BuildObject()
        {
            return new Member(Get(x => x.Name), Get(x => x.State), Get(x => x.Dob));
        }
    }
}
