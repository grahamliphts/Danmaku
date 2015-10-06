// --------------------
// Script Grid Update
// Projet: Damnaku
// --------------------

using UnityEngine;
using System.Collections;

public class GridUpdate : MonoBehaviour 
{
    public bool active = false;
    private ushort nbObject = 0;
    
    void OnTriggerEnter(Collider other)
    {
        nbObject++;
        active = true;
    }

    void OnTriggerExit(Collider other)
    {
        nbObject--;
        if(nbObject <= 0)
            active = false;
    }
	
}
