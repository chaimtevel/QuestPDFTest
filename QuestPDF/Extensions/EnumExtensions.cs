using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace QuestPDFTest.Extensions;

internal static class EnumExtensions
{
    public static string GetDisplay<T>(this T value) where T : Enum
    {
        string strValue = value.ToString();

        FieldInfo? field = value.GetType().GetField(strValue);

        if (field is null) { return strValue; }

        DisplayAttribute? displayAttr = Attribute.GetCustomAttribute(field, typeof(DisplayAttribute)) as DisplayAttribute;

        if (displayAttr?.Name != null)
        {
            return displayAttr.Name;
        }

        DescriptionAttribute? descriptionAttr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

        return descriptionAttr?.Description ?? strValue;
    }
}
