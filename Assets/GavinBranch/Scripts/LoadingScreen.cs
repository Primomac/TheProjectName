using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public Image fill;
    public float maxFill;
    public float currentFill;
    public GameObject loading;
    public int sceneToLoad;


    public bool filling;
    public bool unfilling;
    public bool ClockWise = true;

    // Start is called before the first frame update
    void Start()
    {
        if(ClockWise == false)
        {
            fill.fillClockwise = false;
        }



        if(fill.fillAmount == 1)
        {
            unfilling = true;
            currentFill = maxFill;
            UnFill();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FillScreen()
    {


        filling = true;
        loading.SetActive(true);
        currentFill++;
        fill.fillAmount = currentFill/maxFill;

        StartCoroutine(delay());

        if(currentFill >= maxFill)
        {
            filling = false;
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void UnFill()
    {
        currentFill--;
        fill.fillAmount = currentFill / maxFill;

        StartCoroutine(delay());
        if (currentFill <= 0)
        {
            unfilling = false;
            loading.SetActive(false);
        }
    }


    IEnumerator delay()
    {
        yield return new WaitForSeconds(.01f);
        

        if(filling)
        {
            FillScreen();
        }
        else if(unfilling)
        {
            UnFill();
        }

    }

}
