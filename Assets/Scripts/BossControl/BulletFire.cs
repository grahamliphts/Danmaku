using UnityEngine;
using System.Collections;

public class BulletFire : MonoBehaviour
{
    public GameObject[] bullets;
    private float m_timmer;
    private BulletPath[] m_Bullets_Script;
    private int m_direction;


    void Start()
    {
        // m_Bullet_Amount = 10;
        m_Bullets_Script = new BulletPath[bullets.Length];
        for (int j = 0; j < bullets.Length; j++)
        {
            bullets[j].SetActive(false);
            m_Bullets_Script[j] = bullets[j].GetComponent<BulletPath>();
            //Debug.Log("Instantiating");
        }

        m_timmer = Time.time;
    }

    void Update()
    {

        if (Time.time - m_timmer > 0.5)
        {
            //StartCoroutine(circleSpin(0,bullets.Length));
            //StartCoroutine(doubleSpin(bullets.Length,0));
            //StartCoroutine(crossFire(bullets.Length,0));
            //StartCoroutine(starFire(bullets.Length, 0));
            //StartCoroutine(twinFire(bullets.Length, 0));
            m_timmer = Time.time;
        }
    }

    IEnumerator circleSpin(int max,int spawn = 0)
    {
        while (true)
        {
            if (spawn >= bullets.Length)
                spawn = 0;
            if (bullets[spawn].activeSelf == false)
            {
                bullets[spawn].SetActive(false);
                bullets[spawn].transform.position = new Vector3(0, 0, 0);
                bullets[spawn].SetActive(true);
                m_Bullets_Script[spawn].play(m_direction);


                spawn++;
                if (m_direction < 360)
                    m_direction += 5;
                else
                    m_direction = 0;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator doubleSpin(int max,int spawn = 0)
    {
        while (true)
        {
            if (spawn >= max)
                spawn = 0;
            if (bullets[spawn].activeSelf == false)
            {
                if (spawn % 2 == 0)
                {
                    bullets[spawn].SetActive(false);
                    bullets[spawn].transform.position = new Vector3(0, 0, 0);
                    bullets[spawn].SetActive(true);
                    m_Bullets_Script[spawn].play(m_direction);


                    spawn++;
                }
                else
                {

                    bullets[spawn].SetActive(false);
                    bullets[spawn].transform.position = new Vector3(0, 0, 0);
                    bullets[spawn].SetActive(true);
                    m_Bullets_Script[spawn].play(-m_direction);
                    spawn++;
                }
                if (m_direction < 360)
                    m_direction += 5;
                else
                    m_direction = 0;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator crossFire(int max, int spawn = 0)
    {
        while (true)
        {
            if (spawn >= max)
                spawn = 0;
            if (bullets[spawn].activeSelf == false)
            {
                if (spawn % 2 == 0)
                {
                    bullets[spawn].SetActive(false);
                    bullets[spawn].transform.position = new Vector3(0, 0, 0);
                    bullets[spawn].SetActive(true);
                    m_Bullets_Script[spawn].play(m_direction);


                    spawn++;
                }
                else
                {

                    bullets[spawn].SetActive(false);
                    bullets[spawn].transform.position = new Vector3(0, 0, 0);
                    bullets[spawn].SetActive(true);
                    m_Bullets_Script[spawn].play(-m_direction);
                    spawn++;
                }
                if (m_direction < 360)
                    m_direction += 90;
                else
                    m_direction = 0;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator starFire(int max, int spawn = 0)
    {
        while (true)
        {
            if (spawn >= max)
                spawn = 0;
            if (bullets[spawn].activeSelf == false)
            {
                if (spawn % 2 == 0)
                {
                    bullets[spawn].SetActive(false);
                    bullets[spawn].transform.position = new Vector3(0, 0, 0);
                    bullets[spawn].SetActive(true);
                    m_Bullets_Script[spawn].play(m_direction);


                    spawn++;
                }
                else
                {

                    bullets[spawn].SetActive(false);
                    bullets[spawn].transform.position = new Vector3(0, 0, 0);
                    bullets[spawn].SetActive(true);
                    m_Bullets_Script[spawn].play(-m_direction);
                    spawn++;
                }
                if (m_direction < 360)
                    m_direction += 45;
                else
                    m_direction = 0;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator twinFire(int max, int spawn = 0)
    {
        while (true)
        {
            if (spawn >= max)
                spawn = 0;
            if (bullets[spawn].activeSelf == false)
            {
                if (spawn % 2 == 0)
                {
                    bullets[spawn].SetActive(false);
                    bullets[spawn].transform.position = new Vector3(0, 0, 0);
                    bullets[spawn].SetActive(true);
                    m_Bullets_Script[spawn].play(m_direction);


                    spawn++;
                }
                else
                {

                    bullets[spawn].SetActive(false);
                    bullets[spawn].transform.position = new Vector3(0, 0, 0);
                    bullets[spawn].SetActive(true);
                    m_Bullets_Script[spawn].play(-m_direction);
                    spawn++;
                }
                if (m_direction < 360)
                    m_direction += 50;
                else
                    m_direction = 0;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }


}
