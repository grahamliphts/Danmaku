using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    public GameObject[] projPlayer;
    private PathPlayerShoot[] projPlayerScript;
    private int _max;

    private float _speed;

    [SerializeField]
    private Pool _pool;

    public float fireRate;
    private float _nextFire;
    int _count;
    bool _shooting;

    void Start()
    {
        projPlayer = _pool.items;
        _max = projPlayer.Length;
        projPlayerScript = new PathPlayerShoot[_max];
        for (ushort j = 0; j < _max; j++)
        {
            projPlayerScript[j] = projPlayer[j].GetComponent<PathPlayerShoot>();
        }

        _nextFire = 0f;
        _count = 0;
    }

	void Update ()
    {
        if (Time.time > _nextFire)
        {
            _nextFire = Time.time + fireRate;
            if (_count >= _max)
                _count = 0;
            if (projPlayer[_count].activeSelf == false)
            {
                //Debug.Log(projPlayer[_count]);
                projPlayerScript[_count].play();
                _count++;
            }
        }
    }
}
