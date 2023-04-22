using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using System;

public class StorySystem : MonoBehaviour
{
    private PlayableDirector playableDirector;
    [SerializeField] private GameObject character;
    [SerializeField] private float timeAppearCharacter = 0f;

    private void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        if (character) { character.SetActive(false); }

        if(GameObject.FindGameObjectWithTag("Session").TryGetComponent<GameSession>(out GameSession gameSession))
        {
            string nameScene = SceneManager.GetActiveScene().name;
            string name = nameScene.Substring(0, 5);
            if (name == "Story")
            {
                gameSession.ShowSkipStory(nameScene);
            }
        }
    }
    private void Update()
    {
        if (character == null) { return; }
        if (playableDirector.time >= timeAppearCharacter)
        {
            character.SetActive(true);
        }

        if (playableDirector.state != PlayState.Playing)
        {
            HidingStory();
        }
    }

    public void HidingStory()
    {
        NotifySceneManager notifySceneManager = GameObject.FindGameObjectWithTag("Notification").GetComponent<NotifySceneManager>();
        if (notifySceneManager == null) { return; }
        notifySceneManager.HideStory();
    }
}
