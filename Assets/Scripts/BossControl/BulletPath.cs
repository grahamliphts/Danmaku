using UnityEngine;
using System.Collections;

public class BulletPath : Entity
{
    private int m_direction;
    public float speed = 0.1f;
    public ushort id = 0;
    public GameObject Boss;
    public WorldState __WorldState;

    private bool _manually;
    private Vector3 _dirVector;
    private Vector3 _dirManual;
    void Start()
    {
        speed = speed * Random.Range(0.5F, 1.5F);
    }

    void FixedUpdate()
    {
        Vector3 differential = this.transform.position;
        if (!_manually)
        {
            _dirVector = Quaternion.AngleAxis(m_direction, new Vector3(0, 0, 1)) * Vector3.up;
            transform.Translate(_dirVector * speed);
        }
        else
        {
            transform.Translate(_dirManual * speed);
        }
        differential = this.transform.position - differential;

        __WorldState.updateEntitie(transform.position, differential, transform.rotation, EID, gameObject.activeSelf, speed);

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x >= 1 || pos.y >= 1 || pos.x <= 0 || pos.y <= 0.1)
            reset();
    }


    public void playManually(Vector3 direction)
    {
        gameObject.SetActive(true);
        transform.position = Boss.transform.position;
        transform.rotation = Quaternion.identity;
        _dirManual = direction;
        _manually = true;
    }

    public void reset()
    {
        gameObject.SetActive(false);
    }

    public void play(int direction)
    {
        _manually = false;
        m_direction = direction;

        gameObject.SetActive(true);
        transform.position = Boss.transform.position;
        transform.rotation = Quaternion.AngleAxis(m_direction, new Vector3(0, 0, 1));
        
        Vector3 differential = Vector3.zero;
        __WorldState.updateEntitie(transform.position, differential, transform.rotation, EID, gameObject.activeSelf, speed);
    }

    public void setEID( int eid)
    {
        EID = eid;
    }

}
