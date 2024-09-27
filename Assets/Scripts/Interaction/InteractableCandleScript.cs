using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Actual Useable cases for phone uneven (maybe)

public class InteractableCandleScript : MonoBehaviour
{
    [SerializeField] private List<AudioClip> thingsToPlay;
    [SerializeField] private GameObject Light;
    private QuestManager questManager;
    private bool isOn = false;
    

    public void Awake()
    {
        questManager = FindObjectOfType<QuestManager>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.Mouse0))
        {
            isOn = !isOn;
        }

        //SOUNDS?!?!?
    }

    public void Update()
    {
        Light.SetActive(isOn);
    }

    IEnumerator PlaySounds(AudioSource sound, int soundToPlay, PlayerMovementScript player, string wordToUse = null, int imageToUse = -1, int numberForFinish = -1)
    {
        player.isAlreadyTalking = true;
        sound.clip = thingsToPlay[soundToPlay];
        sound.Play();
        print("ONE " + thingsToPlay[soundToPlay].length);
        yield return new WaitForSecondsRealtime(thingsToPlay[soundToPlay].length);
        print("TWO");

        player.isAlreadyTalking = false;
    }
}
