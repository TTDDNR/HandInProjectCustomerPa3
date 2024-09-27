using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundHandler : MonoBehaviour
{
    [SerializeField] private AudioSource soundHandler; //Private? Maybe
    private bool shouldBePlaying;
    [SerializeField] private bool isPhone;
    private bool soundNotBusy = true;

    public void Update()
    {
        if(shouldBePlaying && !soundHandler.isPlaying && soundNotBusy)
        {
            if (isPhone)
            {
                StartCoroutine(playSounds());
            }
            else
            {
                soundHandler.Play();
            }
        }
        else if(!shouldBePlaying && soundHandler.isPlaying)
        {
            soundHandler.Stop();
        }
    }

    private IEnumerator playSounds()
    {
        soundHandler.Play();
        soundNotBusy = false;
        print("Got here");
        yield return new WaitForSeconds(17f);
        print("Got Past it");
        soundHandler.Stop();
        soundNotBusy = true;
    }

    public void TogglePlaying(bool toggleTo)
    {
        shouldBePlaying = toggleTo;
    }
}
