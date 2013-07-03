using System;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using TestFixtureDataPresentation.Implementation.Models;
using TestFixtureDataPresentation.Implementation.Queries;
using TestFixtureDataPresentation.Tests._05_TestDataBuilderAndObjectMother.Builders;
using TestFixtureDataPresentation.Tests._05_TestDataBuilderAndObjectMother.BuildersWithNBuilder;
using TestFixtureDataPresentation.Tests._05_TestDataBuilderAndObjectMother.ObjectMothers;

namespace TestFixtureDataPresentation.Tests._05_TestDataBuilderAndObjectMother
{
    class GetProductsForMemberTests : QueryTestBase
    {
        private readonly Member _member = ObjectMother.Members.Fred.Build();
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
            var products = Builder<ProductBuilder>.CreateListOfSize(3)
                .TheFirst(1).With(b => b.WithNoCampaigns())
                .TheNext(1).With(b => b.WithCampaign(_now, ObjectMother.Campaigns.NotStarted(_now)))
                .TheNext(1).With(b => b.WithCampaign(_now, ObjectMother.Campaigns.Ended(_now)))
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
                .TheFirst(1).With(b => b.WithName("1").WithCampaign(_now,
                    ObjectMother.Campaigns.Current(_now)
                    .ForAllMembers()
                ))
                .TheNext(1).With(b => b.WithName("2").WithCampaign(_now,
                    ObjectMother.Campaigns.Current(_now)
                    .ForState(State.Act)
                ))
                .TheNext(1).With(b => b.WithName("3").WithCampaign(_now,
                    ObjectMother.Campaigns.Current(_now)
                    .ForState(State.Wa)
                    .WithMinimumAge(9)
                    .WithMaximumAge(11)
                ))
                .BuildList();
            products.ToList().ForEach(p => Session.Save(p));

            var result = Execute(new GetProductsForMember(_now, member));

            Assert.That(result.Select(p => p.Name).ToArray(), Is.EqualTo(new[] { products[0].Name, products[2].Name }));
        }
    }
}
