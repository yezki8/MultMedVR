using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FruitSaberManager : MonoBehaviour
{
    public static FruitSaberManager Instance;
    //public GameObject defeatText;
    //public GameObject victoryText;
    //public GameObject restartText;
    public SpawnManager[] spawnClusters;
    public float minDelay = .1f;
    public float maxDelay = 1f;
    public float minBombDelay = 8.0f;
    public float maxBombDelay = 12.0f;
    public int fruitsPerInterval = 1;
    public int bombsPerInterval = 1;

    [SerializeField] bool DebugGameStart = false;
    float bombDelay;
    bool GameInProgress = false;

    #region Observer
    public event Action<Fruit, int> OnHitAction;
    public event Action<Bomb> OnHitActionBomb;

    public void InvokeOnHit(Fruit target, int fruitScore)
    {
        if (target != null)
        {
            OnHitAction?.Invoke(target, fruitScore);
        }
    }

    public void InvokeOnHitBomb(Bomb target)
    {
        if (target != null)
        {
            OnHitActionBomb?.Invoke(target);
        }
    }

    public void AddOnHitListener(Action<Bomb> listener)
    {
        OnHitActionBomb += listener;
    }

    public void RemoveOnHitListener(Action<Bomb> listener)
    {
        OnHitActionBomb -= listener;
    }

    public void AddOnHitListener(Action<Fruit, int> listener)
    {
        OnHitAction += listener;
    }

    public void RemoveOnHitListener(Action<Fruit, int> listener)
    {
        OnHitAction -= listener;
    }

    // Invoker (attach on Fruit.cs) GameManager.Instance.InvokeOnHit(this,someFloat);
    #endregion

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (DebugGameStart)
        {
            DebugGameStart = false;
            StartGame();
        }
    }

    public void StartGame()
    {
        if (!GameInProgress)
        {
            GameInProgress = true;
            StartCoroutine(SpawnBombs());
            StartCoroutine(SpawnFruits());
            for (int i = 0; i < spawnClusters.Length; i++)
            {
                spawnClusters[i].GetComponentInChildren<SineWaveDisplacer>().isActive = true;
            }
        }
    }

    public void EndGame()
    {
        if (GameInProgress)
        {
            GameInProgress = false;
            for (int i = 0; i < spawnClusters.Length; i++)
            {
                spawnClusters[i].GetComponentInChildren<SineWaveDisplacer>().isActive = false;
            }
        }
    }

    public void EndGameVictory()
    {
        Debug.Log("The player has won the game.");
        //victoryText.SetActive(true);
        //restartText.SetActive(true);
        EndGame();
    }

    public void EndGameDefeat()
    {
        Debug.Log("The player has lost the game.");
        //defeatText.SetActive(true);
        //restartText.SetActive(true);
        EndGame();
    }

    IEnumerator SpawnBombs()
    {
        while (GameInProgress)
        {
            bombDelay = UnityEngine.Random.Range(minBombDelay, maxBombDelay);
            yield return new WaitForSeconds(bombDelay);

            for (int i = 0; i < bombsPerInterval; i++)
            {
                spawnClusters[UnityEngine.Random.Range(0, spawnClusters.Length)].SpawnOneBomb();
            }
        }
    }

    IEnumerator SpawnFruits()
    {
        while (GameInProgress)
        {
            float delay = UnityEngine.Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            for (int i = 0; i < fruitsPerInterval; i++)
            {
                spawnClusters[UnityEngine.Random.Range(0, spawnClusters.Length)].SpawnOneFruit();
            }
        }
    }

}

