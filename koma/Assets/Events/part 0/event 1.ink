//Link external functions here.
EXTERNAL bg(id)
EXTERNAL n(name) //set to empty string to hide namebox.
EXTERNAL talk(mode) //1: true, use quotes. 0: false, use parantheses.
EXTERNAL toggle_font()
EXTERNAL show(whichSlot, portraitID)
EXTERNAL hide(whichSlot)
EXTERNAL stop_music()
EXTERNAL play_music(whichTrack)
EXTERNAL play_sound(whichTrack)
//end functions

//variable controllers here. set by EventManager at scene start.
VAR ic = 0
VAR player = "playerCharName"
//end variables

//SCENE OUTLINE
//Welcome to part 0 event 1. The location is Agairy's town square. It unlocks dungeon0, which the player must clear to proceed.
// -mc and friday arrive in town, where people stop what they're doing to watch them. they're curious of outsiders, friday explains, but don't mean ill.
// -before long, the abess arrives to greet them. the abbess explains that the sister has gone missing. friday asks if they've searched for her, and the abbess says of course, but the town is small and there are not many places to hide. She fears the sister has left the settlement and entered the wilderness. The settlers don't dare go in themselves.
// -the abbess implies that friday should volunteer to look for the sister in the woods, so friday and mc head into the forbidding woods around the settlement.
//=============

part 0 event 1.
-> END

~n("")

//show agairy town square
It's time of day when we finally walk into Agairy's square.
The settlement is __
settlement physical description (church, a circle of houses, quite small)
order sponsored settlement, and what that means (the leader, influence, getting in at the ground floor. if there's one thing the order never lacked, it's foresight.)


//show friday

people standing in the square watch us curiously, but don't approach.
they stop what they're doing to watch. (e.g. a pair of women with water buckets set them down on the ground while they watch)

~n("Friday")
Don't mind them, they're just interested. We're probably the most interesting thing to show up here in who knows how long?

~n("Mc")
Really? What's so interesting about us?

//show friday smile
~n("")
She laughs.

~n("Friday")
makes flattering comments about the two of them.
part two.

~n("")
she makes a show of looking around

~n("friday")
shouldn't someone meet us
aren't we important enough for them
grr
this is why i hate the sticks. do you know, it's never once had the decency to change in 20 years. been the same way since I was a girl.

~n("Me")
when did you spend so much time in the country as a girl (sumthin like that)

~n("Friday")
I'm a city girl, of course, but I traveled all over you know. Dodging responsibilities, suitors, debtors, that sort of thing.
Spent my fair amount of time out here. Which is why it has no right to have never changed. Makes me feel like a girl again.

//~show abbess
~n("")
The door of the church opens and the abbess etc




















//bruh.
