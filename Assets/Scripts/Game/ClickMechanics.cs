using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class ClickMechanics : MonoBehaviour
{
    [SerializeField] private SpawnBehaviour _spawner;
    [SerializeField] private MainMechanics _main;
    
    [Header("Количество набранных очков")]
    private Game.Points _Points;

    private Vector2 mousePos;
    private RaycastHit2D hitObject;

    private void OnEnable()
    {
        _Points = _main._points;
    }

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
                    _Points.points += hitObject.collider.GetComponent<InflateMechanics>().GetPoints(mousePos);
                    _spawner.clearSpawnPoint((Vector2)hitObject.collider.transform.parent.position);
                    Destroy(hitObject.collider.transform.parent.gameObject);
                }
            }
            else
            {
                _Points.points += -50;
            }
        }
    }
}
