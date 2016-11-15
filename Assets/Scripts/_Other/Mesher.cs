using UnityEngine;
using System.Collections.Generic;

public static class Mesher  {

	public static GameObject[] CombineMeshes(GameObject[,] meshHolders)
    {
        int width = meshHolders.GetLength(0);
        int depth = meshHolders.GetLength(1);

        List<CombineInstance> combineInstances = new List<CombineInstance>();

        List<GameObject> results = new List<GameObject>();
    

        for (int x = 0; x < width; x++)
        {
            for(int z = 0; z < depth; z++)
            {
                MeshFilter mf = meshHolders[x, z].GetComponent<MeshFilter>();

                CombineInstance combineInstance = new CombineInstance();

               // MeshRenderer renderer = mf.GetComponent<MeshRenderer>();

                combineInstance.mesh = mf.mesh;
                combineInstance.transform = mf.transform.localToWorldMatrix;

                
                combineInstances.Add(combineInstance);
            }
        }






        /* int piecesInInt16 = (Mathf.CeilToInt(Mathf.Pow(2, sizeof(System.UInt16) * 8)) - 1) / 24;


         for (int i = 0; i < combineInstances.Count; i+=piecesInInt16)
         {
             Mesh combinedMesh = new Mesh();

             GameObject result = new GameObject();

             if(i + piecesInInt16 >= combineInstances.Count)
             {
                 piecesInInt16 = combineInstances.Count - i - 1;
             }

             combinedMesh.CombineMeshes(combineInstances.GetRange(i, piecesInInt16).ToArray());
             result.AddComponent<MeshFilter>().mesh = combinedMesh;

         }*/

        Mesh combinedMesh = new Mesh();
        GameObject result = new GameObject();
        combinedMesh.CombineMeshes(combineInstances.ToArray());
        result.AddComponent<MeshFilter>().mesh = combinedMesh;





        return results.ToArray();
    }


    /*CombineInstance[] CurrentSlice(CombineInstance[] fullArray, int start, int sliceSize)
    {
        if(start + sliceSize >= fullArray.Length)
        {
            return new 
        }
    }*/
}
