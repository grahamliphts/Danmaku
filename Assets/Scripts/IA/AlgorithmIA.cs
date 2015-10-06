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
    public PlayerController playerIA;

    //private bool[]  _idBulletsActive;
    private List<ushort> _idBulletsActive;

    private int _nbBullets = 0;
    private Vector3[] _bulletsPrePosition;
    private Vector3[] _bulletsActPosition;

    private float[] _moveCallibration;

	void Start ()
    {
        _nbBullets = bullets.Length;

        _moveCallibration    = new float[4];
        _idBulletsActive     = new List<ushort>();
        _bulletsPrePosition  = new Vector3[_nbBullets];
        _bulletsActPosition  = new Vector3[_nbBullets];

        IaCallibration();

        StartCoroutine(Sentry());
	}

    IEnumerator Sentry()
    {
        while (true)
        {
            foreach (ushort idActive in _idBulletsActive)
            {
                _bulletsPrePosition[idActive] = _bulletsActPosition[idActive];
                _bulletsActPosition[idActive] = bullets[idActive].transform.position;

                Vector3 Diff = _bulletsActPosition[idActive] - _bulletsPrePosition[idActive];

                int nbIter = PositionComparator(Diff, idActive, 2);
                if (nbIter != -1)
                {

                }

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
            bulletPostPosition = _bulletsActPosition[idActive] + differential * i;

            float CompX = actualPosition.position.x - bulletPostPosition.x;
            float CompY = actualPosition.position.y - bulletPostPosition.y;
            print(CompX + " : " + CompY);
            if ((CompX >= -0.3 && CompX <= 0.3)&&(CompY >= -0.3 && CompY <= 0.3))
            {
                result = i;
                break;
            }
        }

        return result; 
    }

    void Dijkstra(int nbIter)
    {

    }

    void OnTriggerEnter2D(Collider2D bullet)
    {
        BulletPath bulletScript = bullet.gameObject.GetComponent<BulletPath>();
        _idBulletsActive.Add(bulletScript.id);
        //_idBulletsActive[bulletScript.id] = true;
        _bulletsActPosition[bulletScript.id] = bulletScript.transform.position;
    }

    void OnTriggerExit2D(Collider2D bullet)
    {
        BulletPath bulletScript = bullet.gameObject.GetComponent<BulletPath>();
        _idBulletsActive.Remove(bulletScript.id);
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

        print(_moveCallibration[0]);
        print(_moveCallibration[1]);
        print(_moveCallibration[2]);
        print(_moveCallibration[3]);
    }

    /* 
    Fonction Dijkstra (nœuds, fils, distance, début, fin)
    Pour n parcourant nœuds
        n.parcouru = infini   // Peut être implémenté avec -1 (*)
        n.précédent = 0
    Fin pour
    début.parcouru = 0
    pasEncoreVu = nœuds
    Tant que pasEncoreVu != liste vide
        n1 = minimum(pasEncoreVu)   // Le nœud dans pasEncoreVu avec parcouru le plus petit
        pasEncoreVu.enlever(n1)
        Pour n2 parcourant fils(n1)   // Les nœuds reliés à n1 par un arc
            Si n2.parcouru > n1.parcouru + distance(n1, n2)   //  distance correspond au poids de l'arc reliant n1 et n2
                n2.parcouru = n1.parcouru + distance(n1, n2)
                n2.précédent = n1   // Dit que pour aller à n2, il faut passer par n1
            Fin si
        Fin pour
    Fin tant que
    chemin = liste vide
    n = fin
    Tant que n != début
        chemin.ajouterAvant(n)
        n = n.précédent
    Fin tant que
    chemin.ajouterAvant(début)
    Retourner chemin
    Fin fonction Dijkstra
    */
}
