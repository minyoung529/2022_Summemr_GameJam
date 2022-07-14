using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    public void OpenUI(GameObject ui)
    {
        ui.SetActive(true);
    }
    public void CloseUI(GameObject ui)
    {
        ui.SetActive(false);
    }
    
   
    public void Quit()
    {
        Application.Quit();
    }

    public void SceneChange(string str)
    {
        SceneManager.LoadScene(str);
    }

}
