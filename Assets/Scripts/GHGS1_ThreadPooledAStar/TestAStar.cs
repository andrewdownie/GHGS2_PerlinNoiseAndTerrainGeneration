using UnityEngine;
using System.Collections.Generic;
using System;

public class TestAStar : MonoBehaviour{

    [SerializeField]
    private Grid grid;
   

    [SerializeField]
    GameObject pathModel;



    [Header("Testing Threaded")]
    [SerializeField]
    KeyCode runThreaded;
    [SerializeField]
    bool fakeButtonThreaded;
    [SerializeField]
    bool loopThreaded;

    [Header("Testing non-Threaded")]
    [SerializeField]
    KeyCode runNonThreaded;
    [SerializeField]
    bool fakeButton;
    [SerializeField]
    bool loop;

    [Header("Start pos (end defaults to [max, max])")]
    [SerializeField]
    Vector2 startPos;

    Vector2 endPos;

    System.Diagnostics.Stopwatch stopwatch;
    double latency;
    double lastTimeTaken;
    List<GameObject> pathRep;

    bool pathNotFound;

    bool searchingMain;
    int searchingThread;


    PathRequestMailBox mailbox;

    void Start()
    {
        endPos = new Vector2(grid.Width - 1, grid.Height - 1);
        pathRep = new List<GameObject>();
        latency = 0;
        pathNotFound = false;
        mailbox = new PathRequestMailBox();
        searchingMain = false;
        searchingThread = 0;
    }


    void Update()
    {
        //Do this first, so that OnDrawGUI will have a chance to pick up when a player searches on the main thread
        if (searchingMain)
        {
            stopwatch = System.Diagnostics.Stopwatch.StartNew();
            PathRequest request = new PathRequest(grid.Walkable, startPos, endPos);
            PathFound(AStar.FindPath(request));
            searchingMain = false;
        }


        if (Input.GetKeyDown(runThreaded) || fakeButtonThreaded == true || loopThreaded == true)
        {
            ClearPathRep();
            fakeButtonThreaded = false;
            stopwatch = System.Diagnostics.Stopwatch.StartNew();
            PathRequest request = new PathRequest(grid.Walkable, startPos, endPos);
            mailbox.RequestPath(request);
            searchingThread++;
        }


        if (Input.GetKeyDown(runNonThreaded) || fakeButton == true || loop == true)
        {
            ClearPathRep();
            fakeButton = false;
            searchingMain = true;
        }

       


        if (mailbox.UnsafeHasResult())
        {
            PathFound(mailbox.SafeGetResult());
            searchingThread--;
        }


    }
    

    void ClearPathRep()
    {
        if (pathRep != null)
        {
            for (int i = pathRep.Count - 1; i >= 0; i--)
            {
                Destroy(pathRep[i]);
            }

            pathRep.Clear();
        }
    }

    void PathFound(PathResult result)
    {
        lastTimeTaken = result.TimeTaken;
        latency = stopwatch.Elapsed.TotalMilliseconds;
        stopwatch.Stop();
        

        if(result.Path == null)
        {
            pathNotFound = true;
            return;
        }


        foreach(Vector2 v in result.Path)
        {
            GameObject go = (GameObject)Instantiate(pathModel, new Vector3(v.x, grid.FloorLevel + 0.5f, v.y), Quaternion.identity, transform);
            pathRep.Add(go);
        }
    }


    void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 500, 22), "Paths run from bottom left to top right:");
        GUI.Label(new Rect(20, 40, 500, 22), "\tPress '" + runNonThreaded.ToString() + "' to request path on the main unity thread");
        GUI.Label(new Rect(20, 60, 500, 22), "\tPress '" + runThreaded.ToString() + "' to request path on a thread-pool thread (does not work in webgl)");


        if (latency > 0)
        {
            GUI.Label(new Rect(20, 100, 500, 22), "AStar time taken: " + lastTimeTaken + " milliseconds");
        }
        if (latency > 0)
        {
            GUI.Label(new Rect(20, 120, 500, 22), "Request latency: " + latency + " milliseconds");
        }

     
         GUI.Label(new Rect(20, Screen.height - 20, 1000, 22), "Note: due to the inefficent data structure used to sort nodes, and the size of this map, searching may take around 30 seconds");
        
        if(searchingMain || searchingThread > 0)
        {
            string mainMsg = "";
            if (searchingMain)
            {
                mainMsg = "The main thread, and: ";
            }

            GUI.Label(new Rect(20, 140, 1000, 22), "Searching for paths on: " + mainMsg + searchingThread + " other threads");
        }
        else if (pathNotFound)
        {
            GUI.Label(new Rect(20, 140, 500, 22), "PATH NOT POSSIBLE");
        }
    }


    public static void Print(bool[,] boolArray2D)
    {
        for (int y = 0; y < boolArray2D.GetLength(1); y++)
        {
            string row = "";
            for (int x = 0; x < boolArray2D.GetLength(0); x++)
            {
                if (boolArray2D[x, y] == true)
                {
                    row += "-";
                }
                else
                {
                    row += "#";
                }
            }
            Debug.Log(row);
        }
    }
}
