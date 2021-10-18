using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMechanics : MonoBehaviour
{
    [Header("Количество набранных очков")]
    public int points = 0;

    private Vector2 mousePos;
    private RaycastHit2D hitObject;

    private Dictionary<Vector2, bool> fullDictionary;

    // Start is called before the first frame update
    void Start()
    {
        fullDictionary = GetComponent<MainMechanics>().fullDictionary;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hitObject = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hitObject)
            {
                if (hitObject.collider.gameObject.name == "Eye")
                {
                    points += hitObject.collider.GetComponent<InflateMechanics>().GetPoints();
                    fullDictionary[(Vector2)hitObject.collider.transform.parent.position] = false;
                    Destroy(hitObject.collider.transform.parent.gameObject);
                }
            }
            else
            {
                points += -50;
            }
        }
    }
}
