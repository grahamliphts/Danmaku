using UnityEngine;
using System.Collections;

public class GraphState : MonoBehaviour
{
    WorldState[] worldStates;
    float cost;

    void Start()
    {
        //etat initial
       /* worldStates[0] = ..
        for(int i = 1; i< 20; i++)
        {
            worldStates[i] = Step(worldsStates[i - 1]);
        }*/
    }

    void FixedUpdate()
    {
        //worldStates[20] = Step(worldStates[19]);
    }

    public static Entity.EntityStruct[] Step(Entity.EntityStruct[] entitiesStruct, float time)
    {
        for (int i = 1; i < entitiesStruct.Length; i++)
        {

           entitiesStruct[i].position += (Vector2)Vector3.right * time * entitiesStruct[i].angle;//(Vector2)(Vector3.right * entitiesStruct[i].speed * time);
        }

        return entitiesStruct;
    }
}
