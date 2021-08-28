using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum targeting { Heal, Random, LeastHP, MostHP, LeastPdef, LeastMdef, Front, Back, AOE, BestAffMult, Self, HighestBreakLevel }
public class EnemyMove : Move
{
    // enemyMove, which includes some information about how a move should be targeted.
    // just ints and stuff that the ai reads and interprets.
    // you know the drill, done it before.

    [SerializeField] private targeting targeting_preference;
    
    public targeting get_targeting_preference() { return targeting_preference; }
}
