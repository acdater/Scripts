using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // SetActive two panel (start and finish of the game)

    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _finishPanel;
    private void Start()
    {
        _startPanel.SetActive(true);
    }

    public void SetFinishPanel()
    {
        _finishPanel.SetActive(true);
    }
}

   
