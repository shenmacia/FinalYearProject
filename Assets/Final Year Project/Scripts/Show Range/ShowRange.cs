using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowRange : MonoBehaviour
{
    [SerializeField] private GameObject radius;
    [SerializeField] private GameObject upgradePanel;


    public void OpenPanel()
    {
        if(upgradePanel != null)
        {
            bool isActive = upgradePanel.activeSelf;
            upgradePanel.SetActive(!isActive);
        }
    }

    public void OpenRadius()
    {
        if (radius != null)
        {
            bool isActive = radius.activeSelf;
            radius.SetActive(!isActive);
        }
    }
}
