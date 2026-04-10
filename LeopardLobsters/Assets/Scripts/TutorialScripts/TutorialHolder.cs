/* Author: Albert Tan
 * Utilized by: Victoria T.
 * 4/1/26
 ****************************/

using System;
using UnityEngine;
using UnityEngine.Events;

public class TutorialHolder : MonoBehaviour
{
    [TextArea (5, 6)]
    public string howToUse = "Place a TutorialPopup script on the object that   \n" +
        "needs to appear and drag that object into the TutorialList in the order that you want them to appear. \n" +
        "Make a unity event for the event you want to trigger going to the next popup with \n" +
        "Press the plus under the unity event and add the tutorial popup object\n" +
        "Make it trigger the Next function to go to the next popup. \n" +
        "Make it trigger back to go to a previous popup; leave it null to go the one before the current\n";
    
    [SerializeField]
    TutorialPopup[] TutorialList = new TutorialPopup[5];
    TutorialPopup cur => TutorialList[index];
    int index = 0;

    private void Start()
    {
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
}
