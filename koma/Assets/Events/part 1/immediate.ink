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
//Welcome to part 1 immediate. it plays after player clears dun 0 and hits pass time on overworld. starts at morning of next day.
//items in order of occurence:
//- friday wakes mc up. tells him to wash, get dressed and then head down to the church to meet her. they're going to be working on moth.
//- she leaves and mc starts getting ready. mc flashbacks the events of the previous evening/night, which passed in a blur.
//- they brought back moth and the sisters took over care of her.
//- allowenn and friday.
//- ()
//
//=============﻿

~n("")

part 1 immediate.

-> END

//~show bg inn room
//~play sound birds chirping
...
......
.........

//~show friday neutral
~n("Friday")
Still asleep? Tut tut.

~n("")
Mother draws open the curtains, letting in the morning's frigid air. I moan and burrow deeper into the bed.

//~show friday frown
~n("Friday")
Aww, don't be like that.

~n("")
She sits on the edge of the bed and pats me on the legs affectionately.

//~show friday resolute
~n("Friday")
We're going to start working on Moth in a quarter of an hour.
I need you, washed, dressed, and in the church, in a quarter of an hour. Don't be late, OK?

//~hide friday
~n("")
And with that Mother gets back up and flows out of the room. It's still early and she already has this much energy...
Where does she get it all... I still feel exhausted after the events of yesterday.
Sighing and indulging myself in a moment of self-pity, I force my chilled joints to move and clumsily prepare for the day.

//no flashback, instead people just reference/mc recaps when he does his tour of the village in this part's events.

//mc heads down and walks in on friday and allowenn arguing:
//~scene agairy square
I find my way through Agairy in the early morning light. Already, the settlers are outside working and no one pays me any attention.
It's a sudden turnaround from our arrival yesterday. I wonder if I've already become part of the scenery to them.

//~scene church
I arrive into the church in what feels like less than the fifteen minutes Mother asked, but I have no idea since Agairy's only clock is in this building.
The church is more subdued than the settlement outside, and a few Order Sisters are seated along the wooden pews, facing the back, praying. For Moth's recovery, maybe.
But it's all new to me. Despite Mother's connections to the Order, it wasn't something she included her children in. After yesterday, I think I can see why.
I walk towards the back of the church, to the room where they're once again keeping Moth under careful watch. She's been in there since we brought her back yesterday.
She still hasn't woken.

Just outside the room's door, her two caretakers stand arguing venomously with each other.

//~show friday at left
//~show allowenn at right
~n("Allowenn")
It really is a shame that it had to end this way. Moth was one of our most promising initiates, but... she'll have to leave the Order, of course.
It is called the Virginal Order for a reason.

//~show friday smirk
~n("Friday")
Leave? I think the word you're looking for is \'be expelled.\'

//~show friday neutral
~n("Allowenn")
No matter which word you might use, it remains a shame all the same. If only she could have been rescued faster...

~n("Friday")
If only she hadn't escaped in the first place. Given the mess I had to wade into, I think I did rather well.

//~show allowenn fake smile
~n("Allowenn")
Oh, certainly, certainly.
But even so...

//~show friday resolute
~n("Friday")
You know...
You don't have to expel her. This could stay between the two of us.

~n("Allowenn")
Oh? Not your assistant?

~n("Friday")
Of course he knows, he was there. But consider him the same as me; he has my absolute confidence.

~n("Allowenn")
How remarkable, but even so, it's the three of us who know. And the girl herself must know too, of course.
How very like you to forget her.
But I cannot allow the girl to stay in the Order. There are rules, and they must be obeyed.

~n("Friday")
Must they?

~n("Allowenn")
Yes. Or else everything the Order has worked for falls apart.
The smallest hint of corruption within the Order would be enough to destroy us. The people must trust us, and work with us, or we are nothing.

~n("Friday")
... I see

~n("Allowenn")
 Surely you agree?

~n("Friday")
I'm afraid I don't see it in quite the same way.

~n("Allowenn")
Then it's just as well you're no longer with the Order. If you cannot see this basic truth, you would only hinder us.

~n("")
For a moment, Mother's careful composure cracks and her eyes flash as she wheels on Allowenn.
But the Abbess only smiles. She holds her ground, chin high.
Seeing Allowenn's expression, Mother slowly relaxes. She realizes she'd been provoked.

~n("Allowenn")
Do you know, Lady Cenparas, that you have not changed a veritable ounce from when you were a girl?

~n("Friday")
... You're going to destroy her life before it's even begun.

~n("Allowenn")
It's a kindness. She couldn't be happy in the Order, after this.

~n("Friday")
You don't know that.

~n("")
Allowenn shrugs.

~n("Allowenn")
You don't know either.

~n("")
They stare at each other in silence for a while longer.
Then, without a word, Mother turns and walks away.

//~hide allowenn (right)
As she walks along the edge of the church, she sees me and comes over.
I'd been sitting on a pew while they were talking, listening without being noticed.

~n("Friday")
You were here, huh?

~n("")
I nod. Mother sits down next to me and leans back against the church's stone wall.
A moment passes and she doesn't say anything.

~n("Me")
She was provoking you, Mother.

~n("")
Mother lets out a long sigh, and her chest rises and falls with it.

~n("Friday")
I know. But even then, it doesn't seem to matter. Even when I know it's what she wants me to do, some things need to be said even if they'll be ignored, or they'll be forgotten.
And Allowenn will expel the girl no matter what I say. But you saw how she was acting, she wanted me to argue with her. I can't imagine why.
Maybe in a twisted sort of way, it makes her feel better.

~n("Me")
You were talking to her about the people who knew about what happened to Moth... about what happened.
Couldn't we have just hidden from Allowenn?

