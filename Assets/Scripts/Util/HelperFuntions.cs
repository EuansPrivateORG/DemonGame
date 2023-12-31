using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using System;

public static class HelperFuntions
{
    public static List<T> ShuffleList<T>(List<T> list)
    {
        var newShuffledList = new List<T>();
        var listCount = list.Count;

        for (int i = 0; i < listCount; i++)
        {
            var randomElementInList = UnityEngine.Random.Range(0, list.Count);
            newShuffledList.Add(list[randomElementInList]);
            list.Remove(list[randomElementInList]);
        }

        return newShuffledList;
    }

    public static List<T> AddToList<T>(List<T> originlist, List<T> itemsToAdd)
    {
        List<T> list = new List<T>(originlist);

        foreach(T item in itemsToAdd)
        {
            list.Add(item);
        }

        return list;
    }

    public static bool IntGreaterThanOrEqual(int int1, int int2)
    {
        return int1 >= int2;
    }
    public static bool GreaterThanOrEqual(float num1, float num2)
    {
        return num1 >= num2;
    }
#if UNITY_EDITOR
    public static void ClearLog()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
#endif
    public static float GetPercentageOf(float percentage, int total)
    {
        return (percentage / 100) * total;
    }

    public static Queue<T> AddListToQueue<T>(List<T> list)
    {
        Queue<T> q = new Queue<T>();

        foreach (T item in list)
        {
            q.Enqueue(item);
        }

        return q;
    }
    public static List<Transform> GetAllChildrenTransformsFromParent(Transform parent)
    {
        List<Transform> list = new List<Transform>();

        foreach (Transform child in parent)
        {
            list.Add(child);
        }

        return list;
    }
    public static List<Spawner> GetAllChildrenSpawnersFromParent(Transform parent)
    {
        List<Spawner> list = new List<Spawner>();

        foreach (Transform child in parent)
        {
            if(child.TryGetComponent<Spawner>(out Spawner s))
            {
                list.Add(s);
            }        
        }

        return list;
    }
    public static float GetRandomIndexBetweenMinMax(float minPercent, float maxPercent, float total)
    {
        float min = (minPercent / 100) * total;
        float max = (maxPercent / 100) * total;

        return UnityEngine.Random.Range(min, max);
    }

    public static int EvaluateAnimationCuveInt(AnimationCurve curve, float num)
    {
        return (int)curve.Evaluate(num);
    }

    public static List<T> AllChildren<T>(Transform root, bool getRoot = false)
    {
        List<T> result = new List<T>();

        if(getRoot == true)
        {
            if (root.TryGetComponent<T>(out T component))
            {
                result.Add(component);
            }
        }

        if (root.childCount > 0)
        {
            foreach (Transform item in root)
            {
                Searcher(result, item);
            }
        }

        return result;
    }

    public static void Searcher<T>(List<T> list, Transform root)
    {
        if (root.TryGetComponent(out T item)) list.Add(item);

        if (root.childCount > 0)
        {
            foreach (Transform t in root)
            {
                Searcher(list, t);
            }
        }
    }
}