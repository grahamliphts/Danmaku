using UnityEngine;
using System.Collections;

public class PlayerLife : MonoBehaviour {

    private float _life;
    private int _damage;

    void Start()
    {
        _life = 1;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("trigger");
        if (col.gameObject.tag == "projEnemy")
        {
            //Debug.Log("trigger2");
            if (_life > 0)
                _life = _life - 1;
        }

    }

    public float GetLife()
    {
        return _life;
    }
}
