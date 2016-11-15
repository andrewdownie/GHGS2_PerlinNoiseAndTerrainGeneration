using UnityEngine;
using System.Collections;

public class GenerationResult {


    private bool[,] walkable;
    private GameObject[,] model;

    private int width, height;
    float floorLevel;
    Transform parent;

    public GenerationResult(int width, int height, float floorLevel, Transform parent)
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

            Vector3 curLocalPositon = model[x, z].transform.position;
            Vector3 newLocalPositon = new Vector3(curLocalPositon.x, curLocalPositon.y + model[x, z].transform.localScale.y / 2, curLocalPositon.z);
            model[x, z].transform.localPosition = newLocalPositon;
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
