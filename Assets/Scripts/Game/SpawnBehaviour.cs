using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _prefabObj;
    [Header("Диапазон задержки между двумя спаунами")]
    [SerializeField] private float lowTime = 0.1F;
    [SerializeField] private float upTime = 1.0F;

    private Dictionary<Vector2, bool> fullDictionary;

    private GameObject _scenePrefab;
    private int rand;
    private int numOfFS;
    private int itterator;
    private float nextSpawnTime;
    private bool hasPassed = true;

    // Start is called before the first frame update
    void Start()
    {
        fullDictionary = GetComponent<MainMechanics>().fullDictionary;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasPassed)
        {
            hasPassed = false;
            StartCoroutine(SpawnCoroutine());
        }
    }

    IEnumerator SpawnCoroutine()
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
            if(numOfFS == 0)
                yield return new WaitForSeconds(0.5F);
        }

        rand = Random.Range(0, numOfFS);

        itterator = 0;
        foreach(var item in fullDictionary)
        {
            if (item.Value == false && rand == itterator)
            {
                _scenePrefab = Object.Instantiate(_prefabObj, item.Key, Quaternion.identity);
                _scenePrefab.GetComponentInChildren<InflateMechanics>().fullDictionary = fullDictionary;
                fullDictionary[item.Key] = true;
                break;
            }else if(item.Value == false)
            {
                itterator++;
            }
        }

        hasPassed = true;
    }

}
