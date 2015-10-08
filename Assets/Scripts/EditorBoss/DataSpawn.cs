using UnityEngine;
using System.Collections;

[System.Serializable]
public class DataSpawn : ScriptableObject
{
    [System.Serializable]
    public struct Spawn
    {
        public Vector2 position;
        public bool isSet;
    }

    public Spawn[] spawnDatas;

}
