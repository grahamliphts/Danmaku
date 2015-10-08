using UnityEngine;
using System.Collections;

public class AddSpawnPoint : MonoBehaviour {


    public DataSpawn data;

    public GameObject poolBoss1;
    public GameObject poolBoss2;


	void Start ()
    {
        if (data.spawnDatas[0].isSet)
        {
            poolBoss1.SetActive(true);
            poolBoss1.transform.position = data.spawnDatas[0].position;
        }
           
        else
            poolBoss1.SetActive(false);
        if (data.spawnDatas[1].isSet)
        {
            poolBoss2.SetActive(true);
            poolBoss2.transform.position = data.spawnDatas[1].position;
        }
           
        else
            poolBoss2.SetActive(false);

        
    }
	
}
