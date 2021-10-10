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
//Welcome to part 0 event 1. The location is Agairy's town square. It unlocks dungeon0, which the player must clear to proceed.
// -mc and friday arrive in town, where people stop what they're doing to watch them. they're curious of outsiders, friday explains, but don't mean ill.
// -before long, the abess arrives to greet them. the abbess explains that the sister has gone missing. friday asks if they've searched for her, and the abbess says of course, but the town is small and there are not many places to hide. She fears the sister has left the settlement and entered the wilderness. The settlers don't dare go in themselves.
// -the abbess implies that friday should volunteer to look for the sister in the woods, so friday and mc head into the forbidding woods around the settlement.
//=============

part 0 event 1.
-> END

~n("")

//show agairy town square
It's afternoon when we finally walk into Agairy.
Only recently founded, it's one of the many settlements that have springing up in the past few years. They're usually backed by someone or some people in the Kingdom who are looking to expand their influence outwards.
Which is why although each settlement is a sizable risk, the prospect of wealth and power keep interest high. Agairy's own backing is obvious through the icons and emblems of the Virginal Order that abound.

The settlement's layout is a close-knit circle around a central area. Someone has cleared it, probably to prepare it for paving, but it remains beaten down dirt for now.
The buildings are little more than shacks, which is well enough since they'll be torn down eventually when the settlement grows, but at the far end of the square its largest building stands proudly.

//show friday
~n("Friday")
Notice how the only stone building is the church. It's letting everyone know: this building is here to stay and don't you forget it.
If the settlement ever becomes anything greater, you can be sure it'll keep the Order's best interests at heart. Say what you will about them, but if there's one thing the Order never lacked, it's foresight.

~n("")
She shrugs.

~n("Friday")
Oh, well. At least the settlers will have somewhere to hide if we make a mess of the exorcism and things go horribly wrong.

~n("Me")
They'd be safe in there?

~n("Friday")
Certainly not. I don't mean to be dreary but a monster that gets pacst us wouldn't be stopped by something like a building.

~n("")
It's now that I start to notice the people around us. Half-hidden in doorways, or blending inobtrusively into the scenery, a few settlers stand watching us silenty.
They've stopped what they're doing; a man rests a full wheelbarrow on the ground and leans against the wall, a pair of women carrying buckets attached to yokes on their shoulders set them down at their feet.

~n("Me")
Mom, they're staring at us.

~n("Friday")
It would be strange if they weren't. We're probably the most interesting thing to show up here in who knows how long.

~n("Mc")
Really? What's so interesting about us?

//show friday smile
~n("")
She laughs. Some of the watching settler women are staring at Friday's dress.

~n("Friday")
Hmm, I wouldn't really know. Fashion?
Maybe it's just that they don't know our faces. In a settlement this size, everyone knows everyone. They're probably sick of looking at the same thirty faces every single day.

~n("Me")
There's definitely more than thirty people living here.

~n("Friday")
What makes you say that?

~n("Me")
Look around. With this many buildings there could be easily a hundred people living in them.

~n("Friday")
Whether or not there's room for them, having that many people is more of a drain than anything at this stage in the settlement's life. They don't want a hundred mouths to feed.

~n("Me")
That's a lot of empty buildings...

~n("Friday")
Like I said, the Order plans ahead. Once the settlement's become more established, they'll call for another group of settlers and so on. One group then the next, a bit bigger each time. In ten or twenty years, it could very well be its own little town.
But...

~n("")
Mother makes a show of looking around.

~n("Friday")
Shouldn't someone have come to meet us? Aren't we important enough for them?
//show friday frown
This is why I hate the sticks. Do you know, it's never once had the decency to change in 20 years. No matter where you go, it's been the same since I was a girl.

//show friday neutral
~n("Me")
Agairy didn't even exist 20 years ago.

~n("Friday")
And yet it feel like I've been here a hundred times before, and not in the 'feels like home' kind of way.

~n("")
I don't really take some of Mother's stronger opinions completely seriously.
Although I guess it's true that things move slower here. Not one of the settlers, still staring, has said a word. Not even a whisper to the person next to them.

//~play sound doors opening

Just then, the doors of the church open.

//~show abbess neutral

