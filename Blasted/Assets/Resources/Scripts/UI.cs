using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text codeUI;

    public Canvas canvas1;
    public Canvas canvas2;

    public Camera camJ1;
    public Camera camJ2;

    // Start is called before the first frame update
    void Start()
    {
        codeUI.text = "Code game : " + NetworkManager.Instance.code;

        if(NetworkManager.Instance.hosting)
        {
            canvas1.worldCamera = camJ2;
            canvas2.worldCamera = camJ2;
        }
        else
        {
            canvas1.worldCamera = camJ1;
            canvas2.worldCamera = camJ1;
        }
    }

}
