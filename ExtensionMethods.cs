using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public static class ExtensionMethods
{
    private static readonly Random Rnd = new Random();

    /// <summary>
    /// Selects an object from the list at random and returns it
    /// </summary>
    /// <param name="list">List to select from</param>
    /// <typeparam name="T">Underlying type of list</typeparam>
    /// <returns>Randomly selected object</returns>
    public static T GetRandom<T>(this List<T> list)
    {
        return list[Rnd.Next(list.Count)];
    }
    
    /// <summary>
    /// Selects a random colour from a list of colours. Prevents 'except' from being returned
    /// </summary>
    /// <param name="list">List to select from</param>
    /// <param name="except">Colour to not return if selected</param>
    /// <returns>A colour from the list that isn't the specified colour</returns>
    public static Color GetRandom(this List<Color> list, Color except)
    {
        Color target;
        do
        {
            target = list.GetRandom();
        } while (target == except);
        return target;
    }
}