using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public enum Games { slider, guess };

public class MinigameManager : MonoBehaviour
{
    //public SetUpWaterWorksMinigame waterWorksMinigame;
    [SerializeField] private PlayerMovementScript player;
    [SerializeField] private SliderMinigame sliderminigame;
    [SerializeField] private GuessTheWord guessTheWord;

    public void EndGame(Games gamePlayed)
    {
        player.playingMinigame = false;
        if (gamePlayed == Games.slider)
        {
            player.bookToRead.GetComponent<bookScript>().FlipPage();
        }

        if (gamePlayed == Games.guess)
        {
            player.dialoguePlayer.clip = guessTheWord.afterGameToPlay[guessTheWord.numberToGive];
            player.dialoguePlayer.Play();
        }
    }

    public void StartGame(Games gameToPlay, int speed = 100, string word = null, int imageNum = -1, int finishNum = -1)
    {
        player.playingMinigame = true;

        switch (gameToPlay)
        {
            case Games.slider:
                sliderminigame.StartMiniGame(speed);
                break;

            case Games.guess:
                guessTheWord.StartGame(word, imageNum, finishNum);
                break;
        }

    }
}
