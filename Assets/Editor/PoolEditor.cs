using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class PoolEditor : EditorWindow
{
    Object prefab;
    string namePool;
    int nbObject;

    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/PoolEditor")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(PoolEditor));
    }

    void OnGUI()
    {
        GUILayout.Label("Settings", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical();
        namePool = EditorGUILayout.TextField("Pool Name: ", namePool);
        nbObject = EditorGUILayout.IntField("Nombre d'objets: ", nbObject);
        prefab = EditorGUILayout.ObjectField("Prefab: ", prefab, typeof(Object), true) as GameObject;
        EditorGUILayout.EndVertical();

        if (GUILayout.Button("OK"))
            CreatePool(namePool, nbObject, prefab);

    }

    static void CreatePool(string namePool, int nbObject, Object prefab)
    {
        
        if (namePool != "" && nbObject >= 0 && prefab != null)
        {
            GameObject pool = new GameObject();
            pool.name = namePool;
            for(int i = 0; i < nbObject; i++)
            {
                GameObject prefabObj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                prefabObj.transform.parent = pool.transform;
            }

        }
    }
}
