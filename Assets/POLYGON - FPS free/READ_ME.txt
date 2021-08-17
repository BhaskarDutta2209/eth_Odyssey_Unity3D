
Control of the " Polygon_FPS_prefab " :
w,s,a,d -> walk + left shift -> run 
space -> jumping , q -> 3rd view ,r -> reload , e -> picking up a intem

L = locking the mouse cursor in the screen middle



Hello and thanks for buying the asset "Polygon FPS".


About POLYGON - FPS


The Asset provides 30 different weapons (+ equipment) and 15 different Characters.

The different characters are supposed, to get changed, so the 15 different Characters are in different objects, to turn them off and on, to disapper and appear them.

How can i access the Character skins and use them ?



Follow the manual :

1. Drag the Asset " Polygon_FPS_prefab " into the scene, which can you find in the "Project folder" path   POLYGON - FPS / Prefabs / Polygon_FPS_prefab   <- 

2. Open the follow path, of the " Polygon_FPS_prefab "     Polygon_FPS_prefab / Polygon_FPS / charakters     and open it

3. It is simple, as it looks !    Just click as example on "DE_a" and go to the inspector window, which you should find in the upper/right corner and 'Active' it, 
   which can you do on the box, which is next to the name, of the OBJECT in the inspector window.


4.Congratulations, now you should know, how to change your characters!     / Inside the Characters, as example in "DE_a" is the head also included, which get disappear, 
  if we are in the first person view and also as you can see the "dust_protection" and "sun_glasses", which can you deactivate, if you want to remove the sun_glass as example from the face of the "DE_a" Character.


  -------------------------------------------------------------

Manual about the weapons :


1. Please open the follow path, of the " Polygon_FPS_prefab " Prefab,     Polygon_FPS_prefab / Polygon_FPS / Weapons

2. Now you should see the weapons listed, it's the same principe, as the Characters provide.  You just click on the weapon OBJECT, as example "Assault57" and active it, on the inspector window, which should be in the upper/right corner.

3. In the case of this asset "POLYGON - FPS" is the script of the weapons also included in the weapon OBJECTS, in the opened path folder  " Polygon_FPS_prefab / Polygon_FPS / Weapons"

4. Within active a weapon object, you also active the weapon script, which makes the weapon functionable to shoot with.


-------------------------------------------------------------

What is about the EQUIPMENT ?


1. The asset also includes stuff as scopes, silencers and lasers, which is EXTRA equipment to use.

2. Open the follow path in the Prefab "Polygon_FPS_prefab",   "Polygon_FPS_prefab / Polygon_FPS / FPS_rig / Bone / FPS_bone_rotate_aim / FPS_bone_normal / Bone.022 / Bone.003 / Bone.005 / Bone.007 / Bone.009 / Bone.032 "

3. Now you should see the folder "charakter_addons", within the bone  " Bone.032 ",  please open it and you gonna see Objects.

4. As example, open the folder with the name " equipment_assault57 " in the opened folder " charakter_addons ", now you have opened the equipment folder for the "Assault57" rifle.

5. You should note, that in each of the folders, which begin with the name "equipment_", that they all contains the equal equipment(Example : "red_dot_a_prefab"), that's because, to save messy code inside the weapon script, so they already been posited and roated in the "equipment_" folder for each gun, which have equipment.

6. The principle is the same, as with the characters and weapons, you just active and deactivate the wished equipment to apper it on the weapon.


-------------------------------------------------------------



More questions about the opened bone, which contains all equipment ?       "Polygon_FPS_prefab / Polygon_FPS / FPS_rig / Bone / FPS_bone_rotate_aim / FPS_bone_normal / Bone.022 / Bone.003 / Bone.005 / Bone.007 / Bone.009 / Bone.032 "


1. What are the objects, with the name "holder_a_assault57" or "Rear_sight_b_buzzsub" ?

- The  reason, that the weapons are weighted for the animations and aren't adjustable/moveable, have i putted sights as example of the "sight_sce5" inside the bone, where the weapon is also weighted on. The reason is, that the sights need to get rotated down, if you use a red_dot as example, to make the view free.

- To rotate as example the "Rear_sight_b_buzzsub", you need to be in pivot point (which can you find in the upper/left corner of the unity window, if you click on "Center" or if there is already "Pivot" setted )  


2. More objects in the bone  "Polygon_FPS_prefab / Polygon_FPS / FPS_rig / Bone / FPS_bone_rotate_aim / FPS_bone_normal / Bone.022 / Bone.003 / Bone.005 / Bone.007 / Bone.009 / Bone.032 "   ??


- The object  "Shoot_origin" is the start point for the muzzle flash

- The object "bullet_ouput" is the spawn point for the bullet shells

- The object "blast_set_throw_origin" is the throw position/rotation, if you use the weapon " blast_set "

- The objtects "crowbar_collider" and "axe_collider" are the colliders, which get actived, if you use the "axe" or the "crowbar" to detect other colliders from hitting.

- The object "grenade_throw_origin" is as, "blast_set_throw_origin" the spawn postion/rotation for the weapon "grenade"



-------------------------------------------------------------


The BONE   "FPS_bone_rotate_aim",  which can you find on the follow path ::     "Polygon_FPS_prefab / Polygon_FPS / FPS_rig / Bone / FPS_bone_rotate_aim ",


- This is the bone, which gets rotate, to look up and down with the player.(Example is in "Polygon_fps_controller" how to use it)





-------------------------------------------------------------

Where is the MainCamera ?   Open the path, of the prefab " Polygon_FPS_prefab ",    "Polygon_FPS_prefab / Polygon_FPS / FPS_rig / Bone / FPS_bone_rotate_aim / FPS_bone_normal / Main Camera "


- The maincamera of the player includes follow folders 


   1. "point_screen" :  Here are the other cameras for the other scopes included, which get separately activated or deactived, if not needed.  The Aim_point is also inside here "aim_point" <-

   2. "gui" : 
   
               That's the folder, where the weapon ICONS are included, which get activated or deactivated, if required so.

               ammo_gui and health_gui have the component "Text_Mesh", included, to display the ammo_state and health 






-------------------------------------------------------------

Animations & sounds


How to find the animator inside the player ?

- Just path follow inside the "Polygon_FPS_prefab" ,   "Polygon_FPS_prefab / Polygon_FPS" <- 


How to use the animations ?


1. The Animator is already setted up for you, you just open the animator "FPS_animator" <- Which can you enter in the search bar of the project files.

2. Open the animator "FPS_animator" and you gonna see, that all is finished, now i hope, that you know, how to use the animator.

   If not :    https://docs.unity3d.com/Manual/class-Animator.html

3. You can find the document index of the animations for each weapon with the follow path : "POLYGON - FPS " and click on the text file "index Animations".

-------------------------------------------------------------

Avatar mask for the animator


legs : ignores the upper body

upper_corp : ignores the legs



both can you find with the follow path folder -> "POLYGON - FPS"



-------------------------------------------------------------



For questions, please write me on : tmg@fn.de


Much greetings and much success !


















