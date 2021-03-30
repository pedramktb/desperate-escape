// NavMesh.Raycast etc. ported to 2D
using UnityEngine;
using UnityEngine.AI;

public struct NavMeshHit2D
{
    public Vector2 position;
    public Vector2 normal;
    public float distance;
    public int mask;
    public bool hit;
}

public static class NavMesh2D
{
    public const int AllAreas = -1;

    // baked 3D navmesh is perfectly positioned at y=0.
    // 3D objects should be positioned at y=0.5 so that their feet are at y=0.
    // => let's have a const so we don't need to hardcode the value everywhere.
    public const float ProjectedObjectY = 0.5f;

    // based on: https://docs.unity3d.com/ScriptReference/AI.NavMesh.Raycast.html
    public static bool Raycast(Vector2 sourcePosition, Vector2 targetPosition, out NavMeshHit2D hit, int areaMask)
    {
        NavMeshHit hit3D;
        if (NavMesh.Raycast(NavMeshUtils2D.ProjectPointTo3D(sourcePosition),
                            NavMeshUtils2D.ProjectPointTo3D(targetPosition),
                            out hit3D,
                            areaMask))
        {
            hit = new NavMeshHit2D{position = NavMeshUtils2D.ProjectTo2D(hit3D.position),
                                   normal = NavMeshUtils2D.ProjectTo2D(hit3D.normal),
                                   distance = hit3D.distance,
                                   mask = hit3D.mask,
                                   hit = hit3D.hit};
            return true;
        }
        hit = new NavMeshHit2D();
        return false;
    }

    // based on: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
    public static bool SamplePosition(Vector2 sourcePosition, out NavMeshHit2D hit, float maxDistance, int areaMask)
    {
        NavMeshHit hit3D;
        if (NavMesh.SamplePosition(NavMeshUtils2D.ProjectPointTo3D(sourcePosition), out hit3D, maxDistance, areaMask))
        {
            hit = new NavMeshHit2D{position = NavMeshUtils2D.ProjectTo2D(hit3D.position),
                                   normal = NavMeshUtils2D.ProjectTo2D(hit3D.normal),
                                   distance = hit3D.distance,
                                   mask = hit3D.mask,
                                   hit = hit3D.hit};
            return true;
        }
        hit = new NavMeshHit2D();
        return false;
    }
}