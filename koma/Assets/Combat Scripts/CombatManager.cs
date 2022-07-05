using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum playerTurnPhase { NOT, EXECUTING, SELECTUNIT, SELECTMOVE, SELECTTARGET }
public enum whoseTurn { PLAYER, AI, WAIT };
public class CombatManager : MonoBehaviour
{
    //responsible for running the combat.
    //manages control flow, ui elements, and calculations.

    [SerializeField] private Sprite[] affOrbSprites;

    [SerializeField] private DungeonUnitBox[] partyBoxes; //boxes that hold the party. 0-5
    [SerializeField] private DungeonUnitBox[] enemyBoxes; //boxes that hold the enemies. 0-5
    [SerializeField] private Image activePortrait; //player's active portrait. shows the active player pilot image.
    [SerializeField] private Image enemyActivePortrait; //enemy's active portrait. shows the active enemy pilot image.

    [SerializeField] private Sprite emptySlotSprite; //used to fill empty dungeon boxes
    [SerializeField] private DmgText dmgTextPrefab;

    private BattleBrain brain;
    [SerializeField] private PrepDungeonManager pdm;
    [SerializeField] private LossManager loser;
    [SerializeField] private SoundManager SM; //handles playing music and sounds, baby.
    [SerializeField] private Highlighting highlighter;
    [SerializeField] private Previewer previews;
    [SerializeField] private PlayByPlay plays;
    [SerializeField] private MoveSelector moveSelector;
    [SerializeField] private PreviewSlot previewSlot;

    private int wavesNum; //number of waves left.
    private int battleXP;
    private Unit[] pl;
    private Queue<Enemy[]> waves;
    private Enemy[] el;
    private int plAp;
    private int elAp;
    private whoseTurn turn;
    private playerTurnPhase pTurn;
    private int stamina;
    private int round;
    private bool targetingDefendMove; //if true, then player is targeting with a defend move and it target the unit's own tile.
    private bool battleOver;
    private bool playerGoesFirst;

    public playerTurnPhase get_pTurn() { return pTurn; }
    public Unit[] get_pl() { return pl; }
    public Unit[] get_el() { return el; }

    public Unit currentUnit { get; set; }

    public Move playerScheduledMove { get; set; }
    public int playerScheduledMoveSpot { get; set;}
    public Unit playerScheduledUnit { get; set; }

    public Move enemyScheduledMove { get; set; }
    public int enemyScheduledMoveSpot { get; set; }
    public Enemy enemyScheduledUnit { get; set; }

    public Move eorScheduledMove { get; set; }
    public int eorScheduledMoveSpot { get; set; }
    public Unit eorScheduledUnit { get; set; } 
    public bool eorUserIsPlayer { get; set; }
    public bool eorHelperDOTs { get; set; }

    //STARTING BATTLE
    public void close()
    {
        //called when battle is well and truly over.
        gameObject.SetActive(false);
    }
    public void load_battle(Unit[] party, Enemy[][] inWaves, int threat)
    {
        //called from dungeon manager.
        //sets up player party, waves, stamina.
        battleOver = false;
        battleXP = 0;
        if (brain == null) brain = new BattleBrain();

        pl = party;
        for(int i = 0; i < pl.Length; i++)
        {
            if (pl[i] != null) { pl[i].create_status(); pl[i].place = i;  }
        }

        if (waves == null) waves = new Queue<Enemy[]>();
        waves.Clear();
        
        for (int i = 0; i < inWaves.Length; i++)
        {
            Enemy[] someArr = new Enemy[6];
            for(int j = 0; j < 6; j++)
            {
                if (inWaves[i][j] != null)
                {
                    someArr[j] = Instantiate(inWaves[i][j]);
                    //apply variance and threat based stat modifications to the mob  
                    //doing spawning_variance() second emphasizes the stat variations, which we want i guess.
                    someArr[j].stat_modify(threat); //virtual function.
                    someArr[j].spawning_variance(); //non-virtual.

                    //setup the monster for this battle.
                    someArr[j].create_status();
                    someArr[j].set_hp(someArr[j].get_hpMax_actual());
                }
            }
            waves.Enqueue(someArr);
        }
               
        wavesNum = waves.Count;
        el = waves.Dequeue();
        
        //check if has an empty front row and needs someone to be sent to the front.
        if (el[3] == null && el[4] == null && el[5] == null) send_el_backFront();

        for (int i = 0; i < el.Length; i++)
        {
            if (el[i] != null) el[i].place = i;
        }

        round = 0;

        fill_party_slots();
        fill_enemy_slots();

        //randomly choose whether player or enemy goes first in round 1.
        if (UnityEngine.Random.Range(0, 2) == 0) playerGoesFirst = true;
        else playerGoesFirst = false;

        //start up the battle.
        //SM.play_background_music();
        battleOver = false;
        gameObject.SetActive(true);
        start_of_round(true);
    }
    void send_in_next_wave()
    {
        el = waves.Dequeue();

        //check if has an empty front row and needs someone to be sent to the front.
        if (el[3] == null && el[4] == null && el[5] == null) send_el_backFront();

        for (int i = 0; i < el.Length; i++)
        {
            if (el[i] != null) el[i].place = i;
        }

        //clear out any scheduled moves, please.
        eorScheduledMove = null;
        eorScheduledMoveSpot = -1;
        eorScheduledUnit = null;

        playerScheduledMove = null;
        playerScheduledMoveSpot = -1;
        playerScheduledUnit = null;

        enemyScheduledMove = null;
        enemyScheduledMoveSpot = -1;
        enemyScheduledUnit = null;
        
        battleOver = false;
        start_of_round();
    }
    void Update()
    {
        //on rightclick; 
        if ( Input.GetMouseButtonDown(1) && pTurn != playerTurnPhase.NOT && pTurn != playerTurnPhase.SELECTUNIT)
        {
            //player can cancel their actions;
            if ( pTurn == playerTurnPhase.SELECTMOVE)
            {
                //if player is selecting move;
                //then return to select unit.
                //Debug.Log("select move -> select unit");
                pTurn = playerTurnPhase.SELECTUNIT;
                highlighter.unhighlight_user(playerScheduledUnit.place, false);
                moveSelector.hide();              
                allow_player_select_unit();
                
            }
            else if ( pTurn == playerTurnPhase.SELECTTARGET)
            {
                //if player is selecting target;
                //then return to select move.
                //Debug.Log("select target -> select move");
                targetingDefendMove = false;
                pTurn = playerTurnPhase.SELECTMOVE;
                previews.hide();
                plays.hide();
                unhighlight_move(-1);
                load_unit_move_selection();
            }
        }
    }
    IEnumerator waveWait(float time)
    {
        //clear the enemy field.

        wavesNum -= 1;
        for (int i = 0; i < enemyBoxes.Length; i++)
        {
            enemyBoxes[i].fill_dummy(emptySlotSprite);
        }

        previews.hide();
        string toShow = "\n|" + wavesNum;
        if (wavesNum > 1)
        {
            toShow += " waves remaining|";
        }
        else
        {
            toShow += " wave remaining|";
        }
        plays.show(toShow);
        yield return new WaitForSeconds(time);
        send_in_next_wave();
    }

    //COMBAT CONTROL 
    IEnumerator rounderWait(float time, bool startOfBattle)
    {
        if (startOfBattle == true)
        {
            plays.add("\n|" + wavesNum);
            if (wavesNum > 1)
            {
                plays.add(" waves remaining|");
            }
            else
            {
                plays.add(" wave remaining|");
            }

        }
        yield return new WaitForSeconds(time);
        if (startOfBattle) yield return new WaitForSeconds(1.5f);
        control();
    }   
    void start_of_round(bool startOfBattle = false)
    {
        //goes through different units and sets their ap to proper levels.
        previews.hide();
        previewSlot.hide(-1);
        //Debug.Log("new round begins!");
        round++;
        eorHelperDOTs = false;
        plays.show("Round " + round + "\n- Start -");

        //refresh, so we can show which buffs expired.
        plAp = 0;
        elAp = 0;
        int elCount = 0;
        int plCount = 0;
        for (int i = 0; i < 6; i++) //reset ap.
        {
            //functions that not only reset AP, they also adjust buff durations.
            if (pl[i] != null && pl[i].get_ooa() == false)
            {
                if (pl[i].refresh(startOfBattle) == true)
                {
                    show_ui_numbers("Buff expires", true, false, 1.0f, i);
                }
                plAp += pl[i].get_apMax_actual();
                plCount++;
            }

            if (el[i] != null && el[i].get_ooa() == false)
            {
                //Debug.Log("enemy " + i + " is good to go!");
                if (el[i].refresh(startOfBattle) == true)
                {
                    show_ui_numbers("Buff expires", false, false, 1.0f, i);
                }
                elAp += el[i].get_apMax_actual();
                elCount++;
            }
        }

        //sides take turns acting first.
        if (playerGoesFirst == true)
        {
            turn = whoseTurn.PLAYER;
        }
        else
        {
            turn = whoseTurn.AI;
        }
        playerGoesFirst = !playerGoesFirst;

        //wait for a second here.
        StartCoroutine(rounderWait(1.5f, startOfBattle));
    }
    void count_ap()
    {
        //recalculates ap
        plAp = 0;
        elAp = 0;
        for (int i = 0; i < 6; i++) //reset ap.
        {
            //functions that not only reset AP, they also adjust buff durations.
            if (pl[i] != null)
            {
                plAp += pl[i].get_ap();
            }

            if (el[i] != null)
            {
                elAp += el[i].get_ap();
            }
        }        
    }
    void control()
    {
        check_dead(); //need to update previews too.
        count_ap();
        //update each unit box stats
        fill_party_slots();
        fill_enemy_slots();
        if (battleOver == true)
        {
            //Debug.Log("returning from control; battleOver = true");
            return;
        } 

        //keep going while either player has AP remaining
        if (plAp == 0 && elAp == 0 && playerScheduledMove == null && enemyScheduledMove == null)
        {
            
            //first, check if there is an end of round move scheduled.
            if ( eorScheduledMove != null)
            {
                eorHelperDOTs = true;
                eor_move_execute();
            }
            else
            {               
                start_of_round();
            }          
        }
        else if ( turn == whoseTurn.PLAYER)
        {
            //Debug.Log("player turn!");
            player_turn();
        }
        else if ( turn == whoseTurn.AI)
        {
            //Debug.Log("enemy turn!");
            enemy_turn();
        }        
    }

