using System;
using System.Linq;
using NUnit.Framework;
using TestFixtureDataPresentation.Implementation.Models;
using TestFixtureDataPresentation.Implementation.Queries;
using TestFixtureDataPresentation.Tests._03_TestDataBuilder.Builders;

namespace TestFixtureDataPresentation.Tests._03_TestDataBuilder
{
    class GetProductsForMemberTests : QueryTestBase
    {
        private readonly Member _member = new MemberBuilder().Build();
        private readonly DateTime _now = DateTime.UtcNow;

        [Test]
        public void GivenNoProducts_WhenQuerying_ThenReturnNoResults()
        {
            var result = Execute(new GetProductsForMember(_now, _member));

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GivenProductsWithNoCampaignOrACampaignThatIsntCurrent_WhenQuerying_ThenReturnNoResults()
        {
            var products = new[]
            {
                new ProductBuilder().WithNoCampaigns().Build(),
                new ProductBuilder().WithCampaign(_now,
                    new CampaignBuilder().StartingAt(_now.AddDays(1)).EndingAt(_now.AddDays(2))
                ).Build(),
                new ProductBuilder().WithCampaign(_now,
                    new CampaignBuilder().StartingAt(_now.AddDays(-2)).EndingAt(_now.AddDays(-1))
                ).Build()
            };
            products.ToList().ForEach(p => Session.Save(p));

            var result = Execute(new GetProductsForMember(_now, _member));

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GivenProductsWithCurrentCampaignWithSomeThatApplyToTheMember_WhenQuerying_ThenReturnTheProductsThatApplyToTheMember()
        {
            var member = new MemberBuilder().InState(State.Wa).WithAge(10, _now).Build();
            var products = new[]
            {
                new ProductBuilder().WithName("1").WithCampaign(_now,
                    new CampaignBuilder()
                        .WithDemographic(new DemographicBuilder().ForAllMembers())
                        .StartingAt(_now.AddDays(-1))
                        .EndingAt(_now.AddDays(1))
                ).Build(),
                new ProductBuilder().WithName("2").WithCampaign(_now,
                    new CampaignBuilder()
                        .WithDemographic(new DemographicBuilder().ForState(State.Act))
                        .StartingAt(_now.AddDays(-1))
                        .EndingAt(_now.AddDays(1))
                ).Build(),
                new ProductBuilder().WithName("2").WithCampaign(_now,
                    new CampaignBuilder()
                        .WithDemographic(new DemographicBuilder()
                            .ForState(State.Wa)
                            .WithMinimumAge(9)
                            .WithMaximumAge(11)
                        )
                        .StartingAt(_now.AddDays(-1))
                        .EndingAt(_now.AddDays(1))
                ).Build()
            };
            products.ToList().ForEach(p => Session.Save(p));

            var result = Execute(new GetProductsForMember(_now, member));

            Assert.That(result.Select(p => p.Name).ToArray(), Is.EqualTo(new[]{products[0].Name, products[2].Name}));
        }
    }
}
