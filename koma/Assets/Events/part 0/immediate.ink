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
//Welcome to part 0 immediate. It is the game's very first event.
//-friday arrives to the academy at the end of term to get mc, but they'll be making one little stop first.
//-depart towards agairy.
//=============

//creation: ancients (they were evil) -> humans (they were weak) -> monsters (they were failures in all regards because the being tried to make them do everything).
//(after many hundred years of war, the humans learned evil like the monsters.)

~n("")
part 0 immediate.
-> END

//~play music ancients theme
//bg blackness
Many many years ago, there was a being that willed itself into existence. It made the ground and the sea, the rocks and the trees.

//ancients
It made the first people to rule over, for this was in its nature, but it was yet young and did not know of virtue and failing, nor of good and of evil. It made the first people in its own image and they were as brilliant as it, but were cruel and arrogant.
It destroyed them in disgust. These were the first people, the ancients, and some of their ruins still remain, buried beneath the earth or deep in forgotten woods.

//humans
It started again from scratch, and having learned of regret, took time to mould the second people into gentle souls that cared for one another. They lived in harmony with the earth for hundreds of years. But they were of no entertainment, and it grew bored with them. These were our ancestors, but we are not like them any longer. Not quite.

//monsters
And so it began shaping a third people.
This time, it could not be satisfied with its design. It planned for its third people to outshine all of his previous works in every aspect. They would be more clever than the first, more sensitive than the second, and strong like none before them.
As it toiled, it experimented with improvements in his design. That is why the third people are more than one species, and why each differs from one another. And so it observed the third people, but they achieved none of what it had hoped.
They were clever, but only in ways to selfishly cheat one another. They were sensitive, but only in their capacity to hate. They were strong, but their strength was wild and unpredictable, and was used to destroy rather than create.
The Being prepared to strike them down in disgust, but at the last moment it was overwhelmed by its own guilt and how it had made this mistake once. Knowing it was truly to blame, it spared the third people and withdrew inside of its own mind to think.
These were the monsters, but they were not like the monsters of own days. Not quite.

//humans and monsters
It was a thousand years later when it next looked out from its mind to the world, which had been changed beyond recognition. Monsters roamed in great hordes, killing and stealing from each other, and they terrorized the humans.
This brought it more pain than ever before. It could not bear to watch its creations destroy each other and, in desperation, taught the humans to resist the monsters. But they learned too well.
When it next gazed at the Earth, it was heartbroken. Things were worse than before, and violence was rampant among all species. In its grief, it casta terrible curse on each of them.

//curse
It cursed the monsters so that in some species, only females would be born, and in other species, only males. They could not reproduce between species, and so within a lifetime would all be dead.
Then it placed a curse on the humans. It cursed them so that the women could take the seed of monsters and give birth to their monstrous children, and the men's seed could be taken by female monsters to make them pregnant.
Sickened by its own actions and those of its creations, it then prepared itself to die. It had finally grown weary of the world, and consumed by heartbreak and remorse, returned to the nothingness from which it had first come, forever.
And so it was that monsters and humans had finally become like they are today.

//bg of humans and monsters, but broken/shattered image
The monsters hunt the humans as they did before, but rely on them for their own survival, while the humans are sick with the knowledge that they are the very thing that keeps the monsters alive.
A match made in heaven, really. It tends to discredit the story. Is the world really that fair?
Poetic justice belongs in poems, after all.

//~show bg academy grounds
In any case, if it ever happened it was all a long time ago and is unimportant now.
Today, the Kingdom of ___ is an age of reclamation. Its borders stretch as new settlements are founded at its edges by eager or desperate city-dwellers leap at the chance for land of their own.
The monsters are still out there, but not unopposed, and certainly not in the numbers they once had. With time, they've become a part of life like any other.
Forming the core of the Kingdom are the nobility, who support the King, and it's here where their children are educated.

//---^world background ends above^
//~music cue to city theme

