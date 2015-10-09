using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{

    private bool _pause;

    void Start()
    {
        _pause = false;
    }

	public void StartPause()
    {
        if(!_pause)
        {
            _pause = true;
            Time.timeScale = 0;
        }
        else if(_pause)
        {
            _pause = false;
            Time.timeScale = 1;
        }
    }
}
