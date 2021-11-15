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
//10: friday with both breasts pulled out of dress; mc's dick sandwiched between her breasts and finished cumming; breasts relaxed; cum has landed on friday's chest and face
//DONE

//SCENE OUTLINE
// -this scene owes a lot to all quiet on the western front.
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
//    -i'm still such a child, mc says. of course you are, you're my little boy, friday says, but that's not what's bothering you.
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
// -friday smiles gently. how could it be wrong for me to care for boy? who else in all the world could possibly have more of a claim to you than I?
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


// END HAICH PART
// -feel any better, friday asks?
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
