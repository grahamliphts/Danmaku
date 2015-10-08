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
    public GameObject Boss;

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
               // Debug.Log("Click");
                if (m_ManualLaunched < bullets.Length - 1)
                    m_ManualLaunched++;
                else
                    m_ManualLaunched = 0;
               

                var pos = Input.mousePosition;
                pos.z = Boss.transform.position.z - Camera.main.transform.position.z;

                pos = Camera.main.ScreenToWorldPoint(pos);

                Vector3 diff = pos - Boss.transform.position;
                diff.Normalize();

                float angleRad = Mathf.Atan2(diff.y, diff.x);
                Vector3 _direction = new Vector3(Boss.transform.position.x + Mathf.Cos(angleRad), Boss.transform.position.y + Mathf.Sin(angleRad), 0);
                Vector3 dir = Boss.transform.position - _direction;
                Debug.DrawRay(Boss.transform.position, -dir.normalized * 5, Color.red);
                m_Bullets_Script[m_ManualLaunched].playManually(-dir);
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
        StartCoroutine(explodeFire(bullets.Length,0.5F));
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

    IEnumerator explodeFire(int max, float Rate, int spawn = 0)
    {

        //bool isright = true;
        while (spawn < max - 1 && bullets[spawn].activeSelf)
        {
            spawn++;
        }
        while (true)
        {

            if (spawn >= max)
                spawn = 0;

               Debug.Log("Explode");
            if (bullets[spawn].activeSelf == false)
            {
                for (int i = 45; i < 135; i += 5)
                {
                    if (spawn >= max)
                        spawn = 0;
                    m_Bullets_Script[spawn].play(i);

                    spawn++;
                }
            }
            yield return new WaitForSeconds(Rate);
        }
    }
}
