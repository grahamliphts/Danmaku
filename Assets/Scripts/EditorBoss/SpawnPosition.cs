﻿using UnityEngine;
using System.Collections;

public class SpawnPosition : MonoBehaviour
{
    public Transform spawn1;
    public Transform spawn2;

    private DataSpawn.Spawn spawn1Struct;
    private DataSpawn.Spawn spawn2Struct;

    public void Save()
    {
        spawn1Struct = new DataSpawn.Spawn();
        spawn2Struct = new DataSpawn.Spawn();

        spawn1Struct.isSet = false;
        spawn2Struct.isSet = false;

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

        DataSpawn.Instance.spawnDatas[0] = spawn1Struct;
        DataSpawn.Instance.spawnDatas[1] = spawn2Struct;

        Application.LoadLevel("MenuScene");
    }
}
