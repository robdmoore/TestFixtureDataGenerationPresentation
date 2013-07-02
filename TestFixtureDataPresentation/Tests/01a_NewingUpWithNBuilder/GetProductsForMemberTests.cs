using System;
using System.Linq;
using FizzWare.NBuilder;
using NUnit.Framework;
using TestFixtureDataPresentation.Implementation.Models;
using TestFixtureDataPresentation.Implementation.Queries;

namespace TestFixtureDataPresentation.Tests._01a_NewingUpWithNBuilder
{
    class GetProductsForMemberTests : QueryTestBase
    {
        private readonly Member _member = new Member("Name", State.Wa, new DateTime(1970, 1, 1));
        private DateTime _now = DateTime.UtcNow;

        [Test]
        public void GivenProductsWithNoCampaignOrACampaignThatIsntCurrent_WhenQuerying_ThenReturnNoResults()
        {
            Builder<Product>.CreateListOfSize(3)
                .TheFirst(1).WithConstructor(() => new Product("Product with no campaign"))
                .TheNext(1).WithConstructor(() => new Product("Campaign that hasn't started"))
                    .And(p => p.CreateCampaign(_now, Demographic.AllMembers, _now.AddDays(1), _now.AddDays(2)))
                .TheNext(1).WithConstructor(() => new Product("Campaign that has ended"))
                    .And(p => p.CreateCampaign(_now, Demographic.AllMembers, _now.AddDays(-2), _now.AddDays(-1)))
                .Build().ToList().ForEach(p => Session.Save(p));

            var result = Execute(new GetProductsForMember(_now, _member));

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GivenProductsWithCurrentCampaignWithSomeThatApplyToTheMember_WhenQuerying_ThenReturnTheProductsThatApplyToTheMember()
        {
            var member = new Member("Name", State.Wa, _now.AddYears(-10).AddDays(-1));
            var products = Builder<Product>.CreateListOfSize(3)
                .TheFirst(1).WithConstructor(() => new Product("Product 1 (applies)"))
                    .And(p => p.CreateCampaign(_now, Demographic.AllMembers, _now.AddDays(-1), _now.AddDays(1)))
                .TheNext(1).WithConstructor(() => new Product("Product 2 (doesn't apply)"))
                    .And(p => p.CreateCampaign(_now, new Demographic(State.Act, null, null), _now.AddDays(-1), _now.AddDays(1)))
                .TheNext(1).WithConstructor(() => new Product("Product 3 (applies)"))
                    .And(p => p.CreateCampaign(_now, new Demographic(State.Wa, 9, 11), _now.AddDays(-1), _now.AddDays(1)))
                .Build();
            products.ToList().ForEach(p => Session.Save(p));

            var result = Execute(new GetProductsForMember(_now, member));

            Assert.That(result.Select(p => p.Name).ToArray(), Is.EqualTo(new[]{products[0].Name, products[2].Name}));
        }
    }
}
