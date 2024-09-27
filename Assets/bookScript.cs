using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class bookScript : MonoBehaviour
{
    [SerializeField] private List<Sprite> pages;
    [SerializeField] private Image page;
    [SerializeField] private PlayerMovementScript player;
    [SerializeField] private List<AudioClip> afterReading;
    public int onPage = -1;
    public int secondPageVisited = -1;

    public void StartReading()
    {
        FlipPage();
    }

    public void TryToFlipPage()
    {
        if (player.minigameManager != null && onPage % 2 != 1 && secondPageVisited < 2)
        {
            player.minigameManager.StartGame(Games.slider);
        }
        else
        {
            gameObject.SetActive(false);
            player.stopMoving = false;
            player.dialoguePlayer.clip = afterReading[secondPageVisited];
            player.dialoguePlayer.Play();

            if(secondPageVisited == 0)
            {
                player.phoneAmbient.TogglePlaying(true);
            }
        }
    }

    public void FlipPage()
    {
        onPage++;
        if(onPage < pages.Count - 1)
            page.sprite = pages[onPage];
    }
}
