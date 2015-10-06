using UnityEngine;
using System.Collections;

public class BulletPath : Entity
{
    private int m_direction;
    public int speed = 1;
    public ushort id = 0;


    void Start()
    {
        m_direction = Random.Range(0, 360);
        transform.rotation = Quaternion.AngleAxis(m_direction, new Vector3(0, 0, 1));
        gameObject.SetActive(false);
    }

    void Update()
    {
      /*  Debug.Log("Before : " + transform.position);
        transform.Translate(Vector3.right * Time.deltaTime * speed);
        Debug.Log("After : " + transform.position);
        Debug.Log("Vector right" + Vector3.right);*/
        var pos = Camera.main.WorldToViewportPoint(transform.position);
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

    public void play(int direction)
    {
      /*  m_direction = Random.Range(0, 360);
        gameObject.SetActive(true);
        transform.position = new Vector3(0, 3, 0);
        transform.rotation = Quaternion.AngleAxis(direction, new Vector3(0, 0, 1));*/
    }

}
