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

//~show bg cave
//~show creature(slot 0)
~n("Creature")
Rrroaaaahhhh!!

~n("")
Roaring, it pulls its thick arms back and prepares to attack again.
But I can feel it nearing its limit. It's already covered in a multitude of minor wounds. We have been getting the better of it, but I'm feeling exhaustion beginning to creep in.
... I need to last just a little bit longer!

//~show friday resolute(slot 0; replacing the monster)
Mother's breathing is deep and measured; the efficient breaths of a warrior. She's showing no sign of slowing down.
Absentmendidly, she flicks some of her hair out of her face, all while watching the creature intently.
I always knew Mother was strong, but I never grasped exactly how incredile her strength is. How fast she is. How subtle and clever.

//~show creature(slot 1; alongside friday)

How well do I really know her...

~n("Creature")
~shake(5, 0.5)
GRRRooooaaggghhhh!!!

~n("")
It lunges faster than something that size has any right to, straight towards Mother.
Even before I start forming a shield around her, she slips by its heavy swing and inside its guard, her sword upright and close to her chest.
Then, in a practiced motion, she shifts her weight forwards and plants her blade in the monster's side. She presses it in halfway to the hilt before the monster reacts.

//~hide slot 0
//~hide slot 1
//~show bg friday cutin

As it reaches for her, Mother pulls the sword clean and whirls behind the monster, staying a moment ahead of it.
Then, she stabs it once more at the base of the spine and passes her sword through half its waist as she slides it free.

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

//~show friday smile
~n("Friday")
It was nothing.

~n("")
Mother smoothes down the front of her dress and frowns as she notices the splashes of blood that landed on it during the fight.

//~show friday frown
~n("Friday")
Another dress ruined...
{player}, go check on our wayward Sister, will you? I'm going to make sure big guy over here stays dead.

~n("Me")
OK.

//~hide friday
~n("")
We had moved the girl to the side of the cave earlier so she wouldn't be in the line of fire during the fight. She was still unresponsive then, but she was still breathing.
I crouch next to her and shake her gently, trying to rouse her. But there's no reaction.
I try speaking to her but stop after a few seconds when nothing happens, feeling like a fool.
And I don't know what else to do. How pathetic to be stumped after such a short time.
All the while, her shallow steady breathing continues uninterrupted.

//~show friday neutral
~n("Friday")
Anything?

~n("Me")
She hasn't responded to me at all. I don't know what's wrong with her.

~n("Friday")
That's alright. It could be any of a lot of different things; the poor girl's been through a lot.
Well, to be fair she still has more to come, but perhaps the worst is over.

~n("")
Mother sizes her up.

~n("Friday")
I was hoping she'd be able to walk... But it can’t be helped. We should leave here sooner rather than later and there’s no indication of whether the girl is going to wake up anytime soon.
I'll just have to carry her.

~n("Me")
I'll do it.

~n("Friday")
Don't be silly, you're exhausted.
//~show friday smile
And don't act like you're stronger than me, young man. You've still got a ways to go.
Anyway, she doesn't look heavy, so don't worry about it, OK?

~n("")
Mother picks her up gently in her arms, holding her like one holds a baby.

~n("Friday")
~talk(0)
How nostalgic...
~talk(1)
Come now, give me your coat. We need something to cover the poor girl or she’ll catch her death of cold before we get halfway back.

~n("Me")
The Abbess said her name's Moth.

~n("")
That's her name, isn't it? I feel guilty calling her \'the girl.\' for so long. After what I've seen her go through, the least I can do is call her by her proper name.
I pass her my coat. Mother drapes and tucks it around the Sister, doing her best to cover her.

~n("Friday")
Was it? A strange name.
And thanks for lending her your coat. I'd would have given her some of my own clothes, but this dress is rather all or nothing.

~n("Me")
Umm, right. It's not the most practical dress.

~n("Friday")
Nonsense. It’s the perfect dress for the modern noblewoman. Just not this particular one anymore, I suppose.

~n("")
She motions at the bright red bloodstain on its front.

~n("Friday")
Anyway, let's get a move on. This whole episode has raised a lot of questions, like why did the demon bring the girl— Moth, I mean — here? Or did it even mean to?
But now's not the time to answer them.

~n("")
As we leave the cave, I think to myself that I wouldn't mind never seeing this place again. Agairy or any of it.


~n("Me")
She will be okay, won't she?

~n("Friday")
~talk(0)
...
~talk(1)
We'll see.



END
