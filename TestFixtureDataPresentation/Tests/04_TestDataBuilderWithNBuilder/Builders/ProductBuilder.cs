using System;
using System.Collections.Generic;
using FizzWare.NBuilder;
using NTestDataBuilder;
using TestFixtureDataPresentation.Implementation.Models;

namespace TestFixtureDataPresentation.Tests._04_TestDataBuilder.BuildersWithNBuilder
{
    static class ProductBuilderExtensions
    {
        public static IList<Product> BuildList(this IOperable<ProductBuilder> list)
        {
            return list.BuildList<Product, ProductBuilder>();
        }
    }

    class ProductBuilder : TestDataBuilder<Product, ProductBuilder>
    {
        List<Tuple<DateTime, Campaign>> _campaigns = new List<Tuple<DateTime, Campaign>>();

        public ProductBuilder()
        {
            Set(x => x.Name, "A product");
        }

        public ProductBuilder WithName(string name)
        {
            Set(x => x.Name, name);
            return this;
        }

        public ProductBuilder WithNoCampaigns()
        {
            _campaigns = new List<Tuple<DateTime, Campaign>>();
            return this;
        }

        public ProductBuilder WithCampaign(DateTime now, CampaignBuilder campaign)
        {
            _campaigns.Add(Tuple.Create(now, campaign.Build()));
            return this;
        }

        protected override Product BuildObject()
        {
            var product = new Product(Get(x => x.Name));

            foreach (var campaign in _campaigns)
                product.CreateCampaign(
                    campaign.Item1,
                    campaign.Item2.Demographic,
                    campaign.Item2.StartDate,
                    campaign.Item2.EndDate
                );

            return product;
        }
    }
}
