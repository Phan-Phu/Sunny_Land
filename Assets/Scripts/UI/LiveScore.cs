using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LiveScore : MonoBehaviour
{
    Text liveScore;
    GameSession gameSession;
    private void Awake()
    {
        gameSession = GameObject.FindGameObjectWithTag("Session").GetComponent<GameSession>();
        liveScore = GetComponent<Text>();
    }
    private void OnEnable()
    {
        gameSession.updateLives.AddListener(UpdateLive);
    }
    private void UpdateLive(int live)
    {
        liveScore.text = $"{live}";
    }
    private void OnDisable()
    {
        gameSession.updateLives.RemoveListener(UpdateLive);
    }
}
