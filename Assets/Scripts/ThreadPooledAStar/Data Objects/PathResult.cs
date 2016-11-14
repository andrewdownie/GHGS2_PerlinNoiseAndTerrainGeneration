using UnityEngine;

[System.Serializable]
public class PathResult
{
    [SerializeField]
    double timeTaken;
    [SerializeField]
    Vector2[] path;

    public PathResult(Vector2[] path, double timeTaken)
    {
        this.timeTaken = timeTaken;
        this.path = path;
    }
    
    public Vector2[] Path
    {
        get { return path; }
    }
    
    public double TimeTaken
    {
        get { return timeTaken; }
    }
}
