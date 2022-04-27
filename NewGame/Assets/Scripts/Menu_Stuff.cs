using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Stuff : MonoBehaviour
{

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
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
        poweups.wall = true;
    }
    public void PurchaseBomb()
    {
        poweups.bomb = true;
    }

    public void PurchasePup()
    {
        poweups.Pup = true;
    }
}
