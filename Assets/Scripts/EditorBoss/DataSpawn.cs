using UnityEngine;
using System.Collections;

public class DataSpawn : MonoBehaviour
{
    public struct Spawn
    {
        public Vector2 position;
        public bool isSet;
    }

    public Spawn[] spawnDatas;
    public string ressourceName;

    public static DataSpawn Instance;

    public void Start()
    {
        if (!Instance)
        {
            spawnDatas = new Spawn[2];
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