    //PLAYER TURN
    void end_player_turn_p1(bool checkDOTS)
    {
        //call this to end player's turn.
        pTurn = playerTurnPhase.NOT;
        turn = whoseTurn.AI;
        if (checkDOTS == true)
        {
            int num = currentUnit.status.take_dot();
            if (num != 0)
            {
                StartCoroutine(show_dot(num, true));
                return;
            }
        }
        end_player_turn_p2();
    }
    void end_player_turn_p2()
    {
        //now, really end the player's turn.        
        previews.show(playerScheduledMove, enemyScheduledMove, eorScheduledMove);

        //and hide active portrait too. (at this point, it is still the chosen player unit)

        control();
    }
    void player_turn()
    {
        //first, execute stored move if there was one.
        if (playerScheduledMove != null)
        {
            pTurn = playerTurnPhase.EXECUTING;
            StartCoroutine(waitWhilePlayerMoveExecutes(playerScheduledMove, playerScheduledMoveSpot, playerScheduledUnit.place, whoseTurn.PLAYER, false));
            return;
        }

        if (plAp > 0)
        {
            allow_player_select_unit();
            return;
        }
        turn = whoseTurn.AI;
        control();
    }  
    void allow_player_select_unit()
    {
        //waits the game while the player selects a unit.
        plays.show("-select unit-");
        previews.show(playerScheduledMove, enemyScheduledMove, eorScheduledMove);
        pTurn = playerTurnPhase.SELECTUNIT;
    }
    void player_log_defend()
    {
        //the player has sucessfully chosen a unit, chosen to defend, and clicked on that player again.
        //now, it's up to us.

        //set ap to 0.
        currentUnit.drain_ap(currentUnit.get_ap());
        count_ap();

        //adjust ui
        pTurn = playerTurnPhase.NOT;
        unhighlight_move(-1);
        highlighter.unhighlight_user(playerScheduledUnit.place, true);

        //do the defend stuff.
        //(it will end the turn for us)
        targetingDefendMove = false;
        StartCoroutine(waitWhilePlayerDefendExecutes(currentUnit.get_defendMove(), playerScheduledUnit.place));

    }
    void player_log_move()
    {
        //the player has sucessfully chosen a unit, a move, and a target spot.
        //now, it's up to us.

        //drain ap. drain all of it if move's exe time is EOR
        if (currentUnit.nextMove.get_phase() != executionTime.ENDOFROUND)
        {
            currentUnit.drain_ap(currentUnit.nextMove.get_apDrain());
        }
        else
        {
            currentUnit.drain_ap(currentUnit.get_ap());
        }

        count_ap();

        //drain stamina. Yes, we drain it now no matter if the move is ever executed.
        //take into account currentUnit's status.trance
        currentUnit.drain_mp(currentUnit.nextMove.get_mpDrain());

        //three cases: depending on move's execution time.
        pTurn = playerTurnPhase.NOT;
        unhighlight_move(-1);
        highlighter.unhighlight_user(playerScheduledUnit.place, true);
        string phase_words = "next turn...";
        int para = 0; //default to player move. 
        switch (currentUnit.nextMove.get_phase())
        {
            case executionTime.INSTANT:
                playerScheduledUnit = currentUnit;
                StartCoroutine(waitWhilePlayerMoveExecutes(currentUnit.nextMove, playerScheduledMoveSpot, playerScheduledUnit.place, whoseTurn.AI, false));
                return;

            case executionTime.NEXTTURN:
                currentUnit.set_isScheduled(true);
                playerScheduledMove = currentUnit.nextMove;
                break;

            case executionTime.ENDOFROUND:
                phase_words = "at EOR...";
                para = 1; //eor move
                currentUnit.set_isScheduled(true);
                eorScheduledMove = currentUnit.nextMove;
                eorScheduledUnit = currentUnit;
                eorScheduledMoveSpot = playerScheduledMoveSpot;
                eorUserIsPlayer = true;
                break;
        }
        //end_player_turn_p1(true);
        StartCoroutine(pauseAfterPlayerLog(currentUnit.get_nom(), currentUnit.nextMove.get_nom(), phase_words, para, currentUnit.get_activePortrait()));
        
    }
    IEnumerator pauseAfterPlayerLog(string unit_name, string move_name, string phase_words, int highlightPreviewMoveParam, Sprite toShow)
    {
        //mirrors the functionality of pauseAfterEnemyLog coroutine.

        //skip the pause if the enemy is helpless until EOR. (i.e. elAP == 0 and enemyscheduledmove == null)
        if ( !(elAp == 0 && enemyScheduledMove == null) )
        {
            //hide preview move slots
            previews.hide();

            plays.show(unit_name + " prepares " + move_name + " " + phase_words);

            highlight_preview_move(highlightPreviewMoveParam);
            show_active_portrait(toShow);

            yield return new WaitForSeconds(1.5f);

            //afterwards, unhighlight it.
            unhighlight_preview_move(highlightPreviewMoveParam);
        }
        end_player_turn_p1(true);
    }
    void eor_move_execute()
    {
        //Debug.Log("eor move execute called.");
        if (eorUserIsPlayer)
        {
            StartCoroutine(waitWhilePlayerMoveExecutes(eorScheduledMove, eorScheduledMoveSpot, eorScheduledUnit.place, whoseTurn.WAIT, true));
        }
        else
        {
            StartCoroutine(waitWhileEnemyMoveExecutes(eorScheduledMove, eorScheduledMoveSpot, eorScheduledUnit.place, whoseTurn.WAIT, true));
        }
    }

