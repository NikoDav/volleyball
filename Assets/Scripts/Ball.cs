using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameManager _gameManager;

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out RightSide rightSide))
        {
            _gameManager.SetSide(true);
        }
        else if(collision.TryGetComponent(out LeftSide leftSide))
        {
            _gameManager.SetSide(false);
        }

        if(collision.TryGetComponent(out Floor floor))
        {
            if (_gameManager.GetSide())
            {
                _gameManager.AddPoints(false);
            }
            else
            {
                _gameManager.AddPoints(true);
            }
        }
    }
}
