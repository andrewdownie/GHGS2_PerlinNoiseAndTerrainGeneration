using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {
    [SerializeField]
    int width = 20, height = 20, floorLevel = 2;

    [SerializeField]
    GameObject walkableTile, nonWalkableTile;


    GameObject[,] model;
    bool[,] walkable;

    public int FloorLevel
    {
        get { return floorLevel; }
    }

    public int Width
    {
        get { return width; }
    }
    public int Height
    {
        get { return height; }
    }


    public bool[,] Walkable
    {
        get { return (bool[,])walkable.Clone(); }
    }

    void Start()
    {
        model = new GameObject[width, height];
        walkable = new bool[width, height];



        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < height; z++)
            {
                bool w = Random.Range(0, 3) > 0;

                if((x == 0 && z == 0) || (x == width - 1 && z == height - 1)){
                    w = true;
                }

                walkable[x, z] = w;


                GameObject go;
                if (w)
                {
                    go = new GameObject();
                    go.transform.parent = transform;
                    go.transform.position = new Vector3(x, floorLevel, z);
                }
                else
                {
                    go = (GameObject)Instantiate(nonWalkableTile, new Vector3(x, floorLevel, z), Quaternion.identity, transform);
                }

                go.name = "[" + x.ToString("000") + ", " + floorLevel.ToString("000") + ", " + z.ToString("000") + "]";

            }
        }
    }
    



}


