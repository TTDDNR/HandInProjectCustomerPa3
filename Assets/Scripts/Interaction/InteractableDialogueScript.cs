using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDialogueScript : MonoBehaviour
{
    [SerializeField] List<AudioClip> thingsToPlay;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.Mouse0))
        {
            var sound = other.GetComponent<AudioSource>();
            var player = other.GetComponent<PlayerMovementScript>();
            if (!player.isAlreadyTalking && !player.playingMinigame && !player.stopMoving)
            {
                switch (other.gameObject.GetComponent<PlayerMovementScript>().alzheimerStage)
                {
                    case 0:
                        sound.clip = thingsToPlay[0];
                        break;
                    case 1:
                        sound.clip = thingsToPlay[1];
                        break;
                    default:
                        sound.clip = thingsToPlay[0];
                        break;
                }
                sound.Play();
            }
        }
    }
}
