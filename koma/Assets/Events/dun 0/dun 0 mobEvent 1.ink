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
//Welcome to dungeon 0 mob event 1. this is what plays after the boss of dungeon 0 has been defeated.
// -the party defeats the sabaind at large and rescues moth. They can't wake her up though, regardless of what they try.
// -friday says there's no time to stick around here, they'll just carry her back and pray nothing happens.
//=============

dun 0 mob event 1

-> END
