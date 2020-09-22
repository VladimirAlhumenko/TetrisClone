using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour
{
    [SerializeField]
    private Button _backButton;

    private void OnEnable()
    {
        _backButton.onClick.AddListener(LoadMenu);
    }

    private void OnDisable()
    {
        _backButton.onClick.RemoveListener(LoadMenu);
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
