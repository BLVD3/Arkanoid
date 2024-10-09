using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject startLayer;
    [SerializeField] private GameObject inGameLayer;
    [SerializeField] private GameObject finishLayer;

    [SerializeField] private GameObject[] hpIndicators;
    [SerializeField] private TextMeshProUGUI[] scoreLabels;
    [SerializeField] private TextMeshProUGUI wonLostLabel;
    
    public void StartGamePressed()
    {
        GameHandler.Instance.StartGame();
        startLayer.SetActive(false);
        inGameLayer.SetActive(true);
    }

    public void ScoreChanged(int score)
    {
        foreach (TextMeshProUGUI label in scoreLabels)
        {
            label.SetText(score.ToString());
        }
    }

    public void HpChanged(int hp)
    {
        for (int i = 0; i < Mathf.Min(hp, hpIndicators.Length); i++)
        {
            hpIndicators[i].SetActive(true);
        }

        for (int i = hp; i < hpIndicators.Length; i++)
        {
            hpIndicators[i].SetActive(false);
        }
    }
    
    public void DisplayFinish(bool hasWon)
    {
        inGameLayer.SetActive(false);
        finishLayer.SetActive(true);
        wonLostLabel.SetText(hasWon ? "You Won" : "You Lost");
    }
}
