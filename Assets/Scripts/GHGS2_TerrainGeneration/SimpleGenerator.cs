using UnityEngine;
using System.Collections;

public class SimpleGenerator : TerrainGenerator {
    public int chanceIn = 1, chanceOutOf = 3;


    public TerrainPiece unwalkableTerrainPiece;
    public TerrainPiece walkableTerrainPiece;

   

    public override GenerationResult Generate(int width, int height, int floorLevel, Transform parent)
    {
        GenerationResult result = new GenerationResult(width, height, floorLevel, parent);

        
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                bool w = Random.Range(chanceIn, chanceOutOf + 1) > 1;

                if ((x == 0 && z == 0) || (x == width - 1 && z == height - 1))
                {
                    w = true;
                }


                TerrainPiece piece;
                if (w)
                {
                    piece = walkableTerrainPiece;
                }
                else
                {
                    piece = unwalkableTerrainPiece;
                }


                result[x, z] = piece;
            }
        }
        
        return result;
    }
    
   
}
