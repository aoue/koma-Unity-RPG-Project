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

//CG OUTLINE - haich scene 1
//0: friday with dress fully on
//1: friday with dress fully on; mc pressed into chest (back of head visible); friday's hands on his back
//2: friday with her left breast pulled out of dress; mc still pressed into chest (back of head visible); friday's hands on his back
//3: friday with her left breast pulled out of dress; mc sucking her breast (back of head visible); friday's hands on his back and his head
//    -fri: i see. that's more serious than i was imagining. you poor thing, this might not be enough.
//    -one second...
//4: friday with her left breast pulled out of dress; mc's dick exposed, (back of his head no longer visible.); (similar to the original concept image now)
//5: friday with her left breast pulled out of dress; mc's dick sandwiched between her breasts; breasts pushed together
//6: friday with her left breast pulled out of dress; mc's dick sandwiched between her breasts; breasts NOT pushed together; used with [5] to simulate paizuri
//7: friday with both breasts pulled out of dress; mc's dick sandwiched between her breasts; breasts pushed together
//8: friday with both breasts pulled out of dress; mc's dick sandwiched between her breasts; breasts NOT pushed together; used with [7] to simulate paizuri
//9: friday with both breasts pulled out of dress; mc's dick sandwiched between her breasts and cumming; breasts pushed together; cum in the air
//10: friday with both breasts pulled out of dress; mc's dick sandwiched between her breasts and finished cumming; breasts relaxed; cum on friday's chest and face
//DONE
//I read all quiet on the western front.

//SCENE OUTLINE
//Welcome to part 1 event 4 (haich icon)
// -mc is laying in darkness in the room they've been put up in.  just can't seem to sleep.
// -runs through the events of the past days; shock, disturbing things, uncertainty.
//    -he's worried about moth and her future. (guilt, they should have been faster. they could have risked helping her before the monster was done.)
//    -he's worried about friday retroactively (he didn't realize how dangerous her work was all this time)
// -mc supposes since he cant sleep he might as well work. he starts at the spellwork he brought with him. casts a light spell and brings out spellwork.
// -......... ....... ...
// SCENE 1 haich part
// -later, as mc is still working (he doesn't know what time it is), friday arrives back. he hears her coming and extinguishes the light, hides spellwork, etc.
// -he listens to her prepare for bed for a few moments and then slowly come closer... until suddenly she moves down into his bed with him.
//SWITCH TO SCENE 1 HAICH view. Using the special cgs. dress on.
//    -you're not sleeping, she says accusingly. how did she know? she's his mother, of course she knew.
//    -tell me what's wrong, she says. mc tries to be strong, but there's the overpowering urge to collapse and fall into her arms. Finally, he does.
//    -i'm still such a child, mc says.
//    -of course you are, you're my little boy, friday says, but that's not what's bothering you.
// -so a little heart to heart. then friday says, shh, i understand. Here...
// -wha? mom?
// -Let your mommy take care of you. i promise you'll like it.
//UPDATE SCENE 1 HAICH view. remove dress + breastfeeding and head stroking
// -all at once, she hugs my face down to her chest. "there, there," she coos.
// -she guides me to her heavy breast and breathlessly pushes her nipple into my lips.
// -hmmmm, hmmmmm. she starts humming to me gently and running her hand through my hair.
// -this always calmed you down when you were little..., she says.
// -mom, isn't this wrong? ...
// -monologue: but my heart isn't in it. even as i say that, i suck hungrily at her. i inhale her, her soft smell, the gentle, yet firm, yield of her flesh. i absorb this moment and push it down deep within myself to be part of me always.
//    -the things ive heard, that people say, condemning this between mother and son... it seems funny now. How could anyone say that, how could this be wrong. It feels like the most natural thing in the world.
// -friday smiles gently. how could it be wrong for me to care for my son? who else in all the world could possibly have more of a claim to you than I?
// -Now, relax. Everything is going to be okay, I promise. I'll protect you. I'm here for you. I love you. Always.
// -mc: ...momm... he groans out.
// -fri: shhh. you're alright. Now, talk to me.
// -with mc's thinking: it all pours out. the this, the that. fear and worry long buried, from months and years ago;
//    -the loneliness from my first days at the academy; from when i was lost as a child, from every small thing that had ever happened.
// -fri: i see. that's more serious than i was imagining. you poor thing, this might not be enough.
// -one second...
//UPDATE SCENE 1 HAICH view. sandwiched.
// -...there.
// -how's that, any better?
// -monologue: this is a level further.
// DO HAICH STUFF [...]
// THEN IT ENDS.
// -there, says friday, stroking mc's face and chest. feel any better?
// -...yeah.
// -good. remember, im here for you anytime... but this sort of thing is special, you know it's not always possible.
// -i know. Just the sound of your voice is enough, mom.
// -Oh, just that? the other part isn't necessary?
// -i didn't mean-
// -go to sleep, you charmer. Goodnight.
// -monologue: friday starts to move away and into her own bed, but mc grabs her by the arm.
// -...please stay, he says.
// -friday wordlessly comes back and envelops him again in her arms. kisses him on the forehead.
// -she starts to hum a lullaby.
// -i feel, for the first time tonight, the long arms of sleep reaching out to me. I reach back.
// END
//=============﻿

