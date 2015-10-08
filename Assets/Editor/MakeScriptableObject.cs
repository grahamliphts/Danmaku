using UnityEngine;
using System.Collections;
using UnityEditor;

public class MakeScriptableObject : MonoBehaviour
{
    [MenuItem("Assets/MyScriptableObject")]
    public static void CreateAsset()
    {
        DataSpawn asset = ScriptableObject.CreateInstance<DataSpawn>();
        asset.spawnDatas = new DataSpawn.Spawn[2];

        AssetDatabase.CreateAsset(asset, "Assets/Resources/DataSpawnScriptableObject.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
	
}
