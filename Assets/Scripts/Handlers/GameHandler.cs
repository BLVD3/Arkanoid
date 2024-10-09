using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private UI ui;

    public static GameHandler Instance { get; private set; }

    private int _targetCount;
    private int _score;
    private int _hp = 3;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void StartGame()
    {
        BallHandler.Instance.SpawnBall();
    }

    public void NoBallsLeft()
    {
        _hp--;
        ui.HpChanged(_hp);
        if (_hp == 0)
            ui.DisplayFinish(false);
        else
            StartCoroutine(SpawnBallIn3());
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator SpawnBallIn3()
    {
        yield return new WaitForSeconds(2);
        BallHandler.Instance.SpawnBall();
    }

    public void AddTarget()
    {
        _targetCount++;
    }

    public void TargetDestroyed()
    {
        _targetCount--;
        if (_targetCount == 0)
        {
            ui.DisplayFinish(true);
            BallHandler.Instance.DeleteAllBalls();
        }
    }

    public void TargetHit()
    {
        _score++;
        ui.ScoreChanged(_score);
    }
}
