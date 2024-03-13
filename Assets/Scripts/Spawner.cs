using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Spawner : Singleton<Spawner>
{
    public static Action reachedToTotalNumberOfCarLimit;


    [SerializeField] private Transform spawnPosYellow;
    [SerializeField] private Transform spawnPosYellow2;

    [SerializeField] private Transform spawnPosPurple;
    [SerializeField] private Transform spawnPosPurple2;

    [SerializeField] private GameObject yellowCarPrefab;
    [SerializeField] private GameObject purpleCarPrefab;
    [SerializeField] private int SpawnLimitYellow;
    [SerializeField] private int SpawnLimitPurple;


    private int totalSpawnedNumberPurple;
    private int totalSpawnedNumberYellow;

    private Queue<GameObject> carsInYellowLine;
    private Queue<GameObject> carsInPurpleLine;

   
    void Start()
    {

        
        totalSpawnedNumberPurple = 0;
        totalSpawnedNumberYellow = 0;


        carsInYellowLine = new Queue<GameObject>();
        carsInPurpleLine = new Queue<GameObject>();

        CreateYellowCar(spawnPosYellow.position);
        CreateYellowCar(spawnPosYellow2.position);

        CreatePurpleCar(spawnPosPurple.position);
        CreatePurpleCar(spawnPosPurple2.position);

  
    }



    private void OnEnable()
    {
        YellowButton.yellowObstacleOpened += HandleYellowMovement;
        PurpleButton.purpleObstacleOpened += HandlePurpleMovement;

    }
    private void OnDisable()
    {
        YellowButton.yellowObstacleOpened -= HandleYellowMovement;
        PurpleButton.purpleObstacleOpened -= HandlePurpleMovement;
    }

    private void HandleYellowMovement(float fl)
    {
        StartCoroutine(doFirstMoveForWaitingYellow(fl));
    }

    private IEnumerator doFirstMoveForWaitingYellow(float fl)
    {
        yield return new WaitForSeconds(fl);
        
        List<GameObject> grids = LevelManager.Instance.GetGridList();
        List<bool> gridStatuses = LevelManager.Instance.GetGridStatusList();

        GameObject ga = carsInYellowLine.Dequeue();

        Car c = ga.GetComponent<Car>();
        ga.transform.DOMove(grids[12].transform.position, 0.3f).SetEase(Ease.InOutSine);
        c.SetCurrentGrid(12);
        LevelManager.Instance.SetGridBusy(12);

       
         if (totalSpawnedNumberYellow != SpawnLimitYellow)
         {
            GameObject newHeadOfWaitLineYellow = carsInYellowLine.Peek();
            newHeadOfWaitLineYellow.transform.DOMove(spawnPosYellow.position, 1f).OnComplete(() => CreateYellowCar(spawnPosYellow2.position));
         }

        MoveManager.Instance.MoveTheCar(ga);

    }

        private void HandlePurpleMovement(float fl)
        {
             StartCoroutine(doFirstMoveForWaitingPurple(fl));
        }

    private IEnumerator doFirstMoveForWaitingPurple(float fl)
    {

            yield return new WaitForSeconds(fl);
            List<GameObject> grids = LevelManager.Instance.GetGridList();
            List<bool> gridStatuses = LevelManager.Instance.GetGridStatusList();

             GameObject ga = carsInPurpleLine.Dequeue();
     
            ga.transform.DOMove(grids[14].transform.position, 0.3f).SetEase(Ease.InOutSine);

            Car c = ga.GetComponent<Car>();
            c.SetCurrentGrid(14);
            LevelManager.Instance.SetGridBusy(14);

            if (totalSpawnedNumberPurple!= SpawnLimitPurple)
            {

             GameObject newHeadOfWaitLinePurple = carsInPurpleLine.Peek();
             newHeadOfWaitLinePurple.transform.DOMove(spawnPosPurple.position, 1f).OnComplete(() => CreatePurpleCar(spawnPosPurple2.position));
         
            }
       
            MoveManager.Instance.MoveTheCar(ga);
    }

    private void CreateYellowCar(Vector3 vec)
    {
        if (totalSpawnedNumberYellow< SpawnLimitYellow)
        {

            GameObject g = Instantiate(yellowCarPrefab, vec, Quaternion.identity);

            g.transform.Rotate(new Vector3(-90, 180, 0));
            Car c = g.GetComponent<Car>();
            c.SetCurrentGrid(-1);
            carsInYellowLine.Enqueue(g);

            totalSpawnedNumberYellow += 1;
            GameManager.Instance.AddSpawnedCarsList(g);
            if (carsInYellowLine.Count == 1 & totalSpawnedNumberYellow > 2)
            {
                g.transform.DOMove(spawnPosYellow.position, 1f);
            }

        }

    }

    private void CreatePurpleCar(Vector3 vec)
    {
       if (totalSpawnedNumberPurple<SpawnLimitPurple)
        {
            GameObject gg = Instantiate(purpleCarPrefab, vec, Quaternion.identity);

            gg.transform.Rotate(new Vector3(-90, 180, 0));
            Car c = gg.GetComponent<Car>();
            c.SetCurrentGrid(-1);
            carsInPurpleLine.Enqueue(gg);

            totalSpawnedNumberPurple += 1;
            GameManager.Instance.AddSpawnedCarsList(gg);
            if (carsInPurpleLine.Count==1 &totalSpawnedNumberPurple>2)
            {
                gg.transform.DOMove(spawnPosPurple.position, 1f);
            }
        }
    }

    public int GetSpawnLimitYellow()
    {
        return SpawnLimitYellow;
    }
    public int GetSpawnLimitPurple()
    {
        return SpawnLimitPurple;
    }



}
