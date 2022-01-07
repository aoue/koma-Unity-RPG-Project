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
//Welcome to part 0 dungeon tile event 2. this plays on the boss' tile, just before the fight.
//contents: moth (yet unnamed) + sabaind at large. She passes out at the end, during inflation.
//is there wailing and crying out? Yes. The girl is really having a bad time. Scene goes from partially to fully to inflation to the girl passing out.

//=============

dun 0 (tile) event 2
-> END

//~show bg forest
//~play monster roar sound? (the sound that is referenced later)
~n("???")
GGRAOOOOUUU!!

~n("")
We finally come to a stop outside a cave some ways into the forest.
It was easy to find. The trail, more ripped strands of cloth, footprints, all led here. And at the end you could just follow the noise.

//~show friday worry

~talk(0)
~n("Friday")
.........
~talk(1)

~n("")
I notice her complexion paling.

~n("Me")
Mom?

//~show friday resolute
~n("")
When she turns to face me, the expression on her face from a moment ago has vanished.

~talk(0)
~n("Friday")
Why did this have to happen now, when he's with me...
~talk(1)

~n("")
Mother grips my shoulders tightly.

~n("Friday")
Listen to me closely. Whatever you're about to see, don't think about it.
... Just put it out of your mind for now. Stay focused.
Okay?

~n("")
My voice has dried in my throat. I can only nod mutely.
She releases me and kisses my forehead gently.

~n("Friday")
Good boy. Now stay close to me, sweetheart.

//~hide friday

//SCENE 0 BEGINS BELOW
//we have 5 images to work with: (in roughly this order)
// -sc0 partial
// -sc0 fully blur
// -sc0 fully
// -sc0 inflation blur
// -sc0 inflation
//the blur ones are to simulate the instant the action happens.

//~set bg cave
~n("")
......

//~set bg sc0 partial

//~play monster howl sound
~shake(5, 0.35)
~n("???")
GRAOOOOOOUUUU!!!!

//~set bg sc0 fully blur

//~play monster howl sound
~shake(10, 0.5)
GGGGRRAAAAAAOOOOUUUUUU!!!!!!

~n("")
.........

//~set bg sc0 fully
.........
Short sputterings fill the room as the thing reaches is pushed deep inside her, with enough force that her entire body shakes.
As it begins to leave her, she takes in shallow, ragged breaths all at once.

//~set bg scene 0 partial
She hangs limply in the monster's grip. Wearing the tattered remains of a Sister's habit, there are tears in her eyes and and a vacant expression covers her face.
Shock and disbelief at the circumstances she finds herself in.

//~set bg sc0 fully blur
The creature stops withdrawing and, never leaving her completely, switches direction. Impossibly, it sheathes itself back inside the girl.

//~set bg scene 0 full
Its obscene length reaches once again all the way to her breastbone.

~talk(0)
~n("Me")
......
~talk(1)

~n("")
I feel something rising in me. Not something I've felt before.
Then Mother's hand is on my back.

~n("Friday")
... I know. Acting now will only put her in danger. If we attack now, it might kill her.

~n("")
I suddenly find it very hard to swallow.

~n("Me")
It will kill her as it is.

~n("")
Mother shakes her head.

~n("Friday")
... It won't. It needs her alive.

~n("")
Out...
... out...
...... out...

//~set bg scene 0 fully blur
~shake(10, 0.5)

~n("Creature")
GRAOOUUUUU!!

//~set bg fully
~n("")
And in.
Her body shudders as the monster enters her again, so deeply and so forcefully that saliva flies from her mouth and her head lolls.
She's remained silent until now, but a hoarse cry suddenly escapes her lips. A low, dull moan.

//~set bg partial
She feebly tries to raise her head—

//~set bg fully blur
—but another brutal thrust from the monster shakes her body in its grip.
Rather than screaming or pleading, her body relaxes, taking the repeated abuse silently.

//~set bg fully
... But it couldn't have been like this at first.
I hope that maybe she's passed out, but as the monster flops her around again, I see her eyes. She's wide awake.

//~set bg scene 0 partial
~n("")
Out...

//~set bg scene 0 fully blur
~shake(10, 0.5)

~n("Creature")
GRAOOUUUUU!!

~n("")
... and in.

//~set bg scene 0 fully
How long has this been going on already?
The girl's light frame used by the huge monster, over and over, her stomach bulging obscenely with each thrust.
It grips her roughly from her hips and moves her up and down its length. To it, she may as well not be a living thing. Just a toy.
Something to fuck for a while. To be bred, and once she's given birth to its children, to be bred again.


//~set bg scene 0 partial
we stand here in the shadow of the cave's mouth, helplessly waiting for it to finish. Things like this shouldn't happen.

//~set bg scene 0 fully blur
...

//~set bg scene 0 fully
The creature picks up the pace, letting out a savage grunt each time it spears her deeply.

//~set bg scene 0 partial
As she reaches the highest point on its shaft; the moment where the least of the monster's length is inside her, it suddenly switches its grip.
Its huge hands move deceptively quickly up from the girl's hips to over her shoulders and chest, and then—


//~set bg scene 0 fully blur
//~play monster howl sound
~shake(10, 0)
—slams her down. It pushes itself deeper inside her than seems possible and lets out a dreadful scream.
The girl's eyes widen in shock and at the feeling of having something so large so deep inside her.

//~set bg scene 0 fully
Panting, it stops thrusting, instead trying to push its length deeper inside her and, at the same time, pull her down onto it. But it won't go in any farther.
Not without breaking her. After a moment, even the monster's aggressive pushing ceases and it contents itself just to keep a firm hold on her where she is.

//~cum sound effect
gloop... glop...
It becomes clear why the monster has stopped.

//~set bg scene 0 inflation blur
From its member already deep inside the girl, it starts to release its seed. The amount continues to grow with each passing second.
Even as her stomach begins to expand outward from the huge volume expelled inside her, its vice-like grip on her body never slackens. It stops anything from leaking out at anywhere near the rate it's pumping in.
The result is the swelling of her stomach as more of the creature's cum shoots inside her. It's a ridiculous amount.
Slop... slop...


//~set bg scene 0 inflation
gloop...
Finally, it stops expanding. The girl's head hangs limply on her shoulders, and I can tell she's unconscious. Mercifully.
The monster seems to lose interest in her all at once. It lets go of her shoulders and the girl slips down onto the ground where she lands awkwardly in a heap, unmoving.


//--- scene 0 ends
//~set bg cave
The semen is leaking out of her faster now, and her inflated stomach is already beginning to subside...

//show friday in center
~n("Friday")
... Now.

~n("")
Mother is speaking quietly, and her voice is devoid of emotion. Her usual cheerfulness and good-natured confidence are gone, replaced with a cold matter-of-factness.
That's fine though. Nothing else would be appropriate at this time. All I feel is a dull pain deep in my chest and acute awareness of each pulse of my heart.
It feels disgusting.

~n("")
Then, Mother steps out into the center of the cave. Her naked sword in hand.
As I step out after her the creature turns and, snarling, advances on us.

//done.
﻿