    //ENDING BATTLE AND DEATHS
    void check_endOfBattle(bool playerLost, bool playerWon)
    {
        //handles battle ending.
        // things to return: 
        // -units states
        // -stamina
        // -...loot, when we get to it.

        if (playerLost == true)
        {
            battleOver = true;
            plays.show("-defeat-");
            previews.hide();
            //dMan.return_control(0, false);

            //made combat screen go cloudy?

            //give control to loss manager:
            loser.show();
        }
        else if (playerWon == true)
        {
            //if there is another wave of enemies, send them in.
            battleOver = true;
            if (waves.Count > 0)
            {
                //send in the next wave baby.
                StartCoroutine(waveWait(2.0f));
            }
            else
            {
                plays.show("-victory-");

                //reset unit's status then.
                for (int i = 0; i < pl.Length; i++)
                {
                   if (pl[i] != null) pl[i].status.reset(pl[i]);
                }

                //dMan.return_control(battleXP, true, pl);
                pdm.battle_won(battleXP);
            }
        }
    }
    void check_dead()
    {
        //first, update the ooa status of all units. i.e. check if any were suddenly murdered.
        for (int i = 0; i < pl.Length; i++)
        {
            if (pl[i] != null && pl[i].wasKilled() == true)
            {
                if ( playerScheduledUnit != null && playerScheduledUnit.place == i) playerScheduledMove = null;
                if (eorUserIsPlayer == true && eorScheduledUnit != null && eorScheduledUnit.place == i) eorScheduledMove = null;
                pl[i].status.reset(pl[i]);
                partyBoxes[i].kill_unit();
            }

            if (el[i] != null && el[i].wasKilled() == true)
            {
                //add exp
                battleXP += el[i].get_exp();

                if (enemyScheduledUnit != null && enemyScheduledUnit.place == i) { enemyScheduledMove = null; enemyScheduledUnit = null; enemyScheduledMoveSpot = -1; }
                if (eorUserIsPlayer == false && eorScheduledUnit != null && eorScheduledUnit.place == i) { eorScheduledMove = null; eorScheduledUnit = null; eorScheduledMoveSpot = -1; }
                Destroy(el[i].gameObject);
                el[i] = null;
            }
        }
        
        
        
        //check to see if player rows should be swapped
        //0 1 2 (front)
        //3 4 5 (back)
        bool playerFrontRowDead = true;
        for (int i = 0; i < 3; i++)
        {
            if (pl[i] != null && pl[i].get_ooa() == false)
            {
                playerFrontRowDead = false;
                break;
            }
        }
        if (playerFrontRowDead == true) send_pl_backFront();

        //now, check if player has lost the battle. (all units are ooa)
        bool playerDefeated = true;
        for (int i = 0; i < pl.Length; i++)
        {
            if (pl[i] != null && pl[i].get_ooa() == false)
            {
                playerDefeated = false;
                break;
            }
        }

        //check to see if enemy rows should be swapped
        //0 1 2 (back)
        //3 4 5 (front)
        bool enemyFrontRowDead = true;
        for (int i = 3; i < 6; i++)
        {
            if (el[i] != null)
            {
                enemyFrontRowDead = false;
                break;
            }
        }
        if (enemyFrontRowDead == true) send_el_backFront();

        bool enemyDefeated = true;
        for (int i = 0; i < el.Length; i++)
        {
            if (el[i] != null)
            {
                enemyDefeated = false;
                break;
            }
        }
        check_endOfBattle(playerDefeated, enemyDefeated);
    }
    void send_pl_backFront()
    {
        //if all player units are ooa, then return immediately.
        bool pDefeated = true;
        foreach (Unit u in pl)
        {
            if (u != null)
            {
                if (u.get_ooa() == false)
                {
                    pDefeated = false;
                    break;
                }
            }          
        }
        if (pDefeated == true) {return;}
        
        //sends the back row to the front and the front row to the back
        //however: only send a front unit to the back if there is a back unit behind it that has to move forward.

        Unit[] tmpArr = new Unit[3];
        for (int i = 0; i < tmpArr.Length; i++)
        {
            tmpArr[i] = pl[i];
        }

        for (int i = 0; i < 3; i++)
        {
            //if front empty and back empty, no action required.
            //if front not empty but back is empty, no action required
            //if front not empty and back not empty, swap both together
            //if front empty and back not empty, send back to front

            if (pl[i] == null && pl[i+3] != null)
            {
                //if front empty and back not empty, send back to front.
                pl[i] = pl[i + 3];
                pl[i + 3] = null;
            }
            else if (pl[i] != null && pl[i + 3] != null)
            {
                //if both not empty, swap them.
                pl[i] = pl[i + 3];
                pl[i + 3] = tmpArr[i];
            }
        }

        //update unit places
        for (int i = 0; i < pl.Length; i++)
        {
            if(pl[i] != null) pl[i].place = i;
        }
    }
    void send_el_backFront()
    {
        //sends the back row to the front.
        //no need to send front to back, because dead enemies are set to null.
        for (int i = 0; i < 3; i++)
        {
            el[i+3] = el[i];
            el[i] = null;
        }
        //update unit places
        for (int i = 0; i < el.Length; i++)
        {
            if( el[i] != null) el[i].place = i;
        }
    }

