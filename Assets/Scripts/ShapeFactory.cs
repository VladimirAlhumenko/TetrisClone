using UnityEngine;

public static class ShapeFactory
{
    public static Shape CreateRandom()
    {
        var choice = Random.Range(0, 5);

        return CreateShape((ShapeType)choice);
    }

    public static Shape CreateShape(ShapeType shapeType)
    {
        switch (shapeType)
        {
            case ShapeType.Symmetric: return CreateSymmetric();
            case ShapeType.Stick: return CreateStick();
            case ShapeType.Square: return CreateSquare();
            case ShapeType.L: return CreateL();
            case ShapeType.S: return CreateS();

            default: return CreateSymmetric();
        }
    }

    private static Shape CreateSymmetric()
    {
        return new Shape(
            new[,] {
            { 1, 0 },
            { 1, 1 },
            { 1, 0 }
        },

            new[,] {
            { 1, 1, 1 },
            { 0, 1, 0 }
        },
            new[,] {
            { 0, 1 },
            { 1, 1 },
            { 0, 1 }
        },
            new[,] {
            { 0, 1, 0 },
            { 1, 1, 1 }
        });
    }

    private static Shape CreateStick()
    {
        return new Shape(
           new[,] {
            { 1, 1, 1, 1}
       },

           new[,] {
            { 1 },
            { 1 },
            { 1 },
            { 1 }
       });
    }

    private static Shape CreateSquare()
    {
        return new Shape(
            new[,] {
            { 1, 1 },
            { 1, 1 }
        });
    }

    private static Shape CreateL()
    {
        return new Shape(
            new[,] {
            { 1, 1 },
            { 1, 0 },
            { 1, 0 }
        },

            new[,] {
            { 1, 1, 1 },
            { 0, 0, 1 }
        },
            new[,] {
            { 0, 1 },
            { 0, 1 },
            { 1, 1 }
        },
            new[,] {
            { 1, 0, 0 },
            { 1, 1, 1 }
        });
    }

    private static Shape CreateS()
    {
        return new Shape(
            new[,]
            {
                {0, 1, 1},
                {1, 1, 0}
            },

            new[,]
            {
                {1, 0},
                {1, 1},
                {0, 1}
            });
    }
}
