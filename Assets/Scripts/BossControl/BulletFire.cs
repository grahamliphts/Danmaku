using UnityEngine;
using System.Collections;

public class BulletFire : MonoBehaviour
{
    public GameObject[] bullets;
    private float m_timmer;
    private BulletPath[] m_Bullets_Script;

    
    void Start()
    {
        // m_Bullet_Amount = 10;
        m_Bullets_Script = new BulletPath[bullets.Length];
        for (int j = 0; j < bullets.Length; j++)
        {
            bullets[j].SetActive(false);
            m_Bullets_Script[j] = bullets[j].GetComponent<BulletPath>();
            Debug.Log("Instantiating");
        }
        
        m_timmer = Time.time;
    }

    void Update()
    {

        if (Time.time - m_timmer > 0.5)
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                if(bullets[i].activeSelf == false)
                    bullets[i].SetActive(true);
                m_timmer = Time.time;
            }
        }
    }

}
