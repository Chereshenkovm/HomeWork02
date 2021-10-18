using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMechanics : MonoBehaviour
{
    [Header("Пройденное время с начала игры")]
    public int _time;

    private int startTime;
    private bool isPlaying = true;
    // Start is called before the first frame update

    private void OnEnable()
    {
        _time = 0;
        startTime = (int)Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            _time = (int)(Time.time - startTime);
            if (_time >= 60)
            {
                gameObject.SetActive(false);
                Debug.Log("Game Over");
                isPlaying = false;
            }
        }
    }
}