It's the end of the half-year at the academy. Students, nobility each and every one of them, are being escorted back to their family estates.
There's a lot to be done for the young scions. Half the year learning magic here under the finest tutors in the Kingdom, and the other half learning their duties as nobles.
People tend to think the arrogance and general haughtiness of the aristocracy is natural, but the truth is we have to work on it same as everyone else.
It's a wonder we can afford to waste half of each year on frivolities like magic. I guess it keeps us out of trouble.
Not that I've ever really been one for trouble myself. At least, nothing that was ever my fault. Not when you get down to it.

I wonder who's coming to bring me home. Nobles are much too important to go anywhere unprotected, so I can't leave the academy until someone with the right credentials shows up. We're like children that way.
Last year it was Natalia, but I hope it's someone else this time. I know she watches and reports my every move to Mother.
Mother, who feels like she's everywhere as it is.

//~show friday base
~n("Woman")
Hey sweetheart.

The woman is just an inch or two taller than me, tall for a woman, and about 20 or so years older. She's wearing a well-fitted, fashionable city-style dress that shows off her, ahem, ample wealth.
She's also an expert in light magic, possessions in particular, and is often running all over the Kingdom doing important jobs for important people. She was some kind of hero when she was younger and retains much of that influence. She's a personal friend to the King and several of the Virginal Order's cardinals.
Last but not least, she's also of the nobility, titled in full "The Lady Friday Cenparas." The Cenparas are one of many similarly-sized houses, but their lands are old and rich, and in the heartland of the Kingdom.
And as the head of the Cenparas, she's easily one of the most influential women in the Kingdom.

~n("Me")
Hi Mom.

~n("")
Like I said: She's everywhere.

~n("Me")
... But what are you doing here?

//~show friday loving smile
~n("Friday")
I'm here for you, of course.

//~show close up / hug view?
~n("")
She hugs me tightly.

~n("Me")
Oh. I wasn't expecting... I mean, I'm surprised.

~n("Friday")
Pleasantly surprised, I'm sure?

~n("Me")
Well, of course. You just normally have more important things to do.

//~show friday frown
~n("Friday")
Oh come on, you know I would always be there if I could. It's just that there are sometimes things that are... also important.
It's part of our duty as Cenparas. You know we carry some of the Kingdom on our backs.

~n("Me")
I know.

~n("")
She gives me on the forehead and rubs my hair.

~n("Friday")
Good.

//~show bg city streets
//hide friday

~n("")
We fall into step and walk away from the academy grounds together into the streets of the rest of the city proper.
Mother asks me pleasantly about the year and my progress, and I answer honestly. I have made good progress this year.
Magic genuinely interests me and I want it to be something more than merely proof of my station in life. Though affinity with magic is a talent mostly restricted to the noble classes, we're rarely serious about it.
After all, you can't eat magic and the strength of even the most powerful spells can be overcome by a large enough group of soldiers. Compared to the power of a noble house can wield through its wealth and vassals, magic is superfluous.
It's just something you have to do.

//~show friday base
~n("Friday")
I hope that academy learning has been everything it's cracked up to be.

~n("Me")
It definitely hasn't.

~n("Friday")
How disappointing. Should we make this your last year then?
I could find you something to occupy you with, I think. A tour of the Kingdom, maybe? Or you could come along with me.

~n("Me")
Let's not go that far. It might not be ideal; the teachers are purely theoreticians, and it does mean being away from home... but it's still the best place to learn magic.

~n("Friday")
One of the only places.

~n("")
I shrug. Fair enough.

~n("Me")
I don't want to limit myself.

//show friday smirk
~n("Friday")
I wonder if it's worth it. Don't you think about your mother's poor heart?
It's not like you need to know magic. I can protect you.

~n("Me")
You don't want to have to worry about me forever.

//show friday loving smile
~n("Friday")
Where did you get that idea? Of course I do, you silly child, and even if I didn't I have no choice in the matter.
I let you go to your fancy academy and learn your precious magic for four years now. That should entitle me to you anytime I choose.

~n("Me")
All that for four half-years?

//show friday base
~n("")
She sniffs and looks aristocratic.

~n("Friday")
It felt closer to a hundred. I'm being quite generous, really.
Besides, as your mother monopolizing you is entirely within my right.

~n("")
She says so matter-of-factly.

~n("Me")
... Right.

