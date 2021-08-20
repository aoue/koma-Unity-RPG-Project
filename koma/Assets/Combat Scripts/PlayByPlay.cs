using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayByPlay : MonoBehaviour
{
    //part of the combat tri menu.
    //shows the combat's play by play.

    //that means:
    // 'name' uses 'move'
    // ' deals x dmg' (for as many targets as were hit.)

    [SerializeField] private Text playByPlayText;
    private int key; //used to manage sync of possible various type() coroutines running at once.

    bool oneOnLine = false; //used for displaying only 1 thing on a line at a time.

    void Start()
    {
        key = -1;
    }
    
    public void typeItOut(string s)
    {
        //shows the input in playByPlayText, but typing it out, like.
        if (key > 1000) key = 0;
        else key++;
        StartCoroutine(type(s, key));
    }
    IEnumerator type(string s, int k)
    {       
        char[] toType = s.ToCharArray();
        float le = (float)toType.Length;
        playByPlayText.text = "";
        for (int i = 0; i < toType.Length; i++)
        {
            if (k != key) break;

            playByPlayText.text += toType[i];
            yield return new WaitForSeconds(1.0f / le);
        }
    }
    public void show(string play)
    {
        forget();
        playByPlayText.text = play;
        gameObject.SetActive(true);
    }
    public void add(string add)
    {
        if (oneOnLine == true)
        {
            playByPlayText.text += " " + add;
            oneOnLine = false;
        }
        else
        {
            playByPlayText.text += "\n" + add;
            oneOnLine = true;
        }
        
    }
    public void hide()
    {
        gameObject.SetActive(false);

    }
    public void forget()
    {
        oneOnLine = false;
    }
}
