using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : ScreenManager
{
    public void ExitGame(){
        CustomSceneManager.Instance.ReturnToMainMenu();
    }
}
