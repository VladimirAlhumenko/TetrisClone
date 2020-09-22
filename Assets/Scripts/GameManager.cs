using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Shape _shape;
    private ShapeView _shapeView;
    private ShapeController _shapeController;

    private Board _board;
    private BoardView _boardView;

    public float SpeedIncPercent;
    public int ScoreSpeedInc;

    private string _scoreTitle;

    private bool _gameRunning;
    private bool _gameOver = false;

    private int _score = 0;

    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private GameSettings _gameSettings;

    private void Start()
    {
        _scoreTitle = _scoreText.text;
        UpdateScore();

        _gameRunning = true;

        _boardView = GetComponent<BoardView>();
        _board = new Board(_boardView.Rows, _boardView.Columns);

        _shapeView = GetComponent<ShapeView>();

        _shape = SpawnShape(_gameSettings.ShapeStartSpeed);
    }

    private void Update()
    {
        if (!_gameOver)
        {
            if (_gameRunning)
            {
                _shapeController.ReceiveInput();

                StartCoroutine(_shapeController.Fall(stopped =>
                {
                    if (stopped)
                    {
                        _board.PutBlocks(_shape.GetCurrentBlocks());
                        _boardView.PutBlocks(_shape.GetCurrentBlocks());

                        int cleanedCount;
                        var cleanedSome = _board.CleanFullRows(out cleanedCount);

                        if (cleanedSome)
                        {
                            _score += cleanedCount;
                            UpdateScore();
                            _shape.Speed = RecalcSpeed();
                            _boardView.EraseBlocks();
                            _boardView.PutBlocks(_board.GetBlocks());
                        }

                        if (_board.IsOverflow())
                        {
                            Over();
                        }

                        _shape = SpawnShape(_shape.Speed);
                    }
                }));
            }
        }
    }

    private void Over()
    {
        _gameOver = true;
    }

    private void UpdateScore()
    {
        _scoreText.text = string.Format("{0} {1}", _scoreTitle, _score);
    }

    private float RecalcSpeed()
    {
        var maxPercent = 100.0f;
        return _shape.Speed * (_score / ScoreSpeedInc / maxPercent * SpeedIncPercent + 1);
    }

    private Shape SpawnShape(float shapeSpeed)
    {
        Shape shape;

        if (_gameSettings.ShapeType == ShapeType.Random)
        {
            shape = ShapeFactory.CreateRandom();
        }
        else
        {
            shape = ShapeFactory.CreateShape(_gameSettings.ShapeType);
        }

        shape.Pos = new Cell(0, 5);
        shape.Speed = shapeSpeed;
        shape.MovementRestrict = _board.IsEmptyCell;
        _shapeView.PlaceBlocks(shape.GetCurrentBlocks());

        _shapeController = new ShapeController(shape, _shapeView);

        return shape;
    }
}
