#devlog
# Prep
## Target Audience
- Teen 18 - 24
- People okay with a little bit of analysis, but not true technical

## Reference Devlogs
- Blargis: 
	- https://youtu.be/nA0HQGXSSoY?si=xseYpc0ytFi_1tqG
	- https://youtu.be/xKxiOsXC1m0?si=LRiBbNu4cfH2Rd6e
	- https://youtu.be/BtexfEcmdts?si=GCcyn3sOMMkZBYZ9
	- https://youtu.be/BtexfEcmdts?si=upK_5SVtriqOSgzS
- Habbie Star Citizen: https://youtu.be/isLH2GalU4g?si=SyLD45ONarNH0zJn
	- Hilarious
- Watt, Isle Goblin: https://youtu.be/4NmmNFMWCQY?si=n02US7G2BBJ_37DS\
	- Maybe reference for game explanation
- Deep Lands: https://youtu.be/7zDHVeZxzXw?si=wKKQ-Jbe8EpMdCsD
	- Love the sound design
- Jonas Thronefall: https://youtu.be/bnbBd-zoHPQ?si=d6G8ma2lHJAmhmJo
	- refence for intro
- JDH: https://youtu.be/PcMua73C_94?si=-fT8ZwxMBZeUCn4c
	- refrence for intro
- Project Feline: https://youtu.be/e54VbphftBM?si=iFr0n4jysTC4IKAx
	- Reference for "talking head" mix
- Sebastian Lague:
	- Technical explanation refs
	- https://youtu.be/kOkfC5fLfgE?si=MpMB8Vak5MnlyLT6
	- https://youtu.be/SO83KQuuZvg?si=pFb6gK2dinUYuipJ
	- https://youtu.be/C1H4zIiCOaI?si=uOthnoOG2z7-3iNM
- Funke: https://youtu.be/bJEy09Sm37Y?si=CRdl4Ad5V1tHUmtY
	- Comedy / style / aside ref
- WangleLine: https://youtu.be/gK5d3yAAaJs?si=sDXu-cW6BnCp6RK9
	- SFX and Editing reference
- Jam2Go: https://youtu.be/GBTc7eZ-Pyc?si=gWIztdVHfPxn-tkm
	- Vibey
- The Unity Guy: https://youtu.be/OjT9W6q8SfY?si=NpV5H9c42MWg8qHh
- Anbagames: https://youtu.be/fWaRW9PKMYE?si=ESbwT3t9MUZTRNUe
	- Good ref for diving into a mechanic
- Seth Newlin: https://youtu.be/nWyJyt0KWPo?si=lmy_2hAWR5Jmg-G-
	- Chill vibe
- Radnyx: https://youtu.be/cvbCM6Mm4w4?si=sBk9o7lsPjk6KtwY
- Beans: https://youtu.be/Q65YLsKs16M?si=wu9QWnICrZoejXpM
	- Good look into mechanics
- Twosome Saga: https://youtu.be/PxbvG8UmmYY?si=wgiIIc_fLDhudx3q
- VHS: https://www.youtube.com/watch?v=-3Z5NBywQEU
- https://www.youtube.com/watch?v=JPQ2NVwm1Xk
- https://www.youtube.com/watch?v=7zDHVeZxzXw&t=5s
- https://www.youtube.com/watch?v=cOyAz4msWiQ

