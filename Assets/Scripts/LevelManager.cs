using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] float TimeForOneUnit;
    [SerializeField] GameObject[] carPrefabs;
    [SerializeField] List<GameObject> grids;
    [SerializeField] List<Material> materialsForGrids;
    [SerializeField] Material firstColorMat;
    [SerializeField] Material secondColorMat;

    Graph gr;
    private List<bool> isGridFree;

    [SerializeField]private List<string> gridColors;


     void Start()
    {

        gr = new Graph();
        isGridFree = new List<bool>();
       
        /*
        for (int i = 0; i < gr.adjacencyList.Length; i++)
        {
            List<int> arr = gr.adjacencyList[i];
            Debug.Log("head" + i);
            for (int k = 0; k < arr.Count; k++)
            {
                Debug.Log(arr[k]);
            }

        }*/

        for (int i = 0; i < gridColors.Count; i++)
        {
            Debug.Log(gridColors[i].ToString());
            if (gridColors[i]=="F") //first color option, which is purple for this level
            {
                grids[i].GetComponent<Renderer>().material = firstColorMat;
 
            }

            else if (gridColors[i] == "S") // second color option, which is yellow for this level
            {
                grids[i].GetComponent<Renderer>().material = secondColorMat;

            }
        }
            for (int i = 0; i < grids.Count; i++)
        {
            isGridFree.Add(true);
        }

  
    }

    public string GetColorOfGrid(int i)
    {
        if ((i==0 || i>0) && i<gridColors.Count)
        {
            return gridColors[i];

        }
        else
        {
            return "N";
        }
    }
    public void SetGridFree(int i)
    {
        isGridFree[i] = true;

    }

    public void SetGridBusy(int i)
    {
        isGridFree[i] = false;

    }

    public List<bool> GetGridStatusList()
    {
        return isGridFree;

    }
    public List<GameObject> GetGridList()
    {
        return grids;

    }

    public List<int>[] GetAdjacencyList()
    {
        return gr.adjacencyList;

    }

    public Material GetGridMaterial(int i)
    {

        return materialsForGrids[i];
    }

    public float GetTimeForOneUnit()
    {
        return TimeForOneUnit;
    }

}
