using System.Collections;
using UnityEngine;

namespace Utilities
{
    public static class EditorValidation
    {
        public static bool IsEmptyString(Object thisObject, string fieldName, string stringToCheck)
        {
            if (!string.IsNullOrWhiteSpace(stringToCheck)) return false;
            
            Debug.LogWarning(fieldName + " is empty and must contain a value in object " + thisObject.name);
            
            return true;
        }

        public static bool IsNullValue(Object thisObject, string fieldName, Object objectToCheck)
        {
            if (objectToCheck != null) return false;
            
            Debug.Log(fieldName + " is null and must contain a value in object " + thisObject.name);
            
            return true;
        }

        /// <summary>
        ///     List empty or contains null value check - returns true if there is an error
        /// </summary>
        public static bool AreEnumerableValues(
            Object thisObject,
            string fieldName,
            IEnumerable enumerableObjectToCheck)
        {
            var error = false;
            var count = 0;

            if (enumerableObjectToCheck == null)
            {
                Debug.Log(fieldName + " is null in object " + thisObject.name);
                return true;
            }

            foreach (object item in enumerableObjectToCheck)
                if (item == null)
                {
                    Debug.LogWarning(fieldName + " has null values in object " + thisObject.name);
                    error = true;
                } else
                {
                    count++;
                }

            if (count != 0) return error;
            
            Debug.LogWarning(fieldName + " has no values in object " + thisObject.name);

            return true;
        }

        public static bool IsPositiveValue(Object thisObject, string fieldName, int valueToCheck, bool isZeroAllowed = true)
        {
            if (isZeroAllowed)
            {
                if (valueToCheck >= 0) return false;
             
                Debug.LogWarning(fieldName + " must contain a positive value or zero in object " + thisObject.name);
            } else
            {
                if (valueToCheck > 0) return false;
                
                Debug.LogWarning(fieldName + " must contain a positive value in object " + thisObject.name);
            }

            return true;
        }

        public static bool IsPositiveValue(
            Object thisObject,
            string fieldName,
            float valueToCheck,
            bool isZeroAllowed = true)
        {
            if (isZeroAllowed)
            {
                if (!(valueToCheck < 0)) return false;
                
                Debug.LogWarning(fieldName + " must contain a positive value or zero in object " + thisObject.name);

            } else
            {
                if (!(valueToCheck <= 0)) return false;
                
                Debug.LogWarning(fieldName + " must contain a positive value in object " + thisObject.name);
            }

            return true;
        }

        public static bool IsPositiveRange(
            Object thisObject,
            string fieldNameMinimum,
            float valueToCheckMinimum,
            string fieldNameMaximum,
            float valueToCheckMaximum,
            bool isZeroAllowed = true)
        {
            var error = false;

            if (valueToCheckMinimum > valueToCheckMaximum)
            {
                Debug.LogWarning(fieldNameMinimum
                                 + " must be less than or equal to  "
                                 + fieldNameMaximum
                                 + " in object "
                                 + thisObject.name);

                error = true;
            }

            if (IsPositiveValue(thisObject, fieldNameMinimum, valueToCheckMinimum, isZeroAllowed))
                error = true;

            if (IsPositiveValue(thisObject, fieldNameMaximum, valueToCheckMaximum, isZeroAllowed))
                error = true;

            return error;
        }
    }
}
