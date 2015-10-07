using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class PoolEditor : EditorWindow
{
    Object prefab;
    string namePool;
    int nbObject;
    bool createBtn;

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
        createBtn = EditorGUILayout.Toggle("Create pool: ", createBtn);
        EditorGUILayout.EndVertical();
        if (GUILayout.Button("OK"))
            CreatePool(namePool, nbObject, prefab);

    }

    public void CreatePool(string namePool, int nbObject, Object prefab)
    {
        if (namePool != "" && nbObject >= 0 && prefab != null)
        {
            GameObject pool;
            if (createBtn)
            {
                pool = new GameObject();
                pool.name = namePool;
                Pool poolComponent = pool.AddComponent<Pool>();
                poolComponent.items = new GameObject[nbObject];
                for (int i = 0; i < nbObject; i++)
                {
                    GameObject prefabObj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                    prefabObj.transform.parent = pool.transform;
                    poolComponent.items[i] = prefabObj;
                }

            }

            else if ((pool = GameObject.Find(namePool)) != null)
            {
                Pool poolComponent = pool.GetComponent<Pool>();
                int nb = pool.transform.childCount;
                Debug.Log("Nb " + nb);
                Debug.Log("NbObject " + nbObject);
                if (nbObject > nb)
                {
                    GroupResize(nbObject, ref poolComponent.items);
                    for (int i = nb; i < nbObject; i++)
                    {
                        GameObject prefabObj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                        prefabObj.transform.parent = pool.transform;
                        poolComponent.items[i] = prefabObj;
                    }
                }
                else if (nbObject < nb)
                {
                    int nbToRemove = nb - nbObject;
                    Debug.Log(nbToRemove);
                    GroupResize(nbObject, ref poolComponent.items);
                    Transform[] transformToRemove = new Transform[nbToRemove];
                    for (int i = 0; i < nbToRemove; i++)
                    {
                        Debug.Log("i " + i);
                        transformToRemove[i] = pool.transform.GetChild(i);
                    }
                    for(int i = 0; i < nbToRemove; i++)
                    {
                        DestroyImmediate(transformToRemove[i].gameObject);
                    }
                    for (int i = 0; i < poolComponent.items.Length; i++)
                    {
                        poolComponent.items[i] = pool.transform.GetChild(i).gameObject;
                    }
                }
                
            }
        }
    }

    public void GroupResize(int Size, ref GameObject[] Group)
    {

        GameObject[] temp = new GameObject[Size];
        for (int c = 0; c < Mathf.Min(Size, Group.Length); c++)
        {
            temp[c] = Group[c];
        }
        Group = temp;
    }
}
