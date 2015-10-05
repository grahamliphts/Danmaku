using UnityEngine;
using System.Collections;

public class BulletPath : MonoBehaviour
{

    private int m_direction;
    //private Vector3 m_position;
    public float speed = 10;


    void Start()
    {
        reset();
    }

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    public void reset()
    {
        m_direction = Random.Range(0, 360);
        //m_position = transform.position;
        transform.rotation = Quaternion.AngleAxis(m_direction, new Vector3(0, 0, 1));
    }

}
