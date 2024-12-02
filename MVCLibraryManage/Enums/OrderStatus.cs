using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MVCLibraryManage.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "Đã mượn")]
        borrowed = 1,
        [Display(Name = "Đã trả")]
        preparing,
        [Display(Name = "Quá hạn trả")]
        canceled
    }
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
              .GetMember(enumValue.ToString())
              .First()
              .GetCustomAttribute<DisplayAttribute>()
              ?.GetName();
        }
    }
}
