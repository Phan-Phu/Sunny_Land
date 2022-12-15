using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextScore : MonoBehaviour
{
    Text textScore;
    private void Awake()
    {
        textScore = GetComponent<Text>();
    }
    private void OnEnable()
    {
        GameSession.Instance.updateScore.AddListener(UpdateScore);
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
        GameSession.Instance.updateScore.RemoveListener(UpdateScore);
    }
}
