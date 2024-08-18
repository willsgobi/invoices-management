using System.ComponentModel;
using System.Reflection;

namespace InvoiceManagementApp.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            var descriptionAttribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>();

            return descriptionAttribute != null ? descriptionAttribute.Description : enumValue.ToString();
        }
    }
}
