Team Work Makes the Dream Work
Final Project README

---------------------------

Members:
Albert Xing
axing6@gatech.edu
axing6

Corey Wen
cwen38@gatech.edu
cwen38

Joey Bishop
jbishop47@gatech.edu
jbishop47

Tony Wu
twu314@gatech.edu
twu314

---------------------------

Installation Requirements Procedures:
None, simply open up the “Princess Duchess's Excellent Adventure” build.

Gameplay Instructions:
	Move character with WASD (or Xbox left stick)
	Jump with Spacebar (or Xbox “Y” button)
	Press esc key for pause menu
	Left click/  left control for spin attack (or Xbox “A” or Lef Trigger)
	Hold right click and move mouse to move camera (or Xbox Right stick)

There are four “sections” to the game, each one separated by a gate. In order to progress through a gate, you need to collect the 3 llamas in each section. Thus in total, there are 12 llamas.

---------------------------

Rubric Requirement Fullfilments:
Game Feel: it is 3D, with a clear goal (collect all the llamas). There is a Start menu and options to reset and replay. 
Precursors to Fun Gameplay: We clearly communicate the goal of collecting llamas at the beginning. We provide interesting choices via the puzzles (e.g. the pushable crates, the spinning block puzzle) and the platforming (seesaws and moving platforms). The player’s choices with these elements will directly impact the gameplay.

3D Character/Vehicle with Real-time control
Our main character has continuous, dynamic control and responsive root-motion animation controlled by a blend tree. The camera follows the player smoothly and even has obstacle avoidance. There is auditory feedback for the character’s actions (footsteps, jumping, spinning, taking damage). 

3D World with Physics and Spatial Simulation
There is audio for every possible interaction in the game. 
There are interactive scripted objects
gates that open up when you have enough llamas (can see at the very beginning)
fans that blow you up when you spin into them (they can be easily recognized by their dark blue fan blades atop a bright orange base)
Simulated Newtonian physics rigid body objects
Pushable crates in scenes “Section1+2” and “Section3+4”
The see saws in scene “Section3+4”
Trees that fall when spun into (Section1+2)
Animated objects using Mecanim
The moving platforms in “Section3+4”
The wall that slides down at the very end of “Section3+4” after you defeat the skeleton
 State changing or destroyable objects
The walls you explode with bombs in the middle of scene “Section1+2” and the beginning of “Section3+4”

NPC Steering Behaviors/AI
Multiple AI enemies
Simplest is the “Drone” enemy at the beginning of “Section3+4.” Simply shoots bullets and tries to predict your future position.
Next in complexity are the red goblins introduced in “Section1+2” (we also refer to them as “Goombas” like from Mario). They have an “Idle Wander” state and a “Chase Player” state when you get close.
The “bomb” enemy appears in the middle of “Section1+2” and the beginning of “Section3+4.” These have an “Idle Wander” state, a “Chase Player” state when you get close, and an “Explode” state when it detonates.
The final boss is a sword-wielding skeleton. This has an “Idle Wander” state, a “Chase Player” state, a “Sword Swing” state, and a “Cooldown” state where the skeleton sort of looks dazed and occurs after a sword swing to give the player an opportunity to attack. This humanoid AI does utilize root motion

Polish
There is a Start Menu and an in-game pause menu to quit the game.
There are fade in and fade outs between scenes.
There are scripted aesthetic animations (swaying flowers and cacti)
Sound effects for every observable game event
Lots of particle effects (collecting llamas, eating apples, defeating enemies)
A cohesive, minimalistic art style that is mostly low poly, with some enemies (such as the drone and the skeleton) more complex to make them stand out. We went for playful colors and UI with sketch-like qualities to mimic the style of a child’s storybook.


Walkthrough
In this walkthrough, after each game mechanic are parentheses referring to what we felt was the most appropriate bullet point on the rubric.

Collect the llama (goals effectively communicated to player) at the beginning. Spin at the tree to knock it down (simulated Newtonian physics) so you can walk over to the next island. Head over to the right and knock down that tree to get to the island with the crates. Push the crates (simulated Newtonian physics) so you can get to the floating island with a llama. Then push down the double crate stack down and jump down. Rearrange the crates until you can get onto the final floating island to get the third llama. Head up towards the gate, which will open now that you have 3 llamas (animated objects with Mecanim).

Move forward, spinning to attack the red goblins (AI). Spin to fell the tree, then walk over to collect the llama. Collect the apple as well, which will add 1 health. Spin on the orange fan (interactive scripted object) to propel yourself over the forest barrier and down onto the next island. Lure the bombs (AI) over to the big rock wall to explode it (state changing our destroyable object). Solve the two pushable block puzzles to progress and get the fifth llama. 

Next, you’ll face the spinning block puzzle. Head to the left and spin on the fan to get onto a floating island with a black and white map of the bigger blocks. You can spin on these black and white map tiles to change the orientation of the bigger blocks (state changing objects). Rearrange the blocks until you can reach the sixth llama, located on a floating island at the far end of the big blocks (represented on the map by the bouncy flowers). Once you collect this llama, make your way back to the map and rearrange it so you can get to the island on the right. This will take you to another gate, which will open up to the next scene.

