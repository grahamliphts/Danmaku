using UnityEngine;
using System.Collections;

public class Bullet_Pull : MonoBehaviour
{

    public GameObject Bullet;
    private GameObject[] m_Bullets;
    private float m_timmer;
    private BulletPath[] m_Bullets_Script;
    public int m_Bullet_Amount;
    // Use this for initialization
    void Start()
    {
        // m_Bullet_Amount = 10;
        m_Bullets = new GameObject[m_Bullet_Amount];
        m_Bullets_Script = new BulletPath[m_Bullet_Amount];
        Debug.Log(m_Bullet_Amount);
        for (int j = 0; j < m_Bullet_Amount; j++)
        {
            //Bullets[j] = Instantiate(Bullet);
            m_Bullets[j] = Instantiate(Bullet, transform.position, Quaternion.identity) as GameObject;
            m_Bullets[j].transform.parent = transform;
            m_Bullets_Script[j] = m_Bullets[j].GetComponent<BulletPath>();
            Debug.Log("Instantiating");
        }
        for (int i = 0; i < m_Bullets.Length; i++)
        {
            m_Bullets[i].SetActive(true);
        }

        m_timmer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - m_timmer > 0.5)
            for (int i = 0; i < m_Bullets.Length; i++)
            {
                m_Bullets[i].SetActive(false);
                m_Bullets[i].transform.position = new Vector3(0, 0, 0);
                m_Bullets[i].SetActive(true);
                m_Bullets_Script[i].reset();
                m_timmer = Time.time;
            }
    }
}
