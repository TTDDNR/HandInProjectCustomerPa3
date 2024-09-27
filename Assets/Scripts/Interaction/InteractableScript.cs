using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableScript : MonoBehaviour
{
    public bool MeNext = false;
    [Header("Sounds")]
    [SerializeField] List<AudioClip> thingsToPlayQuest;
    [SerializeField] List<AudioClip> thingsToPlay;

    [Header("Important Info")]
    [SerializeField] private bool isPhone;
    [SerializeField] private bool isBill;
    [SerializeField] private bool isCouch;
    [SerializeField] private bool isTV;
    [SerializeField] private bool isCookies;
    public QuestManager questManager;

    [Header("Quest Manager Extra Options")]
    [SerializeField] private bool dissapearAfterQuest;
    public int timesVisisted = 0;

    [Header("Extra Info Bill")]
    [SerializeField] private List<GameObject> billPositions;
    [SerializeField] private GameObject billParent;
    [SerializeField] private int timesGone = 0;
    [SerializeField] private bool readBookNow;

    [Header("OtherObjects")]
    [SerializeField] private bool doIShowObjects;
    [SerializeField] private bool doIHideObjects;
    [SerializeField] private List<GameObject> ObjectsToDoSomethingWith;

    [Header("Animations")]
    [SerializeField] private bool doNotUseSound;
    [SerializeField] private AnimatorCode animator;
    [SerializeField] private AnimatorCode animator2;

    [Header("Room Animations")]
    [SerializeField] private List<AnimatorCode> animators;
    [SerializeField] bool reactOnEnter;
    [SerializeField] bool reactOnLeave;


    public void Awake()
    {
        //Get the quest manager
        questManager = FindObjectOfType<QuestManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.Mouse0) && !reactOnEnter && !reactOnLeave)
        {
            var player = other.gameObject.GetComponent<PlayerMovementScript>();
            //Do something
            if (!doNotUseSound && !player.playingMinigame && !player.stopMoving)
            {
                
                if (!player.isAlreadyTalking && !isPhone && !isBill )
                {
                    print("Interaction Started");
                    if (MeNext)
                    {
                        print("DID GET HERE THOUGH?!?");
                        player.isAlreadyTalking = true;
                        StartCoroutine(playSound(player.dialoguePlayer, player, timesVisisted));
                        questManager.whereInQuestList++;
                        
                    }
                    else
                    {
                        player.dialoguePlayer.clip = thingsToPlay[0];
                        player.dialoguePlayer.Play();
                    }
                }
                else if (!player.isAlreadyTalking)
                {
                    print("Interaction Phone Started");
                    if (MeNext && isPhone)
                    {
                        player.phoneAmbient.TogglePlaying(false);
                        player.isAlreadyTalking = true;
                        StartCoroutine(playSound(player.dialoguePlayer, player, timesVisisted));
                        questManager.whereInQuestList++;
                    }
                    else if (MeNext && isBill)
                    {
                        StartCoroutine(playSound(player.dialoguePlayer, player, timesGone));
                    }
                    else
                    {
                        player.dialoguePlayer.clip = thingsToPlay[0];
                        player.dialoguePlayer.Play();
                    }
                }
            }
            else if (!player.playingMinigame && !player.stopMoving)
            {
                if(animator != null)
                {
                    animator.PlayAnimation();
                }

                if(animator2 != null)
                {
                    animator2.PlayAnimation();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && reactOnEnter && MeNext)
        {
            var player = other.gameObject.GetComponent<PlayerMovementScript>();
            foreach (AnimatorCode animator in animators)
            {
                animator.PlayAnimation();
            }

            if(ObjectsToDoSomethingWith[0] != null && dissapearAfterQuest)
            {
                ObjectsToDoSomethingWith[0].SetActive(true);
                questManager.whereInQuestList++;
            }

            player.isAlreadyTalking = true;
            StartCoroutine(playSound(player.dialoguePlayer, player, timesVisisted));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && reactOnLeave && MeNext)
        {
            var player = other.gameObject.GetComponent<PlayerMovementScript>();

            player.isAlreadyTalking = true;
            questManager.whereInQuestList++;
            StartCoroutine(playSound(player.dialoguePlayer, player, timesVisisted));
        }
    }

    private IEnumerator Lights(bool onOff, float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        ObjectsToDoSomethingWith[0].SetActive(onOff);
    }

    private IEnumerator playSound(AudioSource source, PlayerMovementScript player, int whichToPlay = 0, bool UpAlzheimer = false)
    {
        timesVisisted++;
        if (!isBill)
        {
            MeNext = false;
        }

        if ((isCouch && timesVisisted == 2) || isTV) //Glitching
        {
            print("Got In Here");
            player.phoneAmbient.TogglePlaying(true);

            if(isTV)
                player.musicHandler.AddInstrument(3);
        }

        if (isBill && MeNext && !dissapearAfterQuest) //the bill
        {
            if(animator != null)
            {
                animator.PlayAnimation();
            }
            if (animator2 != null)
            {
                animator2.PlayAnimation();
            }
            yield return new WaitForSeconds(0.417f);
            if (timesGone < 2)
            {
                print("Teleporting");
                billParent.transform.position = billPositions[timesGone + 1].transform.position;
                billParent.transform.rotation = billPositions[timesGone + 1].transform.rotation;
                timesGone++;
            }
            else
            {
                player.musicHandler.AddInstrument(2);
                questManager.whereInQuestList++;
                MeNext = false;
                gameObject.SetActive(false);
            }
        }
        else if(isBill && MeNext && dissapearAfterQuest) //Banana
        {
            if (timesGone < 1)
            {
                print("Teleporting");
                billParent.transform.position = billPositions[timesGone + 1].transform.position;
                billParent.transform.rotation = billPositions[timesGone + 1].transform.rotation;
                timesGone++;
            }
            else
            {
                player.musicHandler.AddInstrument(1);
                questManager.whereInQuestList++;
                MeNext = false;
                player.BANANADONE(gameObject, true);
                //gameObject.SetActive(false);
            }
        }

        if (animator != null) //for closet
        {
            animator.PlayAnimation();
        }
        if (animator2 != null) //for closet
        {
            animator2.PlayAnimation();
            doNotUseSound = true;
        }

        if(isPhone && timesVisisted == 3) //Lights
        {
            StartCoroutine(Lights(false, 9f));
        }

        if (thingsToPlayQuest[whichToPlay] != null) //Check if there is a line
        {
            source.clip = thingsToPlayQuest[whichToPlay];
            source.Play();
            yield return new WaitForSecondsRealtime(thingsToPlayQuest[whichToPlay].length);
        }

        if (isPhone)
        {
            if (timesVisisted == 2)
            {
                player.minigameManager.StartGame(Games.guess, word: "BANANA", imageNum: 0, finishNum: 0);
            }
        }

        if (readBookNow && timesVisisted % 2 != 0)
        {
            player.bookToRead.SetActive(true);
            player.bookToRead.GetComponent<bookScript>().StartReading();
            player.stopMoving = true;
        }

        if (UpAlzheimer)
            player.alzheimerStage++;

        if(reactOnEnter && !dissapearAfterQuest)
        {
            print("Got at lights");
            StartCoroutine(Lights(true, 1f));
            player.phoneAmbient.TogglePlaying(true);
            questManager.whereInQuestList++;
        }

        if(dissapearAfterQuest && !isBill && !reactOnEnter)
        {
            gameObject.SetActive(false);

            if(isCookies)
                player.phoneAmbient.TogglePlaying(true);
        }

        /*if(timesVisisted == 7 && isPhone)
        {
            player.ChangeWallTexture();
        }*/

        player.isAlreadyTalking = false;
    }
}
