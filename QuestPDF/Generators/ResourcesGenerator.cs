using Bogus;
using QuestPDFTest.Models;

namespace QuestPDFTest.Generators;

internal static class ResourcesGenerator
{
    public static IEnumerable<ResourcesPDFModel> GetResourcesPDFModels()
    {
        var resources = new Faker<ResourcesPDFModel>()
            .RuleFor(x => x.AssetType, f => f.PickRandomWithout(AssetType.None))
            .RuleFor(m => m.AccountType, f => f.PickRandomWithout(AccountType.Unknown))
            .RuleFor(m => m.AccountNumber, f => f.Finance.Account())
            .RuleFor(m => m.InstitutionName, f => f.Company.CompanyName())
            .RuleFor(m => m.CurrentValue, f => f.Random.Decimal(1, 1_000_000))
            .RuleFor(m => m.AmountOwed, f => f.Random.Decimal(1, 10_000))
            .RuleFor(m => m.Description, f => f.Lorem.Sentence(1));

        return resources.Generate(5);
    }
}
