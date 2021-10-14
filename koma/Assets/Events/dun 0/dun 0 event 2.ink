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

dun 0 (tile) event 2
-> END

//~show bg forest
//~play the sound? (the sound that is referenced later)

~n("")
We finally come to a stop outside a cave deep in the forest.
It was easy to find. The trail, more ripped strands of cloth, footprints, all led here. And at the end you could just follow the noise.

//~show friday worry/sad (which?)

~talk(0)
~n("Friday")
.........

~talk(1)
~n("")
I notice her complexion paling.

~n("Me")
Mother?

//~show friday resolute
~n("")
When she turns to face me, the palid sheen on her face a moment ago has left without a face.

~talk(0)
~n("Friday")
I was hoping I could ease him into this... forgive me.
~talk(1)

~n("")
Mother grips my shoulders tightly.

~n("Friday")
Listen to me closely.
Whatever you're about to see, don't think about it.
... Just put it out of your mind for now. Stay focused. Okay?

~n("")
My voice has dried in my throat. I can only nod mutely.
She releases me and kisses my forehead gently.

~n("Friday")
Good boy. Stay close to me, now.

//~set background cave
//~hide friday

//~play monster howl sound
~shake(5, 0.35)
~n("Creature")
GRAOOOOOOUUUU!!!!

//~set bg scene 0 full
//~play monster howl sound
~shake(10, 0.5)
GRRAAAAAAOOOOUUUUUU!!!!!!

//okay; get into the down and dirty of the scene.
//we have several images to work with:
// -full (fully sheathed, up to the breast bone)
// -partial (partially sheathed, up to the top of the stomach)
// -inflation (fully sheathed, with the belly bloating around it.)
~n("")
.........
.........
Short sputterings fill the room as the thing moves inside her.

//~set bg scene 0 partial
As it begins to leave her, she takes shallow, rapid breaths all at once.
But despite her rought treatment, the girl, wearing the tattered remains of a Sister's habit, hangs limply in the monster's grip.

The creature stops withdrawing and, never leaving her completely, switches direction. Impossibly, it sheathes itself back inside the girl.
//~set bg scene 0 full
Its obscene length reaches all the way to her breastbone.

~talk(0)
~n("Me")
......
~talk(1)

~n("")
I feel something rising in me. Not something I've felt before.
Then Mother's hand is on my back.

~n("Friday")
... I know.
We must wait. Acting will only put her in more danger. If we attack now, it may kill her.

~n("")
I suddenly find it very hard to swallow.

~n("Me")
It will kill her as it is.

~n("")
Mother shakes her head.

~n("Friday")
It won't. It needs her alive.

//~set bg scene 0 partial
~n("")
Out...
... out...
... ... out...

//~set bg scene 0 full
~shake(10, 0.5)

~n("Creature")
GRAOOUUUUU!!

~n("")
And in.
The Sister's body shudders as the monster enters her again, so deeply and so forcefully that saliva flies from her mouth.
Throughout all this, she's remained silent. Or perhaps unconscious is more appropriate.
Rather than screaming, or pleading, she's mercifully lost unconsciousness, taking the repeated abuse without complaint.
... But it couldn't have been like this at first.

//~set bg scene 0 partial
~n("")
Out...

//~set bg scene 0 full
~shake(10, 0.5)

~n("Creature")
GRAOOUUUUU!!

~n("")
... and in.
How long has this been going on already...
The girl's light frame used by the huge monster, over and over, her stomach bulging obscenely.
It grips her roughly from her hips and moves her up and down its length. To it, she may as well not be a living thing. Just a toy.
Something to fuck for a while. To be bred and once she's given birth to its children, to be bred again.
And we stand here in the shadow of the cave's mouth, waiting for it to finish. I know it isn't right.

//~set bg scene 0 partial
...

//~set bg scene 0 full
...

//~set bg scene 0 partial
The creature picks up the pace.

//~set bg scene 0 full
It lets out a low grunt each time it spears her deeply.

//~set bg scene 0 partial
As she reaches the highest point in the fucking; the moment where the least of the monster's length is inside her, it suddenly switches its grip.
Its huge hands move deceptively quickly up from the girl's hips to over her shoulders and chest, and then—

//~set bg scene 0 full
//~play monster howl sound
~shake(10, 0)
—slams her down. It pushes itself deeper inside her than it yet been and lets out a dreadful howl.
Panting, it stops thrusting, instead trying to push its length deeper inside her still. But it won't go any farther. There is a limit to everything, after all.
After a moment, even the monster's pushing ceases and it contents itself just to keep a firm hold on her where she is.

//~cum sound effect
gloop... glop...
It becomes clear why the monster has stopped.

//~set bg scene 0 inflation
It releases from its member already deep inside the girl, and the amount continues to increase with each passing second.
Even as her stomach begins to expand outward from the huge volume expelled inside her, its vice-like grip on her body stops it from leaking out at anywhere near the rate it's pumping in.
The result is the swelling of her stomach as more of the creature's cum shoots inside her. It's a ridiculous amount, with a ridiculous image to match.
Slop... slop...
Finally, it subsides and the monster seems to lose interest in her at once. It lets go of her shoulders and the girl slips down onto the ground where she lands awkwardly in a heap.

//--- scene 0 ends
//~set bg cave
The semen is leaking out of her faster now, and her inflated stomach is already beginning to subside...

//show friday in center
~n("Friday")
... Now.

~n("")
Mother is speaking quietly, and her voice is devoid of emotion. Her usual cheerfulness and the good-natured confidence in her voice are gone, replaced with a cold matter-of-factness.
That's fine though. I don't feel those myself.
All I feel is a dull pain deep in my chest and acute awareness of each pulse of my heart. It feels disgusting.

Then, Mother steps out into the center of the cave. Her naked sword's already in her hand.
As I step out after her the creature turns and, snarling, advances on us.
//done.
﻿
