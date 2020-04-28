# Zombie_VR
A Zombie_VR game made by Unity3D

Note:This repo only has scripts I have written without 3Dobjects. If you wanna whole projectm please connect me with:
yangjin0817@163.com

Introduction:

In the future, human society crashed because of zombies. Player act a soldier from marine to eliminate to zombies and protect our civilization. Thus, this is an FPS game, player will have fun by shooting and kill zombies. Players are also allowed to use some special skills, so that see a lot of effects in an VR environment. In order to use those special skills, I have made a feature to recognize player’s hand gesture. When player wave their hand like the gesture they have recorded in the game, they can use special skills to kill zombies. What’s more, players can use some weapons. I have made two weapons at the version, that is a Grenade and a Pistol. Player can shoot the zombie by the pistol or throw a grenade to kill couples of zombies. 

VR Application Design:
1.	Animation
All the objects that can interact with players should have animations. For example, when we press the grab button, the hand of the player should have an animation of “Grab”. Those animations can make the game more real and fun. And I have written a lot of C# scripts to controller the animation. For example, if the zombie was attacked by the player, the code controls the animator of the zombie to play the “Hit” animation.
2.	  Gesture Recognition 
A key feature of this game is that player can wave their hand to call a special skill. There are three skills and all of them have wonderful effects. In order to complete this feature, I used the VR Infinite Gesture which is a plugin of Unity and have all the foundations of gesture recognition. So I don’t need to write a lot of codes to do the recognition but use the APIs given by this plugins.
3.	Grab
An important this of a VR app is that players can interact with the objects that around them. So, I have added collider to the all the objects that are Interactive and given them the related tags. And I write C# scripts of the hands so when the hand collides the objects, player can grab them, for instance, the pistol, the magazine and so on.
Integration
1.	Hardware
(1)	HTC VIVE
This game is built on the HTC VIVE, so players must have an HTC VIVE.
(2)	A PC with discrete graphics card
Because I used a lot of 3D models in this project, any PC or Laptop without discrete graphics card can not play this game. I tried on my laptop, it just crashed every time. However, my PC works very well.
2.	Software
(1)	Unity5.6.3
I develop this project on the Unity5.6.3, because it is stable. I tried Unity 2019 and Unity 2018, but when I imported the plugins a lot of errors appeared.
3.	Plugins
(1)	DoTween
This plugin has tools to make gameobject move. I use this plugin to let the UI panel in the main scene slide when player put books on the bookstand.
(2)	Highlighting
This plugin can make gameobject has highlighting, you just need to put some scripts on the gameobject that you want to have highlighting. In this game, in the main scene, when player’s hand collide the objects that can be grabbed, those objects will have highlighting.
(3)	VR Infinite Gesture
This plugin provides basic tools for gesture recognition in the Unity VR environment. I used this plugin to record gesture of player’s hand and test player’s gesture accuracy when player wave their hand.
(4)	Steam VR Plugin
This plugin can connect the Unity with Htc vive. So when click the play button of Unity, you can use your Htc vive.
(5)	Steam VR Unity Toolkit
This plugin gives tools to control the Htc vive in the unity. For example, it provides a camera rig which is a VR camera and gameobjects of two Vive controllers which you can adjust like replacing their models with hand models.
4.	Audio resource
All of the audio resources are from Internet, but no commercial use. 
(1)	Background music in main scene
Silent night(高梨康治)
(2)	Music of zombie walk and attack
(3)	Music of pistol
5.	3D Models
All of the 3D models are from Internet and free, but no commercial use. 
(1)	Environment
Main scene is the room. Game scene is the whole environment, includes roads, house, mountains.
(2)	Zombies
Six zombies prefabs
(3)	Books and bookstand
3 books and 1 bookstand
(4)	Weapons
Pistol and Grenade
(5)	Watch and two hands

User experience:

The main work of this project focuses on the animation of different objects and how to intact with them. 
The hands have an animator to control the animations of hands. When Press the grab button, it will play the grab animation. When hands hold a pistol, it will paly the “holdPistol” animation.  
The Zombies also have an animator. If they are spawned on the map, they will randomly play walk or run animation to move. When they are hit by the pistol bullets, they will play hit animation.
The interaction with objects in the game is also very complicated and main work. I have put collider components on the objects that can be interacted with and write a lot of codes to control what will happen when hands collide into them.
User Guideline
At the beginning, player is standing in a room and in front of a table. There are three books and a bookstand on the table. Player put different book on the bookstand will show different panels. The left one will show a about panel. The middle will show a gesture panel. The right one will start the game to fight with zombie. Player should first go to the gesture panel and grab the pistol on the table. After use the pistol to click the information button. Player can see three items of gestures. Play can click the record button to record their gesture. Then go back to the gesture panel and click the test button to test the accuracy of their gesture. Then, player can go to the game scene by putting the right book on the bookstand.
 	In the game scene, player should firstly go the table and grab the pistol and belt. Then make the belt collide with player’s body, the belt will disappear which means player wear it. After this, player can see a UI panel show the number of bullets and grenades. Also, the watch on the left hand shows the HP of the player. Then when move forward, zombies will appear, player can use pistol to shoot. Or click the touch pad of the controllers, a circular panel on the watch will come out. When press the grenade button, player will have a grenade and can throw it to hurt zombies. When press the controller button, player can wave their hand like the gesture they have recorded to call the skills to kill zombies.


Improvement
(1)	More weapons
Now the weapons only have a pistol and grenade. Obviously it is not enough, more weapons can be added, like machine guns, RPG and so on.
(2)	More Zombies
This version can only generate two waves of zombies. But the map is very huge. So can put more zombies on the map.
(3)	Coin collecting
After killing one zombie, player will get some coins to buy new weapons or upgrade their skills and weapons.
(4)	Obstacle putting:
Player can move around and put some obstacle and traps on the ground to stop zombies.
(5)	Weapon management:
Player can buy more powerful weapons when the game goes. For example, at the very begin, player may only have a knife to stop zombies. But later in the game, player can buy rifles or machine guns.
