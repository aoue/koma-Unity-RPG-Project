//Link external functions here.
EXTERNAL bg(id)
EXTERNAL n(name) //set to empty string to hide namebox.
EXTERNAL talk(mode) //1: use quotes | 0: use parantheses.
EXTERNAL toggle_font()
EXTERNAL show(whichSlot, portraitID)
EXTERNAL hide(whichSlot)
EXTERNAL stop_music()
EXTERNAL play_music(whichTrack)
EXTERNAL play_sound(whichTrack)
EXTERNAL shake(intensity, duration) //camera shake. both parameters are ints. actual duration = 0.05f seconds * duration
EXTERNAL battle(id) //starts battle corresponding to the given id.
EXTERNAL add_units() //adds units attached to the event to the party.
EXTERNAL rest_party() //refresh hp and mp of all party units.
//end functions

//variable controllers here. set by EventManager at scene start.
VAR ic = 0
VAR player = "playerCharName"
//end variables

//SCENE OUTLINE
~rest_party()
~add_units()
~talk(1)
~n("")
p0 imm.
A battle!

~battle(0)

~n("")
battle over 1
battle over 2
battle over 3
battle over 4
battle over 5


-> END