    //MOVE COROUTINES
    IEnumerator waitWhilePlayerDefendExecutes(DefendMove move, int unitIndex)
    {
        //unit is def still alive; these moves are instant cast.
        previews.hide();
        show_active_portrait(pl[unitIndex].get_activePortrait());
        plays.show(pl[unitIndex].get_nom() + " uses " + move.get_nom() + "...");

        //form target group.
        Unit[] targets = f_create_targetList(move, unitIndex);

        for (int i = 0; i < 6; i++)
        {
            if ( targets[i] != null)
            {
                move.apply_defend_move(targets[i]);
                show_ui_numbers(move.get_floating_message(), true, true, 2.0f, i);
            }          
        }

        playerScheduledMove = null;

        yield return new WaitForSeconds(2.0f);
        end_player_turn_p1(true);
    }
    IEnumerator waitWhilePlayerMoveExecutes(Move move, int spot, int unitIndex, whoseTurn setTurnTo, bool isEOR)
    {
        //check that the unit is still alive
        if ((isEOR == false && playerScheduledUnit.get_ooa() == false) || (isEOR == true && eorScheduledUnit != null))
        {
            pl[unitIndex].set_isScheduled(false);

            show_active_portrait(pl[unitIndex].get_activePortrait());
            previews.hide();
            plays.show(pl[unitIndex].get_nom() + " uses " + move.get_nom() + "...");

            //play move sound
            SM.play_moveSound(move.get_sound());

            if ( move.get_isHeal() == true)
            {
                //highlight the move's target tiles
                highlighter.highlight_party(spot, move, true, unitIndex);

                //check if there are still targets
                Unit[] lovelies = f_create_targetList(move, spot);

                if (lovelies.Count(e => e != null) != 0)
                {
                    for (int m = 0; m < Mathf.Max(1, move.get_strikes()); m++)
                    {
                        //for each additionnal strike, play the move sound
                        if (m != 0)
                        {
                            SM.play_moveSound(move.get_sound());
                        }

                        List<int> healList = brain.unit_heals(pl[unitIndex], lovelies, move, unitIndex);

                        //show status text
                        if (move.get_isStatus() == true)
                        {
                            if (move.get_selfStatus() == false) //uses status on enemies only.
                            {
                                for (int i = 0; i < healList.Count; i++)
                                {
                                    //heal units.
                                    if (healList[i] != -1)
                                    {
                                        //if we apply status, then do it here. also, wait while we show status text.
                                        show_ui_numbers(move.get_statusText(), move.get_targetsParty(), true, 1.0f, i);
                                        move.apply_status(pl[i], pl, el, unitIndex);
                                    }
                                }
                                
                            }
                            else //uses status on self only.
                            {
                                show_ui_numbers(move.get_statusText(), true, move.get_selfStatusTextColorGreen(), 1.0f, unitIndex);
                                move.apply_status(null, pl, el, unitIndex);
                            }
                            yield return new WaitForSeconds(1.0f);
                        }
                        
                        //show dmg/heal text
                        for (int i = 0; i < healList.Count; i++)
                        {
                            //heal units.
                            if (healList[i] != -1)
                            {
                                plays.add("Heals " + healList[i] + " damage!");
                                pl[i].heal(healList[i]);

                                //make a damage number
                                show_ui_numbers(healList[i].ToString(), move.get_targetsParty(), true, 1.0f, i);
                            }
                        }
                        yield return new WaitForSeconds(1.0f);
                    }
                }
                else
                {
                    plays.add("But there was no target.");                    
                    yield return new WaitForSeconds(1.0f);
                }
            }
            else
            {
                //highlight the move's target tiles
                highlighter.highlight_enemy(spot, move, true, unitIndex);

                //check if there are still targets
                Enemy[] villains = e_create_targetList(move, spot);

                if (villains.Count(e => e != null) != 0)
                {
                    for (int m = 0; m < Mathf.Max(1, move.get_strikes()); m++)
                    {
                        //for each additionnal strike, play the move sound
                        if ( m != 0 )
                        {
                            SM.play_moveSound(move.get_sound());
                        }

                        List<int> dmgList = brain.unit_attacks(pl[unitIndex], villains, move, unitIndex);

                        //show status text
                        if (move.get_isStatus() == true)
                        {
                            if (move.get_selfStatus() == false)
                            {
                                for (int i = 0; i < dmgList.Count; i++)
                                {
                                    //heal units.
                                    if (dmgList[i] != -1)
                                    {
                                        //if we apply status, then do it here. also, wait while we show status text.
                                        show_ui_numbers(move.get_statusText(), move.get_targetsParty(), false, 1.0f, i);
                                        move.apply_status(el[i], pl, el, unitIndex);

                                    }
                                }
                            }
                            else //uses status on self only.
                            {
                                show_ui_numbers(move.get_statusText(), true, move.get_selfStatusTextColorGreen(), 1.0f, unitIndex);
                                move.apply_status(null, pl, el, unitIndex);
                            }
                            yield return new WaitForSeconds(1.0f);
                        }

                        //show dmg/heal text
                        for (int i = 0; i < dmgList.Count; i++)
                        {
                            //heal units.
                            if (dmgList[i] != -1)
                            {
                                plays.add("Deals " + dmgList[i] + " damage!");

                                //check here for break
                                int breakAmount = Mathf.Min(100, (int)((dmgList[i] * move.get_breakMult() * el[i].get_break_multiplier() / el[i].get_hpMax_actual()) * 150));
                                bool didBreak = el[i].damage(dmgList[i], breakAmount);

                                //display dmg numbers or break
                                if (didBreak == false)
                                {
                                    //show dmg in red and break a line below it in yellow and with % at the end
                                    show_ui_numbers(dmgList[i].ToString() + "\n<color=yellow> " + breakAmount + "%</color>", move.get_targetsParty(), false, 1.0f, i);
                                }
                                else
                                {
                                    show_ui_numbers(dmgList[i].ToString() + "\n<color=yellow>BREAK</color>", move.get_targetsParty(), false, 1.0f, i);
                                    //then, do the breaky stuff to the unit. set ap to 0, cancel move if scheduled.
                                    el[i].break_ap();
                                    if (el[i] == enemyScheduledUnit)
                                    {
                                        enemyScheduledMove = null;
                                        enemyScheduledUnit = null;
                                        enemyScheduledMoveSpot = -1;
                                    }
                                    else if (el[i] == eorScheduledUnit)
                                    {
                                        eorScheduledUnit = null;
                                        eorScheduledMove = null;
                                        eorScheduledMoveSpot = -1;
                                    }
                                }
                            }
                        }
                        yield return new WaitForSeconds(1.0f);
                    }
                }
                else
                {
                    plays.add("But there was no target.");                    
                    yield return new WaitForSeconds(1.0f);
                }
                SM.stop_moveSound();
            }
            
        }
        else
        {
            plays.add("But it failed!"); //the unit that wasw going to use this move is unable to/is dead.
        }

        highlighter.restore_party();
        highlighter.restore_enemies();
        fill_party_slots();
        fill_enemy_slots();

        yield return new WaitForSeconds(2.0f);

        if ( setTurnTo == whoseTurn.WAIT)
        {
            eorScheduledMove = null;
        }
        else if ( setTurnTo == whoseTurn.PLAYER)
        {
            playerScheduledMove = null;
        }

        turn = setTurnTo;

        if (turn == whoseTurn.AI) end_player_turn_p1(true);
        else control();                
    }
    IEnumerator waitWhileEnemyMoveExecutes(Move move, int spot, int unitIndex, whoseTurn setTurnTo, bool isEOR)
    {
        //check unit is still alive.
        if ( (isEOR == false && enemyScheduledUnit != null) || (isEOR == true && eorScheduledUnit != null) )
        {
            el[unitIndex].set_isScheduled(false);
            e_show_active_portrait(el[unitIndex].get_activePortrait());
            previews.hide();
            plays.show(el[unitIndex].get_nom() + " uses " + move.get_nom() + "...");
            SM.play_moveSound(move.get_sound());
            if (move.get_isHeal() == true)
            {
                //highlight the move's target tiles
                highlighter.highlight_enemy(spot, move, false, unitIndex);

                //check if there are still targets
                Enemy[] lovelies = e_create_targetList(move, spot);

                if (lovelies.Count(e => e != null) != 0)
                {
                    for (int m = 0; m < Mathf.Max(1, move.get_strikes()); m++)
                    {
                        List<int> healList = brain.enemy_heals(el[unitIndex], lovelies, move, unitIndex);

                        //show status text
                        if (move.get_isStatus() == true)
                        {
                            if (move.get_selfStatus() == false)
                            {
                                for (int i = 0; i < healList.Count; i++)
                                {
                                    //heal units.
                                    if (healList[i] != -1)
                                    {
                                        //if we apply status, then do it here. also, wait while we show status text.
                                        show_ui_numbers(move.get_statusText(), move.get_targetsParty(), true, 1.0f, i);
                                        move.apply_status(el[i], pl, el, unitIndex);
                                    }
                                }
                            }
                            else
                            {
                                show_ui_numbers(move.get_statusText(), false, move.get_selfStatusTextColorGreen(), 1.0f, unitIndex);
                                move.apply_status(null, pl, el, unitIndex);
                            }
                            yield return new WaitForSeconds(1.0f);
                        }

                        //show dmg/heal text
                        for (int i = 0; i < healList.Count; i++)
                        {
                            //heal units.
                            if (healList[i] != -1)
                            {
                                plays.add("Heals " + healList[i] + " damage!");
                                el[i].heal(healList[i]);

                                //make a damage number
                                show_ui_numbers(healList[i].ToString(), move.get_targetsParty(), true, 1.0f, i);
                            }
                        }
                        yield return new WaitForSeconds(1.0f);
                    }                   
                }
                else
                {
                    plays.add("But there was no target.");                   
                    yield return new WaitForSeconds(1.0f);
                }               
                SM.stop_moveSound();
            }
            else
            {
                //highlight the move's target tiles
                highlighter.highlight_party(spot, move, false, unitIndex);

                //check if there are still targets
                Unit[] villains = f_create_targetList(move, spot);
                
                if (villains.Count(e => e != null) != 0)
                {
                    for (int m = 0; m < Mathf.Max(1, move.get_strikes()); m++)
                    {
                        List<int> dmgList = brain.enemy_attacks(el[unitIndex], villains, move, unitIndex);

                        //show status text
                        if (move.get_isStatus() == true)
                        {
                            if (move.get_selfStatus() == false)
                            {
                                for (int i = 0; i < dmgList.Count; i++)
                                {
                                    //heal units.
                                    if (dmgList[i] != -1)
                                    {
                                        //if we apply status, then do it here. also, wait while we show status text.
                                        show_ui_numbers(move.get_statusText(), move.get_targetsParty(), false, 1.0f, i);
                                        move.apply_status(pl[i], pl, el, unitIndex);
                                        yield return new WaitForSeconds(1.0f);
                                    }
                                }
                            }
                            else
                            {
                                show_ui_numbers(move.get_statusText(), false, move.get_selfStatusTextColorGreen(), 1.0f, unitIndex);
                                move.apply_status(null, pl, el, unitIndex);
                            }
                            yield return new WaitForSeconds(1.0f);
                        }

                        //show dmg/heal text
                        for (int i = 0; i < dmgList.Count; i++)
                        {
                            //heal units.
                            if (dmgList[i] != -1)
                            {
                                plays.add("Deals " + dmgList[i] + " damage!");

                                //check here for break
                                int breakAmount = Mathf.Min(100, (int)((dmgList[i] * move.get_breakMult() * pl[i].get_break_multiplier() / pl[i].get_hpMax_actual()) * 150));
                                bool didBreak = pl[i].damage(dmgList[i], breakAmount);

                                //display dmg numbers or break
                                if (didBreak == false)
                                {
                                    show_ui_numbers(dmgList[i].ToString() + "\n<color=yellow> " + breakAmount + "%</color>", move.get_targetsParty(), false, 1.0f, i);
                                }
                                else
                                {
                                    show_ui_numbers(dmgList[i].ToString() + "\n<color=yellow>BREAK</color>", move.get_targetsParty(), false, 1.0f, i);
                                    //then, do the breaky stuff to the unit. set ap to 0, cancel move if scheduled.
                                    pl[i].break_ap();
                                    if (pl[i] == playerScheduledUnit)
                                    {
                                        playerScheduledMove = null;
                                        playerScheduledUnit = null;
                                        playerScheduledMoveSpot = -1;
                                    }
                                    else if (pl[i] == eorScheduledUnit)
                                    {
                                        eorScheduledUnit = null;
                                        eorScheduledMove = null;
                                        eorScheduledMoveSpot = -1;
                                    }
                                }
                            }
                        }
                        yield return new WaitForSeconds(1.0f);                       
                    }
                    
                }
                else
                {
                    plays.add("But there was no target.");                   
                    yield return new WaitForSeconds(1.0f);
                }
                
            }
            
        }
        else
        {
            plays.show("whiff!");
        }
        highlighter.restore_party();
        highlighter.restore_enemies();
        fill_enemy_slots();
        fill_party_slots();
        yield return new WaitForSeconds(2.0f);

        bool useEnemyScheduledUnit = true;
        if (setTurnTo == whoseTurn.WAIT)
        {
            useEnemyScheduledUnit = false;
            eorScheduledMove = null;
        }
        else if (setTurnTo == whoseTurn.AI)
        {
            enemyScheduledMove = null;
        }

        turn = setTurnTo;

        if (turn == whoseTurn.PLAYER) end_ai_turn_p1(true, useEnemyScheduledUnit);
        else control();        
    }
    IEnumerator show_dot(int amount, bool forPlayer)
    {
        //only called if damage not 0.
        bool isHeal = true;
        if (amount < 0) isHeal = false;

        if (eorHelperDOTs == true)
        {
            if (forPlayer == true)
            {
                show_ui_numbers(Mathf.Abs(amount).ToString(), true, isHeal, 1.0f, eorScheduledUnit.place);
                
            }
            else
            {
                show_ui_numbers(Mathf.Abs(amount).ToString(), false, isHeal, 1.0f, eorScheduledUnit.place);
            }
            if (isHeal == true)
            {
                eorScheduledUnit.heal(amount);
            }
            else
            {
                eorScheduledUnit.damage(Mathf.Abs(amount), 0);
            }
            eorHelperDOTs = false;
        }
        else
        {
            if (forPlayer == true)
            {
                show_ui_numbers(Mathf.Abs(amount).ToString(), true, isHeal, 1.0f, playerScheduledUnit.place);
                if (isHeal == true)
                {
                    playerScheduledUnit.heal(amount);
                }
                else
                {
                    playerScheduledUnit.damage(Mathf.Abs(amount), 0);
                }
                
            }
            else
            {
                show_ui_numbers(Mathf.Abs(amount).ToString(), false, isHeal, 1.0f, enemyScheduledUnit.place);
                if (isHeal == true)
                {
                    enemyScheduledUnit.heal(amount);
                }
                else
                {
                    enemyScheduledUnit.damage(Mathf.Abs(amount), 0);
                }
            }
        }

        yield return new WaitForSeconds(2.0f);

        check_dead();

        if (forPlayer == true) end_player_turn_p2();
        else end_ai_turn_p2();
    }
    