A woman walks out. Her role is immediately obvious from her clothing; an abbess in the Virginal Order. She looks as calm and graceful as one would expect for her position.
I can sense Mother's shoulders tensing next to me, ever so slightly. She gently places a hand on the small of my back, telling me not to interrupt her.
The woman opens her mouth to speak, but Mother preempts her.

//~show friday fake smile
~n("Friday")
Sister Allowenn. How nice to see you again.

//~show abbess fake smile
~n("Allowenn")
Friday Cenparas, can it really be?
What a pleasant surprise. I wonder why you've come all the way out here to humble Agairy?

~n("Friday")
For work, what else? One of the sisters here is possessed; we're here to fix that.

~n("Allowenn")
Oh, how splendid. And who might your escort be?

~n("")
I open my mouth to introduce myself, but Mother places a hand on my back. She answers for me.

~n("Friday")
My assistant. He's very promising. In fact, he had a theory while we coming here, if you'll indulge me.
... It might be a long shot but... could you be the one who's possessed, Sister Allowenn? It would explain a lot.

//~show abbess fake smile level 2
~n("Allowenn")
Of course not.

~n("Friday")
Worth a try.

~n("Allowenn")
Yes, ah- Please, let's talk inside. It doesn't do to speak of demons out-of-doors.
And by the way, not that it's important...

~n("Friday")
Yes?

//~show allowenn fake smile level 1

~n("Allowenn")
It is \'Abbess\' Allowenn.

~n("Friday")
Is that right? My, good for you.

~n("Allowenn")
You're too kind.

//~hide abbess
//~show friday smirk
~n("Friday")
Bitch.

~n("")
She suddenly seems to notice me again.

~n("Friday")
Oh, um... sorry about that.
She's someone I had hoped I wouldn't have to run into for a few thousand more years. Or ever.
Not having to see her was one of the better parts of getting kicked out of the Order.

~n("Me")
You don't like her, do you?

~n("Friday")
That obvious?
Allowenn hasn't changed a bit since we were Sisters in the Order. Still as stubborn as a mule and as much of a bitch as the dogs she resembles.
More, even. And then for some reason the Order's raised her to an Abbess.

~n("Me")
An abbess of the middle of nowhere. Is that really a promotion, or more like \'get out my hair.\'
You may not be the only one who dislikes her.

//~show friday smile
~n("Friday")
Ha, I suppose that's true. Of course I'm not jealous of her. I would even be happy for her if she wasn't such a... wasn't so hard to get along with.
Uh-huh. She's got nothing I want~
I have two beautiful daughters and an amazing son. I can't be annoyed by her, it's simply impossible.

~n("")
On the other hand, the Abbess is probably annoyed we're taking so long.

~n("Friday")
~talk(0)
Okay. Relaxed and composed and remember that you're better than her. Nothing that bitch can do will affect me.

//humourous time skip to friday losing her cool at Allowenn for letting moth leave.
~talk(1)
//bg: agairy church
//~show friday mad
//~show Allowenn frown

~n("Friday")
What?! How could you just let her slip away?

~n("Allowenn")
Hmph! Sister Moth did not just slip away. There was a Sister just outside her door at all times. Which was quite inconvenient... the settlement's a day behind schedule, I don't mind telling you.
For the short, very short, time she was left her unattended, Siter Moth was far too sickly to even stand on her own. The poor girl was certainly in no condition to get up and walk from the church.

~n("")
After we followed the Abbess inside, she shared some disconcerting news regarding the girl we had come here to help: Her name was Moth and she had gone missing early this morning.
When the Sister watching her had gone in to check on her and to bring her food and water, Moth was in a bad way, sweating and gasping for breath. The Sister ran to fetch the Abbess but when they arrived back at the chamber, Moth was gone.
Mother shakes her head angrily. She seems far more upset at the news than the Abbess was.

~n("Friday")
You left her alone, of course she ran for it! ...you thought she was ill?
Of course not! Think for a moment, the demon makes her sick and then, once your air-headed Sister runs to fetch you, it cures her just as fast.

//~show friday sad
~n("")
Mother sighs and runs a hand through her hair. When she speaks again, it's quietly and with resignation.

