Hello, and welcome to our game!

You can control the game with either keyboard & mouse, or with an Xbox360 controller. To move, you use WASD or the left joystick. To jump, you press space or the "Y" controller button. Pressing space/"Y" while in midair lets you double jump. To spin attack, press Left Control or either of the controller's trigger buttons. Spinning in midair gives you a small boost akin to a triple jump, which is very useful! You can also use the mouse or right joystick to control the camera.

Press the "O" key to give yourself more health. Note that because you are only meant to have 3 hearts max, pressing "O" too many times will still give you health, but it will not be reflected in the UI.

Currently, there are two scenes to the game. The first scene has green/pink ground, while the second scene has brown/yellow ground. Press "1" to teleport to the beginning of the first scene, "2" to teleport to the second half of scene 1, and "3" to teleport to the beginning of scene 2. 

At any point, you can press "Escape" to bring up the menu. From here, you can choose to go back to the menu or quit the game.

The goal of the game is to collect llamas. For scene 1 of the game, there are 6 llamas in total. Note that the first section of this scene (meant to be a tutorial section) is blocked off by a gate. You'll need to collect 3 llamas to proceeed. The end of scene 1 also has a gate, which will open when you have all 6 llamas and will transport you to scene 2. Scene 2 is not yet completed and only has 2 llamas. There is no end goal or way to "win" scene 2 yet. You've reached the end when you see the skeleton enemy. 

We have several mechanics that satisfy the requirement for a 3D world with physics and spatial simulation.
- Spin into the trees to cause them to fall.
- The big crates in scene 1 can be pushed around to form platforms to higher areas. 
- The fans can be spun into to launch the player up. 
- The orange platforms in scene 2 react to the player's weight and will tilt like a see-saw. 
- The gray moving platforms in scene 2 were created with Mecanim, and can be stood upon.
- The spikes were also animated with Mecanim.
- The big white platforms in scene 1 are right now are a proof of concept. The idea is that there is a remote "control" panel that you can reach off the side (you need to go up via a fan). Spin on the individual tiles of the control to spin them and the corresponding big tile. Because you can technically double jump your way across the big tiles, thus rendering the remote control useless, we disabled jumping on the big tiles for now until we can find a proper workaround. 

There are multiple enemies that satisfy the real-time NPC steering behaviors/AI. 
- Every enemy has an "idle" state where they wander around aimlessly.
- The little red monsters in scene 1 are the "simple" enemies and will chase the players around.
- The bombs in scene 1 will chase the players and explode after a while. Lure them near the big brown wall and have them explode to destroy it.
- The skeleton at the end of scene 2 is a complex enemy with multiple states. There is an idle state, where he will wander around. Once you are within a certain range, he will begin to walk towards you, and stop at a certain distance. He will then occasionally attack you. which you must dodge by circling around to the back. His backside is the only weakpoint; attacking him from his front will produce a "clang" noise as he defends with his shield. Right now, the states/animations and hitboxes are a bit wonky, but it will of course be polished later on. 





