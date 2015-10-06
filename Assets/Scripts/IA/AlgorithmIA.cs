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
    public Transform bossPosition;

    [SerializeField]
    public BulletPath[] bullets;

    [SerializeField]
    public Transform actualPosition;

    [SerializeField]
    public WorldState worldState;

    [SerializeField]
    public PlayerController playerIA;

    private List<int> _idBulletsActive;

    private int _nbBullets = 0;

    private float[] _moveCallibration;

	void Start ()
    {
        _nbBullets = bullets.Length;

        _moveCallibration    = new float[4];
        _idBulletsActive     = new List<int>();

        //IaCallibration();

        StartCoroutine(Sentry());
	}

    IEnumerator Sentry()
    {
        while (true)
        {
            foreach (int idActive in _idBulletsActive)
            {
                Entity.EntityStruct actualBullet = worldState.GetEntity(idActive);
                //Debug.Log(actualBullet.position);
                /*
                int nbIter = PositionComparator(actualBullet.__WorldState.entities[idActive].d, idActive, 2);
                if (nbIter != -1)
                {
                    AlgoAStar(nbIter);
                }*/

            }
            yield return new WaitForSeconds(0.1f);
        }
    }
     

    int PositionComparator(Vector3 differential, ushort idActive, int iter)
    {
        int result = -1;
        Vector3 bulletPostPosition;

        // Radius IA = 0.4u
        // Radius Projectile = 0.2u

        for (int i = 1; i <= iter; i++)
        {
            //bulletPostPosition = _bulletsActPosition[idActive] + differential * i;
            //float CompX = actualPosition.position.x - bulletPostPosition.x;
            //float CompY = actualPosition.position.y - bulletPostPosition.y;
            /*
            if ((CompX >= -0.3 && CompX <= 0.3)&&(CompY >= -0.3 && CompY <= 0.3))
            {
                result = i;
                break;
            }*/
        }

        return result; 
    }

    void AlgoAStar(int nbIter)
    {
        int cibleX = 0;
        int cibleY = -3;

        List<int> GridOuvert = new List<int>();
        List<int> GridFermer = new List<int>();



    }

    void OnTriggerEnter2D(Collider2D bullet)
    {
        if(bullet.tag == "projEnemy")
        {
            BulletPath bulletScript = bullet.gameObject.GetComponent<BulletPath>();
            _idBulletsActive.Add(bulletScript.EID);
        }
       
        //_idBulletsActive[bulletScript.id] = true;
    }

    void OnTriggerExit2D(Collider2D bullet)
    {
        if (bullet.tag != "projPlayer")
        {
            BulletPath bulletScript = bullet.gameObject.GetComponent<BulletPath>();
            _idBulletsActive.Remove(bulletScript.EID);
        }
        //_idBulletsActive[bulletScript.id] = false;
    }

    float Abs(float number)
    {
        if(number <= 0)
            number = -number;
        return number;
    }

    void IaCallibration()
    {
        Vector3 preMove = actualPosition.position;

        playerIA.MoveUp();
        _moveCallibration[0] = actualPosition.position.x - preMove.x;
        preMove = actualPosition.position;

        playerIA.MoveRight();
        _moveCallibration[1] = actualPosition.position.y - preMove.y;
        preMove = actualPosition.position;

        playerIA.MoveDown();
        _moveCallibration[2] = preMove.x - actualPosition.position.x;
        preMove = actualPosition.position;

        playerIA.MoveLeft();
        _moveCallibration[3] = preMove.y - actualPosition.position.y;
    }
}
