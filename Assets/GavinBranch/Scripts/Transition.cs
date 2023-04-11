using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public Slider slider;
    public float currentValue;
    public int speed;
    public int sceneToLoad;


    public void startTransition()
    {
        if(currentValue*2 == slider.maxValue)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        slider.maxValue = speed;
        slider.value = currentValue;
        currentValue++;
        StartCoroutine(delay());
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(.01f);
        startTransition();
    }
    private void Start()
    {
        if(currentValue == 101)
        {
            startTransition();
        }
    }
}
