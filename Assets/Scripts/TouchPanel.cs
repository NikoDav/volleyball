using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchPanel : MonoBehaviour
{
    [SerializeField] private List<Image> _touchLights = new List<Image>();
    [SerializeField] private GameManager _gameManager;

    public void AddLight()
    {
        if (_gameManager.RightSide)
        {
            if (_gameManager.TeamTouchRight - 1 < 0)
                _touchLights[0].color = Color.red;
            else
            _touchLights[_gameManager.TeamTouchRight - 1].color = Color.red;
        }

        else
        {
            if (_gameManager.TeamTouchLeft - 1 < 0)
                _touchLights[0].color = Color.red;
            else
                _touchLights[_gameManager.TeamTouchLeft - 1].color = Color.red;
        }
    }

    public void ResetLights()
    {
        foreach (var light in _touchLights)
            light.color = Color.white;
    }
}
