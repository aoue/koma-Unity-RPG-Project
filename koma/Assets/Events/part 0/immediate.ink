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
//-gives world's creation story and age of resettlement.
//-fade to present; mc and friday head to agairy, and why they're headed there.
//=============

//creation: ancients (they were evil) -> humans (they were weak) -> monsters (they were failures in all regards because the being tried to make them do everything).
//(after many hundred years of war, the humans learned evil like the monsters.)

~n("Friday")
~talk(1)
~show(0, 0)
Hi, I'm Friday Cenparas and welcome to the world of Koma!
We've met before-- but you know that of course. After all, I'm someone very important to you~

~show(0, 2)
And you to me~

~show(0, 1)
But do you know what?

~show(0, 3)
Sometimes I get really angry at the world!

~show(0, 0)
Anyway, listen to me about to go off. Forget about it.

~show(0, 1)
Or I'll kill you. Ahaha.



//for the bg: something ancient greek looking, stylised with angels and demons and stuff. gratuity too, in the images. it's art, cuz it's like statues.
~n("")

part 0 immediate.

//testing choice stuff
//*choice 1
//ya hit choice 1
//*choice 2
//ya hit choice 2

-> END

//show creator at top. just eyes and outline.
//bg of ancients and golems

In the beginning, there was a mind and it brought itself into existence from nothing. There was a Being and it made the first people, so that it could have someone to rule over, for that was in its nature.
But the Being was young and it did not know of virtue and failing, of good and evil. It made the first people in its own image and they were brilliant like the Being, but were cruel and arrogant.
The Being destroyed them in disgust with what it had created. These were the people that we call today 'the ancients,' and some of their ruins still remain, buried beneath the earth or deep in forgotten forests.

//bg of humans

The Being started again from scratch, and having learned of regret, took time to mould the second people into emphathetic souls that cared for one another. They lived in harmony with the earth for hundreds of years, uneventfully. These were the first humans, the ancestors of us all, but they were not like we are today. Not quite yet.
Eventually, the Being grew bored with the second people, but he did hate them and so left them to live while he began shaping a third people.

//bg of monsters

This time, the Being could not be satisfied in its designs. It planned for its third people to outshine all of his previous works in every aspect. They would be more clever than the first, more sensitive than the second, and strong like none before them.
As it toiled, the Being experimented with improvements in his design. That is why the third people are more than one species, and why they differ from each other. And so the Being observed the third people.
They achieved none of what the Being had hoped. They were clever, but only in ways to selfishly cheat one another. They were sensitive emotionally, but only in their capacity to hate. They were strong, but their strength was wild and unpredictable, and was used to destroy rather than create.
The Being prepared to strike them down in disgust, but at the last moment it once again remembered the first people. Knowing it was truly to blame, the Being spared the third people and withdrew inside of its own mind to think.
These were the monsters, but they were not like the monsters of own days. No, not quite yet.

//bg of humans and monsters

It was a thousand years later when the Being next looked out from its mind at the world, which had become unrecognizable. Monsters roamed in great hordes, killing and stealing from each other, and they terrorized the humans, who had learned wickedness from the monsters.
This brought the Being more pain than ever before. It could not bear to watch its creations tear each other apart. It used its power to stop the fighting, and called all the leaders of the humans and monsters together.
It told them they had a final chance to earn its love and mercy. The Being commanded them to build a great tower together, so that it may stand as a symbol of unity and remind the generations to come of their ancestors commitments to peace. That, and its own watchfulness, the Being hoped, would be enough to save its peoples.

//bg of tower

While the tower was being built, the Being kept a close eye on the peoples and none dared commit an evil act for fear of its wrath. But in the instant that the tower was finished and the two overseers, human and monster, stood side by side on the roof of the tower, the Being's watch slackened for a moment. And that was all it took.
Lunging across the tower, the thid people's overseer shoved his human counterpart from behind, off the tower and onto the ground far below. But as he lost his footing, the human overseer grabbed hold of the his monstrous counterpart's leg, and the two tumbled off together.
Even as they fell they fought and grappled at each other, so that both were already dead before they hit the ground.