    //UI
    public void show_active_portrait(Sprite sp, bool setLastShown = true)
    {
        activePortrait.sprite = sp;
        activePortrait.gameObject.SetActive(true);
    }
    public void e_show_active_portrait(Sprite sp, bool setLastShown = true)
    {
        enemyActivePortrait.sprite = sp;
        enemyActivePortrait.gameObject.SetActive(true);
    }
    void show_ui_numbers(string toShow, bool targetsParty, bool isHeal, float duration, int which)
    {
        //set position
        Transform parent;
        if (targetsParty == true)
        {
            parent = partyBoxes[which].transform;
        }
        else
        {
            parent = enemyBoxes[which].transform;
        }
        Vector3 pos = new Vector3(parent.position.x, parent.position.y - 15f, parent.position.z);

        //instantiate it.
        DmgText dt = Instantiate(dmgTextPrefab, pos, parent.transform.rotation, parent);

        //fill it.
        dt.fill(toShow, isHeal, duration);
    }
    void load_unit_move_selection()
    {
        //activePortrait.sprite = currentUnit.get_fullPortrait();
        //activePortrait.enabled = true;
        
        if ( eorScheduledMove == null)
        {
            moveSelector.show(currentUnit, true, playerScheduledUnit.place);
        }
        else
        {
            moveSelector.show(currentUnit, false, playerScheduledUnit.place);
        }
    }
    public void player_selected_party_unit(int which)
    {
        //called when player selects one of their own units.
        //possibilities:

        //first off, return if a right click.
        if ( Input.GetMouseButtonUp(1) ) return;

        // -player is using defend
        if ( targetingDefendMove == true && playerScheduledUnit.place == which ) 
        {
            //then, it's a valid target spot for a defend move.
                plays.hide();
                previews.hide();
                player_log_defend();  
        }
        // -player is selecting a unit to choose an action
        else if ( targetingDefendMove == false && pTurn == playerTurnPhase.SELECTUNIT && pl[which] != null && pl[which].get_ap() > 0) 
        {
            //of course, the selected index must not be a null spot in party.
            //further, the unit must have ap > 0
            //then, this is a valid selection. so, show move selection.
            plays.hide();
            previews.hide();
            pTurn = playerTurnPhase.SELECTMOVE;
            currentUnit = pl[which];
            playerScheduledUnit = pl[which]; //setting the active unit's id.
            show_active_portrait(currentUnit.get_activePortrait(), true);
            load_unit_move_selection();           
        }
        // -player is selecting a move target location (on their own side)
        else if ( targetingDefendMove == false && pTurn == playerTurnPhase.SELECTTARGET && currentUnit.nextMove.get_targetsParty() == true) 
        {
            //CHECK TO MAKE SURE THERE WOULD BE AT LEAST 1 UNIT IN THE AOE ZONE; IF NOT, RETURN
            //if must hit self; make sure we are, otherwise return.
            if (f_valid_targetSpot(currentUnit.nextMove, which) == false || (currentUnit.nextMove.get_mustTargetSelf() == true && currentUnit.place != which)) return;
           
            //log move.
            playerScheduledMoveSpot = which; //setting the chosen move's target spot. not necessary if move executes instantly, but it does no harm.
            player_log_move();
        }
    }
    public void player_selected_enemy_unit(int which)
    {
        if (Input.GetMouseButtonUp(1)) return;

        //called when player selects one of the enemy units.
        // -player is choosing targets for their moves. if not null and move targets enemies, then log move.
        if ( pTurn == playerTurnPhase.SELECTTARGET && currentUnit.nextMove.get_targetsParty() == false && currentUnit.nextMove.get_mustTargetSelf() == false)
        {
            //CHECK TO MAKE SURE THERE WOULD BE AT LEAST 1 UNIT IN THE AOE ZONE; IF NOT, RETURN
            if (e_valid_targetSpot(currentUnit.nextMove, which) == false) return;

            //i.e., a valid target area. So, log move.
            playerScheduledMoveSpot = which;
            player_log_move();
        }

    }
    public void clicked_move_slot(int which)
    {
        //called when the player clicks one of the move slots.
        //ok, great. we'll set unit's nextMove variable.

        //ADD DEFEND MOVE CASE HERE, where which == 5.
        moveSelector.hide();
        previews.show(playerScheduledMove, enemyScheduledMove, eorScheduledMove);
        if ( which == 5)
        {
            targetingDefendMove = true;
            plays.show("-Select Self-");
            currentUnit.nextMove = null;
        }
        else
        {
            targetingDefendMove = false;
            plays.show("-Select Target-");
            currentUnit.nextMove = currentUnit.get_moveset()[which];            
        }
        //then, allow player to pick a target location.
        pTurn = playerTurnPhase.SELECTTARGET;
    }
    public void clicked_swap_slot(int which)
    {
        //called when player clicks one of the (interactable) swap slots in the move selector interfaces.
        //legend:
        // 0: front
        // 1: back
        // 2: left
        // 3: right

        moveSelector.hide();
        //unhighlight user
        highlighter.unhighlight_user(playerScheduledUnit.place, true);
        //drain user
        currentUnit.drain_ap(1);
       
        //perform the actual swap:
        //it's instant and ends the player's turn.
        //swap the positions in pl[] of the two units.        
        switch (which)
        {
            case 0:
                Unit tmp = pl[playerScheduledUnit.place - 3];
                pl[playerScheduledUnit.place - 3] = currentUnit;
                pl[playerScheduledUnit.place] = tmp;
                break;
            case 1:
                Unit tmp2 = pl[playerScheduledUnit.place + 3];
                pl[playerScheduledUnit.place + 3] = currentUnit;
                pl[playerScheduledUnit.place] = tmp2;
                break;
            case 2:
                Unit tmp3 = pl[playerScheduledUnit.place - 1];
                pl[playerScheduledUnit.place - 1] = currentUnit;
                pl[playerScheduledUnit.place] = tmp3;
                break;
            case 3:
                Unit tmp4 = pl[playerScheduledUnit.place + 1];
                pl[playerScheduledUnit.place + 1] = currentUnit;
                pl[playerScheduledUnit.place] = tmp4;
                break;
        }
        //update unit places
        for (int i = 0; i < pl.Length; i++)
        {
            if (pl[i] != null) pl[i].place = i;
        }
        //refill the partyBoxes.
        fill_party_slots();

        end_player_turn_p1(true);
    }
    void fill_party_slots()
    {
        for(int i = 0; i < 6; i++)
        {
            if (pl[i] == null)
            {
                //Debug.Log(partyBoxes[i]);
                partyBoxes[i].fill_dummy(emptySlotSprite);
            }
            else
            {
                partyBoxes[i].fill_unit(pl[i], affOrbSprites[pl[i].get_affinity()]);
            }
        }
    }
    void fill_enemy_slots()
    {
        if (battleOver) return;
        for (int i = 0; i < 6; i++)
        {
            if (el[i] == null)
            {
                enemyBoxes[i].fill_dummy(emptySlotSprite);
            }
            else
            {
                enemyBoxes[i].fill_unit(el[i], affOrbSprites[el[i].get_affinity()]);
            }
        }
    }

