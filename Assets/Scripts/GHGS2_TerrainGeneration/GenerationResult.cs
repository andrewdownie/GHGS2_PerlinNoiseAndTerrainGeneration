using UnityEngine;
using System.Collections;

public class GenerationResult {


    private bool[,] walkable;
    private GameObject[,] model;



    public GenerationResult(int width, int height, int floorLevel)
    {
        walkable = new bool[width, height];
        model = new GameObject[width, height];
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
