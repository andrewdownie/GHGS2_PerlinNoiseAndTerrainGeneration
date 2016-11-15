using UnityEngine;
using System.Collections;

public class PerlinGenerator : TerrainGenerator
{
    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public bool generateSeed;
    public Vector2 offset;

    public TerrainPiece[] terrainPieces;
    public int[] terrainPiecesProbabilities;

    public override GenerationResult Generate(int width, int height, int floorLevel, Transform parent)
    {
        GenerationResult result = new GenerationResult(width, height, floorLevel, parent);

        if(generateSeed)
        {
            seed = System.Environment.TickCount;
        }
        float[,] noiseMap = PerlinNoise.GenerateNoiseMap(width, height, seed, noiseScale, octaves, persistance, lacunarity, offset);

        

        for(int z = 0; z < height; z++)
        {
            for(int x = 0; x < width; x++)
            {
                int chance = Mathf.FloorToInt(noiseMap[x, z] * 100);


                TerrainPiece piece = ChoosePiece(chance);
                if ((x == 0 && z == 0) || (x == width - 1 && z == height - 1))
                {
                    piece = terrainPieces[2];
                }
                else
                {
                    piece = ChoosePiece(chance);
                }


                result[x, z] = piece;
            }
        }
        


        return result;
    }



    private TerrainPiece ChoosePiece(int chance)
    {

        for (int i = 0; i < terrainPiecesProbabilities.Length; i++)
        {
            if (chance < terrainPiecesProbabilities[i])
            {
                return terrainPieces[i];
            }
        }

        return terrainPieces[0];
    }
    
}
