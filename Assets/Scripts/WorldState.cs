using UnityEngine;
using System.Collections;

public class WorldState : MonoBehaviour
{

    public Entity[] entities;
    Entity.EntityStruct[] entitiesStruct;
    //public BulletPath[] Bullets;

    void Start()
    {
        entitiesStruct = new Entity.EntityStruct[entities.Length];
        for (int i = 0; i < entities.Length; i++)
        {
            entitiesStruct[i] = entities[i].CreateStruct(entities[i].gameObject.transform.position, new Vector3(0,0,0),i, entities[i].gameObject.activeSelf);
            entities[i].EID = i;
        }
    }
    void FixedUpdate ()
    {

      // Entity.EntityStruct[] entitiesStruct = new Entity.EntityStruct[entities.Length];
        for(int i = 0; i < entitiesStruct.Length; i++)
        {
            //entities[i].SetStruct(newEntitiesStruct[i]);
            //Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
            //Debug.DrawRay(entitiesStruct[i].position, forward, Color.green);
            Debug.DrawLine(entitiesStruct[i].position, entitiesStruct[i].position + entitiesStruct[i].direction * 20, Color.red);
            // Debug.Log("Draw Debug");
        }
	}

    public void updateEntitie(Vector3 position,Vector3 ADirection,Quaternion Rotation, int EID,bool isActive,float speed)
    {
        entitiesStruct[EID].position = position;
        entitiesStruct[EID].direction = ADirection;
        entitiesStruct[EID].rotation = Rotation;
        entitiesStruct[EID].isActive = isActive;
        entitiesStruct[EID].speed = speed;
        /*if (EID == 1)
        {
            Debug.Log("Entitie : " + EID + " Updated");
            Debug.Log("position : " + position + " Updated");
            Debug.Log("direction : " + ADirection + " Updated");
            Debug.Log("rotation : " + Rotation + " Updated");
            Debug.Log("speed : " + speed + " Updated");
            Debug.Log("isActive : " + isActive + " Updated");
        }*/
    }

    public Entity.EntityStruct GetEntity(int EID)
    {
        return entitiesStruct[EID];
    }
}
 