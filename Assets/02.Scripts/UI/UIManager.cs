using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    private List<Transform> windowStack = new List<Transform>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (windowStack.Count > 0)
            {
                Transform trn = windowStack[windowStack.Count - 1].transform;
                trn.DOScale(0f, 0.3f).OnComplete
                (() =>
                    trn.gameObject.SetActive(false)
                );
                windowStack.RemoveAt(windowStack.Count - 1);
            }
        }
    }

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

    public void ActiveWindow(GameObject window)
    {
        windowStack.Add(window.transform);

        window.transform.SetSiblingIndex(1);
        window.gameObject.SetActive(true);
    }

    public void EnactiveWindow(GameObject window)
    {
        Transform trn = windowStack.Find(x => x.gameObject == window);
        windowStack.Remove(trn);
    }
}