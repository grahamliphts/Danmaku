using UnityEngine;
using System.Collections;

public class SpawnPosition : MonoBehaviour
{
    public Transform spawn1;
    public Transform spawn2;
    public Transform spawn3;

    private Spawn spawn1Struct;
    private Spawn spawn2Struct;
    private Spawn spawn3Struct;

    public struct Spawn
    {
        public Vector2 position;
        public bool isSet;
    }

    public void Save()
    {
        Spawn spawn1Struct;
        Spawn spawn2Struct;
        Spawn spawn3Struct;

        spawn1Struct.isSet = false;
        spawn2Struct.isSet = false;
        spawn3Struct.isSet = false;

        if (spawn1.parent == null)
        {
            spawn1Struct.position = spawn1.position;
            spawn1Struct.isSet = true;
        }
           
        if (spawn2.parent == null)
        {
            spawn2Struct.position = spawn2.position;
            spawn2Struct.isSet = true;
        }
        if (spawn3.parent == null)
        {
            spawn3Struct.position = spawn3.position;
            spawn3Struct.isSet = true;
        }

        Debug.Log(spawn1Struct.isSet);
        Debug.Log(spawn2Struct.isSet);
        Debug.Log(spawn3Struct.isSet);
    }

    public Spawn GetSpawnStruct(int nb)
    {
        if (nb == 1)
            return spawn1Struct;
        else if (nb == 2)
            return spawn2Struct;
        else if (nb == 3)
            return spawn3Struct;

        Spawn spawnStruct = new Spawn();
        return spawnStruct;
    }
}
