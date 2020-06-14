using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorChecks
{
   const string notSetInInspect = " is empty. Set it in the inspector.";
   const string emptyArry = " array has no elements. Set it in the inspector.";
   const string emptyElement = " has an empty element. Set it in the inspector.";
    public static void NullCheck(Object obj, string name)
    {
        if (obj == null)
        {
            Debug.LogError(name + notSetInInspect);
        }
    }
    public static void EmptyArrayCheck(ArrayList array, string name) 
    {
        if (array.Count == 0)
        {
            Debug.LogError(name + emptyArry);
            return;
        }
        for(int i = 0; i < array.Count; i++)
        {
            if (array[i] == null)
                Debug.LogError(name + emptyElement);

        }
    }
}
