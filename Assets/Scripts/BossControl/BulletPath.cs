using UnityEngine;
using System.Collections;

public class BulletPath : Entity
{
    private int m_direction;
    public int speed = 1;
    public ushort id = 0;
    public GameObject Boss;
    public WorldState __WorldState;
    


    void Start()
    {
        m_direction = Random.Range(0, 360);
        transform.rotation = Quaternion.AngleAxis(m_direction, new Vector3(0, 0, 1));
        gameObject.SetActive(false);
    }

    void Update()
    {
        //  Debug.Log("Before : " + transform.position);
        //transform.Translate(Vector3.right * Time.deltaTime * speed);
        
        Vector3 dirVector = Quaternion.AngleAxis(m_direction, new Vector3(0, 0, 1)) * Vector3.up;
        transform.Translate(dirVector * Time.deltaTime * speed);

        __WorldState.updateEntitie(transform.position, dirVector, transform.rotation, EID, gameObject.activeSelf, speed);
        var pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x >= 1 || pos.y >= 1 || pos.x <= 0 || pos.y <= 0)
            reset();
        //Debug.Log(EID);
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
        m_direction = direction;
        gameObject.SetActive(true);
        transform.position = Boss.transform.position;
        transform.rotation = Quaternion.AngleAxis(m_direction, new Vector3(0, 0, 1));
        Vector3 dirVector = Quaternion.AngleAxis(m_direction, new Vector3(0, 0, 1)) * Vector3.up;

        Vector3 _direction = new Vector3(transform.position.x + Mathf.Cos(m_direction * Mathf.PI / 180.0f), transform.position.y + Mathf.Sin(m_direction * Mathf.PI / 180.0f), 0);
        Vector3 dir = _direction - transform.position;

        Debug.DrawRay(transform.position, dir, Color.red);

        __WorldState.updateEntitie(transform.position, dirVector, transform.rotation, EID, gameObject.activeSelf, speed);
    }

    public void setEID( int eid)
    {
        EID = eid;
    }

}
