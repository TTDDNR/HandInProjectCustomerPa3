using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GuessTheWord : MonoBehaviour
{
    private string wordQuess;
    private string word;
    [SerializeField] private GameObject PhoneGame;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image itemImage;
    public List<AudioClip> afterGameToPlay;

    [SerializeField] private List<Sprite> itemImages;

    public int numberToGive = -1;
    private MinigameManager manager;

    public void Awake()
    {
        manager = GetComponentInParent<MinigameManager>();
    }
    public void StartGame(string WordToUse, int imageNum, int numberForFinish)
    {
        text.text = "";
        word = WordToUse;
        itemImage.sprite = itemImages[imageNum];
        PhoneGame.SetActive(true);
        numberToGive = numberForFinish;
    }


    public void EndGame()
    {
        manager.EndGame(Games.guess);
        PhoneGame.SetActive(false);
        word = null;
        wordQuess = null;
    }


    public void AddLetter(string LetterToAdd)
    {
        wordQuess += LetterToAdd;
        text.text = wordQuess;
        text.DOColor(Color.white, 1f);
    }

    public void RemoveLetter()
    {
        if (wordQuess != null)
        {
            if (wordQuess.Length > 0)
                wordQuess = wordQuess.Remove(wordQuess.Length - 1);

            text.text = wordQuess;
            text.DOColor(Color.white, 1f);
        }
    }

    public void SubmitAnswer()
    {
        if (wordQuess != null && wordQuess.Length > 0)
        {
            if (word.ToLowerInvariant() == wordQuess.ToLowerInvariant())
            {
                EndGame();
                print("You Did It");
            }
            else
            {
                text.DOColor(Color.red, 0.2f);
                print("Wrong");
            }
        }
    }

}
