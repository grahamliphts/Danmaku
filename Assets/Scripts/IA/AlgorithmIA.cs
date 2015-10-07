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

    private List<int> _idBulletsActive;

    void Start()
    {
        _idBulletsActive = new List<int>();

        StartCoroutine(AlgoAStar());
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

        if ((CompX >= -0.4 && CompX <= 0.4) && (CompY >= -0.4 && CompY <= 0.4))
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
        foreach (int idActive in _idBulletsActive)
        {
            Entity.EntityStruct actualBullet = worldState.GetEntity(idActive);

            int res = PositionComparator(actualBullet, postPos, iter);
            if (res != -1)
            {
                result = 1;
                break;
            }
        }
        return result;
    }

    IEnumerator AlgoAStar()
    {
        while(true)
        {

            // Deplacement 0.1 / move
            float cibleX = 0;
            if (bossPosition.goRight)
                cibleX = bossPosition.transform.position.x + 1F;
            else
                cibleX = bossPosition.transform.position.x - 1F;
            float cibleY = -3;

            Dictionary<string, float> GridOuvert = new Dictionary<string, float>();
            List<string> GridFermer = new List<string>();

            float dist = CalculDistance(actualPosition.position.x, actualPosition.position.y, cibleX, cibleY);
             
            GridOuvert.Add("O:0", dist);
            while ((GridOuvert.Count != 0) && dist > 0.1F)
            {
                string closeCase = SelectCloser(GridOuvert);

                string move = closeCase.Split(':')[0];
                int iter = int.Parse(closeCase.Split(':')[1]) + 1;
                 
                if (iter == 8)
                {
                    switch (move.Substring(1, 1))
                    {
                        case "U":
                            playerIA.MoveUp();
                            break;
                        case "R":
                            playerIA.MoveRight();
                            break;
                        case "D":
                            playerIA.MoveDown();
                            break;
                        case "L":
                            playerIA.MoveLeft();
                            break;
                    }
                    break;
                }

                // Up
                Vector3 diff = new Vector3(0F, 0.1F, 0F);
                Vector3 postPos = actualPosition.position + diff;
                if (detectPostCollision(postPos, iter) == 0)
                {
                    dist = CalculDistance(postPos.x, postPos.y, cibleX, cibleY);
                    GridOuvert.Add(move + "U:" + iter, dist);
                }
                // Right
                diff = new Vector3(0.1F, 0F, 0F);
                postPos = actualPosition.position + diff;
                if (detectPostCollision(postPos, iter) == 0)
                {
                    dist = CalculDistance(postPos.x, postPos.y, cibleX, cibleY);
                    GridOuvert.Add(move + "R:" + iter, dist);
                }

                // Down
                diff = new Vector3(0F, -0.1F, 0F);
                postPos = actualPosition.position + diff;
                if (detectPostCollision(postPos, iter) == 0)
                {
                    dist = CalculDistance(postPos.x, postPos.y, cibleX, cibleY);
                    GridOuvert.Add(move + "D:" + iter, dist);
                }

                // Left
                diff = new Vector3(-0.1F, 0F, 0F);
                postPos = actualPosition.position + diff;
                if (detectPostCollision(postPos, iter) == 0)
                {
                    dist = CalculDistance(postPos.x, postPos.y, cibleX, cibleY);
                    GridOuvert.Add(move + "L:" + iter, dist);
                }

                GridFermer.Add(closeCase);
                GridOuvert.Remove(closeCase);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    string SelectCloser(Dictionary<string, float> GridOuvert)
    {
        float dist = 100;
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
            BulletPath bulletScript = bullet.gameObject.GetComponent<BulletPath>();
            _idBulletsActive.Add(bulletScript.EID);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("hit");
    }

    void OnTriggerExit2D(Collider2D bullet)
    {
        if (bullet.tag == "projEnemy")
        {
            BulletPath bulletScript = bullet.gameObject.GetComponent<BulletPath>();
            _idBulletsActive.Remove(bulletScript.EID);
        }
    }
}
