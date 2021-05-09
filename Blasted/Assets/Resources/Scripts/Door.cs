using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Trigger[] triggers;


    // Update is called once per frame
    void Update()
    {
        bool open = true;
        foreach(Trigger trigger in triggers)
        {
            if(!trigger.isTrigger)
            {
                open = false;
                break;
            }
        }
        gameObject.SetActive(!open);
    }
}