Title cards: https://www.youtube.com/watch?v=UR5eikHGtlk
## Vibe
- Retro
- Home made
- Light hearted
## Songs
- Boom Bap Flick
- Easy Shake -> used for drop
- June
- Lazy Walk
- Limousines
- Next Steps
- Randy Butternumbs
- Will 2 Power
# Outline
## *Worldgen was redone to be a mechanic rather than a container for the mechanics*
## Introduction
Worlds don't just have to contain game play. In video games, worlds can be their own game mechanic. Over the last two months I made a new world generator for my game, and I've got some design bits to show you. Be careful, this might get those thinking cogs in your brain spinning.
## The Overall Issue
First, it's important where I came from before we can appreciate some of the decisions that went into the new world gen. 
- the world was made of rooms
- each room would include an enemy encounter before allowing the players to pass
- the rooms would always go up with the occasional turn left and right
- this has a few issues
- the world was very repetitive
- it offered no player agency / choice
- the world in no way interacted with any of the other game mechanics
## Addressing and Resolving Issues
- go to the core of giving player's choice
- I think this is the key to turning an idea into an interesting mechanic
- especially if these ideas are risk and reward
- the most natural choice a world can have is branching paths
- that's exactly what I did
- but, branching paths doesn't necessarily make the choices interesting, or integrate with other mechanics
- Like, I mentioned earlier, I think making decisions risk and reward is fun
- So, I could do this by making taking side paths increase the difficulty of future rooms
- each new room you clear makes future rooms more difficult
- but to make up for this, you'll be able to find loot like new weapons and powerups at the end of these optional branches
- what I've described so far does address the three core issues with the previous world gen
- the new world gen now offers a risk and reward choice that integrates other game mechanics like loot
- this choice as well as the world no longer just going upwards, definitely makes it more interesting, and thankfully a few of the playtesters from my friends and my discord server agree.
- The new world gen is not without issues though.
- At the moment, when you come across a branch, there's no way to tell which is the main path or not 
- I've thought of maybe adding paths on the floor along the main route, but if you have any other ideas, please let me know
## Conclusion
- If you've made it this far I can tell you're probably pretty interested in game design. So if you want to test everything I've talked about so far, join the discord and let me know, I'll send you a build of the game. Thank you for watching, make sure to go grab yourself a nice glass of orange juice, and if you ever give me privilege of your audience again, I'll see you in the next one. Bye.
# Outline Notes
- more jokes
- more detail
- work on explanation
# Outline 2
## Introduction
*Comedic hook*
Procedural generation is like orange juice. Their both the best things in the world. 
*Intro*
Which is why over the last two months I've overhauled the world generator for my bullet hell, where you clear rooms with your friends. I've been meaning to redo how the world is generated because I've noticed some glaring design issues.

Let me explain.

The old generator was made of rooms that would continue to grow in a line as you progressed. Rooms could turn, but never go down, and never branch with multiple exits. This presented a huge issue that I had not predicted. You might be able to guess at what the problem is. I'll give you a hint: The world was completely linear. Try and guess the issue before I reveal it.

One of the game design techniques I like to use is giving the player ambiguous choices. That's a complicated way of saying here's two options, you don't know which one is better. *I like this because there is a certain element of risk, especially if you make one choice riskier but more rewarding.* You can think about it like this: In front of you, you have two caves, you know one has diamonds and the other has only coal. Unfortunately the cave with diamonds has a big scary dragon. Only the most experienced players will try to fight the dragon in order to earn the diamonds.

And I love when games do this. In blood thief there are alternate routes that are harder to pull off but faster. In Risk of Rain, You can choose to fight double the bosses for double to loot. In Spelunky, you can choose the rob the shopkeep for his items, but he'll hunt you down. So many games like to do this because it rewards experienced players with a greater challenge without making it harder for less experienced players. Heck, even Nintendo has been doing this in the Mario games for years, through its optional star coins.

Okay, did you guess that the issue was a lack of player choice? This is what I call disregarding the player's agency. *Because, the player has no choice in where they go in the world it feels very boring and repetitive, a feeling even echoed by playtesters in the discord.* Its almost like watching someone else like a sibling play a game, since you don't have any agency in what your little brother does, you don't get to have the same kind of fun as playing the game.

A simple solution to this problem of player agency that comes to mind, then, is to just add alternate paths. And that's good, but we can do better.

In the new worldgen you'll notice a main path, with smaller branches breaking off to the sides. At the ends of these branches there will be loot like items or trinkets, generally a good reward that an experienced player would want. However, in order to make this choice interesting, and turn this into an ambiguous decision, when taking these paths, the difficulty of future rooms increases. That means more monsters will spawn.

This creates the conundrum: Do I go for the loot, and risk dying along the way and making future rooms harder, or do I play it safe and continue progressing through the level.

