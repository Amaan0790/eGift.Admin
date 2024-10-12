using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Reflection;

namespace eGift.Admin.MVC.Helpers
{
    public static class EnumHelper
    {
        #region Parse Enum To Select List

        //Enum Name Value
        public static SelectList EnumNameToSelectList<TEnum>() where TEnum : struct, Enum
        {
            var enumValues = Enum.GetValues(typeof(TEnum))
                                 .Cast<TEnum>()
                                 .Select(e => new { Id = Convert.ToInt32(e), Name = e.ToString() })
                                 .ToList();

            return new SelectList(enumValues, "Id", "Name");
        }

        //Enum Description Value
        public static SelectList EnumDescriptionToSelectList<TEnum>() where TEnum : struct, Enum
        {
            var enumValues = Enum.GetValues(typeof(TEnum))
                                 .Cast<TEnum>()
                                 .Select(e => new { Id = Convert.ToInt32(e), Description = e.GetEnumDescription<TEnum>() })
                                 .ToList();

            return new SelectList(enumValues, "Id", "Description");
        }

        //Enum Description Name
        public static SelectList EnumNameDescriptionToSelectList<TEnum>() where TEnum : struct, Enum
        {
            var enumValues = Enum.GetValues(typeof(TEnum))
                                 .Cast<TEnum>()
                                 .Select(e => new { Name = e.ToString(), Description = e.GetEnumDescription<TEnum>() })
                                 .ToList();

            return new SelectList(enumValues, "Name", "Description");
        }

        #endregion

        #region Get Enum Description From Element

        // Helper method to get the description attribute or enum name
        public static string GetEnumDescription<TEnum>(this TEnum enumValue) where TEnum : Enum
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : enumValue.ToString();
        }

        #endregion

        #region Get Enum Element From Description

        // Extension method to get enum value from its description
        public static TEnum GetEnumFromDescription<TEnum>(this string description) where TEnum : struct, Enum
        {
            foreach (var field in typeof(TEnum).GetFields())
            {
                var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

                // Match the description attribute
                if (attribute != null && attribute.Description == description)
                {
                    return (TEnum)field.GetValue(null);
                }

                // Fallback to matching the enum name
                if (field.Name == description)
                {
                    return (TEnum)field.GetValue(null);
                }
            }

            throw new ArgumentException($"No enum with description '{description}' found.");
        }

        #endregion
    }
}
