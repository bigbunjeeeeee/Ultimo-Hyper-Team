using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class build : MonoBehaviour
{
    public GameObject Controller_blueprint;

    public void spawn_blueprint()
    {
        Instantiate(Controller_blueprint);
    }
}
