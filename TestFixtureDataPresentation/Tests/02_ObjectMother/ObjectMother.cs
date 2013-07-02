using System;
using TestFixtureDataPresentation.Implementation.Models;

namespace TestFixtureDataPresentation.Tests._02_ObjectMother
{
    static class ObjectMother
    {
        public static class Members
        {
            public static Member Fred
            {
                get { return new Member("Fred", State.Wa, new DateTime(1970, 1, 1));}
            }

            public static Member ForState(State state)
            {
                return new Member("A Member", state, new DateTime(1970, 1, 1));
            }

            public static Member WithAge(int age, DateTime now)
            {
                return WithDateOfBirth(now.AddYears(-age));
            }

            public static Member WithDateOfBirth(DateTime dob)
            {
                return new Member("A Member", State.Wa, dob);
            }
        }

        public static class Demographics
        {
            public static Demographic AllMembers
            {
                get { return Demographic.AllMembers; }
            }

            public static Demographic WithStateAndAgeRange
            {
                get { return new Demographic(State.Wa, 18, 19); }
            }

            public static Demographic ForState(State state)
            {
                return new Demographic(state, null, null);
            }

            public static Demographic WithMinimumAge(int minimumAge)
            {
                return new Demographic(null, minimumAge, null);
            }

            public static Demographic WithMaximumAge(int maximumAge)
            {
                return new Demographic(null, null, maximumAge);
            }
        }
    }
}
