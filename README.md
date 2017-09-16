Every Day the Game
===========

## Play
[Itch.io](https://hinerz.itch.io/every-day-the-game)

[YouTube Link to Test Playthrough Video](https://youtu.be/f06PjgW0cwk)

## Concept
May or June of 2014, we celebrated our first year anniversary at a [paint-your-stuff place](https://www.yelp.com/biz/brushstrokes-studio-berkeley) and I painted this. 

![back](https://github.com/hinasakazaki/EveryDayTheGame/blob/projectile/BalloonGame/docs/mug-back.JPG)
![front](https://github.com/hinasakazaki/EveryDayTheGame/blob/projectile/BalloonGame/docs/mug-front.JPG)

I didn't think of it as a game idea, but a cutesy little design. 

After we moved in together and I started my first full time job as Software Engineer at [Zynga](https://www.zynga.com/), I casually proposed marriage on a daily basis. The answer? "Someday..", "Definitely not now", "If I marry anyone it's you but I'm not ready yet," etc.

Remind you of something? It's probably this: [Stable Marriage Problem](https://en.wikipedia.org/wiki/Stable_marriage_problem) If it's anything that sticked from [CS170](http://cs170.org/), that one was it. I optimized by asking my first choice person every day until he was married to me or somebody else. Except, I decided to stop after a year, and decide to channel all of my energy towards a side project.

Since I started work, I had a difficult time finding a project I felt passionate about. Game Jams are fun, but I wanted to work on something more long term. After serious discussion with my (hopefully) future spouse, it sounded like engagement was something that had to wait a while. Perfect.

By April 2016, I think I had some ideas. This is when the cup I painted comes back. I think it's a good idea to make a game that's focuses on a "support" character. 
I watched a video of a proposal through a game, and it was a dungeon crawler. The player (the developer's girlfriend) played as a hero and saved the princess. The hero then proposed to the princess. While this is cute, I thought there was opportunity for improvement, especially requiring the player to shift their perspective from hero to princess. My boyfriend is no princess, and I didn't want him to be trapped for the whole game (this idea is interesting too, but I'm no good with designing puzzles). Instead, I wanted him to be the healer who has to balance his own safety with the hero's effectiveness at dealing damage where they need to. I wanted the hero to be clearly a DPS, but I wanted enemies to be cats, and I don't want cats to be hurt, so I switched it around to be love bullets. (Thanks [No Game No Life](http://no-game-no-life.wikia.com/wiki/Episode_11)!)

I sketched up stuff on bart, and did some concept art. The concept art is from May 2016.

![sketch](https://github.com/hinasakazaki/EveryDayTheGame/blob/projectile/BalloonGame/docs/notebook.JPG)
![conceptart](https://github.com/hinasakazaki/EveryDayTheGame/blob/projectile/BalloonGame/docs/conceptart.png)

Back at the time, I had been playing a lot of [Fire Emblem: Fates](http://fireemblem.wikia.com/wiki/Fire_Emblem_Fates). 
I loved the combination of choices/dialogue/story and gameplay. The winter prior, the target player was playing a lot of [Electronic Super Joy](http://www.electronicsuperjoy.com/), a very fast paced and exciting sidescroller. I wanted to make something that combined the two.

In general, I like games that bind users to "time". If you play my [Labor Day the Game](https://hinerz.itch.io/labor-day-the-game-webplayer-build), each minute is 2 hours of a workday, so the game lasts 4 minutes. Similar idea for [Slinging Ink](https://hinerz.itch.io/slinging-ink). I wanted the game to be broken up to times of the day:
Level 1. Morning Clouds (9AM - 11AM)
Level 2. Tiled Prison (11AM - 5PM)
Level 3. Setting Sun (5PM - 8PM)
Level 4. Snowy Darkness (8PM - 3AM)
Level 5. Home (3AM - 9AM)
The idea is that the day is experienced between the two main characters of the game, and wish for a day like the one experienced for every day for the rest of their lives.
I tried to correspond each level to shared experiences; however, I will leave this to your imagination.

## Development

My first commit on github is December 2016, and I think that was when I started legitimately writing code on it.

In August of 2016, I had worked on a side scroller with a similar idea called [Amaterasu](https://hinerz.itch.io/amaterasu), which, to be honest, is a pretty crappy game. I tried to experiment with sound beat detection, which is a recurring theme in my development life. But it a similar layout sidescrolling design as this game.

I think I took some of the code from that, used stand-in assets, and started developing during my winter break.
Of course, this was done in secret. Hard to do when we live together. 

This screencapture was five days before Christmas 2016.
![conceptart](https://github.com/hinasakazaki/EveryDayTheGame/blob/projectile/BalloonGame/docs/dev-screenshot.gif)

Back then, I was still attached to Fire Emblem character designs and made variations of existing designs. 

A few months later, I realized that it wasn't very "me", so created the current version character sprites.

![charactersprites](https://github.com/hinasakazaki/EveryDayTheGame/blob/projectile/BalloonGame/Assets/sprites/charSpriteTest.png)

Early 2017, I reached out to my composer friend [Eric Delgado](https://soundcloud.com/ericdelgadomusic). Eric is the first friend I made in [UC Berkeley Symphony Orchestra](https://orchestra.berkeley.edu/). We met to brainstorm the soundtrack in early January 2017.

I got my game design buddy [Calvin 'KirbiKaka' Lu](http://calvinkirbikaka.com/art/) to review my first draft of the script (dialogue) in late December 2017. 

My first deadline ("release date") was mid May of 2017. It was quickly revealed that Mid May was not going to be the release date, not because of development of this game, but due to the target player's life schedule. (Getting a PhD is hard.) I postponed the release date to mid September 2017 after this realization.

I showed Eric my first build mid June of 2017, and shared with KirbiKaka shortly after.

We started compiling bugs after getting a couple more testers on a shared doc. 

At the start of this, the game had only two main mechanics: healing with mushrooms and moving the hero to get as many cats.
I got feedback that some obstacles on the ground and jump mechanics would be nice to have. I didn't want to build out elaborate platformer obstacles, and wanted to stick to a simple, but cute one. I chose tentacles as a fun obstacle that could insta-trap and end the game. 

Lots of bugs were found. Here's one of the screenshots one found:
![conceptart](https://github.com/hinasakazaki/EveryDayTheGame/blob/projectile/BalloonGame/docs/bug.png)

At this point, I had raycasting and line renderers as the attack mechanic of the enemies (evil cats). I still have the code saved on the [radar](https://github.com/hinasakazaki/EveryDayTheGame/tree/radar) branch. 

It looked like this:
![radar gif](https://github.com/hinasakazaki/EveryDayTheGame/blob/projectile/BalloonGame/docs/radar.gif)

Unfortunately, due to the raycast hits being super finnicky, I changed it to projectiles. It was not worth the bad UX. I probably spent the most time in the project trying to get raycast to work.

At this point, there were NO checkpoints in the game.
There were two ways one could lose: 1. tentacle trap insta-kill 2. Hero runs out of HP. 
(Then there are other ways to end the game, like bad decisions)
The most frustrating part was the tentacle trap insta-kill. After a small timing error, a player would have to restart the game from the beginning, including the tutorial. I ended up, as a player, typing "asfadfaa" as the player name and home on many occasions. Something was wrong. Flappybird is not narrative, so you can fast fail and have a quick loop. Not this game.

Checkpoints were actually quite easy to implement due to the code architecture I chose. I think this should be one of the first things to do, including a debug level select. I still don't have a debug level select menu, but I wish that I do on many occasions. Sometimes as developers we can be averse to putting in energy to something that's not directly part of the user experience.

Some numbers I tweaked: speed & damage caused by bullets (hero bullets & cat bullets), mushroom healing power, sine & consine waves of cat bullets, speed of background scroll in each level. It could still use more work. I think this is pretty subjective. 

One of the last pieces that I implemented that is significant in the game design is report card at the end.
![report card](https://github.com/hinasakazaki/EveryDayTheGame/blob/projectile/BalloonGame/docs/reportcard.png)

No matter how many times you fail, you will eventually end up beating the boss with the new checkpoint system.
I didn't like this, because I wanted each experience to matter. However, an experience is just unpleasant if you have to re-do the same levels over and over again.
I'm hoping that the report card functionality gives more replay value, though, it's not really what I'm going for for the core purpose of this game. 
The first priority of this game is telling a story. That's why we have checkpoints, so you can progress through without getting too angry.
But in addition, I wanted to make sure that the player knows what they did in the game mattered in the end. Even if the player gets a proposal at the end no matter what they did to get there, the numbers mean something. 

## Conclusions
The target player, my (hopefully) future spouse doesn't care for surprises.
Nor does he really want something that is showy. For example, he'd definitely not enjoy getting proposed to in the middle of a baseball field during a game. In fact, he probably knows that an official proposal is coming up soon, and that I'm working on a secret project.

So, in the end, this was for me, so I can keep making shoddy pixel art animations, coding for fun, reflecting on game design and development.

# Credits

There is a Credits section in the actual game, but here it is again:

## Design, Art, and Development
Every Day the Game
By hinerz

## Music
Every Day The Game Original Soundtrack
By Eric Delgado

Hurt
By Freesound timgormly 

## Background Images

Shinto Gate
By Michael Huang, US

Shiro
By Simon Child

Street Light
By kadi franson, US

Store
By Aaron K. Kim

Tree
By Daniela Baptista

Tree
By James Keuning, US

Sun
By LSE Designs

Work
By Delwar Hossain

Mountain
By Aleksandr Vector

Shinkansen
By Ameya Narvankar

Snowman
By Alena Artemova

Lecture Hall
By Gan Khoon Lay

Slow Internet
By Gan Khoon Lay

Plaque
By Chameleon Design

House
By Mani Amini
