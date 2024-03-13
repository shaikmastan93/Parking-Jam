using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : Singleton<LineManager>
{
    [SerializeField] private Transform yellowCarPrefab;
    [SerializeField] private Transform purpleCarPrefab;


    private List<GameObject> carInYellowLine;
    private List<GameObject> carInPurpleLine;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
