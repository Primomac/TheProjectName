using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDifferentMenu : MonoBehaviour
{
    public GameObject originalMenu;
    public GameObject menuToDisplay;

    public void SwapUIMenu()
    {
        originalMenu.SetActive(false);
        menuToDisplay.SetActive(true);
    }
}
