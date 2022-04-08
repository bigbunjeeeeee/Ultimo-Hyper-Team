using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyGold : MonoBehaviour
{
    public Text enemyText;
    public int enemyGold = 0;
    private float eachSecondGiveOneGold = 0;
    // Start is called before the first frame update
    void Start()
    {
        enemyText.text = "Gold: " + enemyGold;
    }

    // Update is called once per frame
    void Update()
    {
        eachSecondGiveOneGold += Time.deltaTime;
        if (eachSecondGiveOneGold > 1.0f)
        {
            enemyGold = enemyGold + 1;
            enemyText.text = "Gold: " + enemyGold;
            eachSecondGiveOneGold = 0;
           
        }
    }

    public void CostPerUnit(DecisionMake Decide, bool Bollean,Queue<GameObject>enemies )
    {
        enemyGold = enemyGold - 4;
        Decide.Decide(enemies);
    }
}
