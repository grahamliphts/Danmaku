using UnityEngine;
using System.Collections;

public class BulletFire : MonoBehaviour
{
    public GameObject[] bullets;
    private BulletPath[] m_Bullets_Script;
    private int m_direction;
    enum Shoot { Double, Cross, Star, Twin };
    private uint m_ActiveMode;
    [SerializeField]
    private Pool _pool;
    private int m_ManualLaunched;

    void Start()
    {
        bullets = _pool.items;
        m_Bullets_Script = new BulletPath[bullets.Length];
        for (ushort j = 0; j < bullets.Length; j++)
        {
            m_Bullets_Script[j] = bullets[j].GetComponent<BulletPath>();
        }
        changeMode(1);
        m_ManualLaunched = 0;
    }

    void Update() // For Active Firing
    {
        if (m_ActiveMode == 6)
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Click");
                if (m_ManualLaunched < bullets.Length - 1)
                    m_ManualLaunched++;
                else
                    m_ManualLaunched = 0;
                m_Bullets_Script[m_ManualLaunched].play(270);
            }



    }

    IEnumerator circleSpin(int max, int spawn = 0)
    {
        while (spawn < max - 1 && bullets[spawn].activeSelf)
        {
            spawn++;
        }
        bool isright = true;
        while (true)
        {
            if (spawn >= bullets.Length)
                spawn = 0;
            if (bullets[spawn].activeSelf == false)
            {
                bullets[spawn].transform.position = new Vector3(0, 0, 0);
                bullets[spawn].SetActive(true);
                m_Bullets_Script[spawn].play(m_direction);


                spawn++;
                if (m_direction < 320 && isright)
                {
                    m_direction += 5;
                }
                else
                {
                    isright = false;
                    m_direction -= 5;
                    if (m_direction < 220)
                        isright = true;
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator patternFire(int max, Shoot shoot, int spawn = 0)
    {
        while (spawn < max - 1 && bullets[spawn].activeSelf)
        {
            spawn++;
        }

        while (true)
        {
            if (spawn >= max)
                spawn = 0;

            if (bullets[spawn].activeSelf == false)
            {
                if (spawn % 2 == 0)
                {
                    bullets[spawn].transform.position = new Vector3(0, 0, 0);
                    bullets[spawn].SetActive(true);
                    m_Bullets_Script[spawn].play(m_direction);


                    spawn++;
                }
                else
                {
                    bullets[spawn].transform.position = new Vector3(0, 0, 0);
                    bullets[spawn].SetActive(true);
                    m_Bullets_Script[spawn].play(-m_direction);
                    spawn++;
                }
                if (m_direction < 360)
                {
                    if (shoot == Shoot.Double)
                        m_direction += 5;
                    else if (shoot == Shoot.Cross)
                        m_direction += 90;
                    else if (shoot == Shoot.Star)
                        m_direction += 45;
                    else if (shoot == Shoot.Twin)
                        m_direction += 50;
                }
                else
                    m_direction = 0;
            }
            yield return new WaitForSeconds(0.01f);
        }
    }


    IEnumerator doubleSpin(int max, Shoot shoot, int spawn = 0)
    {

        bool isright = true;
        while (spawn < max - 1 && bullets[spawn].activeSelf)
        {
            spawn++;
        }
        while (true)
        {

            if (spawn >= max)
                spawn = 0;
            

            if (bullets[spawn].activeSelf == false)
            {
                //Debug.Log("Fire Changed");
                if (spawn % 2 == 0)
                {
                    bullets[spawn].transform.position = new Vector3(0, 0, 0);
                    bullets[spawn].SetActive(true);
                    m_Bullets_Script[spawn].play(m_direction);


                    spawn++;
                }
                else
                {
                    bullets[spawn].transform.position = new Vector3(0, 0, 0);
                    bullets[spawn].SetActive(true);
                    m_Bullets_Script[spawn].play(m_direction + 30);
                    spawn++;
                }
                if (m_direction < 360 && isright)
                {
                    m_direction += 5;
                }
                else
                {
                    isright = false;
                    m_direction -= 5;
                    if (m_direction < 160)
                        isright = true;
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void changeMode(uint mode)
    {
        //Debug.Log("Ask for Fire change");
        StopAllCoroutines();
        //Debug.Log("Old Fire Stoped");
        switch (mode)
        {
            case 1:
                StartCoroutine(circleSpin(bullets.Length));
                m_ActiveMode = mode;
                break;
            case 2:
                StartCoroutine(doubleSpin(bullets.Length, Shoot.Double));
                m_ActiveMode = mode;
                break;
            case 3:
                StartCoroutine(patternFire(bullets.Length, Shoot.Cross));
                m_ActiveMode = mode;
                break;
            case 4:
                StartCoroutine(patternFire(bullets.Length, Shoot.Star));
                m_ActiveMode = mode;
                break;
            case 5:
                StartCoroutine(patternFire(bullets.Length, Shoot.Twin));
                m_ActiveMode = mode;
                break;
            case 6:
                m_ActiveMode = mode;
                break;
            default:
                StartCoroutine(circleSpin(bullets.Length, 0));
                m_ActiveMode = mode;
                break;
        }
    }
}
