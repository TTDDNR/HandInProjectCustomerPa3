using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CinematicsEndScript : MonoBehaviour
{
    private PlayableDirector director;
    [SerializeField] private AudioSource finalAudio;
    [SerializeField] private string sceneToSwitchTo;
    private bool alreadyPlaying = false;
    private void Awake()
    {
        director = gameObject.GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        if(director.state != PlayState.Playing && !alreadyPlaying)
        {
            print("getting there");
            alreadyPlaying = true;
            StartCoroutine(playSound());
        }
    }

    private IEnumerator playSound()
    {
        finalAudio.Play();
        yield return new WaitForSecondsRealtime(finalAudio.clip.length);
        SceneManager.LoadScene(sceneToSwitchTo);

    }
}
