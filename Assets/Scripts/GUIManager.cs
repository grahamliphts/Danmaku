using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour
{

    public void LoadGame()
    {
        Debug.Log("LoadGame");
        Application.LoadLevel("MainScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
