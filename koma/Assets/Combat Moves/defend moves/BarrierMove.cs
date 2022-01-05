using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierMove : DefendMove
{
    //a basic defend move for magic-oriented units.
    //increases mdef.

    public override string generate_defend_string()
    {
        //returns a version of the move string, but geared more for
        //defend moves.
        //we've got 4 lines.

        string toReturn = "";
        toReturn += get_nom() + " | All AP\nMdef -> Mdef+ |"
            + get_xSize() + "x" + get_ySize() + "\n\n"
            + get_flavour();
        return toReturn;
    }

    public override void apply_defend_move(Unit u)
    {
        //here is where the defend move actually does things.
        //overriden for other classes, but this is also the base defend move.

        //does: pdef up.
        u.status.mdef_up(amount, amount_ceiling, dur, dur_ceiling);
    }

}
