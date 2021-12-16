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
// -establish dependence.
// -first haich for mc
// END
//=============﻿

~n("")
part 1 event 4.
-> END


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
You know, if you don't start looking like you're enjoying yourself I may become annoyed.

~n("")
At once all the tension I had been feeling evaporates. What had I been thinking? It's painfully simple.
I press myself back into her chest forcefully and latch onto her breast.
I suck hungrily at her; inhaling her and her scent, the gentle, yet firm, yielding of her flesh. I absorb this moment and push it down deep within myself to be kept and cherished always.
She resumes her petting and I bask in it.

~n("Friday")
Now tell your mother what's on your mind.

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
Now, relax. Everything is going to be okay, I promise. I'll protect you. I love you.

//~set background scene1-6 ; paizuri

~n("Me")
... Mommmm...

~n("")
It's all I can do to moan in content.

//~set background scene1-5 ; paizuri

~n("Friday")
Uh-huh? Feeling any better?

//~set background scene1-6 ; paizuri

~n("")
I manage a nod. Mother beams at me.

~n("Friday")
Good.

//~set background scene1-5 ; paizuri

~n("")
She pushes her breasts tightly together and pauses there. She jiggles her breasts around my shaft, and I watch the rippling of her pale flesh.
I wasn't expecting this at all. Mother... my mother, the strong, kind, proud woman who raised me... whose love has always shone out like a fire, and equal to one in warthm and intensity.
And now she's wrapping her breasts around me, stroking me almost absentmindedly while trying to reassure me. Love pushes its way forward to the front of my chest and I'm reminded how important Mother is to me.

//~set background scene1-6 ; paizuri

But I want to impress her again.

//~set background scene1-8 ; friday with both breasts pulled out of dress; mc's dick sandwiched between her breasts; breasts NOT pushed together; used with [7] to simulate paizuri

~n("Friday")
Ahhh?

~n("")
I waited until Mother's breasts were relaxed against my member, then reached forward and pulled at her dress. The half-in half-out thing was killing me.
Seeing both of her heavy, firm breasts out in the open is more than I can take. Any plans I had to take further action are stopped in their tracks at the sight.
Sooo big... Sooo soft...

~n("Friday")
None of that now, you'll stretch my dress.

//~set background scene1-7 ; friday with both breasts pulled out of dress; mc's dick sandwiched between her breasts; breasts pushed together

If this is what you wanted, you only had to say something, you know. Satisfied?
You're acting just like a child. When you used to nurse, you were always pawing at me just like that. Trying to pull my dress down and the like...
You've always had a special place in your heart for my breasts, haven't you?

//~set background scene1-8 ; not pushed together

~n("")
Of course I have. Who wouldn't? Until today, I've never been able to decide if it was lucky or unlucky to have the best breasts I've ever seen be on my mom; Always in sight, but out of reach.
Not so out of reach, apparently.

//~set background scene1-7 ; pushed together

~n("Friday")
Of course I don't blame you. It's natural to be interested in that type of thing. I've always been a bit worried about you in that respect.
Sometimes you seemed so disinterested in anything that wasn't one of your magic books. Not really worried, I just wanted to make sure I didn't rush you.

//~set background scene1-8 ; not pushed together

~n("")
With both of her breasts out now, Mother starts stroking me faster and harder. She pushes against the bottoms and then drops them back down, ready to catch them and raise them back up.
It feels so soft and warm, and I wonder how much longer I can last.

//~set background scene1-7 ; pushed together

~n("Friday")
But you're ready, whether you like it or not. From watching you today, I'm sure of it.

//~set background scene1-8 ; not pushed together

The way you handled yourself in the forest...

//~set background scene1-7 ; pushed together

And in the cave...

//~set background scene1-8 ; pushed together

How you fought against the monsters... I can see it.

//~set background scene1-7 ; pushed together

You've grown into such a fine...

//~set background scene1-8 ; pushed together

... young...

//~set background scene1-7 ; pushed together

... man...

//~set background scene1-8 ; pushed together

And your mother is so proud of you and who you've become.

//~set background scene1-7 ; pushed together

You've really grown a lot this last year, my beautiful boy.

//~set background scene1-9 ; friday with both breasts pulled out of dress; mc's dick sandwiched between her breasts and cumming; breasts pushed together; cum in the air

~n("")
That pushes me over the edge, and my vision blurs as I cum violently, more intensely than I've ever cum before.

//~set background scene1-10 ; friday with both breasts pulled out of dress; mc's dick sandwiched between her breasts and finished cumming; breasts relaxed; cum has landed on friday's chest and face

It lands all over Mother's chest and face and she slows down her pace before finally stopping her titfucking, relaxing her breasts. But even so, they continue to spill against each other, exerting pressure under their own weight on my member between them.

~n("Friday")
... Good boy.

~n("")
Mother look so beautiful like this.

~n("Friday")
You've been awfully quiet for a while. At a loss for words or something?

~n("Me")
... Yeah.

~n("Friday")
Feel any better?

~n("")
I nod and she smiles brightly at me in return.

~n("Friday")
Good. Remember, I'm here for you anytime... but this sort of thing is special. It's not always possible. Sometimes you'll have to make do with a hug.

~n("Me")
I know, Mom. Just the sound of your voice is enough.

~n("Friday")
Oh, you charmer.

~n("Me")
But Mom... not that I'm complaining, but what was this all about?

~n("Friday")
What do you mean?

~n("Me")
Well, you've... we've... never done anything like that before.

~n("")
She shrugs like it's obvious.

~n("Friday")
You've never been that upset before.

~n("")
How about that. Mother kisses me on the forehead.

~n("Friday")
Try to get some sleep now, alright? I'm going to need your help again tomorrow.

~n("Me")
OK. Mom... you can rely on me, you know. When we're fighting or... when anything.

~n("Froday")
I know I can, sweetheart. But now, sleep. For real. You've had an exciting day, stayed up too late, and I dare say you've had an exciting night too.

~n("")
Mother starts to get up, but I grab hold of her sleeve.

~n("Friday")
Hey. What did i say about pulling on my dress?

~n("Me")
Mom... don't go.

~n("")
She smiles heartmeltingly down at me and winks.

~n("Friday")
Well, OK, you spoiled little boy.

~n("")
Mother lowers herself back down into bed, facing me. She turns me over so my back is facing her and pulls me in close to her warmth, envelopping me.
She rubs my back, and then, so gently that it's barely audible, I catch the first notes of a familiar lullaby from her mouth. I feel totally at ease.
I don't hear the rest.



-> END
