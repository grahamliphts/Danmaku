using UnityEngine;
using System.Collections;

public class dijkstraAlgorithm : MonoBehaviour
{

    public BossMove bossPosition;
    public WorldState __worldState;
    public PlayerController controlPlayer;
    public float radius = 0.05F;

    private Entity.EntityStruct[] m_worldArray;
    private bool m_amountSet = false;

    private float[] m_explored;
    private int[] m_bulletInside;
    private int m_bulletInsideIndex = -1;
    private bool m_idle = true;

    // 0 right
    // 1 down
    // 2 left
    // 3 up 
    private int[] m_dirArray = {0,1,1,2,2,3,3,3,0,0,0,1,1,1,1,2,2,2,2,3,3,3,3,3,0,0,0,0,0,1,1,
    1,1,1,1,2,2,2,2,2,2,3,3,3,3,3,3,3,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,3,3,3,3,3,3,3,3};

    /*{0,1,1,2,2,3,3,3,0,0,0,1,1,1,1,2,2,2,2,3,3,3,3,3,0,0,0,0,0,1,1,1,1,1,1,2,2,2,2,2,2,
                                3,3,3,3,3,3,3,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,3,3,3,3,3,3,3,3};*/
// Use this for initialization
void Start()
    {
        Debug.Log(m_dirArray.Length);
        m_explored = new float[79];
        m_bulletInside = new int[81];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!m_amountSet && __worldState.getEntityAmount() != -1)
        {
            m_worldArray = new Entity.EntityStruct[__worldState.getEntityAmount()];
            // Debug.Log(m_worldArray.Length);
            m_amountSet = true;
        }
        if (m_amountSet)
        {
            int iterator = 0;
            bool solution = false;
            Vector3 actualPosition = transform.position;
            while (iterator < 79 && !solution)
            {
                /*if (m_bulletInsideIndex != -1)
                    for (int i = 0; i < m_bulletInsideIndex; i++)
                    {
                        __worldState.GetEntity(m_bulletInside[i]);
                    }*/

                //actualPosition += new Vector3(0, 0.05F, 0);

                Vector3 bossPos = new Vector3(bossPosition.transform.position.x, -2, 0);

                  if (areaValue(actualPosition, 0.05F) > 0) //TODO
                {
                    Debug.DrawLine(actualPosition - new Vector3(0.05F, 0.05F, 0), actualPosition + new Vector3(0.05F, 0.05F, 0), Color.red);
                    Debug.DrawLine(actualPosition - new Vector3(0.05F, -0.05F, 0), actualPosition + new Vector3(0.05F, -0.05F, 0), Color.red);
                    m_idle = false;
                   // m_explored[iterator] = 1000F;
                    m_explored[iterator] = Vector3.Distance(actualPosition, bossPos);

                }
                else
                {
                    Debug.DrawLine(actualPosition - new Vector3(0.05F, 0.05F, 0), actualPosition + new Vector3(0.05F, 0.05F, 0), Color.green);
                    Debug.DrawLine(actualPosition - new Vector3(0.05F, -0.05F, 0), actualPosition + new Vector3(0.05F, -0.05F, 0), Color.green);
                   // m_explored[iterator] = Vector3.Distance(actualPosition, bossPos);
                    m_explored[iterator] = 1000F;
                    m_idle = true;
                }

                /* if(bossPosition.transform.position - actualPosition )
                 {

                 }*/
                
               
                actualPosition = nextPos(iterator, actualPosition); // TODO
                iterator++; // TODO
            }
        }
        // TEMPORARY **********************
       // if (!m_idle)
       // {
            float min = 10000;
            int posIndex = 0;
            float[] dirdec = { 0, 0, 0, 0, 0, 0, 0, 0 };
            //for (int j = 0; j < 79; j = j + 8 )
           // {
                int incr = 0;
                for (int temp = 0; temp < 8; temp++)
                {
                    dirdec[incr] += m_explored[0 + temp];

                    incr++;
                }
            //}
            for(int i = 0; i < 8; i++)
            {
                if (dirdec[i] < min)
                    {
                        posIndex = i;
                        min = dirdec[i];
                    }
            }
            switch (posIndex)
            {
                case 0:
                    controlPlayer.MoveUp();
                    Debug.DrawLine(transform.position, Vector3.up, Color.green);
                    break;
                case 1:
                    controlPlayer.MoveUp();
                    controlPlayer.MoveRight();
                    Debug.DrawLine(transform.position, Vector3.up + Vector3.right, Color.green);
                    break;
                case 2:
                    controlPlayer.MoveRight();
                    Debug.DrawLine(transform.position, Vector3.up + Vector3.right, Color.green);
                    break;
                case 3:
                    controlPlayer.MoveRight();
                    controlPlayer.MoveDown();
                    Debug.DrawLine(transform.position, Vector3.down + Vector3.right, Color.green);
                    break;
                case 4:
                    controlPlayer.MoveDown();
                    Debug.DrawLine(transform.position, Vector3.down, Color.green);
                    break;
                case 5:
                    controlPlayer.MoveDown();
                    controlPlayer.MoveLeft();
                    Debug.DrawLine(transform.position, Vector3.left + Vector3.down, Color.green);
                    break;
                case 6:
                    controlPlayer.MoveLeft();
                    Debug.DrawLine(transform.position, Vector3.left, Color.green);
                    break;
                case 7:
                    controlPlayer.MoveUp();
                    controlPlayer.MoveLeft();
                    Debug.DrawLine(transform.position, Vector3.up + Vector3.left, Color.green);
                    break;
            }
        //}


