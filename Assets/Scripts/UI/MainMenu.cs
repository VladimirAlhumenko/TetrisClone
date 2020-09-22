using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameSettings _gameSettings;

    [SerializeField]
    private Transform _buttonsArea;

    [SerializeField]
    private MenuButton _menuButtonPrefab;

    private void Start()
    {
        InitButtonsArea();
    }

    private void InitButtonsArea()
    {
        var shapes = Enum.GetValues(typeof(ShapeType)).Cast<ShapeType>();

        foreach (var shape in shapes)
        {
            var menuButton = Instantiate(_menuButtonPrefab,_buttonsArea);

            menuButton.Init(shape);

            menuButton.ButtonClick += LoadGame;
        }
    }

    private void LoadGame(ShapeType shapeType)
    {
        _gameSettings.ShapeType = shapeType;

        SceneManager.LoadScene("Game");
    }

    private void ContinueGame()
    {

    }

}
