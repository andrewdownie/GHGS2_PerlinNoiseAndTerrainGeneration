using UnityEngine;
using System.Threading;

public class PathRequestMailBox {
    [SerializeField]
    private PathResult result;

    public void SafeAddResult(PathResult result)
    {
        lock(this){
            this.result = result;
        }
        
    }

    public PathResult SafeGetResult()
    {
        lock (this)
        {
            PathResult r = result;
            result = null;
            return r;
        }
    }

    public bool UnsafeHasResult()
    {
        return result != null;
    }

    public bool RequestPath(PathRequest request)
    {
        lock (this)
        {
            if (result != null)
            {
                return false;
            }
        }
        
        
        return ThreadPool.QueueUserWorkItem(
            new WaitCallback(AStar.FindPath),
            new MailboxRequest(request, this)
        );
    }

}

public class MailboxRequest
{
    public PathRequestMailBox mailbox;
    public PathRequest pathRequest;

    public MailboxRequest(PathRequest pathRequest, PathRequestMailBox mailbox)
    {
        this.mailbox = mailbox;
        this.pathRequest = pathRequest;
    }
}