    //MOVE HIGHLIGHTING
    public void unhighlight_preview_move(int which)
    {
        //0: player scheduled move,
        //1: eor move
        //2: enemy scheduled move

        //reset active portrait to its pre-preview move highlight state
        

        //we unhighlight the target squares and we unhighlight the move's user.
        switch (which)
        {
            case 0:
                if (playerScheduledMove != null)
                {
                    highlighter.unhighlight_user(playerScheduledUnit.place, true);
                    if ( playerScheduledMove.get_isHeal() == true)
                    {
                        highlighter.restore_party();
                    }
                    else
                    {
                        highlighter.restore_enemies();
                    }
                }
                break;
            case 1:
                if (eorScheduledMove != null)
                {
                    if (eorUserIsPlayer == true)
                    {
                        highlighter.unhighlight_user(eorScheduledUnit.place, true);
                        if (eorScheduledMove.get_isHeal() == true)
                        {
                            highlighter.restore_party();
                        }
                        else
                        {
                            highlighter.restore_enemies();
                        }
                    }
                    else
                    {
                        highlighter.unhighlight_e_user(eorScheduledUnit.place, true);
                        if (eorScheduledMove.get_isHeal() == true)
                        {
                            highlighter.restore_enemies();
                        }
                        else
                        {
                            highlighter.restore_party();
                        }
                    }
                    
                }
                break;
            case 2:
                if (enemyScheduledMove != null)
                {
                    highlighter.unhighlight_e_user(enemyScheduledUnit.place, true);
                    if (enemyScheduledMove.get_isHeal() == true)
                    {
                        highlighter.restore_enemies();
                    }
                    else
                    {
                        highlighter.restore_party();
                    }
                }
                break;
        } 
    }
    public void highlight_preview_move(int which)
    {
        //we highlight the target squares and the move's user.       
        switch (which)
        {
            case 0:
                if (playerScheduledMove != null)
                {
                    //show the unit who is going to be using it in active portrait
                    show_active_portrait(playerScheduledUnit.get_activePortrait(), false);
                    if ( playerScheduledMove.get_isHeal() == true)
                    {
                        highlighter.highlight_party(playerScheduledMoveSpot, playerScheduledMove, true, playerScheduledUnit.place);
                    }
                    else
                    {
                        highlighter.highlight_enemy(playerScheduledMoveSpot, playerScheduledMove, true, playerScheduledUnit.place);
                    }
                    highlighter.highlight_user(playerScheduledUnit.place);
                }
                break;
            case 1:
                if (eorScheduledMove != null)
                {
                    
                    if ( eorUserIsPlayer == true )
                    {
                        show_active_portrait(eorScheduledUnit.get_activePortrait(), false);
                        if ( eorScheduledMove.get_isHeal() == true)
                        {
                            highlighter.highlight_party(eorScheduledMoveSpot, eorScheduledMove, true, eorScheduledUnit.place);
                        }
                        else
                        {
                            highlighter.highlight_enemy(eorScheduledMoveSpot, eorScheduledMove, true, eorScheduledUnit.place);
                        }
                        highlighter.highlight_user(eorScheduledUnit.place);
                    }
                    else
                    {
                        e_show_active_portrait(eorScheduledUnit.get_activePortrait(), false);
                        if (eorScheduledMove.get_isHeal() == true)
                        {
                            highlighter.highlight_enemy(eorScheduledMoveSpot, eorScheduledMove, false, eorScheduledUnit.place);
                        }
                        else
                        {
                            highlighter.highlight_party(eorScheduledMoveSpot, eorScheduledMove, false, eorScheduledUnit.place);
                        }
                        highlighter.highlight_e_user(eorScheduledUnit.place);
                    }                    
                }
                break;
            case 2:
                if (enemyScheduledMove != null)
                {       
                    e_show_active_portrait(enemyScheduledUnit.get_activePortrait(), false);
                    if ( enemyScheduledMove.get_isHeal() == true)
                    {
                        highlighter.highlight_enemy(enemyScheduledMoveSpot, enemyScheduledMove, false, enemyScheduledUnit.place);
                    }
                    else
                    {
                        highlighter.highlight_party(enemyScheduledMoveSpot, enemyScheduledMove, false, enemyScheduledUnit.place);
                    }
                    highlighter.highlight_e_user(enemyScheduledUnit.place);
                }
                break;
        }
    }
    public void unhighlight_move(int which)
    {
        if (currentUnit == null || currentUnit.nextMove == null) return;
        if ( currentUnit.nextMove.get_isHeal() == true)
        {
            highlighter.restore_party();
        }
        else
        {
            highlighter.restore_enemies();
        }

    }
   
    //TARGETING VALIDATION
    private Unit[] f_create_targetList(Move move, int spot)
    {
        // 0    1    2
        // 3    4    5

        Unit[] targets = new Unit[6];

        //first, add indexes that will be in the AoE.
        //after, add units from el into targets by the added indexes.
        int helper = 1;
        if (spot > 2) helper = -1;

        if (spot % 3 == 2 && move.get_xSize() > 1) spot--;
        else if (spot % 3 != 1 && move.get_xSize() == 3)
        {
            if (spot < 3) spot = 1;
            else spot = 4;
        }

        List<int> affectedSpots = new List<int>();
        for (int y = 0; y < move.get_ySize(); y++)
        {
            //do rows by adding 3*y to all calculations.
            switch (move.get_xSize())
            {
                case 1:
                    if (pl[spot + (helper * y * 3)] != null) affectedSpots.Add(spot + (helper * y * 3));
                    break;

                case 2:
                    //can just check the middle row. nice little cheat.
                    if (pl[spot + (helper * y * 3)] != null) affectedSpots.Add(spot + (helper * y * 3));
                    if (pl[spot + 1 + (helper * y * 3)] != null) affectedSpots.Add(spot + 1 + (helper * y * 3));
                    break;

                case 3:
                    //can check any old spot.                 
                    if (pl[spot - 1 + (helper * y * 3)] != null) affectedSpots.Add(spot - 1 + (helper * y * 3));                  
                    if (pl[spot + (helper * y * 3)] != null) affectedSpots.Add(spot + (helper * y * 3));
                    if (pl[spot + 1 + (helper * y * 3)] != null) affectedSpots.Add(spot + 1 + (helper * y * 3));
                    break;
            }
        }
        //sort list in ascending order.
        affectedSpots.Sort();
        foreach (int i in affectedSpots)
        {
            targets[i] = pl[i];
        }

        return targets;
    }
    private Enemy[] e_create_targetList(Move move, int spot)
    {
        // 0    1    2
        // 3    4    5

        Enemy[] targets = new Enemy[6];

        //first, add indexes that will be in the AoE.
        //after, add units from el into targets by the added indexes.
        int helper = 1;
        if (spot > 2) helper = -1;

        if (spot % 3 == 2 && move.get_xSize() > 1) spot--;
        else if (spot % 3 != 1 && move.get_xSize() == 3)
        {
            if (spot < 3) spot = 1;
            else spot = 4;
        }

        List<int> affectedSpots = new List<int>();
        for (int y = 0; y < move.get_ySize(); y++)
        {
            //do rows by adding 3*y to all calculations.
            switch (move.get_xSize())
            {
                case 1:
                    if (el[spot + (helper * y * 3)] != null) affectedSpots.Add(spot + (helper * y * 3));
                    break;

                case 2:
                    //can just check the middle row. nice little cheat.
                    if (el[spot + (helper * y * 3)] != null) affectedSpots.Add(spot + (helper * y * 3));
                    if (el[spot + 1 + (helper * y * 3)] != null) affectedSpots.Add(spot + 1 + (helper * y * 3));
                    break;

                case 3:
                    //can check any old spot.
                    if (el[spot - 1 + (helper * y * 3)] != null) affectedSpots.Add(spot - 1 + (helper * y * 3));
                    if (el[spot + (helper * y * 3)] != null) affectedSpots.Add(spot + (helper * y * 3));
                    if (el[spot + 1 + (helper * y * 3)] != null) affectedSpots.Add(spot + 1 + (helper * y * 3));
                    break;
            }
        }
        //sort list in ascending order.
        affectedSpots.Sort();
        foreach (int i in affectedSpots)
        {
            targets[i] = el[i];
        }

        return targets;
    }
    bool f_valid_targetSpot(Move move, int spot)
    {
        //for validating target locations on the player's side.
        //if there would be 0 targets in the resultant AoE, then return false
        //otherwise, return true.

        int helper = 1;
        if (spot > 2) helper = -1;

        if (spot % 3 == 2 && move.get_xSize() > 1) spot--;
        else if (spot % 3 != 1 && move.get_xSize() == 3)
        {
            if (spot < 3) spot = 1;
            else spot = 4;
        }

        for (int y = 0; y < move.get_ySize(); y++)
        {
            //do rows by adding 3*y to all calculations.
            switch (move.get_xSize())
            {
                case 1:
                    if (pl[spot + (helper * y * 3)] != null) return true;
                    break;

                case 2:
                    //can just check the middle row. nice little cheat.
                    if (pl[spot + (helper * y * 3)] != null) return true;
                    if (pl[spot + 1 + (helper * y * 3)] != null) return true;
                    break;

                case 3:
                    //can check any old spot.
                    if (pl[spot - 1 + (helper * y * 3)] != null) return true;                  
                    if (pl[spot + (helper * y * 3)] != null) return true;
                    if (pl[spot + 1 + (helper * y * 3)] != null) return true;
                    break;
            }
        }
        return false;
    }
    bool e_valid_targetSpot(Move move, int spot)
    {
        //for validating target locations on the enemy's side.
        //if there would be 0 targets in the resultant AoE, then return false
        //otherwise, return true.
        //Debug.Log("checking whether spot " + spot + " is a valid target spot.");
        int helper = 1;
        if (spot > 2) helper = -1;

        if (spot % 3 == 2 && move.get_xSize() == 2) spot--;
        else if (spot % 3 != 1 && move.get_xSize() == 3)
        {
            if (spot < 3) spot = 1;
            else spot = 4;
        }

        for ( int y = 0; y < move.get_ySize(); y++)
        {
            //do rows by adding 3*y to all calculations.
            switch (move.get_xSize())
            {
                case 1:
                    if (el[spot + (helper * y * 3)] != null) return true;
                    break;

                case 2:
                    //can just check the middle row. nice little cheat.
                    if (el[spot + (helper * y * 3)] != null) return true;
                    if (el[spot + 1 + (helper * y * 3)] != null) return true;                    
                    break;

                case 3:
                    //can check any old spot.
                    if (el[spot - 1 + (helper * y * 3)] != null) return true;
                    if (el[spot + (helper * y * 3)] != null) return true;
                    if (el[spot + 1 + (helper * y * 3)] != null) return true;
                    break;
            }
        }
        return false;
    }

