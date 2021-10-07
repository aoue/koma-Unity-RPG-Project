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
We were too late.

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
//~shake screen effect.
//~set font size large
~n("???")
GRAOOOOOOUUUU!!!!

//~set background scene 0, fully-sheathed.

//~play monster howl sound
//~shake screen effect.
GRRAAAAAAOOOOUUUUUU!!!!!!

//okay; get into the down and dirty of the scene.
//we have several images to work with:
// -full (fully sheathed, up to the breast bone)
// -partial (partially sheathed, up to the belly button)
// -inflation (fully sheathed, but the belly is bloating around it.)

//despite her rought treatment, the girl, wearing the tattered remains of a sister's habit, hangs limply.
//she's breathing shallowly.
//start partially sheathed, then mc watches as the monster, impossibly, sheathes itself completely in moth.

//'we have to help her, says mc'
//'friday holds him back. you have to do as i say or you'll put her in danger. we can't risk interrupting right now, it may kill her'
//'mc feels strongly. it will kill her as it is.'
//'friday shakes her head sadly. it won't. she's no use to it dead.

//the monster brings itself back out again, then resheathes itself with full force (screen shake)
//GRAOOUUUUU!!


//talk about moth's condition. what's she doing? (passed out). how is she reacting (not at all).



//done.
﻿