In the next scene, you are greeted by two drones which will attempt to predict your future position and shoot there (AI). Notice the wall with the target. You’ll need to position yourself so a drone shot hits the target and destroys the wall (interactive scripted object). Then, lead the bombs over to the rocks on the ground and blow it up, collecting the 7th llama.

Next, head over to the “main” island. Spin to defeat the goblins, and make your way onto the moving platform (animated object with Mecanim) to progress. The next moving platform will have the 8th llama (floating the air). Continue onwards until you see the orange see saws (simulated Newtonian physics), which you will need to position properly to progress up. Once you have passed the second see saw, you will be in an area wtih a moving platform and a moving see saw platform above it. Ignore this for now and keep heading forward. You’ll reach a fan with a bigger see saw. Spin into the fan and launch yourself towards the ends of the see saw repeatedly until it is nearly vertical. Then spin into the fan once more to get into the alcove with the 9th llama. Head back the path you came from and maneuver up the moving platform and moving see saw platform. You will reach another gate which will open when you have 9 llamas.

Continue forwards and you’ll see the spikes (animated objects using Mecanim). Be careful in your timing as you progress across them. After making it across the spikes, jump on the moving platforms. In between the two platforms in the 10th llama. Jump on to the second moving platform and use the fan to go up. You’ll reach a platform with another fan, which you should use to go up further. Then you’ll be in a section with three moving platforms, a drone to the right, and the 11th llama (if you fall off the platforms, there is a fan on the lower ground to get you back up). Maneuver carefully across the platforms to get to the fan on the far end, and propel yourself up to a section with more pushable crates and the 12th and final llama. Move the crates to collect the llama and get to the next section. Use the fan to propel yourself up (but time it properly to avoid the spikes) and proceed through the gate to the “final boss” (AI).

This sword-wielding skeleton will attempt to chase you. Once it gets close enough, it will slash its sword. After slashing, it will pause for a bit, which is the best time to get in close to spin attack. Be careful though, because the skeleton itself does do contact damage. You need to spin attack the skeleton 5 times to kill it. Killing it will activate a sequence where the far wall will drop to reveal platforms leading up. Climb up to the top of the mountain and jump onto the throne seat, which is the end of the game!

---------------------------

Deficiencies/ Known Bugs:
Final boss skeleton’s AI can be kind of wonky. Sometimes you can get near him but off to the side a bit, so he’ll stay in one place and continually swing his sword but never hit you.

Very rarely, you can encounter an “infinite spinning” glitch. This is when the character doesn’t look like she’s spinning, but the hurtbox that activates only when she’s in the spinning animation stays activated even when the animation is over. The easy way to fix this is just to spin again.

If you load up Section3&4 alone, the health will most likely go wack. This is due to our implementation using PlayerPrefs. When the game is played starting from Section1&2 it’s fine.

---------------------------

External Resources Used:

Assets
	3D Cartoon Apple - PPRODUCTION
	3D Monster Bomb!! - JKTimmons
	Animated Flag - CGTurf
	Baby monster - t pose - Pixel-bit
	Cloth animation based Flag - telecaster
	DL Fantasy RPG Effects - Dreamlevel
	Effect textures and prefabs - Magicpoint Inc.
	Free Low Poly Desert Pack - 23 Space Robots and Counting…
	Free Stylized Garden Asset - Easy3D
	Little Girl Princess Free 3d Model - Open3D Model (no creator given)
	LowPoly Llama - Romulo Enrique Gandolfo
	Lowpoly Skeleton - Ironic Game
	Low Poly Combat Drone - VooDooPlay
	Low Poly Pack - Environment Lite - Solum Night
	Low Poly Rock Pack - Broken Vector
	Pilkius Romeus font - Ultra Cool Fonts
	Simple throne - godofthefoolarcana
	Simplistic Low-Poly Nature - Acorn Bringer
	Starter Particle Pack - FullTiltBoogie
	Racing Flag Animated - 3D Factorio
	Wooden Sign 3D - thelostnorth

Sound Effects
	Sound Effects downloaded from freesound.org
	
Music
	Come And Find Me - Erik Skiff
	Come And Find Me - B Mix - Erik Skiff
	Underclocked (underclocked mix) - Erik Skiff
---------------------------

Who Did What:

Albert:
Worked on the skeleton final boss
Animations
Finite state machine and NavMesh agent
Sound effects
Lots of volunteer playtesting to find bugs/exploits

Corey:
Spinning block puzzle
Red goblins (we call them Goombas)
Drone enemy
Fan
Bombs
Target Wall

Joey:
Level Design
User Interface
Llamas
Gates/ Flags/ Respawn Zones
Flowers/ Grass/ Cacti
Trees
Pushable Crates
Music Implementation
Various Sound Effects
Various Particle Effects

Tony:
Handled the main character’s movement and animations
Level design
Moving platforms
See-saw platforms
Spikes
Various sound effects
Various particle effects

---------------------------

What scenes to open in Unity:
Start with “MainMenu.” This transitions into “Section1+2,” which transitions into “Section3&4.” Finally, it ends on “Credits” (which is also accessible from the “MainMenu” scene).
