using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gold : MonoBehaviour
{
    private int gold = 0;
    public Text goldText;
    private Text enemyText;
    private int enemyGold = 0;
    private float eachSecondGiveOneGold = 0;
    // Start is called before the first frame update
    void Start()
    {

        goldText.text = "Gold: " + gold + "\nEnemy Gold: " +enemyGold;
    }

    // Update is called once per frame
    void Update()
    {
        eachSecondGiveOneGold += Time.deltaTime;
        if (eachSecondGiveOneGold > 1.0f)
        {
            gold = gold + 1;
            enemyGold = enemyGold + 1;
            goldText.text = "Gold: " + gold + "\nEnemy Gold: " + enemyGold;//+ "\nEnemy Gold: " + enemyGold;

            eachSecondGiveOneGold = 0;
        }

        Debug.Log(eachSecondGiveOneGold);

       
       
    }
}
