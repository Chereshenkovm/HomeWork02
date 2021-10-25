using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class SpawnBehaviour : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private MainMechanics _main;
    [SerializeField] private GameObject _prefabObj;

    [Header("Обрезание экрана 'Для точек спауна'")] [SerializeField]
    private float cutRation = 0.85f;
    [Header("Диапазон задержки между двумя спаунами")]
    [SerializeField] private float lowTime = 0.1F;
    [SerializeField] private float upTime = 1.0F;

    private Dictionary<Vector2, bool> fullDictionary;

    private GameObject _scenePrefab;
    private int rand;
    private int numOfFS;
    private int itterator;
    private float nextSpawnTime;
    
    private int numberOfRows = 2;
    private int numberOfCollumns = 4;

    private IEnumerator _spawner;
    
    private Vector2 screenCoords;
    
    public Vector2[] spawnPoints;
    

    private void OnEnable()
    {
        spawnPoints = null;
        fullDictionary = null;
        numberOfRows = _main.numberOfRows;
        numberOfCollumns = _main.numberOfCollumns;
        
        screenCoords = (Vector2)mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        spawnPoints = new Vector2[numberOfRows * numberOfCollumns];
        fullDictionary = new Dictionary<Vector2, bool>();

        for (int i = 0; i < numberOfRows; i++)
        {
            for (int j = 0; j < numberOfCollumns; j++)
            {
                spawnPoints[i * numberOfCollumns + j] = new Vector2((float)(2 * j + 1) / (numberOfCollumns * 2) * screenCoords.x * 2 - screenCoords.x, (float)(2 * i + 1) / (numberOfRows * 2) * screenCoords.y * 2 * cutRation - screenCoords.y);
                fullDictionary.Add(spawnPoints[i * numberOfCollumns + j], false);
            }
        }
        _spawner = SpawnCoroutine();
        StartCoroutine(_spawner);
    }

    private void OnDisable()
    {
        StopCoroutine(_spawner);
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            nextSpawnTime = Random.Range(lowTime, upTime);
            yield return new WaitForSeconds(nextSpawnTime);

            numOfFS = 0;
            while (numOfFS == 0)
            {
                numOfFS = 0;
                foreach (bool isFull in fullDictionary.Values)
                {
                    if (isFull == false)
                        numOfFS += 1;
                }

                if (numOfFS == 0)
                    yield return new WaitForSeconds(0.5F);
            }

            rand = Random.Range(0, numOfFS);

            itterator = 0;
            foreach (var item in fullDictionary)
            {
                if (item.Value == false && rand == itterator)
                {
                    _scenePrefab = Object.Instantiate(_prefabObj, item.Key, Quaternion.identity);
                    fullDictionary[item.Key] = true;
                    break;
                }
                else if (item.Value == false)
                {
                    itterator++;
                }
            }
        }
    }

    public void clearSpawnPoint(Vector2 vec)
    {
        fullDictionary[vec] = false;
    }

}
