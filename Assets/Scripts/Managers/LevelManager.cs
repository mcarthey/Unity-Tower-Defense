using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int _lives = 10;

    public int TotalLives { get; private set; }

    private void Start()
    {
        TotalLives = _lives;
    }
    private void ReduceLives(Enemy enemy)
    {
        TotalLives--;
        if (TotalLives <= 0)
        {
            TotalLives = 0;
            // TODO Game Over Logic
        }
    }

    private void OnEnable()
    {
        Enemy.OnEndReached += ReduceLives;
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= ReduceLives;
    }
}
