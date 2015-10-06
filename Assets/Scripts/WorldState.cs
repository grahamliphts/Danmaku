using UnityEngine;
using System.Collections;

public class WorldState : MonoBehaviour
{

    public Entity[] entities;
    // Update is called once per frame
    void FixedUpdate ()
    {

        Entity.EntityStruct[] entitiesStruct = new Entity.EntityStruct[entities.Length];
        for (int i = 0; i < entities.Length; i++)
        {
            entitiesStruct[i] = entities[i].CreateStruct();
        }
        var newEntitiesStruct = GraphState.Step(entitiesStruct, Time.deltaTime);
        for(int i = 0; i < entities.Length; i++)
        {
            entities[i].SetStruct(newEntitiesStruct[i]);
        }
	}
}
