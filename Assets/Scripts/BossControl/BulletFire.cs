using UnityEngine;
using System.Collections;

public class BulletFire : MonoBehaviour
{
    public GameObject[] bullets;
    private BulletPath[] m_Bullets_Script;
    private int m_direction;

    void Start()
    {
        m_Bullets_Script = new BulletPath[bullets.Length];
        for (ushort j = 0; j < bullets.Length; j++)
        {
            bullets[j].SetActive(false);
            m_Bullets_Script[j] = bullets[j].GetComponent<BulletPath>();
            m_Bullets_Script[j].id = j;
        }
        changeMode(1);
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

    public void changeMode(uint mode)
    {
        StopAllCoroutines();
        switch (mode)
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
}
