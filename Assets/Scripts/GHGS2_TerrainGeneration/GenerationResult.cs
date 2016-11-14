using UnityEngine;
using System.Collections;

public class GenerationResult {
    private int width, height, floorLevel;


    private bool[,] walkable;
    private GameObject[,] model;



    public GenerationResult(int width, int height, int floorLevel)
    {
        this.width = width;
        this.height = height;
        this.floorLevel = floorLevel;

        walkable = new bool[width, height];
        model = new GameObject[width, height];
    }

    public int Width
    {
        get { return width; }
    }
    public int Height
    {
        get { return height; }
    }
    public int FloorLevel
    {
        get { return floorLevel; }
    }


    public void Walkable(int x, int z, bool val)
    {
        walkable[x, z] = val;
    }

    public bool[,] Walkable()
    {
        return walkable;
    }



    public void Model(int x, int z, GameObject val)
    {
        model[x, z] = val;
    }

    public GameObject[,] Model()
    {
        return model;
    }
}
