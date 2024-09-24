using System.ComponentModel.DataAnnotations;

namespace QuestPDFTest.Models;

public enum AssetType
{
    None,
    [Display(Name = "Bank Account")]
    Bank,
    Investment,
    Property,
    [Display(Name = "Life Insurance")]
    LifeInsurance,
    Vehicle,
    Trust,
    Other,
    [Display(Name = "Burial Arrangement")]
    BurialArrangement
}


public enum AccountType
{
    //if has not been used can reorder
    Unknown,

    Checking,

    Savings,

    Annuity,

    [Display(Name = "Credit Union")]
    CreditUnion,

    CD,

    [Display(Name = "IRA/401K")]
    IRA401k,

    KEOGH,

    [Display(Name = "Money Market")]
    MoneyMarket,

    [Display(Name = "403B")]
    B403,

    [Display(Name = "401K")]
    K401,

    IRA,

    Stocks,

    Bonds,

    [Display(Name = "Mutual Fund")]
    MutualFund,

    ETF,

    Home,

    [Display(Name = "Other Homes")]
    OtherHomes,

    Land,

    Building,

    TimeShare,

    [Display(Name = "Life Estate")]
    LifeEstate,

    [Display(Name = "Whole Life")]
    WholeLife,

    [Display(Name = "Term Life")]
    TermLife,

    [Display(Name = "Universal Life")]
    UniversalLife,

    [Display(Name = "Irrevocable Income Trust")]
    IrrevocableIncomeTrust,
    [Display(Name = "Testamentary Trust")]
    TestamentoryTrust,

    [Display(Name = "Special Needs Trust")]
    SpecialNeedsTrust,

    Business,
    [Display(Name = "Holiday/Vacation Club")]
    HolidayVacationClub,

    [Display(Name = "Burial Plot")]
    BurialPlot,

    [Display(Name = "Burial Account")]
    BurialAccount,

    [Display(Name = "Through Life Insurance Policy")]
    ThroughLifeInsurance,

    Other,
}