~n("")
part 1 event 4.
-> END﻿﻿

//~set background darkened agairy room.
I feel tired.
Using so much magic yesterday still has me feeling drained. As for today... well, I could never feel quite at ease.
Unfamiliar places have that effect and Agairy is about as unfamiliar a place as any I've ever been. The people, the buildings; all quite normal. But being out here on the very edge of civilization affects everything.
Or perhaps we're beyond the edge. I think of the events of yesterday. The girl, Moth, and how she was abused by a horrible monster. What will her future hold? Does she even have a chance at one? I realize I know next to nothing about the Order Sisters.
And I worry about Mother. I never realized how dangerous her work was and I start to worry. There have been so many times she may not have come back.
I think back to crushing hugs delivered unexpectedly before she would leave sometimes, and understand. She wasn't sure whether she would be coming back.
But she always did. My Mother is the strongest person I've ever known. But even knowing that, I can't help feel shaken after what I've now seen. I want to forget.
I realize a moment later what a selfish thought that is.
Some of us aren't able to forget. Moth, for instance. It will undeniably shape everything else in her life from now on, if she ever wakes up...
And so I spend a long while thinkig of this sort of thing in the dark without falling asleep, despite my fatigue. Accompanied by a dull ache in my stomach and the side of my chest, I feel I'm back at the academy the night before examinations.
I decide to do what I would have done then: If you can't sleep, work.
I fumble in the darkness for the spellwork for a moment before giving up and lighting a candle on the bedside stand.

//~set background agairy room with some more light; candle lit
As I work my way through the familiar verses, I feel myself become calmer. My mind fills with the complex back and forth of magics and my worries are momentarily set aside.
.........
......
...
Footfall outside the door.
I look up for the first time in a while. I'm not sure how long it's been. It's still dark outside at least, but I'm quick enough to extinguish the candle I had lit.

//~set background darkened agairy room.
The door opens, it must be Mother returning to our room, courtesy of the Order. They've put us in here together, but there's two beds so it's fine.
I don't want her to see me awake. She'd lecture me, quite rightly, about what it does to my body and what it will do to me tomorrow. Of course she'll know tomorrow anyway when the fatigue shows in my face, but for some reason keeping it from her until then feels vitally important.
Long ago I realized I've never stopped trying to impress her. Oh well, I pull my spellwork under the blanket and focus on breathing softly and regularly to give the perfect appearance of sleep.
......
...
//~show friday outline
I can only just about make out Mother moving about in the room. It has to be her. I can tell from the figure alone.
My mind flits back to the thoughts I was having earlier tonight. Was she thinking about the same things, I wonder.
Probably not, I think, and reasons supply themselves: "She's seen this before, she's been far too busy to worry, she's planning our next three moves..."
But a reason, even a good one, shouldn't be confused with the real one. I know I just don't want to imagine Mother worrying about this. She isn't allowed to.
I know it's selfish and arrogant to think like this but I don't care. I feel like a very small, very afraid child, wanting to be held.

