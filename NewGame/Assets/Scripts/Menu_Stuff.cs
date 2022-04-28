using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Stuff : MonoBehaviour
{
    bool purchased;

    // Start is called before the first frame update

    // Update is called once per frame
    void Start()
    {
        purchased = false;
    }

    public void startGame()
    { 
        SceneManager.LoadScene("Loading");
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void PurchaseWall()
    {
        if (purchased == false)
        {
            poweups.wall = true;
            purchased = true;
        }
    }
    public void PurchaseBomb()
    {
        if (purchased == false)
        {
            poweups.bomb = true;
            purchased = true;
        }
    }

    public void PurchasePup()
    {
        if (purchased == false)
        {
            poweups.Pup = true;
            purchased = true;
        }
    }
}
