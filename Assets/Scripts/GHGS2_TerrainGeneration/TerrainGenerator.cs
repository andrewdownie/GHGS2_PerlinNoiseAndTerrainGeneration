using UnityEngine;


public abstract class TerrainGenerator : MonoBehaviour {
    

    public abstract GenerationResult Generate(int width, int height, int floorLevel);
}


