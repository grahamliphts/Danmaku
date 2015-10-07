using UnityEngine;
using System.Collections;

public class BossMove : MonoBehaviour 
{
    private Vector3 m_CamPos;
    public bool goRight;

	// Use this for initialization
	void Start () 
    {
        m_CamPos = Camera.main.WorldToViewportPoint(transform.position);
        goRight = true;
    }
	
	// Update is called once per frame
	void Update () 
    {
        m_CamPos = Camera.main.WorldToViewportPoint(transform.position);
        if (m_CamPos.x <=0.71 && goRight)
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime);
        else if (m_CamPos.x >= 0.3)
        {
            transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime);
            goRight = false;
        }
        else
            goRight = true;
        //else if( || m_CamPos.y >= 1 || m_CamPos.x <= 0 || m_CamPos.y <= 0)
	}
}
