using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquareBehaviour : MonoBehaviour
{
    public Image myself;

    public bool isTouchingBeginning;
    public bool isBeginning;
    public bool isEnd;

    public void TurnSquare()
    {
        isTouchingBeginning = false;
        myself.rectTransform.Rotate(0, 0, 90);
    }

    public void Update()
    {
        if(isTouchingBeginning && isEnd)
        {
            GetComponentInParent<SetUpWaterWorksMinigame>().EndGame();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print("Has Collision Stay");
        if (collision.GetComponent<SquareBehaviour>() != null)
        {
            
            print("Getting in the stay");
            var square = collision.GetComponent<SquareBehaviour>();

            if (!isBeginning)
            {
                if (isTouchingBeginning)//If you are already touching, check neighbours
                {
                    square.isTouchingBeginning = isTouchingBeginning;
                }
                else
                {
                    isTouchingBeginning = false;
                }
            }
            else
            {
                square.isTouchingBeginning = isBeginning;
            }
        }
    }
}
