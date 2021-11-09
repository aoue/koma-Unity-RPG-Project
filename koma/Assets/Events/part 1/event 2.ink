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
//Welcome to part 1 event 2
// -location is on the edge of town, where the sister is doing laundry or something. she calls out to mc when she sees him, wants to ask him about moth.
// -in this conversation, we see a little bit into the order sisters' lives. she's on her own and doing extra work as punishment for letting moth escape under her watch.
//  -she feels like she deserves it though. everyone's been so kind to me... and i just keep letting them down
// -mc hears about it, then the sister has to go. she rattles off a list of other things she has to do before evening meal.
// -mc wonders if this is what it was like for friday growing up.
// -end, keep it short.
//=============﻿


~n("")
part 1 event 2.
-> END


//~scene agairy square
I find one of the Sisters at the edge of the settlement. She's working intently hanging up clothes to dry, one piece at a time from a veritable mountain.
It must be the laundry for the entire settlement. But isn't this the time the Sisters are usually praying?

//~show sister (just use their combat sprite, or something very close to it)

~n("Sister")
I fully apologize!

~n("")
I hadn't realized she'd even noticed me, but she swings towards me fluidly and abruptly shouts out.

~n("Sister")
It's really all my fault! So please... direct all your hate towards only me...

~n("")
She hangs her head down and sniffles a bit. And then... as we stand there for a moment and then another, she quickly glances back up at me.

~n("Sister")
You're... I haven't got the wrong person, have I? Aren't you Lady Cenparas' assistant?

~n("")
I really am getting tired of being called Mother's assistant.

~n("Me")
That's me.

~n("Sister")
Then I must strongly apologize!

~n("Me")
OK.

~n("Sister")
Um... what?

~n("Me")
I accept. Whatever you're apologizing for, forget it.

~n("Sister")
Oh. Really?

~n("Me")
Sure.

~n("Sister")
Oh, thank goodness! This feels amazing, like a rock crushing my legs has been gently lifted off.
Now I concentrate on all this with a light and joyful heart!

~n("")
She sets back into the laundry with with intense energy.

~n("Sister")
Umm... is there something you need?

~n("Me")
I was just having a look around. This is a lot of work for one person, you know.

~n("Sister")
Everyone in the settlement has to do their part. I was already doing my part and Sister Moth's part—that's the Sister who got sick—and now I have another one to do, too.
But that's my own fault, aha. If I hadn't been so foolish, then a lot of trouble for other people could have been avoided.

~n("")
She sighs dramatically.

~n("Sister")
Everyone's so kind to me and I just keep letting them down.

~n("Me")
What did you do, exactly?

~n("Sister")
... it's too shameful.

~n("Me")
Oh. I'm sorry, then. You don't have to say anything.

~n("Sister")
Ah, no. I should. It will be good for me.
I...
When Sister Moth left, I mean, ah, I don't know what I mean, yesterday, it was my fault. I was supposed to be watching her.
But I foolishly left her alone when I went to get the Abbess. And I made you and Lady Cenparas go to so much trouble to find her.

~n("Me")
It was no trouble.

~n("")
She doesn't even know the half of it; Mother and I aren't the ones who will suffer the most from this.
Being thrown out of the Order. This Sister has affected Moth's life in a huge way. She doesn't know, I think. And better that she doesn't, it's not really her fault.
Who could blame this girl. She didn't intend any harm. She isn't even aware of what she's done.
Can you be guilty of a crime you don't know you've committed? Not that profound, of course you can. But even so.
It would be unforgivably reductionary to try to blame it all on one person.

~n("Sister")
I really have to get on with my work. This laundry won't hang itself.

~n("")
If I was still as stupid as when I was younger, I would have offered to help her. Instead I turn and slowly leave.
I realize I never got her name. That bothers me a little, until I decide it doesn't matter.
That Sister, everyone here in Agairy, we live in different spheres. A meeting between us is only an accident.


-> END
