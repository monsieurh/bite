using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace mpm_modules.Populate
{
    [AttributeUsage(AttributeTargets.Field)]
    public class GetComponentInParentAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class GetComponentAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class GetComponentInChildrenAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class FindObjectsOfTypeAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class GetComponentsInChildrenAttribute : Attribute
    {
    }


    [AttributeUsage(AttributeTargets.Field)]
    public class FindObjectOfTypeAttribute : Attribute
    {
    }

    public static class ComponentFinder
    {
        private static readonly Type[] RegisteredArrayAttributes =
        {
            typeof(FindObjectsOfTypeAttribute),
            typeof(GetComponentsInChildrenAttribute),
        };

        private static readonly Type[] RegisteredAttributes =
        {
            typeof(GetComponentAttribute),
            typeof(GetComponentInChildrenAttribute),
            typeof(FindObjectOfTypeAttribute),
        };

        // ReSharper disable once UnusedMethodReturnValue.Global
        public static bool Populate(this MonoBehaviour monoBehaviour)
        {
            return monoBehaviour.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Aggregate(true, (current, fieldInfo) => current | PopulateField(monoBehaviour, fieldInfo));
        }

        private static bool PopulateField(Component monoBehaviour, FieldInfo fieldInfo)
        {
            return fieldInfo.FieldType.IsArray
                ? RegisteredArrayAttributes.Any(attType => TryPopulateArray(monoBehaviour, fieldInfo, attType))
                : RegisteredAttributes.Any(attType => TryPopulate(monoBehaviour, fieldInfo, attType));
        }

        private static bool IsList(FieldInfo fieldInfo)
        {
            Type oType = fieldInfo.FieldType;
            return oType.IsGenericType && oType.GetGenericTypeDefinition() == typeof(List<>);
        }

        private static bool TryPopulateArray(Component monoBehaviour, FieldInfo fieldInfo, Type attributeType)
        {
            Attribute attribute = fieldInfo.GetCustomAttribute(attributeType);
            if (attribute == null) return false;
            fieldInfo.SetValue(
                monoBehaviour,
                GetArrayValue(monoBehaviour, fieldInfo, attribute)
            );

            return true;
        }

        private static bool TryPopulate(Component monoBehaviour, FieldInfo fieldInfo, Type attributeType)
        {
            Attribute attribute = fieldInfo.GetCustomAttribute(attributeType);
            if (attribute == null) return false;

            fieldInfo.SetValue(monoBehaviour, GetFieldValue(monoBehaviour, fieldInfo, attribute));

            return true;
        }

        private static Object GetFieldValue(Component monoBehaviour, FieldInfo fieldInfo, Attribute attributeType)
        {
            switch (attributeType)
            {
                case GetComponentAttribute _:
                    return monoBehaviour.GetComponent(fieldInfo.FieldType);
                case GetComponentInParentAttribute _:
                    return monoBehaviour.GetComponentInParent(fieldInfo.FieldType);
                case GetComponentInChildrenAttribute _:
                    return monoBehaviour.GetComponentInChildren(fieldInfo.FieldType);
                case FindObjectOfTypeAttribute _:
                    return Object.FindObjectOfType(fieldInfo.FieldType);
                default:
                    return null;
            }
        }

        private static Component[] GetArrayValue(Component monoBehaviour, FieldInfo fieldInfo, Attribute attribute)
        {
            switch (attribute)
            {
                case FindObjectsOfTypeAttribute _:
                    return Object.FindObjectsOfType(fieldInfo.FieldType.GetElementType()) as Component[];
                case GetComponentsInChildrenAttribute _:
                    return monoBehaviour.GetComponents(fieldInfo.FieldType.GetElementType());
                default:
                    return null;
            }
        }
    }
}
