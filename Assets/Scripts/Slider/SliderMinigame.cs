using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class SliderMinigame : MonoBehaviour
{
    [SerializeField] Image Bar;
    [SerializeField] Image CorrectSpot;
    [SerializeField] Image Slider;
    [SerializeField] Image LongBar;
    private MinigameManager manager;

    private PlayerMovementScript player;

    private bool gameHasStarted = false;

    public bool spacePressed = false;

    [Range(1, 1000)]
    public int speed;

    float moveSlider = 1;

    public void StartMiniGame(int speedGotten)
    {
        if (!gameHasStarted)
        {
            gameObject.SetActive(true);
            speed = speedGotten;
            spacePressed = false;
            CorrectSpot.rectTransform.position = new Vector3(0, 0, 0);

            int randomX = Random.Range(-150, 150);
            CorrectSpot.rectTransform.position = new Vector3(Bar.rectTransform.position.x + randomX, Bar.rectTransform.position.y, 0);
            print("Set Pos 1");
            gameHasStarted = true;
        }
    }

    public void Awake()
    {
        manager = GetComponentInParent<MinigameManager>();
        player = FindObjectOfType<PlayerMovementScript>();
    }

    private void Update()
    {
        if (!spacePressed)
            MoveSlider();
    }

    public void MoveSlider()
    {
        print(Bar.rectTransform.rect.width + " & " + Bar.rectTransform.rect.height);
        if (Bar.rectTransform.position.x + Bar.rectTransform.rect.width / 2 < Slider.rectTransform.position.x)
        {
            moveSlider = -1;
        }

        if (Bar.rectTransform.position.x - Bar.rectTransform.rect.width / 2 > Slider.rectTransform.position.x)
        {
            moveSlider = 1;
        }

        Slider.rectTransform.position += new Vector3(moveSlider, 0, 0) * Time.deltaTime * speed;

        var slider = Slider.rectTransform;
        var scale = (slider.localPosition.x + 200);

        LongBar.transform.localScale = new Vector3(scale, 1, 1);

        if (Input.GetKey(KeyCode.Mouse0))
        {

            if (Slider.rectTransform.position.x <= CorrectSpot.rectTransform.position.x + CorrectSpot.rectTransform.rect.width / 2 && Slider.rectTransform.position.x >= CorrectSpot.rectTransform.position.x - CorrectSpot.rectTransform.rect.width / 2)
            {
                print("HIT");
                spacePressed = true;
                gameObject.SetActive(false);
                FindAnyObjectByType<PlayerMovementScript>().playingMinigame = false;
                player.bookToRead.GetComponent<bookScript>().secondPageVisited++;
                manager.EndGame(Games.slider);
                gameHasStarted = false;
            }
            else
            {
                StartCoroutine(WaitABit());
                print("Retry");
                spacePressed = true;
            }
        }
    }

    public IEnumerator WaitABit()
    {
        CorrectSpot.DOColor(Color.red, 0.4f);

        yield return new WaitForSeconds(0.5f);

        CorrectSpot.DOColor(Color.white, 0.1f);

        int randomX = Random.Range(-150, 150);
        CorrectSpot.rectTransform.position = new Vector3(Bar.rectTransform.position.x + randomX, Bar.rectTransform.position.y, 0);
        print("Set Pos 2");
        spacePressed = false;
    }
}
