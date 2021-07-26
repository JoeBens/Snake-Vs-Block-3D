using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelParameters", menuName = "LevelParameters")]
public class LevelParameters : ScriptableObject
{
    public float levelLength;
    public Color levelColor;

    public float[] powerX = new float[] { -4.5f, -1.5f, 1.5f, 4.5f };
    public float[] killerCubesX = new float[] { -6f, -3f, 0f, 3f, 6f };

    public int chunkSize;
    
    public float unit = 7f;

    public int specialCubeChance = 20;

    public int[] startingKillerCubeHP = new int[] { 1, 3 };
    public int[] killerCubeHP = new int[] { 1, 50 };

    public int[] foodValue = new int[] { 1, 6 };

    public List<Color> killerCubesColors = new List<Color>() { Color.white, Color.blue, Color.yellow, Color.red, Color.black};
    public Color specialCubeColor;

    public Color foodColor;



}
