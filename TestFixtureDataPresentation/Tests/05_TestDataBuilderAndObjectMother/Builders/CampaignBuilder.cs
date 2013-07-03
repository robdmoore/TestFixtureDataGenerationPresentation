using System;
using NTestDataBuilder;
using TestFixtureDataPresentation.Implementation.Models;

namespace TestFixtureDataPresentation.Tests._05_TestDataBuilderAndObjectMother.Builders
{
    class CampaignBuilder : TestDataBuilder<Campaign, CampaignBuilder>
    {
        private readonly DemographicBuilder _demographic = new DemographicBuilder();

        public CampaignBuilder()
        {
            _demographic.ForAllMembers();
            Set(x => x.StartDate, new DateTime(2000, 1, 1));
            Set(x => x.EndDate, new DateTime(2000, 1, 2));
        }

        public CampaignBuilder ForAllMembers()
        {
            _demographic.ForAllMembers();
            return this;
        }

        public CampaignBuilder ForState(State state)
        {
            _demographic.ForState(state);
            return this;
        }

        public CampaignBuilder WithMinimumAge(int minimumAge)
        {
            _demographic.WithMinimumAge(minimumAge);
            return this;
        }

        public CampaignBuilder WithMaximumAge(int maximumAge)
        {
            _demographic.WithMaximumAge(maximumAge);
            return this;
        }

        public CampaignBuilder StartingAt(DateTime startDate)
        {
            Set(x => x.StartDate, startDate);
            return this;
        }

        public CampaignBuilder EndingAt(DateTime endDate)
        {
            Set(x => x.EndDate, endDate);
            return this;
        }

        protected override Campaign BuildObject()
        {
            return new Campaign(_demographic.Build(), Get(x => x.StartDate), Get(x => x.EndDate));
        }
    }
}