using UnityEngine;
using System.Collections;

public class ExperimentalGenerator : TerrainGenerator
{

    public TerrainPiece[] terrainPieces;
    public int[] terrainPiecesProbabilities;

    public override GenerationResult Generate(int width, int height, int floorLevel)
    {
        GenerationResult result = new GenerationResult(width, height, floorLevel);

        int[,] results = new int[width, height];

        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < height; z++)
            {
                results[x, z] = ChoosePiece();
            }
        }



        ///
        /// My attempt at smoothing
        ///
        int smoothIterations = 1;
        for(int smoothIteration = 0; smoothIteration < smoothIterations; smoothIteration++)
        {

        
            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    int[] matchCount = new int[terrainPieces.Length];
                    for(int i = 0; i < matchCount.Length; i++)
                    {
                        matchCount[i] = 0;
                    }

                    for(int xOff = -1; xOff <= 1; xOff++)
                    {
                        for(int zOff = -1; zOff <=1; zOff++)
                        {
                            int newX = x + xOff, newZ = z + zOff;
                            if(newX < 0 || newX >= width || newZ < 0 || newZ >= height)
                            {
                                continue;
                            }

                            matchCount[results[newX, newZ]]++;

                        }
                    }

                    results[x, z] = BiggestIndex(matchCount);
                }
            }
        }


        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GameObject go;
                go = terrainPieces[results[x, z]].Instantiate(x, z, floorLevel, transform);
                result.Model(x, z, go);
            }
        }

        return result;
    }

    int BiggestIndex(int[] list)
    {
        int biggest = -1;

        for(int i = 0; i < list.Length; i++)
        {
            if(list[i] > biggest)
            {
                biggest = i;
            }
        }

        return biggest;
    }

    private int ChoosePiece()
    {
        int rand = Random.Range(1, 101);

        for (int i = terrainPiecesProbabilities.Length - 1; i >= 0; i--)
        {
            if (rand > terrainPiecesProbabilities[i])
            {
                return i;
            }
        }

        return 0;
    }
    
}