//~hide friday outline
...
Her shadow disappears and I feel like I've had something taken from me.
.........
......
...

// SCENE 1 HAICH PART BEGINS
//~set background scene1-0 ; dress fully on
~n("Friday")
You're not sleeping.

~n("Me")
How did you know?

~n("Friday")
You're resting in an unnatural position, sleeping people don't lie like that. Also, your breathing is all wrong, and you haven't moved at all. Someone who's really sleeping would shift around when the door opens, when the airflow changes.
Lastly and most importantly, I'm your mother. It's only natural I know everything about you.

~n("")
So that it explains it then. For some reason Mother's not in her bed and has moved down into mine.

~n("Friday")
So? Tell me what's wrong.

//~set background scene1-1 ; mc pressed into chest (back of head visible); friday's hands on his back
~n("")
I consider trying to put on a brave face for all of half a second before giving in to the overpowering urge to fold up and collapse into her arms.
She holds me silently, and one hand rubs my back gently.

~n("Me")
... I'm still such a child.

~n("Friday")
Of course you are, you're my little boy. But that's not what's bothering you.

//~set background scene1-2 ; friday with her left breast pulled out of dress; mc still pressed into chest (back of head visible); friday's hands on his back
~n("")
The sound of clothes moving, but I can't see from my position with my face pressed into her chest. I feel the soft fabric of her dress whish gently past my cheek and the give of her chest against the weight of my head.

~n("Friday")
There, there... Let your mother take care of you.

//~set background scene1-3 ; friday with her left breast pulled out of dress; mc sucking her breast (back of head visible); friday's hands on his back and his head
~n("Me")
Whaht- Mohmh?

~n("")
I instinctively speak out in surprise as I realize what's happened, but the words don't come out right. My mouth is rather full. Very full. There's more breast around my face than I thought possible, surrounding me on every side.
Still in confusion, I try to pull away but her hand holds me steadfastly to her bare breast. She breathlessly pushes her nipple between my lips.

~n("Friday")
This would always calm you so when you were a baby. There, there...

~n("")
She's humming and carefully running her hand through my hair.
Suddenly, she freezes.

~n("Friday")
You know, if you don't start looking like you're enjoying yourself I might get offended.

~n("")
At once all the tension I had been feeling at this sudden turn evaporates. What had I been thinking? It's so painfully simple.
I press myself back into her chest forcefully and latch onto her breast.
I suck hungrily at her. I inhale her; her soft smell, the gentle, yet firm, yield of her flesh. I absorb this moment and push it down deep within myself to be kept and cherished always.
Her petting resumes and I bask in it.

~n("")
Everything that had been on my mind earlier, all my worries, come out all at once. Fear, worry, loneliness, every small thing that's every been on my mind; they all seem silly now, like somebody else's problems. How could I have felt this way with Mother right there?
She just listens silently, soaking it up, and after a while I run out of words. After waiting a moment as if to make sure I was really finished, Mother hugs me tighter.

~n("Friday")
Shhh. I understand. You poor thing.
It's more serious than I'd been imaginaning. This might not be enough.

//LONGER FADE HERE
//~set background scene1-4 ; friday with her left breast pulled out of dress; mc's dick exposed, (back of his head no longer visible.); (similar to the original concept image now)
One second...

~n("")
Mother changes position on the bed. One of her heavy breasts is out in the open.

~n("Friday")
I wish I had better advice to give you. It's a messy world. Dangerous, ugly. It's natural to be disturbed by things like what's happened. I'm afraid I've gotten rather used to it.
Unfortunately, they happen all the time. Every monster is evidence of a horrible act having been committed to some poor soul.
Just remember why you're fighting; a good reason is something to anchor yourself to, to give you the strength you need to continue from outside when you can't find it inside.

