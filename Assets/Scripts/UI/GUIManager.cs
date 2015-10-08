using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour
{
    public void LoadGame()
    {
        Application.LoadLevel("MainSceneAlone");
    }

    public void LoadGameWithIA()
    {
        Application.LoadLevel("MainScene");
    }

    public void LoadBossEditor()
    {
        Application.LoadLevel("EditorBossScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
