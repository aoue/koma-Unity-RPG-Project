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

//NOTES
Kill it in a cutscene? Monster roars, leans back and prepares to attack,
Show Friday cut-in and a sword flash image.
She kills the monster and it falls down to the ground. She is still facing it, in case it gets back up.
Go, to the girl! She shouts. (she’s telling mc to go check on moth.)
Mc arrives at Moth’s side. He tries to wake her up or something, but nothing happens. He tries to detect magic at work on her, but finds nothing. (which means there’s nothing or it’s hidden by magic better than his.)

As he finishes checking, Friday walks over, satisfied that it’s well and truly over.
Mc says how she’s unresponsive. She’s still breathing shallowly though.
Friday looks her over for a long moment before coming to a decision.
It can’t be helped, she says. We should leave here sooner rather than later and there’s no indication of whether the girl is going to wake up anytime soon.
Any idea what’s causing it, mc asks?
A few, says Friday. She lists them; stress reaction, fainted and taking a long time to come out of it, simple exhaustion... or something else, I don’t know. But let’s not stand here talking about it.
We’ll have the time to figure out which of them it is once we’re safely back in Agairy.
*Friday picks up Moth gently in her arms, holding the small girl like one holds a baby.
(how nostalgic...) she thinks.
Come now, she says, give me your coat. We need something to cover the poor girl or she’ll catch her death of cold before we make it back.
*mc nods and does so.
*Friday drapes it over her and tucks it around Moth.
Some comment about how she would give moth some of her own clothes, but her dress is sort of all or nothing.
It’s not the most practical dress, says mc.
Nonsense, says friday. It’s the perfect dress for the modern noblewoman.
END
