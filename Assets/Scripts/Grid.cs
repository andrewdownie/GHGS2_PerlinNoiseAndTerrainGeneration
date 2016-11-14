using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {
    [SerializeField]
    TerrainGenerator terrainGenerator;


    [SerializeField]
    protected int width = 40, height = 20, floorLevel = 2;
    
    
    




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

    void Awake()
    {
        if(terrainGenerator == null)
        {
            Debug.LogError("Grid needs a TerrainGenerator instance reference");
            return; 
        }

        GenerationResult gr = terrainGenerator.Generate(width, height, floorLevel);
        model = gr.Model();
        walkable = gr.Walkable();
        
    }
    



}


