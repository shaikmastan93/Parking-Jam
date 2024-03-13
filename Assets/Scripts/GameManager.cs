using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject confetti;
    [SerializeField] private GameObject cryFace;
    [SerializeField] private GameObject smileFace;


    private List<GameObject> allCarsSpawned;

    private new void Awake()
    {
        allCarsSpawned = new List<GameObject>();

    }

    public void GameOver()
    {
        cryFace.SetActive(true);
        StartCoroutine(ReloadSceneRoutine());

    }

    private IEnumerator ReloadSceneRoutine()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddSpawnedCarsList(GameObject g)
    {
        allCarsSpawned.Add(g);
    }

    private void OnEnable()
    {
        MoveManager.checkIfGameEnded += CheckIfWin;
    }

    private void OnDisable()
    {
        MoveManager.checkIfGameEnded -= CheckIfWin;

    }

    private void CheckIfWin()
    {
        bool notFinished = false;


            for (int i = 0; i < allCarsSpawned.Count; i++) //check again the grid colors and car colors
            {
                GameObject currentGameObject = allCarsSpawned[i].gameObject;
                Car currentCar = currentGameObject.GetComponent<Car>();


                if (currentGameObject.tag == "SecondColor" && LevelManager.Instance.GetColorOfGrid(currentCar.GetCurrentGrid()) == "S")
                {
                    continue;
                }
                else if (currentGameObject.tag == "FirstColor" && LevelManager.Instance.GetColorOfGrid(currentCar.GetCurrentGrid()) == "F")
                {
                    continue;
                }
                else
                {
                    notFinished = true;
                    //not finished
                }
            }


            if (!notFinished) 
            {
                //win
                Sequence seq = DOTween.Sequence();

                for (int i = 0; i < allCarsSpawned.Count; i++)
                {

                    if (i == 0)
                    {
                        seq.Append(allCarsSpawned[i].transform.DOShakeScale(0.01f, 0.01f));
                        seq.Insert(1, allCarsSpawned[i].transform.DOShakeScale(10, 0.1f, 1));


                    }
                    else
                    {
                        seq.Insert(1, allCarsSpawned[i].transform.DOShakeScale(10, 0.1f, 1));

                    }

                }
                StartCoroutine(ScaleUpAndDownRoutine(seq));

            }
    }


    private IEnumerator ScaleUpAndDownRoutine(Sequence seq)
    {
        yield return new WaitForSeconds(0.5f);
        confetti.SetActive(true);

        yield return new WaitForSeconds(3f);
        seq.Play();

        yield return new WaitForSeconds(2f);
        smileFace.SetActive(true);
        yield return new WaitForSeconds(1f);
        StartCoroutine(ReloadSceneRoutine());


    }


}
