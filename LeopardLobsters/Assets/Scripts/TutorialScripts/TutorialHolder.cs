

using System;
using UnityEngine;
using UnityEngine.Events;

public class TutorialHolder : MonoBehaviour
{
    [SerializeField]
    TutorialPopup[] TutorialList = new TutorialPopup[5];
    TutorialPopup cur => TutorialList[index];
    int index = 0;
    public static BaseTower curTower;

    [SerializeField]
    [Tooltip("use either this or tutorial list")]
    GameObject TutorialListObject;

    private void Start()
    {
        if (!Global.inTutorial) { return; }
        Global.inTutorial = false;

        //gets a list from the object children
        if (TutorialListObject) {
            int childC = TutorialListObject.transform.childCount;
            TutorialList = new TutorialPopup[childC];
            for (int i = 0; i < childC; i++) {
                TutorialList[i] = TutorialListObject.transform.GetChild(i).GetComponent<TutorialPopup>();
            }
        }

        OpenNew(cur);
    }

    void NextPopup() {

        CloseOld(cur);

        index++;
        if (index >= TutorialList.Length || cur == null) { 
            return; 
        }

        OpenNew(cur);
    }

    void GoBack(TutorialPopup prev) {

        if (prev = null) { //when no tutorial popup is put in it automatically goes back one
            CloseOld(cur);
            index--;
            if(index<0) Debug.LogError("Tutorial Holder trying to prev where isnt one");
            OpenNew(cur);
        }

        for (int i = index; i >= 0; i--) {
            if (TutorialList[i] = prev) {
                CloseOld(cur);
                OpenNew(TutorialList[i]);
            }
        }
        Debug.LogError("Tutorial Holder Cannot Find Prev");
    }

    void CloseOld(TutorialPopup popUp) {
        popUp.gameObject.SetActive(false);
        popUp.next.RemoveListener(NextPopup);
        popUp.back.RemoveListener(GoBack);
    }
    void OpenNew(TutorialPopup popUp) {
        popUp.next.AddListener(NextPopup);
        popUp.back.AddListener(GoBack);
        popUp.gameObject.SetActive(true);
        popUp.started.Invoke();
    }

    public void tutorialDone() {  }
}
