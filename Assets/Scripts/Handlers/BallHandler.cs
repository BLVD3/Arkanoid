using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    public static BallHandler Instance { get; private set; }
    
    private readonly List<Ball> _balls = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public void SpawnBall()
    {
        GameObject newBall = Instantiate(ballPrefab);
        newBall.transform.position = new Vector3(0, 0, 1);
        _balls.Add(newBall.GetComponent<Ball>());
    }

    public void DuplicateBalls()
    {
        Ball[] balls = _balls.ToArray();
        foreach (Ball ball in balls)
        {
            Ball newBall = Instantiate(ball);
            newBall.ReverseVelocity();
            if (ball.HasPenetration)
                newBall.ActivatePenetration();
            _balls.Add(newBall);
        }
    }

    public void DeleteAllBalls()
    {
        Ball[] balls = _balls.ToArray();
        foreach (Ball ball in balls)
        {
            Destroy(ball);
        }
    }

    public void BallDestroyed(Ball ball)
    {
        _balls.Remove(ball);
        if (_balls.Count == 0)
        {
            GameHandler.Instance.NoBallsLeft();
        }
    }

    public void TriggerPenetration()
    {
        foreach (Ball ball in _balls)
        {
            ball.ActivatePenetration();
        }
    }
}
