using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendMove : Move
{
    //has all the stuff a move needs, even if it doesn't need it.
    //other defend moves are children of this guy here.

    [SerializeField] protected float amount;
    [SerializeField] protected float amount_ceiling;
    [SerializeField] protected int dur;
    [SerializeField] protected int dur_ceiling;
    [SerializeField] protected string floating_message; //shows in the damage spot of the move.

    public string get_floating_message() { return floating_message; }

    public virtual string generate_defend_string()
    {
        //returns a version of the move string, but geared more for
        //defend moves.
        //we've got 4 lines.

        string toReturn = "";
        toReturn += get_nom() + " | All AP\nPdef -> Pdef+ |" 
            + get_xSize() + "x" + get_ySize() + "\n\n"
            + get_flavour();
        return toReturn;
    }

    public virtual void apply_defend_move(Unit u)
    {
        //here is where the defend move actually does things.
        //overriden for other classes, but this is also the base defend move.

        //does: pdef up.
        u.status.pdef_up(amount, amount_ceiling, dur, dur_ceiling);
    }

}
