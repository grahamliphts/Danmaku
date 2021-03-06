﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{

    public RawImage imageClose;
    public Text closeInfo;

    public BossLife lifeBoss;
    public PlayerLife lifePlayer;

    public GameObject player;
    public GameObject boss;
    public GameObject playerShoot;

    // Update is called once per frame
    void Update ()
    {
	    if (lifeBoss.GetLife() <= 0)
            StartCoroutine("CloseParty", "You win");

        else if(lifePlayer.GetLife() <= 0)
            StartCoroutine("CloseParty", "Game over");
    }

    IEnumerator CloseParty(string text)
    {
        player.SetActive(false);
        boss.SetActive(false);
        playerShoot.SetActive(false);
        imageClose.gameObject.SetActive(true);
        closeInfo.text = text;

        yield return new WaitForSeconds(3);
        Application.LoadLevel("MenuScene");
    }

}
