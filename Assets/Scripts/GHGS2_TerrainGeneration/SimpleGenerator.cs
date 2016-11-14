using UnityEngine;
using System.Collections;

public class SimpleGenerator : TerrainGenerator {
    public int chanceIn = 1, chanceOutOf = 3;


    public TerrainPiece unwalkableTerrainPiece;

   

    public override GenerationResult Generate(int width, int height, int floorLevel)
    {
        GenerationResult result = new GenerationResult(width, height, floorLevel);

        
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                bool w = Random.Range(chanceIn, chanceOutOf + 1) > 1;

                if ((x == 0 && z == 0) || (x == width - 1 && z == height - 1))
                {
                    w = true;
                }
                
                result.Walkable(x, z, w);


                GameObject go;
                if (w)
                {
                    go = new GameObject();
                    go.transform.parent = transform;
                    go.transform.position = new Vector3(x, floorLevel, z);
                }
                else
                {
                    go = unwalkableTerrainPiece.Instantiate(x, z, floorLevel, transform);
                }

               

                result.Model(x, z, go);
            }
        }
        
        return result;
    }
    
   
}
