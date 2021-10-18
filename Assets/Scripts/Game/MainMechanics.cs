using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnBehaviour))]
[RequireComponent(typeof(ClickMechanics))]
[RequireComponent(typeof(TimeMechanics))]
public class MainMechanics : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    [Header("Число строк и столбиков")]
    public int numberOfRows = 2;
    public int numberOfCollumns = 4;
    [Header("Координаты спаунов")]
    public Vector2[] spawnPoints;

    public Dictionary<Vector2, bool> fullDictionary;

    private Vector2 screenCoords;


    // Start is called before the first frame update
    void Start()
    {
        screenCoords = (Vector2)mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        spawnPoints = new Vector2[numberOfRows * numberOfCollumns];
        fullDictionary = new Dictionary<Vector2, bool>();

        for (int i = 0; i < numberOfRows; i++)
        {
            for (int j = 0; j < numberOfCollumns; j++)
            {
                spawnPoints[i * numberOfCollumns + j] = new Vector2((float)(2 * j + 1) / (numberOfCollumns * 2) * screenCoords.x * 2 - screenCoords.x, (float)(2 * i + 1) / (numberOfRows * 2) * screenCoords.y * 2 - screenCoords.y);
                fullDictionary.Add(spawnPoints[i * numberOfCollumns + j], false);
            }
        }
    }

    private void Awake()
    {
        Debug.Log("Правила игры:\n Зелёные глаза от 0 до 100 очков,\n Жёлтые глаза от 100 до 200 очков,\n Красные глаза от 200 до 400 очков,\n Глаза бомбы забирают 1000 очков,\n Промах забирает 50 очков,\n Чем быстрее нажимается глаз, тем больше очков получается.");
    }

    public void StartTheGame()
    {
        gameObject.GetComponent<SpawnBehaviour>().enabled = true;
        gameObject.GetComponent<ClickMechanics>().enabled = true;
        gameObject.GetComponent<TimeMechanics>().enabled = true;
    }
}
