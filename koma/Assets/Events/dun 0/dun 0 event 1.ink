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
Mother reaches down and pulls a shred of a pale dress from a low tree branch.

//~show friday frown
~n("Friday")
At least we're headed in the right direction. Looks like our demon's in some kind of hurry.

~n("Me")
Running from us?

~n("Friday")
Maybe. It could be able to sense us, but probably it just found out we were coming when it was still in the settlement. I doubt it's a coincidence it choose today of all days to make its escape.
It doesn't change anything, of course.

~n("Me")
That seems unwise; waiting for us, I mean. If it had left earlier it would have had more time to get a lead.
Which means it wants us to chase it. We're the ones it's truly after.

~n("Friday")
Yes, or someone like us. Possession demons have a very simple motive. They want to get hold of the most powerful host they can.
The way they try to do that is different, of course. They're individuals. And even if they weren't it'd be impossible to understand the reasoning behind every action they do. Their minds are too mishapen to be fully understood.
But the goal is always the same.

~n("")
Mother stops us for a moment.

~n("Friday")
This is worrying. There's signs of wildlife around here. Monsters, too. Either one is enough to cause trouble for our girl.

~n("Me")
Won't the demon protect her?

~n("Friday")
Until a better host comes along. But They're individuals, like I said. They're flawed and can make mistakes. I should know, my job is to take advantage of that.
... We'll just have to find her first.

~n("")
I nod. Here in the woods, with monsters not too far away, the gravity of the situation is starting to hit me.
Mother notices, of course.

//~show friday loving smile
~n("Friday")
Don't worry. I said I'd protect you, didn't I?

-> END



//done.
