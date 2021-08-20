using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonHealer : MonoBehaviour
{
    //manages the player interaction element of the dungeon heal capabilities
    //just the gameobject visuals, etc.

    [SerializeField] private DungeonManager theBoss;
    [SerializeField] private Text promptText; //tells whether we can heal to full or not. tells stam cost.
    [SerializeField] private Button healButton;

    public void show(string prompt, bool canHeal)
    {
        healButton.interactable = canHeal;
        promptText.text = prompt;
        gameObject.SetActive(true);
    }
    public void hide()
    {
        gameObject.SetActive(false);
    }



}
