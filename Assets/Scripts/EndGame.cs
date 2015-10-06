using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{

    public RawImage imageClose;
    public Text closeInfo;

    public BossLife lifeBoss;

    // Update is called once per frame
    void Update ()
    {
	    if (lifeBoss.GetLife() <= 0)
            StartCoroutine("CloseParty", "You win");
    }

    IEnumerator CloseParty(string text)
    {
        imageClose.gameObject.SetActive(true);
        closeInfo.text = text;

        yield return new WaitForSeconds(3);
        Application.LoadLevel("MenuScene");
    }

}
