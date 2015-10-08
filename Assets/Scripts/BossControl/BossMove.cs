using UnityEngine;
using System.Collections;

public class BossMove : MonoBehaviour 
{
    private Vector3 m_CamPos;
    public bool goRight;
    public bool IDLE = false;

    public Transform wave1;
    public Transform wave2;

	// Use this for initialization
	void Start () 
    {
        m_CamPos = Camera.main.WorldToViewportPoint(transform.position);
        goRight = true;
    }
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if (!IDLE)
        {
            m_CamPos = Camera.main.WorldToViewportPoint(transform.position);
            if (m_CamPos.x <= 0.71 && goRight)
            {
                transform.Translate(new Vector3(1, 0, 0) * 0.05F);
                if(wave1.gameObject.activeSelf)
                    wave1.Translate(new Vector3(1, 0, 0) * 0.05F);
                if (wave2.gameObject.activeSelf)
                    wave2.Translate(new Vector3(1, 0, 0) * 0.05F);
            }
               
            else if (m_CamPos.x >= 0.3)
            {
                transform.Translate(new Vector3(-1, 0, 0) * 0.05F);
                if (wave1.gameObject.activeSelf)
                    wave1.Translate(new Vector3(-1, 0, 0) * 0.05F);
                if (wave2.gameObject.activeSelf)
                    wave2.Translate(new Vector3(-1, 0, 0) * 0.05F);
                goRight = false;
            }
            else
                goRight = true;
            //else if( || m_CamPos.y >= 1 || m_CamPos.x <= 0 || m_CamPos.y <= 0)
            Debug.DrawRay(transform.position + new Vector3(1, 0, 0), Vector3.down * 10);
            Debug.DrawRay(transform.position + new Vector3(-1, 0, 0), Vector3.down * 10);
        }
	}
}