~n("")
I can only wonder what she's talking about sometimes.
As we turn onto another street, Mother leading, I realize we're heading the wrong way. Of course, we aren't actually, since I know Mother wouldn't get lost.
I ask her where we're going.

//show friday base
~n("Friday")
Home. After a short errand.

~n("")
Figures. Mother wouldn't come all the way out here just for me. She couldn't afford to.

~n("Me")
The Order?

~n("")
Mother nods. She was educated and trained in the Order like some daughters of the nobility, but eventually some kind of falling out happened. Something to do with me and my sisters, I think.
I mean, it is called the Virginal Order for a reason.
I'm not sure on the whole story; Mother was never keen on sharing that. It seems unbelievable that I don't know something as basic as that about my mom, but there are topics that are off-limits in any family.
But even with all that, she still somehow remains on good terms with some of its upper echelons.

//show friday frown
~n("Friday")
But don't start saying I wouldn't come for you without another reason. I am here, aren't I?

~n("Me")
You happened to be nearby.

~n("Friday")
Oh, calm down. You're not a baby anymore, you don't need me to be there watching every time you take a step.
So what if I was close by? I thought it would be nice to have a chance to see each other again sooner rather than later. We'll have time on the way there to catch-up, and you won't have to share me with your sisters.
I'm pratically handing you a gift, you could stand to be a little more grateful. It's the perfect opportunity for some mother-son bonding.

~n("")
I can't help but grin. I've always been a momma's boy, and though it wasn't what I was expecting at the start of today, being adaptable is another important skill for the nobility, or so I'm told.
You could say it's one of our oldest traditions, or so I'm told.

//~show friday loving smile
~n("Friday")
No matter how grumpy or cynical you might pretend to be, I know how you really feel.

~n("Me")
Yeah, yeah, I'm jumping with joy. What is it anyway, this errand?

//~show friday base
~n("Friday")
Right, it's nothing major.
Something's wrong with one of the Sisters at one of the Order-sponsored frontier settlements.
Normally this kind of thing is beneath me, but the Order is funny about any settlement effort they've put their lot in with. They think any setback is tantamount to failure and will bring the immediate collapse of society.
So... we go.

It's not so far either. A week, all told, is what this sort of thing would normally take for me alone, barring any complications. Of course with you along it'll probably take, say, the same actually.
Sorry, but you probably won't have much effect on the outcome here.

~n("Me")
That's fine with me. Finally something I'm not going to be graded on.

~n("Friday")
Don't worry, I'll be grading you, and taking notes here—

~n("")
She pushes a finger against her head.

~n("Friday")
You should be far more worried, actually. None of your tutors could disinherit you.

~n("Me")
I'll have to do my best then.

~n("")
I think for a second.

~n("Me")
This was a personal favour for someone, wasn't it?

~n("Friday")
Yes. Cardinal Salvenu wrote to me personally.

~n("")
I recognise the name. One of Mother's teachers when she was in the Order. Of course he wasn't a cardinal back then.
Doesn't it seem odd how powerful people all seem to know each other? It shouldn't. It's all carefully arranged, really. And it's the probably the main reason for the academy, too.

~n("Me")
They don't have other people they can send?

~n("Friday")
I don't know, honestly. The Order doesn't exactly keep me in the loop.

~n("Me")
They're taking advantage of you, you know.

~n("")
She shrugs.

~n("Friday")
I don't mind. They help people. If they want to use me to do it, so be it.
Besides, what else would I do?

~n("")
Mother, who is already stretching herself thin doing a hundred different things all over the Kingdom. No, I don't think she realizes.

//~bg stables
We finally arrive at the stables where a groom hands Mother the reins of her horse.

~n("Friday")
I only brought the one with me, but the groom will find you something to ride. It's too far to walk.
That's the problem with these frontier settlements, they're on the edge of nowhere.

~n("")
I can ride, of course, another essential part of my education, and as we mount up I feel anticipation for what's to come.
Sure, there's some disappointment at not going home right away, and at my planned leisurely trip home transforming into riding to edge of the Kingdom, but excitement replaces all those feelings.
I don't get to spend that much time with Mother these days... Now I have a chance to prove myself to her and to learn from her.
I can't help it, I feel like a little kid being allowed to stay up an hour later on a festival night.


-> END
