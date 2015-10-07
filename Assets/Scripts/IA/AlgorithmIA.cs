// --------------------
// Script Intelligence Artificielle - Algorithme
// Projet: Damnaku
// --------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AlgorithmIA : MonoBehaviour
{
    [SerializeField]
    public BossMove bossPosition;

    [SerializeField]
    public Transform actualPosition;

    [SerializeField]
    public WorldState worldState;

    [SerializeField]
    public PlayerController playerIA;

    //private List<int> _idBulletsActive;
    private int[] m_bulletInside;
    private int m_bulletInsideIndex = -1;
    private char last = 'R';

    void Start()
    {
        m_bulletInside = new int[81];
    }
     
    void FixedUpdate()
    {
        // Deplacement 0.1 / move
        float cibleX;
        if (bossPosition.goRight)
            cibleX = bossPosition.transform.position.x + 1;
        else
            cibleX = bossPosition.transform.position.x - 1;
        float cibleY = -2;
        
        Dictionary<string, float> GridOuvert = new Dictionary<string, float>();

        float dist = CalculDistance(actualPosition.position.x, actualPosition.position.y, cibleX, cibleY);

        GridOuvert.Add("O:0", dist);

        string move = "";
        int iter = 0;
         
        while (GridOuvert.Count != 0)
        {
            string closeCase = SelectCloser(GridOuvert);

            move = closeCase.Split(':')[0];
            iter = int.Parse(closeCase.Split(':')[1]) + 1;

            if (iter >= 6)
            {
                Movement(move, 1);
                break;
            }
             
            Vector3 actuPos = actualPosition.position;
            foreach(char dir in move)
            {
                switch(dir)
                {
                    case 'U':
                        actuPos += new Vector3(0F, 0.1F, 0F);
                        break;
                    case 'R':
                        actuPos += new Vector3(0.1F, 0F, 0F);
                        break;
                    case 'D':
                        actuPos += new Vector3(0F, -0.1F, 0F);
                        break;
                    case 'L':
                        actuPos += new Vector3(-0.1F, 0F, 0F);
                        break;
                    default:
                        break;
                }
            }

            #region Add Node
            // Up
            Vector3 diff = new Vector3(0F, 0.1F, 0F);
            Vector3 postPos = actuPos + diff;
            Vector3 cameraPos = Camera.main.WorldToViewportPoint(postPos);
            if ((detectPostCollision(postPos, iter) == 0) && (cameraPos.y < 0.8))
            {
                dist = CalculDistance(postPos.x, postPos.y, cibleX, cibleY);
                GridOuvert.Add(move + "U:" + iter, dist);
                Debug.DrawLine(postPos, postPos + new Vector3(0.05F, 0.05F, 0), Color.red);
            }

            // Up Right
            diff = new Vector3(0.1F, 0.1F, 0F);
            postPos = actuPos + diff;
            cameraPos = Camera.main.WorldToViewportPoint(postPos);
            if ((detectPostCollision(postPos, iter) == 0) && (cameraPos.x < 1) && (cameraPos.y < 0.8))
            {
                dist = CalculDistance(postPos.x, postPos.y, cibleX, cibleY);
                GridOuvert.Add(move + "TUR:" + iter, dist);
                Debug.DrawLine(postPos, postPos + new Vector3(0.05F, 0.05F, 0), Color.red);
            }

            // Right
            diff = new Vector3(0.1F, 0F, 0F);
            postPos = actuPos + diff;
            cameraPos = Camera.main.WorldToViewportPoint(postPos);
            if ((detectPostCollision(postPos, iter) == 0) && (cameraPos.x < 1))
            {
                dist = CalculDistance(postPos.x, postPos.y, cibleX, cibleY);
                GridOuvert.Add(move + "R:" + iter, dist);
                Debug.DrawLine(postPos, postPos + new Vector3(0.05F, 0.05F, 0), Color.red);
            }

            // Down Right 
            diff = new Vector3(0.1F, -0.1F, 0F);
            postPos = actuPos + diff;
            cameraPos = Camera.main.WorldToViewportPoint(postPos);
            if ((detectPostCollision(postPos, iter) == 0) && (cameraPos.x < 1) && (cameraPos.y > 0.1))
            {
                dist = CalculDistance(postPos.x, postPos.y, cibleX, cibleY);
                GridOuvert.Add(move + "TDR:" + iter, dist);
                Debug.DrawLine(postPos, postPos + new Vector3(0.05F, 0.05F, 0), Color.red);
            }

            // Down
            diff = new Vector3(0F, -0.1F, 0F);
            postPos = actuPos + diff;
            cameraPos = Camera.main.WorldToViewportPoint(postPos);
            if ((detectPostCollision(postPos, iter) == 0) && (cameraPos.y > 0.1))
            {
                dist = CalculDistance(postPos.x, postPos.y, cibleX, cibleY);
                GridOuvert.Add(move + "D:" + iter, dist);
                Debug.DrawLine(postPos, postPos + new Vector3(0.05F, 0.05F, 0), Color.red);
            }

            // Down Left
            diff = new Vector3(-0.1F, -0.1F, 0F);
            postPos = actuPos + diff;
            cameraPos = Camera.main.WorldToViewportPoint(postPos);
            if ((detectPostCollision(postPos, iter) == 0) && (cameraPos.x > 0) && (cameraPos.y > 0.1))
            {
                dist = CalculDistance(postPos.x, postPos.y, cibleX, cibleY);
                GridOuvert.Add(move + "TDL:" + iter, dist);
                Debug.DrawLine(postPos, postPos + new Vector3(0.05F, 0.05F, 0), Color.red);
            } 

            // Left
            diff = new Vector3(-0.1F, 0F, 0F);
            postPos = actuPos + diff;
            cameraPos = Camera.main.WorldToViewportPoint(postPos);
            if ((detectPostCollision(postPos, iter) == 0) && (cameraPos.x > 0))
            {
                dist = CalculDistance(postPos.x, postPos.y, cibleX, cibleY);
                GridOuvert.Add(move + "L:" + iter, dist);
                Debug.DrawLine(postPos, postPos + new Vector3(0.05F, 0.05F, 0), Color.red);
            }

            // Up Left
            diff = new Vector3(-0.1F, 0.1F, 0F);
            postPos = actuPos + diff;
            cameraPos = Camera.main.WorldToViewportPoint(postPos);
            if ((detectPostCollision(postPos, iter) == 0) && (cameraPos.x > 0) && (cameraPos.y < 0.8))
            {
                dist = CalculDistance(postPos.x, postPos.y, cibleX, cibleY);
                GridOuvert.Add(move + "TUL:" + iter, dist);
                Debug.DrawLine(postPos, postPos + new Vector3(0.05F, 0.05F, 0), Color.red);
            }
            #endregion

            GridOuvert.Remove(closeCase);
        }
        if (iter < 6)
        {
            Movement(move, 1);
        }
    }
    
    void Movement(string move, int index)
    {
        switch (move[index])
        {
            case 'U':
                playerIA.MoveUp();
                break;
            case 'R':
                playerIA.MoveRight();
                break;
            case 'D':
                playerIA.MoveDown();
                break;
            case 'L':
                playerIA.MoveLeft();
                break;
            case 'T':
                Movement(move, index + 1);
                Movement(move, index + 2);
                break;
            default:
                break;
        }
    }

    int PositionComparator(Entity.EntityStruct projectile, Vector3 posPlayerIA, int iter)
    {
        int result = -1;
        Vector3 bulletPostPosition;
        Vector3 differential = projectile.direction;

        // Radius IA = 0.4u
        // Radius Projectile = 0.2u

        bulletPostPosition = projectile.position + differential * iter;
        float CompX = posPlayerIA.x - bulletPostPosition.x;
        float CompY = posPlayerIA.y - bulletPostPosition.y;

        if ((CompX >= -0.3f && CompX <= 0.3f) && (CompY >= -0.3f && CompY <= 0.3f))
        {
            result = iter;
        } 
         
        return result;
    }

    float CalculDistance(float posX, float posY, float endX, float endY)
    {
        float result = 0;
        result += posX > endX ? posX - endX : endX - posX;
        result += posY > endY ? posY - endY : endY - posY;
        return result;
    }
    
    int detectPostCollision(Vector3 postPos, int iter)
    {
        int result = 0;
        for (int i = 0; i < m_bulletInsideIndex; i++)
        {
            int id = m_bulletInside[i];
            Entity.EntityStruct actualBullet = worldState.GetEntity(id);

            int res = PositionComparator(actualBullet, postPos, iter);
            if (res != -1)
            {
                result = 1;
                break;
            }
        }
        return result;
    }

    string SelectCloser(Dictionary<string, float> GridOuvert)
    {
        float dist = 1000;
        string resultCloser = "O:0";

        foreach (KeyValuePair<string, float> Case in GridOuvert)
        {
            if (Case.Value < dist)
            {
                resultCloser = Case.Key;
                dist = Case.Value;
            }
        }
        return resultCloser;
    }
     
    void OnTriggerEnter2D(Collider2D bullet)
    {
        if (bullet.tag == "projEnemy")
        {
            /*
            BulletPath bulletScript = bullet.gameObject.GetComponent<BulletPath>();
            _idBulletsActive.Add(bulletScript.EID);*/
            
            if (m_bulletInsideIndex == -1)
                m_bulletInsideIndex++;
             
            BulletPath bulletScript = bullet.gameObject.GetComponent<BulletPath>();
            m_bulletInside[m_bulletInsideIndex] = bulletScript.EID;
            m_bulletInsideIndex++;
        }
    }

    void OnTriggerExit2D(Collider2D bullet)
    {
        if (bullet.tag == "projEnemy")
        {
            /*
            BulletPath bulletScript = bullet.gameObject.GetComponent<BulletPath>();
            _idBulletsActive.Remove(bulletScript.EID);*/

            BulletPath bulletScript = bullet.gameObject.GetComponent<BulletPath>();
            bool flag = false;
            for (int i = 0; i < m_bulletInsideIndex; i++)
            {
                if(flag || (m_bulletInside[i] == bulletScript.EID && i < m_bulletInsideIndex - 1))
                {
                    m_bulletInside[i] = m_bulletInside[i + 1];
                    flag = true;
                }
            }
            m_bulletInsideIndex--;
        }
    }
}
