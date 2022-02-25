using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingUnits : MonoBehaviour
{

    public GameObject placeableArea;
    public GameObject prefab;
    bool isHolding = false;
    GameObject heldUnit;

    // Update is called once per frame
    void Update()
    {
        if (isHolding)
        {
            heldUnit.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -2.0f);
        }
    }

    public void OnMouseDown()
    {
        heldUnit = Instantiate(prefab, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -2.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
        isHolding = true;
    }

    void OnMouseUp()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, LayerMask.GetMask("UI"));
        if (hit.collider != null)
        {
            if (hit.collider.tag == "placingArea")
            {
                heldUnit.GetComponent<moveTest>().placed = true;
            }
            else
            {
                Destroy(heldUnit);
            }
        }
        else
        {
            Destroy(heldUnit);
        }

        isHolding = false;
        heldUnit = null;
    }
}
