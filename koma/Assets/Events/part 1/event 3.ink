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
//Welcome to part 1 event 3
// -location is some other spot in town.
// -mc happens upon a pair of settlers who are painting a wall or something to weather-proof it. they call him over and talk to him.
// -they introduce themselves and fast-talk to him. they are Anornen and Nornen ;unskilled workers who stick together for a few jobs now
//  -they tell mc he sure is lucky with his work, yessir, they would love his job. wander around, live off other's hospitality --oh, not that we blame ya. 'course i'd do the same, if 'n i could
//  -not to mention, spendin all that time traveling with a lady like that, hoho, now that's a treat. you might be young, but you've got a fine eye to squirm yer way into a spot like that
//  -let me give ya a piece a advice. woman like that -- you know, rich etc, but with a killer body, you just need to be a little rough with em. just a bit, and then they'll do whatever ya say.
//  -whatever ya say, if 'n you catch my drift.
// -...thanks for the advice, says mc. next line is him thinking 'drop dead, asshole'
// -as he leaves, he reflects that it's not the ordinary people who would be part of a tentative settlement, would it.
// -end, keep it short.
//=============﻿

~n("")
part 1 event 3.
-> END

//scene agairy square
I come across a pair of men weatherproofing one of the buildings at the edge of the settlement.
An older and a younger one, both dressed in the plain, affordable clothes that the settlers would have bought before coming here. That's because in a frontier settlement like Agairy, good clothes are hard to come by.
But despite the slight chill in the air, remaining even now in the afternoon, the two of them are sweating as they work.
Then one nudges the other and they take notice of me.

//show two settlers

~n("Younger Settler")
'Ey, it's the boy who came with the fancy lady.

~n("Older Settler")
'Ey, so it is.

~n("")
It's nice when other people make your introduction for you. I guess in Agairy any outsider sticks out like a sore thumb.

~n("Older Settler")
Well don't just stand there, come and have a seat!

~n("Younger Settler")
Yeah, why not? Come, and tell us something about the cities back home. It's been what feels like ages.

//scene agairy square
~n("")
...
Despite asking me to tell them about the cities, they barely give me a chance to speak before starting in with their own complicated tale.

~n("??")
Nornen and Anornen; that's us.

~n("")
He waves his hands vaguely as he introduces them, and so I don't know which is which. I assign the names to them at random.

~n("Nornen")
We've been labourers, farmers, workers, builders, diggers, miners, you name it. We go where there is work, and leave when we've done it.
It's been a few years now we've been doing that.

~n("Anornen")
Fourteen now, was it?

~n("Nornen")
Can't be that many. But it's been a few, that's for certain.
And that's what we did, until our numbers came up; got us a place here for our trouble.

~n("")
The resettlement lottery. There's more people than can fit on a settlement all at once. If they left all at once with organization, well, there'd be fighting, riots, and so much et cetera.
Instead the Kingdom controls the flow, never sending more people to a place than it can support at the time. At least in theory, they're not perfect; but it's still better than nothing.
Needless to say, as a member of the Cenparas family, we don't participate in the lottery.

~n("Me")
That's good luck for both your numbers to come up together.

~n("Anornen")
Aye, it was. Some sort of sign, probably.
But not sure if it was worth it. The work's harder, the pay's worse, and worst of all it's in the middle of nowhere.

~n("Nornen")
Ah, that's not all. It's a damn sight better than what we'd have if we'd stayed at home. We do get something out of this.

~n("Anornen")
Of course. Our promised land.
Fifteen years' hard work and then our pick of the land.

~n("Me")
They're really giving you land?

~n("Anornen")
Hahaha, Hardly!

~n("Nornen")
They're giving us the opportunity to buy land.
Who knows if we'll be able to afford it in fifteen years' time. Not to mention young enough to work it.
It's fine for young folks, but I wonder if this was the right decision for every man.

~n("Anornen")
You're exagerating. It's been a bit rough until now, but things are finally starting to look up.
The rest of the buildings are starting to shape up; we had to start with the granary and storehouse first, of course, and then the church, naturally, and other essentials, but the sleeping rooms will be good in no time.
And then at some point, more settlers will come. Maybe even some women.

~n("Nornen")
Stands to reason, it does. If you're going to have a settlement for more than one generation, you'll have to send some women.

~n("")
The Sisters from the Order are here now, but obviously they don't count. Not for what these two are thinking of.
A shame about that one Sister though, they say. They didn't quite hear what happened to her, only that is was rather bad.
After a few minutes talking to them, I start to get a picture of the state of these new settlements. They're filled with desperate people without a better place to go.
Finally, they stop for breath.

~n("Anornen")
Don't be so shy now, lad. When will you tell us about yerself?

~n("Nornen")
And more importantly, about that lovely lady you've somehow gotten yourself in with.
Hoho, what a treat every day must be, just the two of you, travelin' alone like that. You might be young, but you've got a fine eye to squirm your way into a spot like that.

~n("Me")
Well... I've known her for a long time. Ever since I was a kid.

~n("Anornen")
A friend of your family?

~n("Me")
Yeah.

~n("")
I feel a little bit bad lying to this two who have been so open about their own lives, but Mother would be upset if she knew I strayed from the story. And she would know.

~n("Anornen")
Some people have all the luck, don't they. But hey- it you knew a nobby woman like that, you must be some sort of noble yourself, eh?

~n("Me")
Something like that. A very minor family.

~n("Nornen")
Let's hear it then.

~n("Me")
... what?

~n("Anornen")
Come on now, I love the sound of those fancy noble names.

~n("")
My mind is suddenly going blank.
I reach to a familiar name. Sorry about this, mate.
Oh well, I'm sure he'd understand.

~n("Me")
Oh. It's... Alcernas.

~n("")
They shrug.

~n("Nornen")
Ha! Never heard of it.

~n("Me")
Like I said: very, very minor nobility.

~n("")
We talk a few minutes more before and then I start to leave.

~n("Anornen")
Before you go, let me give ya a piece of advice, lad. It's good advice, mind, so listen closely.
A woman like that lady, rich and snobby with a killer body, you just need to be a little rough with 'em. Just once, and just a bit, and then they'll do whatever you say.

~n("")
He winks suggestively.

~n("Anornen")
If you understand my meaning. I'm sure you do, smart lad like you.

~n("Me")
... Thanks for the advice.

~n("Anornen")
Don't worry about it. Remember, all women want to be dominated by a man. Just show them you're stronger, and they'll fall over themselves trying to please you.

~n("")
As I wander away, I think about what he's said.
I can't imagine anyone being rough with Mother and still being alive afterwards. And doing what someone else says? Simply not possible.
Mother's the sort of person who's born to give orders; it's part of the whole nobility thing. The only time she'd take an order is if it was something she was going to do herself anyway, and then doesn't really count.
Sure, there's exceptions to every rule, but those two aren't exactly a wellspring of authority. After all, they're looking at fifteen more years of hard work in a desolate, dangerous frontier settlement.
Is that what people who can rich noblewomen to do whatever they want would be doing?
Ah well, I guess it's not the ordinary people who would live in a place like this.
-> END
//end
