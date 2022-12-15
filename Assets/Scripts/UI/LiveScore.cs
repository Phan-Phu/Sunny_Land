using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LiveScore : MonoBehaviour
{
    Text liveScore;
    private void Awake()
    {
        liveScore = GetComponent<Text>();
    }
    private void Start()
    {
        UpdateLive(3);
    }
    private void OnEnable()
    {
        GameSession.Instance.updateLives.AddListener(UpdateLive);
    }
    private void UpdateLive(int live)
    {
        liveScore.text = $"{live}";
    }
    private void OnDisable()
    {
        GameSession.Instance.updateLives.RemoveListener(UpdateLive);
    }
}
