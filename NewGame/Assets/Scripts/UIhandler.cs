using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIhandler : MonoBehaviour
{

    public GameObject wallUI;
    public GameObject bombUI;
    public GameObject PupUI;


    // Start is called before the first frame update
    void Start()
    {
        loadUI();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void loadUI()
    {
        if (poweups.wall == true)
        {
            wallUI.gameObject.SetActive(true);
        }
        else
        {
            wallUI.gameObject.SetActive(false);
        }

        if (poweups.bomb == true)
        {
            bombUI.gameObject.SetActive(true);
        }
        else
        {
            bombUI.gameObject.SetActive(false);
        }

        if (poweups.Pup == true)
        {
            PupUI.gameObject.SetActive(true);
        }
        else
        {
            PupUI.gameObject.SetActive(false);
        }
    }

}