    //AI CONTROL
    void end_ai_turn_p1(bool checkDOTs, bool useEnemyScheduledUnit)
    {
        //call this to end an ai unit's turn.
        if (checkDOTs == true)
        {
            //Ah-hah! I figured it out.
            //the problem
            int num;
            if (useEnemyScheduledUnit == true)
            {
                num = enemyScheduledUnit.status.take_dot();
            }
            else
            {
                num = eorScheduledUnit.status.take_dot();
            }

            if (num != 0)
            {
                StartCoroutine(show_dot(num, false));
                return;
            }
        }
        end_ai_turn_p2();
    }
    void end_ai_turn_p2()
    {
        //now, really end the player's turn.        
        previews.show(playerScheduledMove, enemyScheduledMove, eorScheduledMove);
        turn = whoseTurn.PLAYER;
        control();
    }
    void enemy_turn()
    {
        /*
        //dummy turn - just passes over enemy's turn.
        for(int i = 0; i < el.Length; i++)
        {
            if (el[i] != null) el[i].drain_ap(el[i].get_ap());
        }
        turn = whoseTurn.PLAYER;
        control();
        return;
        //end dummy turn       
        */

        //first off, if there is a scheduled move, then execute it.
        if (enemyScheduledMove != null)
        {
            StartCoroutine(waitWhileEnemyMoveExecutes(enemyScheduledMove, enemyScheduledMoveSpot, enemyScheduledUnit.place, whoseTurn.AI, false));
            return;
        }
        //otherwise, if elAp > 0, let player choose a unit to act.
        if (elAp > 0)
        {
            pick_enemy_actor();
            return;
        }
        //if neither, then just continue and do your thing.
        turn = whoseTurn.PLAYER;
        control();
    }
    void pick_enemy_actor()
    {
        //pick a unit to act from el.
        //how does it work? well, for each non-null unit in el, calculate a priority.

        //first, we go over el and see if any units want healing done.
        bool needHealing = false;
        for (int i = 0; i < el.Length; i++)
        {
            if (el[i] != null && el[i].is_concerned() == true) { needHealing = true; break; }
        }

        //now, calculate and pick the one with highest priority.
        int g_max = -10000;
        int chosenEnemy = -1;
        for (int i = 0; i < el.Length; i++)
        {
            if (el[i] != null && el[i].get_ap() > 0)
            {
                int l_max = el[i].calc_priority(needHealing);
                if ( l_max > g_max)
                {
                    g_max = l_max;
                    chosenEnemy = i;
                }
                else if ( l_max == g_max)
                {
                    if (UnityEngine.Random.Range(0, 2) == 0) chosenEnemy = i;
                }
            }
        }
        //Debug.Log("pick_enemy_actor(): unit " + chosenEnemy + " chosen!");
        //Debug.Log("chosen enemy id: " + chosenEnemy);
        pick_enemy_actor_move(chosenEnemy);
    }
    void pick_enemy_actor_move(int chosenID)
    {
        //for a given enemy, pick a move.
        //how does it work? all moves have a stamina cost, and units have independent stamina.
        //the unit will roll a number from 1-100, add stamina, and pick the most stamina depending move it can afford.
        //(moving from the back to the front of its moveset)

        int roll = Mathf.Max(1, UnityEngine.Random.Range(1, 101) + el[chosenID].get_mp());

        EnemyMove chosenMove;
        if (eorScheduledMove == null)
        {
            chosenMove = el[chosenID].pick_move(roll, true);
        }
        else
        {
            chosenMove = el[chosenID].pick_move(roll, false);
        }       
        pick_enemy_actor_move_target(chosenID, chosenMove);
    }
    void pick_enemy_actor_move_target(int chosenID, EnemyMove chosenMove)
    {
        //for a chosen move's targeting preference, pick a target spot.

        int spot = -1;
        int lowest = 100000;
        List<int> possibleTargetLocations = new List<int>();
        int w;
        switch (chosenMove.get_targeting_preference())
        {
            case targeting.MostHP:
                //target playerunit with highest hp; as a flat number, not percentage of maxhp.
                int highest0 = 0;
                for (int i = 0; i < pl.Length; i++)
                {
                    if (pl[i] != null && pl[i].get_ooa() == false)
                    {
                        if (pl[i].get_hp() > highest0)
                        {
                            highest0 = pl[i].get_hp();
                            spot = i;
                        }
                        else if (pl[i].get_hp() == highest0)
                        {
                            if (UnityEngine.Random.Range(0, 2) == 0) spot = i;
                        }
                    }
                }
                break;

            case targeting.HighestBreakLevel:
                //target the playerunit with the highest break_level that ALSO has ap > 0.
                int highest1 = 0;
                for (int i = 0; i < pl.Length; i++)
                {
                    if (pl[i] != null && pl[i].get_ooa() == false)
                    {
                        if (pl[i].get_break() > highest1)
                        {
                            highest1 = pl[i].get_break();
                            spot = i;
                        }
                        else if (pl[i].get_break() == highest1)
                        {
                            if (UnityEngine.Random.Range(0, 2) == 0) spot = i;
                        }
                    }
                }
                break;
            case targeting.Self:
                spot = el[chosenID].place;
                break;
            case targeting.BestAffMult: //pick the unit who we would get the highest mult factor against.
                //calculate_aff_mod(int attacker_aff, int defender_aff)
                float best = 0f;
                for (int i = 0; i < pl.Length; i++)
                {
                    if (pl[i] != null && pl[i].get_ooa() == false)
                    {
                        float check = brain.calculate_aff_mod(chosenMove.get_affinity(), pl[i].get_affinity());
                        if (check > best)
                        {
                            best = check;
                            spot = i;
                        }
                        else if (check == best)
                        {
                            if (UnityEngine.Random.Range(0, 2) == 0) spot = i;
                        }
                    }
                }
                break;

            case targeting.Heal: //randomly heal a concerned unit.
                //we want to maximize the number of concerned el units that we hit. that's all.
                int highest = -1;
                for (int i = 0; i < el.Length; i++)
                {
                    //a valid target is not null and is concerned.
                    int targetsHit = e_get_validTargetNumber(chosenMove, i);
                    if (targetsHit > highest)
                    {
                        highest = targetsHit;
                        spot = i;
                    }
                    else if (targetsHit == highest)
                    {
                        if (UnityEngine.Random.Range(0, 2) == 0) spot = i;
                    }
                }
                break;
            case targeting.Random: //random target               
                for (int i = 0; i < pl.Length; i++)
                {
                    if (pl[i] != null && pl[i].get_ooa() == false) possibleTargetLocations.Add(i);
                }                
                w = UnityEngine.Random.Range(0, possibleTargetLocations.Count);
                spot = possibleTargetLocations[w];
                break;
            case targeting.LeastHP:
                for (int i = 0; i < pl.Length; i++)
                {
                    if (pl[i] != null && pl[i].get_ooa() == false)
                    {
                        if (pl[i].get_hp() < lowest)
                        {
                            lowest = pl[i].get_hp();
                            spot = i;
                        }
                        else if ( pl[i].get_hp() == lowest)
                        {
                            if (UnityEngine.Random.Range(0, 2) == 0) spot = i;
                        }
                    }
                }
                break;
            case targeting.LeastPdef:
                for (int i = 0; i < pl.Length; i++)
                {
                    if (pl[i] != null && pl[i].get_ooa() == false)
                    {
                        if (pl[i].get_pdef_actual() < lowest)
                        {
                            lowest = pl[i].get_pdef_actual();
                            spot = i;
                        }
                        else if (pl[i].get_pdef_actual() == lowest)
                        {
                            if (UnityEngine.Random.Range(0, 2) == 0) spot = i;
                        }
                    }
                }
                break;
            case targeting.LeastMdef:              
                for (int i = 0; i < pl.Length; i++)
                {
                    if (pl[i] != null && pl[i].get_ooa() == false)
                    {
                        if (pl[i].get_mdef_actual() < lowest)
                        {
                            lowest = pl[i].get_mdef_actual();
                            spot = i;
                        }
                        else if (pl[i].get_mdef_actual() == lowest)
                        {
                            if (UnityEngine.Random.Range(0, 2) == 0) spot = i;
                        }
                    }
                }
                break;
            case targeting.Front:
                //there must be a unit in the front.
                for(int i = 0; i < 3; i++)
                {
                    if (pl[i] != null && pl[i].get_ooa() == false) possibleTargetLocations.Add(i);
                }
                w = UnityEngine.Random.Range(0, possibleTargetLocations.Count);
                spot = possibleTargetLocations[w];
                break;
            case targeting.Back:
                //first check back. if back is empty, then check front. there must be a unit in the front.
                for (int i = 0; i < 3; i++)
                {
                    if (pl[i] != null && pl[i].get_ooa() == false) possibleTargetLocations.Add(i);
                }
                if ( possibleTargetLocations.Count == 0 )
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (pl[i] != null && pl[i].get_ooa() == false) possibleTargetLocations.Add(i);
                    }
                }
                w = UnityEngine.Random.Range(0, possibleTargetLocations.Count);
                spot = possibleTargetLocations[w];
                break;
            case targeting.AOE:
                //we want to maximize the number of pl units that we hit. that's all.
                int highest2 = -1;
                for (int i = 0; i < 6; i++)
                {
                    int targetsHit = f_get_validTargetNumber(chosenMove, i);
                    if (targetsHit > highest2)
                    {
                        highest2 = targetsHit;
                        spot = i;
                    }
                    else if (targetsHit == highest2)
                    {
                        if (UnityEngine.Random.Range(0, 2) == 0) spot = i;
                    }
                }
                break;
        }
        log_enemy_move(chosenID, chosenMove, spot);
    }
    void log_enemy_move(int chosenID, EnemyMove move, int targetSpot)
    {
        //drain ap (stamina already drained when enemy finished picking move.)

        if (move.get_phase() != executionTime.ENDOFROUND)
        {
            el[chosenID].drain_ap(move.get_apDrain());
        }
        else
        {
            el[chosenID].drain_ap(el[chosenID].get_ap());
        }
        count_ap();

        //three cases: depending on move's execution time.
        int para = 2;
        string phase_words = "";
        bool useEnemyScheduledUnit = true;

        //if player has no ap remaining and no scheduled player move, and enemy move is not EOR, then execute immediately to save time.
        if (plAp == 0 && playerScheduledMove == null && move.get_phase() != executionTime.ENDOFROUND)
        {
            //execute immediately. 
            enemyScheduledUnit = el[chosenID];
            //Debug.Log("executing enemy move right away. target spot is " + targetSpot);
            StartCoroutine(waitWhileEnemyMoveExecutes(move, targetSpot, chosenID, whoseTurn.PLAYER, false));
            return;
        }

        switch (move.get_phase())
        {
            case executionTime.INSTANT:
                //execute immediately. 
                enemyScheduledUnit = el[chosenID];
                StartCoroutine(waitWhileEnemyMoveExecutes(move, targetSpot, chosenID, whoseTurn.PLAYER, false));
                return;
            case executionTime.NEXTTURN:
                el[chosenID].set_isScheduled(true);
                enemyScheduledMove = move;
                enemyScheduledUnit = el[chosenID];
                enemyScheduledMoveSpot = targetSpot;
                phase_words = "next turn...";
                break;
            case executionTime.ENDOFROUND:
                el[chosenID].set_isScheduled(true);
                eorScheduledMove = move;
                eorScheduledUnit = el[chosenID];
                eorScheduledMoveSpot = targetSpot;
                eorUserIsPlayer = false;
                useEnemyScheduledUnit = false;
                para = 1;
                phase_words = "at EOR...";
                break;

        }
        //end turn
        //end_ai_turn_p1(true);
        StartCoroutine(pauseAfterEnemyLog(el[chosenID].get_nom(), move.get_nom(), phase_words, para, useEnemyScheduledUnit, el[chosenID].get_activePortrait()));
    }
    private int e_get_validTargetNumber(Move move, int spot)
    {
        //creates a targetlist on player map and returns the number of valid targets that would be hit.
        //valid target: pl[i] where != null, != ooa
        // 0    1    2
        // 3    4    5

        int number = 0;

        //first, add indexes that will be in the AoE.
        //after, add units from el into targets by the added indexes.
        int helper = 1;
        if (spot > 2) helper = -1;

        if (spot % 3 == 2 && move.get_xSize() > 1) spot--;
        else if (spot % 3 != 1 && move.get_xSize() == 3)
        {
            if (spot < 3) spot = 1;
            else spot = 4;
        }

        for (int y = 0; y < move.get_ySize(); y++)
        {
            //do rows by adding 3*y to all calculations.
            switch (move.get_xSize())
            {
                case 1:
                    if (el[spot + (helper * y * 3)] != null && el[spot + (helper * y * 3)].is_concerned() == true) number++;
                    break;
                case 2:
                    //can just check the middle row. nice little cheat.
                    if (el[spot + (helper * y * 3)] != null && el[spot + (helper * y * 3)].is_concerned() == true) number++;
                    if (el[spot + 1 + (helper * y * 3)] != null && el[spot + 1 + (helper * y * 3)].is_concerned() == true) number++;
                    break;
                case 3:
                    //can check any old spot.                 
                    if (el[spot - 1 + (helper * y * 3)] != null && el[spot - 1 + (helper * y * 3)].is_concerned() == true) number++;
                    if (el[spot + (helper * y * 3)] != null && el[spot + (helper * y * 3)].is_concerned() == true) number++;
                    if (el[spot + 1 + (helper * y * 3)] != null && el[spot + 1 + (helper * y * 3)].is_concerned() == true) number++;
                    break;
            }
        }
        return number;
    }
    private int f_get_validTargetNumber(Move move, int spot)
    {
        //creates a targetlist on player map and returns the number of valid targets that would be hit.
        //valid target: pl[i] where != null, != ooa
        // 0    1    2
        // 3    4    5

        int number = 0;

        //first, add indexes that will be in the AoE.
        //after, add units from el into targets by the added indexes.
        int helper = 1;
        if (spot > 2) helper = -1;

        if (spot % 3 == 2 && move.get_xSize() > 1) spot--;
        else if (spot % 3 != 1 && move.get_xSize() == 3)
        {
            if (spot < 3) spot = 1;
            else spot = 4;
        }

        for (int y = 0; y < move.get_ySize(); y++)
        {
            //do rows by adding 3*y to all calculations.
            switch (move.get_xSize())
            {
                case 1:
                    if (pl[spot + (helper * y * 3)] != null && pl[spot + (helper * y * 3)].get_ooa() == false) number++;
                    break;
                case 2:
                    //can just check the middle row. nice little cheat.
                    if (pl[spot + (helper * y * 3)] != null && pl[spot + (helper * y * 3)].get_ooa() == false) number++;
                    if (pl[spot + 1 + (helper * y * 3)] != null && pl[spot + 1 + (helper * y * 3)].get_ooa() == false) number++;
                    break;
                case 3:
                    //can check any old spot.                 
                    if (pl[spot - 1 + (helper * y * 3)] != null && pl[spot - 1 + (helper * y * 3)].get_ooa() == false) number++;
                    if (pl[spot + (helper * y * 3)] != null && pl[spot + (helper * y * 3)].get_ooa() == false) number++;
                    if (pl[spot + 1 + (helper * y * 3)] != null && pl[spot + 1 + (helper * y * 3)].get_ooa() == false) number++;
                    break;
            }
        }
        return number;
    }
    IEnumerator pauseAfterEnemyLog(string unit_name, string move_name, string phase_words, int highlightPreviewMoveParam, bool useEnemyScheduledUnit, Sprite toShow)
    {
        //if enemy used a next turn or eor move, then we pause here and show the player a preview of what's to come.
        //consists of:
        // -plays: unitname uses movename
        // -emap/pmap: highlight the target areas and user
        //once it's done, then continue to end_ai_turn_p1(true)
        previews.hide();
        plays.show(unit_name + " prepares " + move_name + " " + phase_words);
        highlight_preview_move(highlightPreviewMoveParam);
        e_show_active_portrait(toShow);
        yield return new WaitForSeconds(1.5f);
        
        //afterwards, unhighlight it.
        unhighlight_preview_move(highlightPreviewMoveParam);

        end_ai_turn_p1(true, useEnemyScheduledUnit);
    }

}
