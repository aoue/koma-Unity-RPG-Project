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
EXTERNAL shake(intensity, duration) //camera shake. both parameters are ints. actual duration = 0.05f seconds * duration
//end functions

//variable controllers here. set by EventManager at scene start.
VAR ic = 0
VAR player = "playerCharName"
//end variables

//SCENE OUTLINE
//Welcome to part 1 event 1
// -location is in the church. mc enters while the abbess is leading some of the sisters in prayer.
// -in the back of his mind, mc is thinking about winning over allowenn and ending her feuding with friday. brief tutorial, then they can talk about other things:
//they talk about: [choice menu]
// -so who is mc? mc answers honestly but without giving his identity as friday's son away
// -how long has he been friday's assistant? actually, it's his first time. Strange says the abbess, friday had said earlier he had her absolute trust. mc says something.
// -what's it like working under friday? the abbess loads the question negatively. Mc flips it around and says how great it is and disses agairy and the abbess.
// -End. well, there's always more to do for an important woman like me, says the abbess, do say hello to your mentor for me.
//lastly,
// -the abbess tries to recruit him for the order. we take anyone, she says, as long as they have room in their heart for compassion and love. and one other thing, tu es un vierge, n'est pas?
//=============﻿

part 1 event 1.

-> END
//scene agairy church
~n("")

After a while, I wander back into the church. As there always seems to be, a few of the Order Sisters are seated along the wooden pew, hands clasped together, praying.
The abbess is standing at the very front, facing back towards the rest of them.
I feel like I'm intruding here. I watch for just a moment longer and consider ducking out before I'm seen.
But it's too late.

//show allowenn
~n("Allowenn")
Pray, wait a moment longer, child.

~n("")
She's seen me from across the room and comes over.

~n("Allowenn")
Please, if you have come to pray, you musn't let our presence disturb you. There is room enough for us to pray, Sisters of the Order and not, alike.

~n("Me")
I was just watching.

~n("Allowenn")
I see. I must apologize, I didn't get your name when we were introduced.

~n("")
Of course she didn't, there was no introduction. Those two were clawing at each other from the very start.

~n("Me")
{player}.

~n("Allowenn")
And as you certainly know, I am Abbess Vena Allowenn, but \'Abbess\' or \'Abbess Allowenn\' will do when you address me.
And now, you must pray with us. It does the Sisters such good to have the occasional guest. It does so reminds them of why the Order is important.

//scene bg church

~n("")
Mother brought all her children to the churches in the city many times, though they weren't much like this one. Brighter and larger for a start, and crowded at all hours.
But it's familiar in the way that all churches are.

~n("Allowenn")
Lady Cenparas is a devout woman in her own way, I'm sure...

~n("Me")
Yes, she is.

~n("")
Is she? Sometimes I think so, sometimes I'm not so sure.
Maybe that's how she feels herself. It changes. No bright or bitter mood can last forever, after all.

~n("Allowenn")
I'm sure you could say much more about her than that.

~n("")
Not very subtle. But If she thinks I'll say anything against Mother, she's wrong.

~n("Allowenn")
How long have you been her assistant, then?

//mc reasons why he continues the deception
//and that he feels annoyed as being referred to as an assistant; he feels it's underselling him.
~n("")
Mother introduced me as her assistant at the start, and I suppose I have to keep it up now. Not that I completely understand why.
Though I think assistant is underselling my abilities.

~n("Me")
Not long. But it feels like longer.

~n("Allowenn")
My, is that all? And yet she says you have her absolute trust.

~n("Me")
Because I'm absolutely a trustworthy person, I guess.

~n("Allowenn")
How did you come to be her student? She's quite the character, you must admit.

~n("Me")
I've known her for a long time, actually.

~n("Allowenn")
Oh, a friend of the family?

~n("Me")
Something like that.

~n("Allowenn")
I see. Lady Cenparas was always difficult to work with while she was in the Order. I imagine it isn't easy for you either.
Even more so because you're on your own with her. No one to help you handle her more... extreme qualities.

~n("Me")
Yeah, sometimes it can be hard to keep up with her. That's hardly her fault, though. It seems like your Sisters don't have to worry about that.

//~show allowenn fake smile. she's offended.

Because you're more considerate, I mean.

~n("Allowenn")
Quite. There's always more to do for a woman as important as me. Ah, do say hello to Lady Cenparas on my behalf the next time you see her. She really ought to come pray with us before she goes.

//~show allowenn fake smile 2, she's attacking.

And when she becomes too much for you to put up with, think of us. There's room in the Order for anyone who makes room in their hearts for it.

~n("Me")
No, thank you. I've seen how you treat your members.

~n("Allowenn")
You mean... yes, it's unfortunate. No one regrets it more than I do. But it's for the best.

~n("Me")
I've heard your arguments already.

~n("Allowenn")
Then you disagree.

~n("")
She shakes her head sadly.

~n("Allowenn")
You should learn to think for yourself rather than accepting everything and anything your teacher tells you. All students surpass their teachers inevitably, though for some it doesn't take long.

~n("Me")
...

~n("Allowenn")
... we do a lot of good in this otherwise cruel world.

~n("Me")
So does Lady Cenparas, and she would never cast me aside. I'm still young and interested in long-term employment, you see, so that kind of thing is important to me.

//~show allowenn neutral
~n("Allowenn")
How little you know of her. She has blinded you.

~n("Me")
Really, it's not so bad.

-> END
