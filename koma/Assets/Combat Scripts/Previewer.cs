using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Previewer : MonoBehaviour
{
    //part of combat's the tri menu.
    //only job is to display:
    // -player's next scheduled move
    // -enemy's next scheduled move
    // -end of round scheduled move

    [SerializeField] private Text nextPlayerMoveText;
    [SerializeField] private Text nextEnemyMoveText;
    [SerializeField] private Text EORmoveText;


    public void show(Move pMove, Move eMove, Move eor)
    {
        if ( pMove == null )
        {
            nextPlayerMoveText.text = "None";
        }
        else
        {
            nextPlayerMoveText.text = pMove.get_nom();
        }

        if ( eMove == null )
        {
            nextEnemyMoveText.text = "None";
        }
        else
        {
            nextEnemyMoveText.text = eMove.get_nom();
        }

        if (eor == null)
        {
            EORmoveText.text = "None";
        }
        else
        {
            EORmoveText.text = eor.get_nom();
        }

        //converts each move argument into correct text form.
        gameObject.SetActive(true);
    }
    public void hide()
    {
        gameObject.SetActive(false);
    }

    public void highlight(int which)
    {
        //called when one of the previews is highlighted.
        //displays some more information about it.


    }

    


}
