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
Me working on world generation is like me with orange juice. Their both like the best things in the world. 
*Intro*
Which is why over the last two months I've overhauled the world generator for my bullet hell, 
*Game Description*
where you clear rooms with your friends. I've been meaning to redo how the world is generated because I've noticed some glaring design issues.

Let me explain.

The old generator was made of rooms that would continue to grow in a line as you progressed. Rooms could turn, but never go down, and never branch with multiple exits. This presented a huge issue that I had not predicted. You might be able to guess at what the problem is. I'll give you a hint: The world was completely linear. Try and guess the issue before I reveal it.

One of the game design techniques I like to use is giving the player ambiguous choices. That's a complicated way of saying here's two options, you don't know which one is better. I like this because there is a certain element of risk, especially if you make one choice riskier but more rewarding. 

Okay, did you guess that the issue was a lack of player choice? This is what I call disregarding the player's agency. Because, the player has no choice in where they go in the world it feels very boring and repetitive, a feeling even echoed by playtesters in the discord.

A simple solution to this problem of player agency that comes to mind, then, is to just add alternate paths. And that's good, but we can do better.

In the new worldgen you'll notice a main path, with smaller branches breaking off to the sides. At the ends of these branches there will be loot like items or trinkets, generally a good reward that an experienced player would want. However, in order to make this choice interesting, and turn this into an ambiguous decision, when taking these paths, the difficulty of future rooms increases. That means more monsters will spawn.

This creates the conundrum: Do I go for the loot, and risk dying along the way and making future rooms harder, or do I play it safe and continue progressing through the level.

This is the type of decision the original worldgen was missing. Without it, the world goes from being a mechanic that players can interact with, to just a container to house gameplay.

Ambiguous choices is how I like to think about it.

Now if you want to playtest everything I just talked about, I'd be happy to send you a beta steam key through the discord. Thank you for watching, make sure to go grab yourself a nice glass of orange juice, and if you ever give me privilege of your audience again, I'll see you in the next one. Bye.