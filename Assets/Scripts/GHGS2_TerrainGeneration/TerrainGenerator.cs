using UnityEngine;


public abstract class TerrainGenerator : MonoBehaviour {
    [SerializeField]
    protected int width = 40, height = 20, floorLevel = 2;

    public abstract GenerationResult Generate();
}


