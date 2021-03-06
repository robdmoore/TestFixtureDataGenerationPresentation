﻿using System;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using TestFixtureDataPresentation.Implementation.Models;
using TestFixtureDataPresentation.Implementation.Queries;
using TestFixtureDataPresentation.Tests._04_TestDataBuilder.BuildersWithNBuilder;

namespace TestFixtureDataPresentation.Tests._04_TestDataBuilderWithNBuilder
{
    class GetProductsForMemberTests : QueryTestBase
    {
        private readonly Member _member = new MemberBuilder().Build();
        private readonly DateTime _now = DateTime.UtcNow;

        [Test]
        public void GivenProductsWithNoCampaignOrACampaignThatIsntCurrent_WhenQuerying_ThenReturnNoResults()
        {
            var products = Builder<ProductBuilder>.CreateListOfSize(3)
                .TheFirst(1).With(b => b.WithNoCampaigns())
                .TheNext(1).With(b => b.WithCampaign(_now, new CampaignBuilder()
                    .StartingAt(_now.AddDays(1)).EndingAt(_now.AddDays(2))
                ))
                .TheNext(1).With(b => b.WithCampaign(_now, new CampaignBuilder()
                    .StartingAt(_now.AddDays(-2)).EndingAt(_now.AddDays(-1))
                ))
                .BuildList();
            products.ToList().ForEach(p => Session.Save(p));

            var result = Execute(new GetProductsForMember(_now, _member));

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GivenProductsWithCurrentCampaignWithSomeThatApplyToTheMember_WhenQuerying_ThenReturnTheProductsThatApplyToTheMember()
        {
            var member = new MemberBuilder().InState(State.Wa).WithAge(10, _now).Build();
            var products = Builder<ProductBuilder>.CreateListOfSize(3)
                .TheFirst(1).With(b => b.WithName("1").WithCampaign(_now, new CampaignBuilder()
                    .ForAllMembers()
                    .StartingAt(_now.AddDays(-1))
                    .EndingAt(_now.AddDays(1))
                ))
                .TheNext(1).With(b => b.WithName("2").WithCampaign(_now, new CampaignBuilder()
                    .ForState(State.Act)
                    .StartingAt(_now.AddDays(-1))
                    .EndingAt(_now.AddDays(1))
                ))
                .TheNext(1).With(b => b.WithName("3").WithCampaign(_now, new CampaignBuilder()
                    .ForState(State.Wa)
                    .WithMinimumAge(9)
                    .WithMaximumAge(11)
                    .StartingAt(_now.AddDays(-1))
                    .EndingAt(_now.AddDays(1))
                ))
                .BuildList();
            products.ToList().ForEach(p => Session.Save(p));

            var result = Execute(new GetProductsForMember(_now, member));

            Assert.That(result.Select(p => p.Name).ToArray(), Is.EqualTo(new[]{products[0].Name, products[2].Name}));
        }
    }
}
