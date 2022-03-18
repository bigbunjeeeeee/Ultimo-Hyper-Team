using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gold : MonoBehaviour
{
    private int gold = 0;
    public Text goldText;
   
    private float eachSecondGiveOneGold = 0;
    // Start is called before the first frame update
    void Start()
    {

        goldText.text = "Gold: " + gold;
    }

    // Update is called once per frame
    void Update()
    {
        eachSecondGiveOneGold += Time.deltaTime;
        if (eachSecondGiveOneGold > 1.0f)
        {
            gold = gold + 1;
            goldText.text = "Gold: " + gold ;

            eachSecondGiveOneGold = 0;
        }

       

       
       
    }
}
