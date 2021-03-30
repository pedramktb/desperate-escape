using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Action OnTutorialComplete;
    public void ShowTutorialPanel(){

    }

    public void TutorialDone(){
        OnTutorialComplete.Invoke();
    }
}