This is the type of decision the original worldgen was missing. *Without it, the world goes from being a mechanic that players can interact with, to just a container to house gameplay.* Imagine if you couldn't modify the world in Minecraft. It wouldn't at all the gem so many people love.

Ambiguous choices is how I like to think about it.

Now if you want to playtest everything I just talked about, I'd be happy to send you a beta steam key through the discord. 

But before I end the video, I want to give an update on some of the other changes I've made and why I've made them:

The bow's range was heavily nerfed, so that you can no longer cheese rooms by hitting enemies that are outside of your view.

A new enemy called the wood spirit was added to the enemy pool. This is experimenting with adding more ranged enemies in the game.

I've done a lot of work on the visuals to spice things up. You'll notice improved grass, waving animations, and new decorations around the borders of rooms. Let me know what you think. I'm hoping that continuing to make the game look better will help more people get interested.

I've also disabled a few features while I touch them up. You can't access the other locations at the moment because their visuals aren't done. The boss fight has been removed until I implement a better way to end locations. The altar system where you can get new trinkets has been removed as I'm going to be reworking it real soon.

Thanks for watching, go grab a nice cup of orange juice, I'll see you in the next one. Bye.
# Revision 3
## Introduction
*Comedic hook*
You know, procedural generation is kind of like orange juice. I can never get enough of it.
*Intro*
Which is why over the last two months I've overhauled the world generator for my cooperative bullet hell, where you clear rooms with your friends. I've been meaning to redo how the world is generated because I've noticed some glaring design issues.

Let me explain.

The old generator was made of rooms that would continue to grow in a line as you progressed. Rooms could turn, but never go down, and never branch with multiple exits. This presented a huge issue that I had not predicted. You might be able to guess at what the problem is. I'll give you a hint: The world was completely linear. Try and guess the issue before I reveal it.

One of the game design techniques I like to use is giving the player ambiguous choices. That's a complicated way of saying here's two options, you don't know which one is better. *I like this because there is a certain element of risk, especially if you make one choice riskier but more rewarding.* You can think about it like this: In front of you, you have two caves, you know one has diamonds and the other has only coal. Unfortunately the cave with diamonds has a big scary dragon. Only the most experienced players will try to fight the dragon in order to earn the diamonds.

And I love when games do this. In blood thief there are alternate routes that are harder to pull off but faster. In Risk of Rain, You can choose to fight double the bosses for double to loot. In Spelunky, you can choose the rob the shopkeep for his items, but he'll hunt you down. So many games like to do this because it rewards experienced players with a greater challenge without making it harder for less experienced players. Heck, even Nintendo has been doing this in the Mario games for years, through its optional star coins.

Okay, did you guess that the issue was a lack of player choice? This is what I call disregarding the player's agency. *Because, the player has no choice in where they go in the world it feels very boring and repetitive, a feeling even echoed by playtesters in the discord.* Its almost like watching someone else like a sibling play a game, since you don't have any agency in what your little brother does, you don't get to have the same kind of fun as playing the game.

A simple solution to this problem of player agency that comes to mind, then, is to just add alternate paths. And that's good, but we can do better.

In the new worldgen you'll notice a main path, with smaller branches breaking off to the sides. At the ends of these branches there will be loot like items or trinkets, generally a good reward that an experienced player would want. However, in order to make this choice interesting, and turn this into an ambiguous decision, when taking these paths, the difficulty of future rooms increases. That means more monsters will spawn.

This creates the conundrum: Do I go for the loot, and risk dying along the way and making future rooms harder, or do I play it safe and continue progressing through the level.

This is the type of decision the original worldgen was missing. *Without it, the world goes from being a mechanic that players can interact with, to just a container to house gameplay.* Imagine if you couldn't modify the world in Minecraft. It wouldn't at all the gem so many people love.

Ambiguous choices is how I like to think about it.

Now if you want to playtest everything I just talked about, I'd be happy to send you a beta steam key through the discord. 

But before I end the video, I want to give an update on some of the other changes I've made and why I've made them:

