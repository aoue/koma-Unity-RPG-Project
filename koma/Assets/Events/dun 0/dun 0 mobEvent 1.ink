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
// -the party defeats the sabaind at large and gets to the girl, but she is still out of it. They carry her back.
//=============

dun 0 mob event 1

-> END

//~show bg cave
//~show creature(slot 0)
~n("Creature")
Rrroaaaahhhh!!

~n("")
Roaring, it pulls its thick arms back and prepares to attack again.
But I can feel it nearing its limit, already covered in a layer of minor wounds. We have been getting the better of it, but I'm feeling my own exhaustion beginning to creep in.
... I need to last just a little while longer!

//~show friday resolute(slot 0; replacing the monster)
Mother's breathing is deep and measured; the efficient breaths of a warrior. She's showing no sign of slowing down.
Absentmendidly, she flicks some of her hair out of her face. Her gaze nevers moves off of the creature.
I always knew Mother was strong, but I never grasped exactly how incredible her strength is. How fast, how strong she is.

//~show creature(slot 1; alongside friday)

How well do I really know my own mother?

~n("Creature")
~shake(5, 0.5)
GRRRooooaaggghhhh!!!

~n("")
It lunges faster than something that size has any right to.
Even before I start forming a shield around her, Mother slips past its heavy swing and inside its reach, her sword upright and close to her chest.
Then, in a practiced motion, she shifts her weight forwards and plants her blade in the monster's side. It's in halfway to the hilt before the monster reacts.

//~hide slot 0
//~hide slot 1
//~show bg friday cutin

As it reaches for her, Mother pulls the sword clean and whirls behind the monster, staying a moment ahead of it.
Then, she stabs it once more at the base of the spine. Her sword passes through a quarter of its waist as she slides it free.

//~show bg cave
//~show friday resolute slot 0
//~show sabaind at large slot 1

~n("Friday")
Haaaah...

//~hide sabaind at large
~n("")
The monster falls to its knees and then slowly to the ground. A layer cloud of dirt and dust is sent upwards as its huge frame finally lands.

~n("Me")
Mother! That was— it was amazing!

~n("")
She nods her head in acknowledgement.

//~show friday worry
~n("Friday")
It was nothing.

~n("")
Mother smoothes down the front of her dress and frowns as she notices the splashes of blood that landed on it during the fight.

//~show friday frown
~n("Friday")
Ruined.
{player}, go check on our wayward sister, will you? I'm going to make sure big guy over here stays dead.

~n("Me")
OK.

//~hide friday
~n("")
We had positioned ourselves so the girl wouldn't be in the line of fire during the fight. She was unresponsive, but still breathing.
I crouch next to her and shake her gently, trying to rouse her. There's no reaction.
I try speaking to her but stop after a few seconds when nothing happens, feeling like a fool.
And I don't know what else to do. How pathetic to be stumped so quickly.
Her shallow steady breathing continues uninterrupted.

//~show friday neutral
~n("Friday")
Anything?

~n("Me")
She's passed out. I don't know what's wrong with her.

~n("Friday")
That's alright. It could be any of a lot of different things; the poor girl's been through a lot.
She still has more to come, I'm sorry, but the worst should be over.

~n("")
Mother sizes her up.

~n("Friday")
I was hoping she'd be able to walk... But it can’t be helped. There’s no indication of whether the girl is going to wake up anytime soon And we should leave here sooner rather than later.
I'll just have to carry her.

~n("Me")
... I can do it.

~n("Friday")
Don't be silly, you're exhausted.
//~show friday smile
No need to pretend with me, young man. You've still got a ways to go.
Anyway, she doesn't look heavy, so don't worry about it, OK?

~n("")
Mother picks her up gently in her arms, cradling her like a baby.

~n("Friday")
How nostalgic...
Come now, give me your coat. We need something to cover the poor girl or she’ll catch her death of cold before we get back.

~n("Me")
The girl. We've called her that this entire time. After what I've seen her go through, the least I could do is call her by her name, but I can't. I don't even know what it is.
We've saved the body, but were we too late for the girl?
I hand over my coat. Mother drapes and tucks it around the sister, doing her best to cover her for warthm and modesty.

~n("Friday")
Thanks for lending her your coat, she'll appreciate it later. I'd give her some of my own, but this dress is rather all or nothing. And ruined now, too.

~n("")
She motions at the bright red bloodstain on its front.

~n("Friday")
Let's get a move on.

~n("")
As we leave the cave, I think to myself that I wouldn't mind never seeing this place again. Agairy, the cave, any of it.

~n("Me")
She's going to be okay, won't she?

~n("Friday")
~talk(0)
...
~talk(1)
Of course she will, sweetheart. Now come on.


-> END
