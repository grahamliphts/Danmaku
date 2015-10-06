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
    public Transform _actualPosition;

    //private bool[]  _idBulletsActive;
    private List<ushort> _idBulletsActive;

    private int _nbBullets = 0;
    private Vector3[] _bulletsPrePosition;
    private Vector3[] _bulletsActPosition;

	void Start ()
    {
        _nbBullets = bullets.Length;

        _idBulletsActive     = new List<ushort>();
        _bulletsPrePosition  = new Vector3[_nbBullets];
        _bulletsActPosition  = new Vector3[_nbBullets];

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

                int nbIter = PositionComparator(Diff, idActive, 4);
                if (nbIter != -1)
                {
                    print(idActive);
                    print(nbIter);
                }

            }
            yield return new WaitForSeconds(0.1f);
        }
    }
     

    int PositionComparator(Vector3 Differential, ushort idActive, int iter)
    {
        int result = -1;
        Vector3 bulletPostPosition;

        // Radius IA = 0.4u
        // Radius Projectile = 0.2u

        for (int i = 1; i <= iter; i++)
        {
            bulletPostPosition = _bulletsActPosition[idActive] + Differential * i;
            print(_bulletsActPosition[idActive] + " : " + bulletPostPosition);

            float CompX = _actualPosition.position.x - bulletPostPosition.x;
            float CompY = _actualPosition.position.y - bulletPostPosition.y;
            print(CompX + " : " + CompY);
            if ((CompX >= -0.6 && CompX <= 0.6)&&(CompY >= -0.6 && CompY <= 0.6))
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
