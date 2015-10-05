using UnityEngine;
using System.Collections;

public class BulletPath : MonoBehaviour
{

    private int m_direction;
    //private Vector3 m_position;
    public float speed = 10;


    void Start()
    {
        m_direction = Random.Range(0, 360);
        //m_position = transform.position;
        transform.rotation = Quaternion.AngleAxis(m_direction, new Vector3(0, 0, 1));
        gameObject.SetActive(false);
    }

    void Update()
    {
        gameObject.transform.Translate(Vector3.right * Time.deltaTime * speed);
        var pos = Camera.main.WorldToViewportPoint(transform.position);
        //Debug.Log(pos.x);
        if (pos.x >= 1 || pos.y >= 1 || pos.x <= 0 || pos.y <= 0)
            reset();
    }

    public void reset()
    {
        m_direction = Random.Range(0, 360);
        transform.position = new Vector3(0, 3, 0);
        transform.rotation = Quaternion.AngleAxis(m_direction, new Vector3(0, 0, 1));
        gameObject.SetActive(false);
    }

    public void stop()
    {
        gameObject.SetActive(false);
        transform.position = new Vector3(0, 3, 0);
    }

    public void play(int direction)
    {
        m_direction = Random.Range(0, 360);
        gameObject.SetActive(true);
        transform.position = new Vector3(0, 3, 0);
        transform.rotation = Quaternion.AngleAxis(direction, new Vector3(0, 0, 1));
        
    }

}
