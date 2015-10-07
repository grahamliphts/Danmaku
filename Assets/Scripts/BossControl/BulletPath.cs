using UnityEngine;
using System.Collections;

public class BulletPath : Entity
{
    private int m_direction;
    public float speed = 0.1f;
    public ushort id = 0;
    public GameObject Boss;
    public WorldState __WorldState;
    


    void Start()
    {
       // m_direction = Random.Range(0, 360);
        //transform.rotation = Quaternion.AngleAxis(m_direction, new Vector3(0, 0, 1));
        gameObject.SetActive(false);
    }

    void Update()
    {
        //  Debug.Log("Before : " + transform.position);
        //transform.Translate(Vector3.right * Time.deltaTime * speed);
       /* Vector3 differential = this.transform.position;

        Vector3 dirVector = Quaternion.AngleAxis(m_direction, new Vector3(0, 0, 1)) * Vector3.up;
        transform.Translate(dirVector * speed);

        differential = this.transform.position - differential;

        __WorldState.updateEntitie(transform.position, differential, transform.rotation, EID, gameObject.activeSelf, speed);
        var pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x >= 1 || pos.y >= 1 || pos.x <= 0 || pos.y <= 0.1)
            reset();*/
        //Debug.Log(EID);
    }

    public void reset()
    {
        //m_direction = Random.Range(0, 360);
        //transform.position = new Vector3(0, 3, 0);
        //transform.rotation = Quaternion.AngleAxis(m_direction, new Vector3(0, 0, 1));
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    public void play(int direction)
    {
        m_direction = direction;
        gameObject.SetActive(true);
        transform.position = Boss.transform.position;
        transform.rotation = Quaternion.AngleAxis(m_direction, new Vector3(0, 0, 1));
        Vector3 differential = this.transform.position;
        Vector3 dirVector = Quaternion.AngleAxis(m_direction, new Vector3(0, 0, 1)) * Vector3.up;
        differential = this.transform.position - differential;

        Vector3 _direction = new Vector3(transform.position.x + Mathf.Cos(m_direction * Mathf.PI / 180.0f), transform.position.y + Mathf.Sin(m_direction * Mathf.PI / 180.0f), 0);
        Vector3 dir = _direction - transform.position;

        Debug.DrawRay(transform.position, dir, Color.red);

        __WorldState.updateEntitie(transform.position, differential, transform.rotation, EID, gameObject.activeSelf, speed);
        StartCoroutine(bulletMove());
    }

    public void setEID( int eid)
    {
        EID = eid;
    }

    IEnumerator bulletMove()
    {
        while (true)
        {
            Vector3 differential = this.transform.position;
            Vector3 dirVector = Quaternion.AngleAxis(m_direction, new Vector3(0, 0, 1)) * Vector3.up;
            transform.Translate(dirVector * speed);
            differential = this.transform.position - differential;
            __WorldState.updateEntitie(transform.position, differential, transform.rotation, EID, gameObject.activeSelf, speed);
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
            if (pos.x >= 1 || pos.y >= 1 || pos.x <= 0 || pos.y <= 0.1)
                reset();
            yield return new WaitForSeconds(0.01f);
        }
    }

}
