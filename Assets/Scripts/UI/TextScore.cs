using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextScore : MonoBehaviour
{
    Text textScore;
    GameSession gameSession;
    private void Awake()
    {
        gameSession = GameObject.FindGameObjectWithTag("Session").GetComponent<GameSession>();
        textScore = GetComponent<Text>();
    }
    private void OnEnable()
    {
        gameSession.updateScore.AddListener(UpdateScore);
    }
    private void Start()
    {
        UpdateScore(0);
    }
    private void UpdateScore(int score)
    {
        textScore.text = $"{score}";
    }
    private void OnDisable()
    {
        gameSession.updateScore.RemoveListener(UpdateScore);
    }
}
