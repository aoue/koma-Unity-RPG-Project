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

//for the bg: something ancient greek looking, stylised with angels and demons and stuff. gratuity too, in the images. it's art, cuz it's like statues.
~n("Woman's voice")
~talk(1)

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
n("Woman")
Of course, that was all a long time ago. These days, we just try to stay out of each other's way. Maybe they'll be thoughtful and just die on their own, you know?

n("Me")
There's something that bothers me about that story.

n("Woman")
What is it?

n("Me")
WHY are you telling it to me now?

//show friday shocked
n("Woman")
What? I thought you liked that story.

n("Me")
I used to, at least before I heard it a thousand and one times at the academy. It gets old fast.

//show friday neutral
n("Woman")
That's too bad.

//show friday mad
Those bastards at the academy! I send them my only son and they send him back three years older and tired of his old bedtime stories.
n("")
She shakes her head.
n("Woman")
I won't forgive them.

n("Me")
Well... they did teach me some other useful things. And I was there for three years; it's not their fault time passes.
It's not like I didn't come home during breaks, Mom.

//show friday sad
~n("Mom")
Whatever they taught you, it wasn't worth it.
//show friday mad
At least I have you now. I won't let those creepy professors take you away again.

~n("Me")
Instead you'll just bring me everywhere with you for the rest of forever.

~n("Mom")
That's right. It's my right as your mother to monopolize you.
//show friday neutral
Besides, it'll be fun. Haven't you always wondered what it is I do? Now's your big chance.

~n("Me")
Mother... you wouldn't let us go to bed until you finished telling us about your work. You were always really open about it.

~n("Mom")
I guess I was. That's where I went wrong, then. I should have kept it more mysterious so you dreamed of following in my footsteps.

~n("Me")
No one can follow in your footsteps, Mom.

~n("Mom")
Flatterer, but that's okay, I don't mind. Maybe one of your sisters will be able to manage it.

~n("")
Despite her joking around, Mom's footsteps and her larger path in life are extraordinary by any standards. She's the type of person to live only once in a generation and leave her fingerprints all over history. For some reason, she insists that part of that is through her children.
I don't know the details of exactly everything she did before I was born, but these days she does things for the well-connected; nobles, cardinals, even the king sometimes.
Even though she's just the Countess Friday Cenparas, a minor noble without much land, she has influence far exceeding what you might expect. I imagine it's a long story.


//retry from here

~n("Me")
I admit, I am a little curious to see you in action.

~n("Mom")
Of course you are, it's only natural.
This is a personal favour to Cardinal Parvenu from the Virginal Order, so I'll be relying on you as my assistant.

~n("")
I nod
~n("Me")
Cardinal Parvenu? You've mentioned him before, I think.

~n("Mom")
That's right, he was one of my teachers way back when I was still with the Order, though he wasn't a cardinal ten. He's the only one of them that still talks to me.
That's why when he asked me, I couldn't bring myself to say no. He's a decent man, for someone in the Order. Said he would have done it himself if he could make the trip.

~n("Me")
It's just an exorcism, right? I thought that was the kind of thing the order was supposed to specialise in.

~n("Mom")
Not that many of them, I'm afraid.





//okay, fine up to here. next:
// -resettlement era
// -the present: friday, mc.

//turns out friday was telling the story to mc. mc says why are you telling me this story ugh ive heard it too many times
//friday frowns, you liked it when you were a kid... you were so sweet, etc. oh... you're okay now too, i suppose.






















-> END
//end of file.
