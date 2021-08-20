using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DmgText : MonoBehaviour
{
    [SerializeField] private Text repText;

    public void fill(string toShow, bool isHeal, float duration)
    {

        if (isHeal)
        {
            repText.color = Color.green;
        }
        else
        {
            repText.color = Color.red;
        }
        repText.text = toShow;

        Destroy(this.gameObject, duration);
    }


}
