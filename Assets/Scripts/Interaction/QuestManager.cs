using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<InteractableScript> questItems;
    [SerializeField] private string sceneToSwitchTo;

    public int whereInQuestList = 0;
    private int prevWhereInQuestList = -1;

    // Update is called once per frame
    void Update()
    {
        if (whereInQuestList != prevWhereInQuestList && whereInQuestList <= questItems.Count - 1)
        {
            questItems[whereInQuestList].MeNext = true;
            prevWhereInQuestList = whereInQuestList;
        }

        if (whereInQuestList > questItems.Count - 1)
        {
            SceneManager.LoadScene(sceneToSwitchTo);
        }
    }
}
