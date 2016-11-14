using UnityEngine;
using System.Collections;

public class GenerationResult {


    private bool[,] walkable;
    private GameObject[,] model;

    private int width, height, floorLevel;
    Transform parent;

    public GenerationResult(int width, int height, int floorLevel, Transform parent)
    {
        walkable = new bool[width, height];
        model = new GameObject[width, height];


        this.height = height;
        this.width = width;
        this.floorLevel = floorLevel;
        this.parent = parent;
    }

    public TerrainPiece this[int x, int z]
    {
        set {
            walkable[x, z] = value.walkable;
            model[x, z] = value.Instantiate(x, z, floorLevel, parent);
        }
    }

   /* public void Walkable(int x, int z, bool val)
    {
        walkable[x, z] = val;
    }*/

    public bool[,] Walkable()
    {
        return walkable;
    }



   /* public void Model(int x, int z, GameObject val)
    {
        model[x, z] = val;
    }*/

    public GameObject[,] Model()
    {
        return model;
    }
}
