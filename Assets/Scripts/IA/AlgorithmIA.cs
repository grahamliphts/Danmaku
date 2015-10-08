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
    private int[] _bulletInside;
    private int _bulletInIndex = -1;
    private int _explo = 6;

    private _gridData[] _gridOuvert;
    private int _nbGrid = 0;
    private float[] _cible;

    private struct _gridData
    {
        public string move;
        public int iter;
        public float dist;
    };

    void Start()
    {
        _bulletInside = new int[81];
        int zone = 64 * (_explo * _explo);
        _gridOuvert = new _gridData[zone];
        _cible = new float[2];
        _cible[1] = -2;
    } 
     
    void FixedUpdate()
    {
        // Deplacement 0.1 / move
        if (bossPosition.goRight)
            _cible[0] = bossPosition.transform.position.x + 1;
        else
            _cible[0] = bossPosition.transform.position.x - 1;
        
        float dist = CalculDistance(actualPosition.position.x, actualPosition.position.y, _cible[0], _cible[1]);

        string move = "";
        int iter = 0;

        AddGrid("O", 0, dist);
         
        while (_nbGrid != 0)
        {
            int closeCase = SelectCloser();

            move = _gridOuvert[closeCase].move;
            iter = _gridOuvert[closeCase].iter + 1;

            if (iter >= _explo)
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
            OpenGrid(actuPos, new Vector3(0F   , 0.1F , 0F), "U"  , closeCase); // Up
            OpenGrid(actuPos, new Vector3(0.1F , 0.1F , 0F), "TUR", closeCase); // Up Right
            OpenGrid(actuPos, new Vector3(0.1F , 0F   , 0F), "R"  , closeCase); // Right
            OpenGrid(actuPos, new Vector3(0.1F , -0.1F, 0F), "TDR", closeCase); // Down Right 
            OpenGrid(actuPos, new Vector3(0F   , -0.1F, 0F), "D"  , closeCase); // Down
            OpenGrid(actuPos, new Vector3(-0.1F, -0.1F, 0F), "TDL", closeCase); // Down Left
            OpenGrid(actuPos, new Vector3(-0.1F, 0F   , 0F), "L"  , closeCase); // Left
            OpenGrid(actuPos, new Vector3(-0.1F, 016F , 0F), "TUL", closeCase); // Up Left
            #endregion

            RemoveGrid(closeCase);
        }
        if (iter < _explo)
        {
            if(move != "O")
                Movement(move, 1);
        }
        _nbGrid = 0;
    }

    void OpenGrid(Vector3 actuPos, Vector3 diff, string move, int indexClose)
    {
        Vector3 postPos = actuPos + diff;
        Vector3 cameraPos = Camera.main.WorldToViewportPoint(postPos);
        bool access = false;

        switch(move)
        {
            case "U":
                if (cameraPos.y < 0.8)
                    access = true;
                break;
            case "TUR":
                if ((cameraPos.x < 1) && (cameraPos.y < 0.8))
                    access = true;
                break;
            case "R":
                if (cameraPos.x < 1)
                    access = true;
                break;
            case "TDR":
                if ((cameraPos.x < 1) && (cameraPos.y > 0.1))
                    access = true;
                break;
            case "D":
                if (cameraPos.y > 0.1)
                    access = true;
                break;
            case "TDL":
                if ((cameraPos.x > 0) && (cameraPos.y > 0.1))
                    access = true;
                break;
            case "L":
                if (cameraPos.x > 0)
                    access = true;
                break;
            case "TUL":
                if ((cameraPos.x > 0) && (cameraPos.y < 0.8))
                    access = true;
                break;
        }

        if (access && (detectPostCollision(postPos, _gridOuvert[indexClose].iter) == 0))
        {
            float dist = CalculDistance(postPos.x, postPos.y, _cible[0], _cible[1]);
            AddGrid(_gridOuvert[indexClose].move + move, _gridOuvert[indexClose].iter + 1, dist);
            Debug.DrawLine(postPos, postPos + new Vector3(0.05F, 0.05F, 0), Color.red);
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
        for (int i = 0; i < _bulletInIndex; i++)
        {
            int id = _bulletInside[i];
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

    void AddGrid(string move, int iter, float dist)
    {
        _gridOuvert[_nbGrid].move = move;
        _gridOuvert[_nbGrid].iter = iter;
        _gridOuvert[_nbGrid].dist = dist;
        _nbGrid++;
    }

    void RemoveGrid(int index)
    {
        for (int i = index; i < _nbGrid - 1; i++)
        {
            _gridOuvert[i] = _gridOuvert[i + 1];
        }
        _nbGrid--;
    }

    int SelectCloser()
    {
        float dist = 1000;
        int closerIndex = 0;

        for (int i = 0; i < _nbGrid ; i++)
        {
            if (_gridOuvert[i].dist < dist)
            {
                closerIndex = i;
                dist = _gridOuvert[i].dist;
            }
        }
        return closerIndex;
    }
     
    void OnTriggerEnter2D(Collider2D bullet)
    {
        if (bullet.tag == "projEnemy")
        {
            /*
            BulletPath bulletScript = bullet.gameObject.GetComponent<BulletPath>();
            _idBulletsActive.Add(bulletScript.EID);*/

            if (_bulletInIndex == -1)
                _bulletInIndex++;
             
            BulletPath bulletScript = bullet.gameObject.GetComponent<BulletPath>();
            _bulletInside[_bulletInIndex] = bulletScript.EID;
            _bulletInIndex++;
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
            for (int i = 0; i < _bulletInIndex; i++)
            {
                if (flag || (_bulletInside[i] == bulletScript.EID && i < _bulletInIndex - 1))
                {
                    _bulletInside[i] = _bulletInside[i + 1];
                    flag = true;
                }
            }
            _bulletInIndex--;
        }
    }

}