The bow's range was heavily nerfed, so that you can no longer cheese rooms by hitting enemies that are outside of your view.

A new enemy called the wood spirit was added to the enemy pool. This is experimenting with adding more ranged enemies in the game.

I've done a lot of work on the visuals to spice things up. You'll notice improved grass, waving animations, and new decorations around the borders of rooms. Let me know what you think. I'm hoping that continuing to make the game look better will help more people get interested.

I've also disabled a few features while I touch them up. You can't access the other locations at the moment because their visuals aren't done. The boss fight has been removed until I implement a better way to end locations. The altar system where you can get new trinkets has been removed as I'm going to be reworking it real soon.

Thanks for watching, go grab a nice cup of orange juice, I'll see you in the next one. Bye.
# Revision 4
Procedural Generation. Thank goodness I no longer have to pay my level designer.

*dungeon in wall*

Now I just keep him in the dungeon right here. I just love procedural generation. That's why over the last two months I overhauled world generator for the cooperative bullet hell I'm working on. I've been meaning to redo how the world is generated because I've noticed a few glaring design issues that I want to share with you today, and how I fixed them.

Let me explain.

*movie theatre zoome out bit*

This is the old world generator. It's made of rooms that grow in a line as you progress. Rooms can turn but never branch with multiple exits, and as you can imagine, this turned out to be quite a design flaw. Some of the playtesters even called it boring. Well, they used to be playtesters.

*show graves*

One of the game design techniques I like to use is giving the player ambiguous choices. That's just a complicated way of saying here's two options, and you don't know which one is better. Imagine you can choose between fighting a dragon with a million dollars or a dancing baby with ten dollars.

*show dragon and baby*

One choice is way more riskier, but the reward is also way larger.

*shoot dragon and collect money*

I like this because there is an element of risk. More advanced players can optionally take the difficult route for the greater reward, while newer players don't have to fight the dragon. Not only is this way more interesting than just one choice, but it caters to both beginners and advanced players. This technique is so broken, it's banned in competitive game development.

Diagetic UI??!
*me rages and punches screen*

So many great games use this technique, and I absolutely love it when they do. In blood thief there are alternate routes that are harder to pull off but faster. In Risk of Rain, You can choose to fight double the bosses for double to loot. In Spelunky, you can choose the rob the shopkeep for his items, but he'll hunt you down. So many games like to do this because it rewards experienced players with a greater challenge without making it harder for less experienced players. Heck, even Nintendo has been doing this in the Mario games for years, through its optional star coins. If you keep an eye out for it, you'll notice this type of design everywhere.

The question still remains, how can this be incorporated into a procedurally generated world? Here's an idea: We can add branches off of the main path with extra loot at the end, but then make more enemies spawn when you take these paths.

This creates the conundrum: Do I go for the loot, and risk dying along the way and making future rooms harder, or do I play it safe and continue progressing through the level.

*show two paths, in castle, one looks dangerous*

But is this interesting? Let's ask the experts,

*me but fancy sips cup*

yes

*zoom out through retro monitor*

Ambiguous choices is how I like to think about it.

Now if you want to playtest everything I just talked about, I'd be happy to send you a beta steam key through the discord. 

But before I end the video, I want to give an update on some of the other changes I've made and why I've made them:

*zoom back in*

The bow's range was heavily nerfed, so that you can no longer cheese rooms by hitting enemies that are outside of your view.

A new enemy called the wood spirit was added to the enemy pool. This is experimenting with adding more ranged enemies in the game.

I've done a lot of work on the visuals to spice things up. You'll notice improved grass, waving animations, and new decorations around the borders of rooms. Let me know what you think. I'm hoping that continuing to make the game look better will help more people get interested.

I've also disabled a few features while I touch them up. You can't access the other locations at the moment because their visuals aren't done. The boss fight has been removed until I implement a better way to end locations. The altar system where you can get new trinkets has been removed as I'm going to be reworking it real soon.

That's everything! Thanks for watching, go grab a nice cup of orange juice, and I'll see you in the next one. Bye.