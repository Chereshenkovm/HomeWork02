using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Points
    {
        public int points;

        public Points()
        {
            points = 0;
        }
    }


    [RequireComponent(typeof(SpawnBehaviour))]
    [RequireComponent(typeof(ClickMechanics))]
    [RequireComponent(typeof(TimeMechanics))]
    public class MainMechanics : MonoBehaviour
    {
        [Header("���� UI")]
        [SerializeField] private StartWindow _startWindow;
        [SerializeField] private SettingsWindow _settingsWindow;
        [SerializeField] private InstructionWindow _instructionWindow;

        [Header("����� ����")] 
        public int _time = 60;
        [Header("����� ����� � ���������")]
        public int numberOfRows = 2;
        public int numberOfCollumns = 4;

        public Points _points = new Points();


        // Start is called before the first frame update
        void Start()
        {
            _startWindow.QuitEvent += () =>
            {
                Application.Quit();
            };

            _startWindow.StartEvent += () =>
            {
                _startWindow.gameObject.SetActive(false);
                StartTheGame();
            };

            _startWindow.SettingsEvent += () =>
            {
                _startWindow.gameObject.SetActive(false);
                _settingsWindow.gameObject.SetActive(true);
                _settingsWindow.SetTimeRC(_time.ToString(), numberOfRows, numberOfCollumns);
            };

            _startWindow.InstrEvent += () =>
            {
                _startWindow.gameObject.SetActive(false);
                _instructionWindow.gameObject.SetActive(true);
            };

            _settingsWindow.CloseSet += () =>
            {
                CloseWindow();
            };

            _settingsWindow.OnApply += (t,R,C) =>
            {
                _time = Int32.Parse(t);
                numberOfCollumns = C;
                numberOfRows = R;
                CloseWindow();
            };

            _instructionWindow.OnCloseWindow += () =>
            {
                _instructionWindow.gameObject.SetActive(false);
                _startWindow.gameObject.SetActive(true);
            };
        }

        private void Awake()
        {
            if (PlayerPrefs.HasKey("Game Time"))
            {
                _time = PlayerPrefs.GetInt("Game Time");
                numberOfRows = PlayerPrefs.GetInt("NumberOfRows");
                numberOfCollumns = PlayerPrefs.GetInt("NumberOfCols");
            }
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.SetInt("Game Time", _time);
            PlayerPrefs.SetInt("NumberOfRows", numberOfRows);
            PlayerPrefs.SetInt("NumberOfCols", numberOfCollumns);
            PlayerPrefs.Save();
        }

        private void CloseWindow()
        {
            _settingsWindow.gameObject.SetActive(false);
            _startWindow.gameObject.SetActive(true);
        }

        public void StartTheGame()
        {
            _points.points = 0;
            gameObject.GetComponent<SpawnBehaviour>().enabled = true;
            gameObject.GetComponent<ClickMechanics>().enabled = true;
            gameObject.GetComponent<TimeMechanics>().enabled = true;
        }

        public void StopTheGame()
        {
            _startWindow.SetHighScore(_points.points);
            gameObject.GetComponent<SpawnBehaviour>().enabled = false;
            gameObject.GetComponent<ClickMechanics>().enabled = false;
            gameObject.GetComponent<TimeMechanics>().enabled = false;
        }
    
    }
}