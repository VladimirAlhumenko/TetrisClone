using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _buttonText;

    private ShapeType _shapeType;

    public event Action<ShapeType> ButtonClick;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }


    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        ButtonClick?.Invoke(_shapeType);
    }

    public void Init(ShapeType shapeType)
    {
        _shapeType = shapeType;

        _buttonText.text = $"{shapeType}";
    }
}
