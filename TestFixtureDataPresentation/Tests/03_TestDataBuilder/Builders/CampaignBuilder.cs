using System;
using NTestDataBuilder;
using TestFixtureDataPresentation.Implementation.Models;

namespace TestFixtureDataPresentation.Tests._03_TestDataBuilder.Builders
{
    class CampaignBuilder : TestDataBuilder<Campaign, CampaignBuilder>
    {
        public CampaignBuilder()
        {
            Set(x => x.Demographic, Demographic.AllMembers);
            Set(x => x.StartDate, new DateTime(2000, 1, 1));
            Set(x => x.EndDate, new DateTime(2000, 1, 2));
        }

        public CampaignBuilder WithDemographic(DemographicBuilder demographic)
        {
            Set(x => x.Demographic, demographic.Build());
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
            return new Campaign(Get(x => x.Demographic), Get(x => x.StartDate), Get(x => x.EndDate));
        }
    }
}