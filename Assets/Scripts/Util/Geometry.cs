using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Geometry
{
    public static Vector2 SamplePathNormalized(this Vector2[] path, float t)
    {
        if (path.Length < 1) return default;
        if (path.Length < 2) return path[0];

        var pathLength = 0f;
        for (int i = 0; i < path.Length - 1; i++) pathLength += (path[i] - path[i + 1]).magnitude;
        t = Mathf.Clamp01(t);
        var d = t * pathLength;
        var pathIndex = 0;
        while (d > 0 && pathIndex < path.Length)
        {
            var delta = path[pathIndex + 0] - path[pathIndex + 1];
            var dst = delta.magnitude;
            if (d <= dst) return path[pathIndex] + delta.normalized * d;
            d -= dst;
            pathIndex++;
        }
        return default;
    }

    public static Vector2 SamplePath(this Vector2[] path, float d)
    {
        if (path.Length < 1) return default;
        if (path.Length < 2) return path[0];

        var pathIndex = 0;
        while (pathIndex < path.Length)
        {
            var delta = path[pathIndex + 1] - path[pathIndex + 0];
            var dst = delta.magnitude;
            if (d <= dst) return path[pathIndex] + delta.normalized * d;
            d -= dst;
            pathIndex++;
        }
        return default;
    }

    public static Vector2[] RemoveFromStart(this Vector2[] path, float distanceToRemove)
    {
        if (path.Length < 2) return path;
        var queue = new Queue<Vector2>(path);
        while (distanceToRemove > 0 && queue.Count > 1)
        {
            var p = queue.Dequeue();
            var d = (queue.Peek() - p).magnitude;
            if (distanceToRemove >= d)
            {
                distanceToRemove -= d;
                continue;
            }
            var t = distanceToRemove / d;
            var p1 = Vector2.Lerp(p, queue.Peek(), t);
            return queue.Prepend(p1).ToArray();
        }
        return queue.ToArray();
    }
}