using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class build_Giant : MonoBehaviour
{
    public GameObject Controller_blueprint;
    private Gold playerGold;
    public GameObject PlayerGoldObj;
    void Start()
    {
        PlayerGoldObj = GameObject.Find("PlayerGold");

        playerGold = PlayerGoldObj.GetComponent<Gold>();

    }
    public void spawn_blueprint()
    {
        playerGold.TakeGoldFromThePlayer(Controller_blueprint);
    }
}
