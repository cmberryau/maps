using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace Maps.Extensions
{
    /// <summary>
    /// Provides a set of extensions for Enums
    /// </summary>
    public static class EnumExtensions
    {
        // holds mapping between member attribute values and their fields
        private static readonly Lazy<Dictionary<Type, Dictionary<string, FieldInfo>>> 
            EnumMemberNameField = new Lazy<Dictionary<Type, Dictionary<string, FieldInfo>>>(
                () => new Dictionary<Type, Dictionary<string, FieldInfo>>());

        // holds mapping between field values as strings and their member attribute values
        private static readonly Lazy<Dictionary<Type, Dictionary<string, string>>>
            EnumFieldMemberAttribute = new Lazy<Dictionary<Type, Dictionary<string, string>>>(
                () => new Dictionary<Type, Dictionary<string, string>>());

        /// <summary>
        /// Evaluates if the given value string is present for the given Enum
        /// </summary>
        /// <typeparam name="T">The Enum type to evaluate for</typeparam>
        /// <param name="valueString">The value string</param>
        public static bool HasEnumValueFromMemberValue<T>(this string valueString)
        {
            // extensions can be called on null objects
            if (valueString == null)
            {
                throw new ArgumentNullException(nameof(valueString));
            }

            // ensure that the given type is mapped
            var type = typeof(T);
            EnsureMapping(type);

            return EnumMemberNameField.Value[type].ContainsKey(valueString);
        }

        /// <summary>
        /// Returns an Enum value where the given string matches the 
        /// Value attribute of the Enum member
        /// </summary>
        /// <typeparam name="T">The Enum type to evaluate for</typeparam>
        /// <param name="valueString">The value string</param>
        public static T GetEnumFromMemberValue<T>(this string valueString)
        {
            // extensions can be called on null objects
            if (valueString == null)
            {
                throw new ArgumentNullException(nameof(valueString));
            }

            // ensure that the given type is mapped
            var type = typeof(T);
            EnsureMapping(type);

            // if the key is there, it will match the field, which can give 
            // us access to the member attribute value
            if (EnumMemberNameField.Value[type].ContainsKey(valueString))
            {
                return (T) EnumMemberNameField.Value[type][valueString].GetValue(null);
            }

            throw new ArgumentException("Requested Enum from Member Value " + valueString + 
                " was not found", nameof(valueString));
        }

        private static void EnsureMapping(Type type)
        {
            // store the type's member attributes against fields
            if (!EnumMemberNameField.Value.ContainsKey(type))
            {
                if (!type.IsEnum)
                {
                    throw new InvalidOperationException();
                }

                EnumMemberNameField.Value[type] = new Dictionary<string, FieldInfo>();

                foreach (var field in type.GetFields())
                {
                    // skip the layout descriptor
                    if (field.Name == "value__")
                        continue;

                    var memberAttribute = Attribute.GetCustomAttribute(field,
                        typeof(EnumMemberAttribute)) as EnumMemberAttribute;

                    // skip it if it has no member attribute
                    if (memberAttribute == null)
                        continue;

                    // set the lookup key to the member attribute value
                    EnumMemberNameField.Value[type][memberAttribute.Value] = field;
                }
            }
        }

        /// <summary>
        /// Returns an Enum value's member value string
        /// </summary>
        /// <typeparam name="T">The Enum type to evaluate for</typeparam>
        /// <param name="enumValue">The enum value to evaluate for</param>
        public static string GetEnumMemberValue<T>(this T enumValue)
        {
            // confirm that T is infact an enum
            var type = typeof(T);

            if (!type.IsEnum)
            {
                throw new InvalidOperationException();
            }

            // store the type's field names against their member attribute values
            if (!EnumFieldMemberAttribute.Value.ContainsKey(type))
            {
                EnumFieldMemberAttribute.Value[type] = new Dictionary<string, string>();

                foreach (var field in type.GetFields())
                {
                    // skip the layout descriptor
                    if (field.Name == "value__")
                        continue;

                    var memberAttribute = Attribute.GetCustomAttribute(field,
                        typeof(EnumMemberAttribute)) as EnumMemberAttribute;

                    // skip it if it has no member attribute
                    if (memberAttribute == null)
                        continue;

                    // set the lookup key to the member attribute value
                    EnumFieldMemberAttribute.Value[type][field.Name] = memberAttribute.Value;
                }
            }

            if (EnumFieldMemberAttribute.Value[type].ContainsKey(enumValue.ToString()))
            {
                return EnumFieldMemberAttribute.Value[type][enumValue.ToString()];
            }

            throw new ArgumentException("Requested MemberValue for " + enumValue + " not found",
                nameof(enumValue));
        }
    }
}