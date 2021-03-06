# Orbital
Orbital is a relatively simple mobile game based around a concept that I find to be a lot of
fun in games, as rarely as it’s seen. That mechanic is small amounts of gravity with respect to
orbital mechanics. While that sounds boring, I find that a lot of the more fun parts of games like
Super Mario Galaxy or Kerbal Space Program are due to the way different gravitational fields affect the
player, and moving around in that environment. In this game, you will control a ship which must
navigate through a series of gravity wells in order to make it to the end of the level.

##General Information

In this game, you will control a ship which must navigate through a series of gravity wells in
order to make it to the end of the level. It is a game with increasingly
challenging level.

You start each level by aiming the ship in a direction and giving it some amount of starting
power. During flight you have a certain number of “boosters” depending on the level that
will increase the player’s forward velocity when used (though only in the direction they are
currently moving to avoid unnecessary complications with an aiming system during flight.)

##Implementation Details

In order to simplify things like graphics and collision detection, Orbital was made using the Unity game engine. The ship object is given a script in order to handle player input and move itself accordingly. 
The planetary Bodies are also given scripts to dictate how their gravitational fields are applied to the other objects in the game, as well as the general intensity of their gravitational impact.
