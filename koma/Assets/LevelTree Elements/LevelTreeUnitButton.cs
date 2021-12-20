using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTreeUnitButton : MonoBehaviour
{
    [SerializeField] private Image unitPortraitSlot;
    [SerializeField] private Text nameText;


    public void fill(Sprite sp, string te)
    {
        nameText.text = te;
        unitPortraitSlot.sprite = sp;
    }

}