~n("Friday")
She's being possessed. The demon won't kill her, but it'll go almost the entire way if it thinks it has to.
You don't take this seriously because she doesn't appear to be a monster. She still looks the same. There's no scales, or wings, or pointed tails to fear, and so we let our guards down.
But while there's a demon inside them, they are just as much of a threat, just as dangerous as any other monster. They have no regard for life besides their own and their host's. No inhibitions.
Humanity will learn this eventually, I think... but not until more people have died.

~n("")
The Abbess' frown has only curved further downwards as Mother spoke.

//~show friday neutral
~n("Allowenn")
Why, you speak as if you have all of life's answers. Obviously the Sister keeping watch should not have left Moth unattended for any length of time... In any case, she has already been disciplined.

~n("Friday")
You should have impressed the importance of her duty to her.

~n("Allowenn")
Naturally, I did that. But you must understand we are only a few in Agairy and the Sisters are needed all over the settlement at any given time.

~n("")
The Abbess spreads her hands helplessly.

~n("Allowenn")
There are not enough of us from the Order here to suppress every mistake before it's made, and the Sisters are still inexperienced. These things simply happen.

~n("Friday")
~talk(0)
She's unbelievable. It's the responsibility of the leader to accept blame for the actions of her subordinates.
~talk(1)
... You've searched the town for her already?

~n("Allowenn")
Of course. Agairy is not a large settlement; if Sister Moth had remained she have been found her almost at once. No, I fear she has gone out into the greater wilderness. She could have gone in any direction.

//~show friday frown
~n("Friday")
Not quite any direction. A monster like a demon wants to keep its host safe, which is why it fled from Agairy. But at the same time, it relies on others for its own survival. A demon doesn't want to risk itself, so it gets others to do its bidding.
Has the settlement been having any trouble with monsters? Have you found colonies nearby?

~n("Allowenn")
Oh dear. I really must hope poor Sister Moth has not wandered into a den of monsters. And yes, there is one some distance away. It was deemed far enough away not to be an immediate danger, rather a matter to be taken care of in due time.

~n("Friday")
I suppose we must deal with it now rather than later, then.

~n("Allowenn")
Quite.

//show agairy town square. slow fade, even.
//hide Allowenn
//show friday neutral in center (will be automatic, though)

~n("Friday")
She makes me tired.

~n("")
I've been silent the whole time we were inside. Mother and Allowenn seemed to fill any silence easily enough.
Mother said they've known each other since they were Order Sisters. There's probably more to the story, though...

~n("Me")
Mother, tell me about Allowenn.

~n("Friday")
There's not much to tell. She's had it out for me from the start. I mean, a lot of the girls did, but her especially.

~n("Me")
Really? Why wouldn't she like you?

~n("")
She laughs.

~n("Friday")
Ah... well there were probably a bunch of reasons. I was more aggressive when I was younger. I might have tormented her a bit.

~n("Me")
I'm sure She deserved it.

//~show
~n("Friday")
I love your blind loyalty, it's so cute. Really though, don't worry about it. We've put it behind us and mostly don't go further than being sarcastic.
I can tell you more later, if you still want me to.
Now come on, let's find our runaway before she gets too far away.

-> END

//the dungeon is unlocked. next event is dun 0 event 1.


//outside:
fr//a shepherdess tending to her flock? Well, maybe they were once, but the order is filling with more and more people like the abbess. Not evil. Just petty, incompetent.
fr//they don't go out seeking to do harm but it follows in their path, like an aftershock.
mc//the abbess cares, i think. She just doesn't want to admit it. especially in front of you, mom.
fr//why would she care what i think? she clearly hates me.
mc//because she's afraid of you. i thought it was obvious.
mc//i mean, you're threatening to her. you're here to clean up her mess, but you don't follow her ideology, or a part of the order.
mc//as such, you represent an attack on her way of life. and you're winning. For a woman who has spent her whole life in it like Abbess Allowenn has, you're forcing her to consider that her entire life up to now has been a mistake.
mc//...
fr//...
mc//... I'm done now. Sorry, I didn't mean to go off. i built up a lot to say; i felt like a spectator in there.
fr//sorry. Allowenn and i just have a lot to say to each other.
fr//and That's okay. I know I do the same thing. you probably learned it from me.
fr//anyway, you may be right, but I don't even if the abbess is even capable of doubting herself.
mc//i think everyone is.
