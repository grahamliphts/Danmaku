using UnityEngine;
using System.Collections;

public class WorldState : MonoBehaviour
{

    public Entity[] entities;
    Entity.EntityStruct[] entitiesStruct;
    //public BulletPath[] Bullets;

    private bool m_isSet = false;
    void Start()
    {
        entitiesStruct = new Entity.EntityStruct[entities.Length];
        for (int i = 0; i < entities.Length; i++)
        {
            entitiesStruct[i] = entities[i].CreateStruct(entities[i].gameObject.transform.position, new Vector3(0,0,0),i, entities[i].gameObject.activeSelf);
            entities[i].EID = i;
        }
        m_isSet = true;
    }
    void FixedUpdate ()
    {

      // Entity.EntityStruct[] entitiesStruct = new Entity.EntityStruct[entities.Length];
        for(int i = 0; i < entitiesStruct.Length; i++)
        {
            //entities[i].SetStruct(newEntitiesStruct[i]);
            //Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
            //Debug.DrawRay(entitiesStruct[i].position, forward, Color.green);
            for (int j = 0; j < 20; j++ )
            {
                if(j%2 == 0)
                    Debug.DrawLine(entitiesStruct[i].position + entitiesStruct[i].direction * j, entitiesStruct[i].position + entitiesStruct[i].direction * (j + 1), Color.red);
                else
                    Debug.DrawLine(entitiesStruct[i].position + entitiesStruct[i].direction * j, entitiesStruct[i].position + entitiesStruct[i].direction * (j + 1), Color.green);
                
            }
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

    public int getEntityAmount()
    {
        if (m_isSet)
            return entitiesStruct.Length;
        else
            return -1;
    }
}
 