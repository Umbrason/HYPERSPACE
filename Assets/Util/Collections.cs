using System.Collections.Generic;
using System.Linq;

public static class CollectionUtils
{
    public static T RandomElement<T>(this IList<T> collection)
    {
        var r = new System.Random();

        return collection[r.Next(collection.Count)];
    }

    public static T[] RandomElements<T>(this IList<T> collection, int count)
    {
        var r = new System.Random();
        return Enumerable.Range(0, count).Select(_ => collection[r.Next(collection.Count)]).ToArray();
    }

    public static T[] RandomElementsNoDuplicates<T>(this IList<T> collection, int count)
    {
        var r = new System.Random();
        return collection.OrderBy(_ => r.Next()).Take(count).ToArray();
    }
}