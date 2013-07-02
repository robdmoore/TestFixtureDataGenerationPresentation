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

            public static Member InWaWithAge(int age, DateTime now)
            {
                return new Member("WA member", State.Wa, now.AddYears(-age));
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

        public static class Products
        {
            public static Product NoCampaign
            {
                get { return new Product("Product with no campaign"); }
            }

            public static Product NotStarted(DateTime now)
            {
                var product = new Product("Product with campaign that hasn't started");
                product.CreateCampaign(now, Demographic.AllMembers, now.AddDays(1), now.AddDays(2));
                return product;
            }

            public static Product Ended(DateTime now)
            {
                var product = new Product("Product with campaign that has ended");
                product.CreateCampaign(now, Demographic.AllMembers, now.AddDays(-2), now.AddDays(-1));
                return product;
            }

            public static Product CurrentForAllMembers(DateTime now)
            {
                var product = new Product("Product with current campaign for all members");
                product.CreateCampaign(now, Demographic.AllMembers, now.AddDays(-1), now.AddDays(1));
                return product;
            }

            public static Product CurrentForAllActMembers(DateTime now)
            {
                var product = new Product("Product with current campaign for ACT members");
                product.CreateCampaign(now, new Demographic(State.Act, null, null), now.AddDays(-1), now.AddDays(1));
                return product;
            }

            public static Product CurrentForWaMembersBetween9And11YearsOld(DateTime now)
            {
                var product = new Product("Product with current campaign for WA members between 9 and 11");
                product.CreateCampaign(now, new Demographic(State.Wa, 9, 11), now.AddDays(-1), now.AddDays(1));
                return product;
            }
        }
    }
}
