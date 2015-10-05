using UnityEngine;
using System.Collections;

public class BulletFire : MonoBehaviour
{
    public GameObject[] bullets;
    private float m_timmer;
    private BulletPath[] m_Bullets_Script;
    private int m_direction;
    private uint m_mode = 1;


    void Start()
    {
        // m_Bullet_Amount = 10;
        m_Bullets_Script = new BulletPath[bullets.Length];
        for (int j = 0; j < bullets.Length; j++)
        {
            bullets[j].SetActive(false);
            m_Bullets_Script[j] = bullets[j].GetComponent<BulletPath>();
            //Debug.Log("Instantiating");
            launchCoroutine();
        }

        m_timmer = Time.time;
    }

    /*void Update()
    {

        if (Time.time - m_timmer > 0.5)
        {
            launchCoroutine();
            m_timmer = Time.time;
        }
    }*/

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

    public void changeMode(uint mode)
    {
        switch (mode)
        {
            case 1:
                stopCurrentCoroutine();
                m_mode = 1;
                launchCoroutine();
                break;
            case 2:
                stopCurrentCoroutine();
                m_mode = 2;
                launchCoroutine();
                break;
            case 3:
                stopCurrentCoroutine();
                m_mode = 3;
                launchCoroutine();
                break;
            case 4:
                stopCurrentCoroutine();
                m_mode = 4;
                launchCoroutine();
                break;
            case 5:
                stopCurrentCoroutine();
                m_mode = 5;
                launchCoroutine();
                break;
            default:
                stopCurrentCoroutine();
                m_mode = 1;
                launchCoroutine();
                break;
        }
    }

    void launchCoroutine()
    {
        switch (m_mode)
        {
            case 1:
                StartCoroutine(circleSpin(bullets.Length));
                break;
            case 2:
                StartCoroutine(doubleSpin(bullets.Length));
                break;
            case 3:
                StartCoroutine(crossFire(bullets.Length));
                break;
            case 4:
                StartCoroutine(starFire(bullets.Length));
                break;
            case 5:
                StartCoroutine(twinFire(bullets.Length));
                break;
            default:
                StartCoroutine(circleSpin(bullets.Length,0));
                break;
        }
    }

    void stopCurrentCoroutine()
    {
        switch (m_mode)
        {
            case 1:
                StopCoroutine(circleSpin(bullets.Length));
                break;
            case 2:
                StopCoroutine(doubleSpin(bullets.Length));
                break;
            case 3:
                StopCoroutine(crossFire(bullets.Length));
                break;
            case 4:
                StopCoroutine(starFire(bullets.Length));
                break;
            case 5:
                StopCoroutine(twinFire(bullets.Length));
                break;
            default:
                StopCoroutine(circleSpin(bullets.Length, 0));
                break;
        }
    }

}
