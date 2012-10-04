using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace Personal.Lib
{
    public static class EnumUtils
    {
        public static string GetEnumDescription(Type enumType, int value)
        {
            return GetEnumDescription(enumType, Enum.GetName(enumType, value));
        }

        public static string GetEnumDescription(Type enumType, Enum e)
        {
            return GetEnumDescription(enumType, Enum.GetName(enumType, e));
        }

        /// <summary>
        /// Obtém a descrição definida no atributo para o valor do enumerado (se existir)
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Type enumType, string name)
        {
            var fieldInfo = enumType.GetField(name);
            var descAttributes = fieldInfo
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                as DescriptionAttribute[];

            return descAttributes.Length > 0
                ? descAttributes[0].Description
                : name;
        }
    }
}
