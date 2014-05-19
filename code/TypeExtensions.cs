using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class TypeExtensions
{

public static T As<T>(this object target)
        {
            return (T) target;
        }


public static bool CanBeCastTo<T>(this Type type)
        {
            if (type == null) return false;
            Type destinationType = typeof (T);

            return CanBeCastTo(type, destinationType);
        }

        public static bool CanBeCastTo(this Type type, Type destinationType)
        {
            if (type == null) return false;
            if (type == destinationType) return true;

            return destinationType.IsAssignableFrom(type);
        }

         public static T Create<T>(this Type type)
        {
            return (T) type.Create();
        }

        public static object Create(this Type type)
        {
            return Activator.CreateInstance(type);
        }
}