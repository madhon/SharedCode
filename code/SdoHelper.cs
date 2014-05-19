using System;
using System.Reflection;
using System.Runtime.InteropServices;
using SDO = Sage.DataObjects190;

namespace Sage
{
    internal static class SdoHelper
    {

        /// <summary>
        /// Writes to an SDO field
        /// </summary>
        /// <param name="oObject">The object to write to</param>
        /// <param name="fname">Name of the required field</param>
        /// <param name="value">Value you wish to write</param>
        public static void Write(object oObject, String fname, object value)
        {
            //Stores the required field name in an object
            SDO.Fields fields = GetFields(oObject);
            SDO.Field field = GetField(fields, fname);

            field.Value = value;
            Marshal.FinalReleaseComObject(field);
            Marshal.FinalReleaseComObject(fields);
        }

        /// <summary>
        /// Reads an SDO field and returns its value as an object
        /// </summary>
        /// <param name="oObject">The object to read from</param>
        /// <param name="fname">Name of the required field</param>
        /// <returns>Returns an Object containing the value from the field</returns>
        public static object Read(object oObject, String fname)
        {
            //Stores the required field name in an object
            SDO.Fields fields = GetFields(oObject);
            SDO.Field field = GetField(fields, fname);
            object value = field.Value;

            Marshal.FinalReleaseComObject(field);
            Marshal.FinalReleaseComObject(fields);

            return value;
        }

        private static SDO.Field GetField(SDO.Fields fields, string fName)
        {
            object fieldName = fName;
            return fields.Item(ref fieldName);
        }


        private static SDO.Fields GetFields(Object oObject)
        {
            return (SDO.Fields)oObject.GetType().InvokeMember("Fields", BindingFlags.GetProperty, null, oObject, null);
        }

        /// <summary>
        /// Invokes the Add() method of an items collection
        /// </summary>
        /// <param name="oObject">The items collection you wish to invoke the Add() method on</param>
        /// <returns></returns>
        public static Object Add(object oObject)
        {
            //Uses reflection to invoke the Add() Method on the required object
            return oObject.GetType().InvokeMember
              ("Add", BindingFlags.InvokeMethod, null, oObject, null);
        }
    }
}
