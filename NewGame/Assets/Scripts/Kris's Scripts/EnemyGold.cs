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

    public void CostPerUnit(DecisionMake Decide, bool Bollean,GameObject enemy )
    {
        enemyGold = enemyGold - 4;
        Decide.Decide(enemy);
    }
    public void CostPerRandomUnit(GameObject RandomAlly,Vector2 randomSpawn)
    {
        enemyGold = enemyGold -4 ;
        EnemyValues getIsPteam = RandomAlly.GetComponent<EnemyValues>();
        getIsPteam.PTeam = false;
        Debug.Log(getIsPteam.PTeam);
        Instantiate(RandomAlly, randomSpawn, Quaternion.identity);
    }
}
