using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Car : MonoBehaviour
{
    public int currentGrid;
    public void SetCurrentGrid(int g)
    {
        currentGrid = g;
    }

    public int GetCurrentGrid()
    {
        return currentGrid;
    }

}
