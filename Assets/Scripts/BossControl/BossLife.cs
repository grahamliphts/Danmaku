using UnityEngine;
using System.Collections;

public class BossLife : MonoBehaviour
{

    [SerializeField]
    private float _life;
    [SerializeField]
    private int _damage;

    void Start()
    {
        _life = 100;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //does not work now // create a taf for players shoot
        if (col.gameObject.tag == "projPlayer")
        {
            if (_life > 0)
                _life = _life - _damage;
        }
       
    }

    public float GetLife()
    {
        return _life;
    }
}
