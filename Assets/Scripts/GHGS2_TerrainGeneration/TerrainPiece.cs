using UnityEngine;
using System.Collections;

[System.Serializable]
public class TerrainPiece
{
    public bool walkable;
    public GameObject model;


    public int percentMovementModifier;


    public GameObject Instantiate(int x, int z, float floorLevel, Transform parent)
    {
        GameObject go = (GameObject)GameObject.Instantiate(model, new Vector3(x, floorLevel, z), Quaternion.identity, parent);

        go.name = "[" + x.ToString("000") + ", " + floorLevel.ToString("000") + ", " + z.ToString("000") + "]";

        return go;
    }
}
