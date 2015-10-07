using UnityEngine;
using System.Collections;

public class dijkstraAlgorithm : MonoBehaviour {

    public BossMove bossPosition;
    public WorldState __worldState;
    public PlayerController controlPlayer;

    private Entity.EntityStruct[] m_worldArray;
    private bool m_amountSet = false;

	// Use this for initialization
	void Start () {

        /* while (__worldState.getEntityAmount() == -1) ;
         int entityAmount = __worldState.getEntityAmount();
         m_worldArray = new Entity.EntityStruct[entityAmount];

         Debug.Log(entityAmount);*/
        //Debug.Log(__worldState.getEntityAmount());
    }
	
	// Update is called once per frame
	void Update () {
        if (!m_amountSet && __worldState.getEntityAmount() != -1)
        {
            m_worldArray = new Entity.EntityStruct[__worldState.getEntityAmount()];
           // Debug.Log(m_worldArray.Length);
            m_amountSet = true; 
        }

        //for(int i = 0)
    }
}
