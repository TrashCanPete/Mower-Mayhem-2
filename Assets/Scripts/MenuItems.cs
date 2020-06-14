/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class MenuItems : MonoBehaviour
{
    public static GameObject Grass;
    [MenuItem("GameObject/Utility/Grass Area", false, 10)]
    static void CreateCustomGameObject(MenuCommand menuCommand)
    {
        GameObject go = Instantiate(Resources.Load("Grass Area")) as GameObject;
        // Ensure it gets reparented if this was a context click (otherwise does nothing)
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }
}*/