       // END OF TEMP
    }

    void OnTriggerEnter2D(Collider2D bullet)
    {
        if (bullet.tag == "projEnemy")
        {
            if (!m_amountSet && __worldState.getEntityAmount() != -1)
            {
                m_worldArray = new Entity.EntityStruct[__worldState.getEntityAmount()];
                // Debug.Log(m_worldArray.Length);
                m_amountSet = true;
            }
            if (m_bulletInsideIndex == -1)
                m_bulletInsideIndex++;
            BulletPath bulletScript = bullet.gameObject.GetComponent<BulletPath>();
            m_bulletInside[m_bulletInsideIndex] = bulletScript.EID;
            m_worldArray[m_bulletInsideIndex] = __worldState.GetEntity(bulletScript.EID);
            m_bulletInsideIndex++;
        }
    }
    void OnTriggerExit2D(Collider2D bullet)
    {
        if (bullet.tag == "projEnemy")
        {
           

            BulletPath bulletScript = bullet.gameObject.GetComponent<BulletPath>();
            for (int i = 0; i < m_bulletInsideIndex; i++)
                if (m_bulletInside[i] == bulletScript.EID && i < m_bulletInsideIndex - 1)
                {
                    m_bulletInside[i] = m_bulletInside[i + 1];
                    m_worldArray[i] = m_worldArray[i + 1];
                }
            m_bulletInsideIndex--;
        }
    }

    Vector3 nextPos(int step, Vector3 currentPos)
    {
        switch(m_dirArray[step])
        {
            case 0:         // move right
                return currentPos + new Vector3(0.1F, 0, 0);
                break;
            case 1:         // move down
                return currentPos + new Vector3(0, -0.1F, 0);
                break;
            case 2:         // move left
                return currentPos + new Vector3(-0.1F, 0, 0);
                break;
            case 3:         // move up
                return currentPos + new Vector3(0, 0.1F, 0);
                break;
            default:        //Error
                return currentPos + new Vector3(0, 0, 0);
                break;
        }
    }

    int areaValue(Vector3 position, float radius)
    {
        for(int i =0; i < m_bulletInsideIndex; i++)
        {
            Entity.EntityStruct watched = m_worldArray[i];
            watched.position = watched.position + watched.direction ;
            if (inArea(watched.position, position, 0.01F, 0.1F)) // TODO
            {
                //Debug.Log(watched.ID + " Is on" + watched.position);
                return 1000;
            }
        }
     
        return 0;
    }

    bool inArea(Vector3 toTest, Vector3 center, float radiusArea, float radiusTotest)
    {
        if(toTest.x + radiusTotest >= center.x - radiusArea || toTest.x - radiusTotest <= center.x + radiusArea) // X radius in area
        {
            if (toTest.y + radiusTotest <= center.y - radiusArea || toTest.y - radiusTotest >= center.y + radiusArea)
                return true;
        }
        return false;
    }
}
