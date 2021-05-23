using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShopOpener : MonoBehaviour
{

    public GameObject TurretShopPanel;


    public void TurretShopOpen()
    {
        if(TurretShopPanel != null)
        {
            bool isActive = TurretShopPanel.activeSelf;
            TurretShopPanel.SetActive(!isActive);
        }
    }

}
