using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPointsMechanics : MonoBehaviour
{
    public GameObject main;

    private Text score;
    private ClickMechanics CM;

    // Start is called before the first frame update
    void Start()
    {
        CM = main.GetComponent<ClickMechanics>();
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = CM.points.ToString();
    }
}
