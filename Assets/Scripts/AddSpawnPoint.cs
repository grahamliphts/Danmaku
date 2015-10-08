using UnityEngine;
using System.Collections;

public class AddSpawnPoint : MonoBehaviour {


    public DataSpawn data;

    public GameObject poolBoss1;
    public GameObject poolBoss2;


	void Start ()
    {
        if (DataSpawn.Instance.spawnDatas[0].isSet)
        {
            poolBoss1.SetActive(true);
            poolBoss1.transform.position = DataSpawn.Instance.spawnDatas[0].position;
        }
           
        else
            poolBoss1.SetActive(false);
        if (DataSpawn.Instance.spawnDatas[1].isSet)
        {
            poolBoss2.SetActive(true);
            poolBoss2.transform.position = DataSpawn.Instance.spawnDatas[1].position;
        }
           
        else
            poolBoss2.SetActive(false);
    }
	
}