//~set background scene1-6 ; friday with her left breast pulled out of dress; mc's dick sandwiched between her breasts; breasts NOT pushed together; used with [5] to simulate paizuri
~n("")
She leans forward casually, without stopping speaking, and places me between her breasts.

//~set background scene1-5 ; friday with her left breast pulled out of dress; mc's dick sandwiched between her breasts; breasts pushed together

~n("Friday")
You and your sisters have always been mine. I want you all to live in a better world than this one.

~n("")
The feeling as she presses her breasts together is overwhelming. It's comfort on a level I've never felt before, but also excitement and anticipation. It feels like torture, but also the most pleasurable feeling I've ever experienced.
She laughs, a little at my reaction and a little at her own words.

~n("Friday")
And to that end I've dragged you right into the middle of an undisputable mess. I wonder, does that make me clueless or a hypocrite? On balance, I think I'd prefer the former.

//~set background scene1-6 ; paizuri

~n("")
The world... I remember all of a sudden that I'm a part of it, and the things I've heard people say about mothers and sons, together like this.
They seem absurd now. How could anyone even suggest it's wrong in any way? It feels like the most natural thing in the world.
As if reading my thoughts, Mother laughs a little.

//~set background scene1-5 ; paizuri

~n("Friday")
How could it be wrong for me to care for you? Who else in all the world could possibly have more of a claim to you than I?
Now, relax. Everything is going to be okay, I promise. I'll protect you. I'm here for you and I love you, always.

//~set background scene1-6 ; paizuri

~n("Me")
... Mommmm...

~n("")
It's all I can do to moan in content through my full mouth.

//~set background scene1-5 ; paizuri

~n("Friday")
Yeessss? Feeling any better?

~n("")
I barely manage a nod.

~n("Friday")
I'm glad. I'd hate to think this was all for nothing.

//~set background scene1-6 ; paizuri




//this was all to make me feel better, mc asks? implication; won;'t happen again
//friday mock scowls; don't be silly, i wouldn't do this with anyone who was feeling down. no, it's all because your my son, and i love you.

//friday speeds up, and knocks the rest of her dress off her chest; switch to 7-8 for paizuri
//amazing softness talky
//





// POST HAICH:
// -there, says friday, stroking mc's face and chest. feel any better?
// -...yeah.
// -good. remember, im here for you anytime... but this sort of thing is special, you know it's not always possible.
// -i know. Just the sound of your voice is enough, mom.
// -Oh, just that? the other part isn't necessary?
// -i didn't mean-
// -go to sleep, you charmer. Goodnight.
// -monologue: friday starts to move away and into her own bed, but mc grabs her by the arm.
// -...please stay, he says.
// -friday wordlessly comes back and envelops him again in her arms. kisses him on the forehead.
// -she starts to hum a lullaby.
// -i feel, for the first time tonight, the long arms of sleep reaching out to me. I reach back.
// END
//=============﻿


//CG OUTLINE - haich scene 1
//5 ; friday with her left breast pulled out of dress; mc's dick sandwiched between her breasts; breasts pushed together
//6: friday with her left breast pulled out of dress; mc's dick sandwiched between her breasts; breasts NOT pushed together; used with [5] to simulate paizuri
//7: friday with both breasts pulled out of dress; mc's dick sandwiched between her breasts; breasts pushed together
//8: friday with both breasts pulled out of dress; mc's dick sandwiched between her breasts; breasts NOT pushed together; used with [7] to simulate paizuri
//9: friday with both breasts pulled out of dress; mc's dick sandwiched between her breasts and cumming; breasts pushed together; cum in the air
//10: friday with both breasts pulled out of dress; mc's dick sandwiched between her breasts and finished cumming; breasts relaxed; cum has landed on friday's chest and face
//DONE




















-> END
