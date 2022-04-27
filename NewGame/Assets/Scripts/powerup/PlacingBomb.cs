using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacingBomb : MonoBehaviour
{
    public GameObject placeableArea;
    public GameObject prefab;
    bool isHolding = false;
    GameObject heldUnit;

    bool onCooldown = false;
    public float cooldownTime;
    float timer = 0.0f;

    public Color normalColour, cooldownColour;

    private void Start()
    {
        if (onCooldown)
        {
            GetComponentInParent<Image>().color = cooldownColour;
        }
        else
        {
            GetComponentInParent<Image>().color = normalColour;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (onCooldown)
        {
            timer += Time.deltaTime;
            if (timer >= cooldownTime)
            {
                onCooldown = false;
                GetComponentInParent<Image>().color = normalColour;
            }
        }

        if (isHolding)
        {
            heldUnit.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -2.0f);
        }
    }

    public void OnMouseDown()
    {
        if (!onCooldown)
        {
            heldUnit = Instantiate(prefab, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -2.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
            isHolding = true;
        }
    }

    void OnMouseUp()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, LayerMask.GetMask("UI"));
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("placingArea"))
            {
                print("placed");
                timer = 0.0f;
                onCooldown = true;
                GetComponentInParent<Image>().color = cooldownColour;


                heldUnit.GetComponent<bomb>().placed = true;
            }
            else
            {
                print("missed area");
                Destroy(heldUnit);
            }
        }
        else
        {
            print("missed everthing");
            Destroy(heldUnit);
        }

        
        isHolding = false;
        heldUnit = null;
    }
}