When the Being learned of this, it was heartbroken. The two overseers had fallen, and so the Being made it that the second and third people fell with them. It cast a terrible curse on each of them.
It cursed the monsters so that in some species, only females would be born, and in other species, only males. They could not reproduce between species, and so within a few generations would all be dead.
Then the Being placed its second curse, this time on the humans. It cursed them so that the women could take the seed of monsters and give birth to their monstrous children, and the men's seed could be taken by female monsters and make them pregnant.
Sickened by its own actions and those of its creations, the Being prepared itself to die. It had finally grown weary of the world, and consumed by heartbreak and remorse, returned to the nothingness from which it had first come, forever.
And so it was that monsters and humans had finally become like they are today.

//bg of humans and monsters
The monsters hunt the humans as they did before, but rely on them for their own survival, while the humans are sick with the knowledge that they are the very thing that keeps the monsters alive. A perfect match, really.

//snap bg to current day. bg should be like a lightly wooded dirt road through plains.
//show friday smiling, she's enjoying the attention.
~n("Woman")
Of course, that was all a long time ago. These days, we just try to stay out of their way. Maybe they'll be considerate and just wait to die on their own, you know?

~n("")
The two of us walk down the new dirt road towards Agairy Settlement, right on the edge of civilization and the kingdom's influence. Inhabited by the desperate and enterprising alike, it's also one the kingdom's fingertips into the wider world.

~n("Me")
I don't think that will happen.

~n("Woman")
No, but it would be nice.

~n("")
The woman walking with me is just an inch or two shorter than I am; tall for a woman. She's wearing a fashionable city-style dress in defiance of the countryside we now find ourselves in.
She's also an expert in Light Magic, exorcism in particular, and that's the reason we've come all the way out here.
Oh, and she's also my mother, called in full "the Countess Friday Cenparas", or "Mom" for short.
The Abbess directing Agairy Settlement (one of the settlements sponsored by the Order) reported that one of the sisters under her has been possessed. Apparently the exact circumstances were quite unusual, too.

~n("Friday")
What are you thinking about all seriously over there?

~n("Me")
Oh, just that this wasn't how I was imagining performing an exorcism would be.

//show friday smile
~n("Friday")
No? The world's a big place, most of any job is just walking around. With exorcism, half the time it turns out someone just had stomachache. The other half are deadly dangerous, though.

~n("Me")
What does this one feel like?

//show friday neutral
~n("Friday")
Hmmm, I wonder. Maybe somewhere in the middle?
You know, normally I wouldn't come all the way to the middle of nowhere; like you said it's too much travel time. But it was an old friend of mine from the Order who asked me.
My old teacher, Cardinal Parvenu. Not that he was a cardinal back then. Do you know, he's the only one who still speaks to me.

~n("Me")
I think you've mentioned him before.

~n("")
I know that Mother was educated and trained in the Order like some daughters of the nobility, but eventually had a falling out with them. Something to do with me and my sisters, I think.
I mean, it is called the Virginal Order.

~n("Friday")
He asked me specifically to do this for him, as a favor, so it's probably not just a waste of time. He said it would have been the sort of thing he'd have done himself, if he were still young.
//show friday smile
When he put it that way, how could I refuse. It's a wonderful thing to be relied on.
And I'll be relying on ~you~ too, so don't disappoint me.

~n("")
When Mother first got this request, I had just gotten back from the end third year at the academy. Since the students are entirely made up from the children of nobles, the academy understands its students have other obligations and offer instruction only half the year.
Anyway, Mother decided I should come with her and that was that.

~n("Friday")
We'll see if that academy learning is everything it's cracked up to be.

~n("Me")
I learned useful things at the academy, Mom.

~n("Friday")
Whatever it was, it can't have been worth it. Don't you think about your mother's poor heart?

~n("Me")
You're a dramatist, Mom.

~n("")
She shakes her head in faux-disappointment.

~n("Friday")
You really didn't learn anything, did you?

~n("Me")
I learned about irony.

~n("Friday")
Ohh, how terrifying.
It's not like you need to know magic, in any case. I'll protect you.

~n("Me")
I know, but you don't want to have to worry about me forever.

~n("Friday")
Where did you get that idea? Of course I do, silly boy.
I let you go to your precious academy and learn your fancy magic, that should entitle me to you for at least ten or twenty years.

~n("Me")
Ten or twenty? The academy only lasts 4... and for only half of each of those years.

~n("Friday")
Each year felt more like a hundred. Besides, as your mother monopolizing you is my right. So stop complaining.

~n("Me")
... Sure, Mom.

~n("")
We continue walking.

-> END
//end of file.
