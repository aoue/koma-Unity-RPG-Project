using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    //fade manager. used for fading to black and then back.
    /*
    - overworld -> event
	- overworld -> dungeon
	- dungeon -> event
	- dungeon -> combat
	- combat -> dungeon
	- dungeon -> overworld
    */

    //at scene start:
    // -object not active.

    [SerializeField] Image blackOutSquare;

    public void hide()
    {
        gameObject.SetActive(false);
    }

    public void fade_to_black(float time = 0.5f)
    { 
        gameObject.SetActive(true);
        StartCoroutine(fade(true, time));
    }
    public void fade_from_black(float time = 0.5f)
    {
        gameObject.SetActive(true);
        StartCoroutine(fade(false, time));
    }

    IEnumerator fade(bool fadeToBlack, float fadeSpeed = 0.5f)
    {
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount;
      
        if (fadeToBlack == true)
        {
            while ( blackOutSquare.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
            fade_from_black(fadeSpeed);
        }
        else
        {
            while ( blackOutSquare.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
            gameObject.SetActive(false);
        }
        
    }

}
