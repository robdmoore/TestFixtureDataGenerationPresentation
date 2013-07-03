using System;
using System.Collections.Generic;
using NTestDataBuilder;
using TestFixtureDataPresentation.Implementation.Models;

namespace TestFixtureDataPresentation.Tests._03_TestDataBuilder.Builders
{
    class ProductBuilder : TestDataBuilder<Product, ProductBuilder>
    {
        List<Tuple<DateTime, CampaignBuilder>> _campaigns = new List<Tuple<DateTime, CampaignBuilder>>();

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
            _campaigns = new List<Tuple<DateTime, CampaignBuilder>>();
            return this;
        }

        public ProductBuilder WithCampaign(DateTime now, CampaignBuilder campaign)
        {
            _campaigns.Add(Tuple.Create(now, campaign));
            return this;
        }

        protected override Product BuildObject()
        {
            var product = new Product(Get(x => x.Name));

            foreach (var campaign in _campaigns)
                product.CreateCampaign(
                    campaign.Item1,
                    campaign.Item2.Get(x => x.Demographic),
                    campaign.Item2.Get(x => x.StartDate),
                    campaign.Item2.Get(x => x.EndDate)
                );

            return product;
        }
    }
}