//~show friday smile (proud of her boy)

~n("")
She shakes her head sadly.

~n("Friday")
Not realistic.

//~show friday neutral

I'm not sure how much you know on the subject; circumstances have forced me to become rather well-versed. But when a monster... impregnates a human, it's not quite the same as a human pregnancy.
For one thing, conception is a sure thing. You can be certain that Moth is pregnant with that monster's offspring, though it's still in the early stages.

~n("Mc")
But that can be removed, can't it? With magic?

~n("Friday")
Exactly right. It can removed in other ways, too, but light magic is the safest and most effective method. The catch is you have to act fast enough.
A monster's offspring is ready to be born rather faster than a human child. It takes just over a month from conception, with the point of no return being at roughly the halfway point: Two weeks, to play it safe.
Once those pass, no magic is going to stop it. Removal at that point relies on messier methods. Much messier.

~n("Me")
That's not a lot of time.

~n("Friday")
You're right. And that leads into the other catch; you need an expert to perform that sort of light magic.

~n("Me")
And the experts in light magic are part of the Order.

~n("Friday")
Right again. Just looking at the logistics of it, there's no way to cure Moth without Allowenn noticing, since we'd have to leave Agairy.
And even if we could manage that, the Order has records on its members. She'd be found out and then expelled immediately, and far less gently.
With exceptional luck, we might be able to get her another few months.

~n("Me")
Couldn't you just cast the magic on Moth?

//~show friday smile
~n("Friday")
I'm flattered. Your mother is very talented, you know, but that's not my area of expertise. Sorry to disappoint.

~n("Me")
Oh...

//~show friday smirk
~n("Friday")
Hey, try not to look so crestfallen. You know this is just one of the many things I can't do, right?
Part of growing up is learning your Mom isn't all-powerful.

~n("Me")
I know.

~n("Friday")
Hey, you OK? You did sleep, right? You still look tired.

~n("Me")
I'm exhausted.


~n("Friday")
It's the magic. Tires you out more that it has any right to.

~n("Me")
I've never used this much in such a short period.

~n("Friday")
I'm not surprised. You've never had to fight for your life before.
You'll have to work on building endurance, then. It can be done.

~n("Me")
Aren't you tired at all, Mom?

~n("Friday")
Not at all. How could I be, when I was with you all day? Just a look at your face is enough to wash away all of your Mother's fatigue~
Well, not really. Of course I'm a little tired.

~n("")
She leans over and gives me on the forehead.

~n("Friday")
But you did well yesterday.
Be good today and I promise we won't have to fight for our lives more than once or twice.
OK?

~n("Me")
OK...

~n("Friday")
Good boy. Now, let's check on our patient.

//~hide friday
//~scene change to agairy church room. (A bed in there, with adj chair, and moth is laying in in, asleep. stone walls, same as rest of church.)

Mother leads us past the Sister at the door, who averts her gaze as we slide past her and into the cramped room at the back of the church.
There's just barely room for the flimsy wooden bed and the chair in there now. It probably used to be a storage closet or something, and was hastily converted when Moth fell under... whatever it is she's under.

//~show friday neutral
~n("Friday")
Go on, tell me what you make of her.

~n("")
At Mother's urging, I step to the bedside and take a close look at Moth.

//~hide friday
She's just laying there, unmoving. The same as she was back in the cave.
Like then too, her breathing is normal and at regular intervals, accompanied by the gentle rise and fall of her chest.

~n("Me")
She's the same as yesterday. No change at all.

//~show friday
~n("Friday")
Right. And what do we make of that. Good or... ?

~n("Me")
... good, I guess.

~n("")
Mother raises an eyebrow, as if to say: \'go on.\'

~n("Me")
Well, we haven't been able to break her out of this state yet, but her condition hasn't changed. So we probably have more time.

~n("")
Mother's started doing something as I talk. After a moment, I recognize the telltale feeling of magic flowing out from her and forming a loose cocoon around Moth.

~n("Me")
And since we have more time, that means she's not going to die or anything then.
Which is good.

//~show friday smile
~n("Mother")
A truly enlightening analysis, and right enough.
I've already tried a few things to get her up today, but no luck so far. There's not much to go on, I'm afraid, so I just have to try everything I know.
I've done this enough that it'll take me a while to run out of things to try.

~n("Me")
That magic you were casting on her...

//~show friday neutral
~n("Friday")
Simple protection magic. The same kind I keep on you and your sisters.
I shouldn't think it will improve her conditon, but it may stop it from getting worse. Maybe. It all depends on the nature of what's ailing her...

~n("Me")
... and we don't know what that is.

~n("Friday")
Precisely. And now that you're here, we'll be able to exhaust in vain the expanse of my exorcism knowledge much faster.
Here, take these and divide them into four equal piles. Then this scroll and repeat after me, oh, and remember to enunciate...

//~hide friday
//~scene church
...
......
.........
Before long, afternoon comes around and Mother declares a break. We return to the main room where the air is fresher.

//not satisfied with from here to the end.

//~show friday frown
~n("Friday")
That's nearing the limit of what we can do without more extensive preparation.
Why don't you have a look around the settlement if you like.

~n("Me")
I can't help with the prepations?

~n("Friday")
No, not really. Besides, you must be dreafully bored. Read a book, if you don't want to walk the settlement.

~n("Mc")
That's alright, Mother, I'll stay with you.

~n("Friday")
I feel guilty keeping you here all to myself. A kid your age should be exploring the world.

~n("")
I shrug.

~n("Me")
I came so I could learn from you and you know I'm not the exploring type.

~n("Friday")
Of course you are, it's in your blood, same as me.
Now shoo. If anything at all happens I'll send one of the Sisters to get you. Don't wander too far off now.













//END
