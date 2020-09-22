using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "GameConfig", order = 1)]
public class GameSettings : ScriptableObject
{
    [SerializeField]
    private float shapeStartSpeed;

    public float ShapeStartSpeed { get => shapeStartSpeed; set => shapeStartSpeed = value; }

    public ShapeType ShapeType { get; set; }
}
