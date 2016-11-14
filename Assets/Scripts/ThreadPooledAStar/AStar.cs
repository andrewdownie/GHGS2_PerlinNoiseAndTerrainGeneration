using UnityEngine;
using System.Collections.Generic;

public static class AStar {

    public static void FindPath(object mailboxRequest)
    {
        MailboxRequest mbr = (MailboxRequest)mailboxRequest;
        PathResult result = FindPath(mbr.pathRequest);
        mbr.mailbox.SafeAddResult(result);
    }

	public static PathResult FindPath(PathRequest request)
    {
        System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

        
        List<Vector2> openSet = new List<Vector2>();
        List<Vector2> closedSet = new List<Vector2>();


        int gridWidth = request.grid.GetLength(0);
        int gridHeight = request.grid.GetLength(1);
        Vector2[,] parent = new Vector2[gridWidth, gridHeight];
        int[,] hCost = new int[gridWidth, gridHeight];
        int[,] fCost = new int[gridWidth, gridHeight];

        Vector2 current = request.startPos;
        Vector2 target = request.endPos;
        int currentHCost = Distance(current, target);

        openSet.Add(current);

        while (true)
        {
            if(openSet.Count == 0)
            {
                LogOnMain("Could not find target node");
                stopwatch.Stop();
                return new PathResult(null, stopwatch.Elapsed.TotalMilliseconds);
            }


            current = LowestFCost(fCost, hCost, openSet);
            currentHCost = hCost[ (int)current.x, (int)current.y ];
            openSet.Remove(current);
            closedSet.Add(current);


            if(current == target)
            {
                break;
            }


            ///
            /// Go thorugh each neighbour, and do the entire algo
            ///
            for (int offX = -1; offX <= 1; offX++)
            {
                for (int offY = -1; offY <= 1; offY++)
                {
                    int currentX = Mathf.RoundToInt(current.x);
                    int currentY = Mathf.RoundToInt(current.y);


                    if(offX == 0 && offY == 0)
                    {
                        continue;
                    }
                    if(currentX + offX < 0 || currentX + offX >= request.grid.GetLength(0))
                    {
                        continue;
                    }
                    if (currentY + offY < 0 || currentY + offY >= request.grid.GetLength(1))
                    {
                        continue;
                    }

                    int neighbourX = currentX + offX;
                    int neighbourY = currentY + offY;
                    Vector2 neighbour = new Vector2(neighbourX, neighbourY);

                    if(request.grid[neighbourX, neighbourY] == false || closedSet.Contains(neighbour))
                    {
                        continue;
                    }
                    hCost[neighbourX, neighbourY] = Distance(neighbour, target);

                    int dist = Distance(current, neighbour);

                    if (openSet.Contains(neighbour) == false ||
                       gCost(fCost, hCost, currentX, currentY) + dist < gCost(fCost, hCost, neighbourX, neighbourY) )
                    {
                        fCost[neighbourX, neighbourY] = fCost[currentX, currentY] + dist;
                        parent[neighbourX, neighbourY] = current;

                        if(openSet.Contains(neighbour) == false)
                        {
                            openSet.Add(neighbour);
                        }
                    }

                }
            }
        }


        
        PathResult result = new PathResult(RebuildPath(request.startPos, request.endPos, parent), stopwatch.Elapsed.TotalMilliseconds);
        stopwatch.Stop();
        return result;
    }


    private static Vector2 LowestFCost(int[,] fCost, int[,] hCost, List<Vector2> vList)
    {
        Vector2 lowest = vList[0];


        foreach(Vector2 v in vList)
        {
            if (fCost[(int)v.x, (int)v.y] < fCost[(int)lowest.x, (int)lowest.y])
            {
                lowest = v;
            }
            else if(fCost[(int)v.x, (int)v.y] == fCost[(int)lowest.x, (int)lowest.y])
            {
                if(gCost(fCost, hCost, (int)v.x, (int)v.y) < gCost(fCost, hCost, (int)lowest.x, (int)lowest.y)){
                    lowest = v;
                }
            }
        }


        return lowest;
    }

    private static Vector2[] RebuildPath(Vector2 startNode, Vector2 endNode, Vector2[,] parent)
    {


        List<Vector2> path = new List<Vector2>();
        Vector2 current = endNode;

        //LogOnMain("Current v2: " + current);

        while(current != startNode)
        {
            path.Add(current);
            current = parent[(int)current.x, (int)current.y];
        }

        path.Add(startNode);

        path.Reverse();

        
        return path.ToArray();
    }


    private static int gCost(int[,] fCost, int[,] hCost, int x, int y)
    {
        return fCost[x, y] + hCost[x, y];
    }


    public static int Distance(Vector2 start, Vector2 end)
    {
        int dx = Mathf.RoundToInt(end.x) - Mathf.RoundToInt(start.x);
        int dy = Mathf.RoundToInt(end.y) - Mathf.RoundToInt(start.y);

        dx = Mathf.Abs(dx);
        dy = Mathf.Abs(dy);

        int angled = Mathf.Min(dx, dy);
        int straight = Mathf.Max(dx, dy) - angled;

        return (angled * 14) + (straight * 10);
    }


   

    private static void LogOnMain(string message)
    {
        if(System.Threading.Thread.CurrentThread.ManagedThreadId == 1)
        {
            Debug.Log(message);
        }
    }
    
}
