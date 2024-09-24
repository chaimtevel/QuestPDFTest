using QuestPDFTest.Extensions;

namespace QuestPDFTest.Models;

public class ResourcesPDFModel
{
    public AssetType AssetType { get; init; }
    public AccountType AccountType { get; init; }
    public string? AccountNumber { get; init; }
    public string? InstitutionName { get; init; }
    public virtual decimal CurrentValue { get; set; }
    public virtual decimal? AmountOwed { get; init; }
    public virtual string? Description { get; init; }


    #region read-only

    public string AssetTypeDisplay => AssetType.GetDisplay();
    public string AccountTypeDisplay => AccountType.GetDisplay();

    #endregion
}
