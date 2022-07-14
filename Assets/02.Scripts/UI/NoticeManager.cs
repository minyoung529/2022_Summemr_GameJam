using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeManager : MonoBehaviour
{
    public NoticePanel panelPrefab;
    private static List<NoticePanel> panels = new List<NoticePanel>();

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            NoticePanel panel = Instantiate(panelPrefab, transform);
            panels.Add(panel);
        }
    }

    public static void AddNotice(Sprite sprite, string info)
    {
        NoticePanel panel = panels.Find(x => x.canUse);

        if (panel)
            panel.SetInfo(sprite, info);
    }
}
