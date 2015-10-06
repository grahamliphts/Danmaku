using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    [SerializeField]
    public BulletFire bossControl;
	
	void Update () 
    {  
        if (Input.GetButtonDown("CircleSpin"))
            bossControl.changeMode(1);
        if (Input.GetButtonDown("DoubleSpin"))
            bossControl.changeMode(2);
        if (Input.GetButtonDown("CrossFire"))
            bossControl.changeMode(3);
        if (Input.GetButtonDown("StarFire"))
            bossControl.changeMode(4);
        if (Input.GetButtonDown("TwinFire"))
            bossControl.changeMode(5);
        if (Input.GetButtonDown("MouseFire"))
            bossControl.changeMode(6);
    }
}
 