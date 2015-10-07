using UnityEngine;
using System.Collections;

public class WorldState : MonoBehaviour
{

    public Entity[] entities;
    Entity.EntityStruct[] entitiesStruct;
    //public BulletPath[] Bullets;
    // Update is called once per frame
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

       /* Entity.EntityStruct[] entitiesStruct = new Entity.EntityStruct[entities.Length];
        for (int i = 0; i < entities.Length; i++)
        {
            entitiesStruct[i] = entities[i].CreateStruct();
        }
        var newEntitiesStruct = GraphState.Step(entitiesStruct, Time.deltaTime);
        for(int i = 0; i < entities.Length; i++)
        {
            entities[i].SetStruct(newEntitiesStruct[i]);
        }*/
	}

    public void updateEntitie(Vector3 position,Vector3 ADirection,Quaternion Rotation, int EID,bool isActive,float speed)
    {
        for (int i = 0; i < entitiesStruct.Length; i++)
        {
            if (entitiesStruct[i].ID == EID)
            {
                entitiesStruct[i].position = position;
                entitiesStruct[i].direction = ADirection;
                entitiesStruct[i].rotation = Rotation;
                entitiesStruct[i].isActive = isActive;
                entitiesStruct[i].speed = speed;
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
        }
    }

    public Entity.EntityStruct GetEntity(int EID)
    {
        Entity.EntityStruct result = new Entity.EntityStruct();
        for (int i = 0; i < entitiesStruct.Length; i++)
        {
            if (entitiesStruct[i].ID == EID)
            {
                result = entitiesStruct[i];
            }
        }
        return result;
    }
}
 