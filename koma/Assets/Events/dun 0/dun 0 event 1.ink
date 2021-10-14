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
EXTERNAL shake(intensity, duration) //camera shake
//end functions

//variable controllers here. set by EventManager at scene start.
VAR ic = 0
VAR player = "playerCharName"
//end variables

//SCENE OUTLINE
//Welcome to part 0 dungeon event 1. It occurs early in the exploration of dungeon 0, just 1 or 2 tiles in.
//it is short. the purpose is for friday to confirm that moth has probably come this way. they're headed in the right direction.
//talk a bit about demons, too.
//=============

dun 0 (tile) event 1
-> END


//~show bg forest. (trees, no path. some light peeking through the trees.)
//~show friday

~n("")
Mother reaches down and pulls a shred of dress from a low tree branch.

//~show friday worry

~n("Friday")
At least we're headed in the right direction. But I wonder... is our demon in that much of a hurry, or did it leave this for us?
A taunt, maybe.

~n("")
She makes a show of weighs the options for a moment.

//~show friday neutral
~n("Friday")
It doesn't change anything, I guess.

~n("Me")
It would taunt us? It doesn't even know we're coming.

~n("Friday")
They're individuals. And even if they weren't it'd be impossible to understand every action they do. Their minds are the wrong shape.
But this is worrying. There's signs of wildlife around here. Monsters, too.
Big enough to cause our demon trouble if it has the misfortune.

~n("Me")
But the demon will protect its host.

~n("Friday")
To a point. At least until a better one doesn't come along. But They're individuals, like I said. They're imperfect. I should know, the job of an exorcist is to take advantage of that.

~n("Me")
A better host? That's us, isn't it?

//~show friday worried
~n("Friday")
I wouldn't worry about it. //some wrongshadowing











//done.
