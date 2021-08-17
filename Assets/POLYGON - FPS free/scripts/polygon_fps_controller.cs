using UnityEngine;

public class polygon_fps_controller : MonoBehaviour
{

    
    
    public GameObject FPS_camera;

    public GameObject aim_point;
    public GameObject shoot_handle;

    // Key input

    public bool Key_w;
    public bool Key_s;
    public bool Key_d;
    public bool Key_a;
    public bool key_reload;
    public bool key_duck;
    public bool key_jump;
    public bool key_run;


    // Status of walking
    public bool idle;

    public bool forward;
    public bool back;
    public bool right;
    public bool left;

    public bool forward_right;
    public bool back_right;

    public bool forward_left;
    public bool back_left;

    public bool duck;
    public bool duck_walk;
    public bool reload;

    public bool jump;
    public bool run;
    public bool cam_toggled;



    private void Start()
    {
        saved_roation_X = vertical_aim_bone_default.transform.eulerAngles.x;



        Cursor.visible = false;

       // Here we register the player for the bunnies

        GameObject target_add = GameObject.FindGameObjectWithTag("targets");

        target_add.GetComponent<targets_for_bunny>().Add_Target(gameObject);








        if (active_assault57.activeSelf)
        {


            old_pistol_bool = false;
           
            assault57_bool = false;



            assault57_bool = true;


            exit_weapons();

            active_assault57.SetActive(true);
            Icon_assault57.SetActive(true);
            active_assault57.GetComponent<assault57>().Start();

            // restarting animator
            animator_obj.SetActive(false);
            animator_obj.SetActive(true);





        }
       
        if (active_fireaxe.activeSelf)
        {


          
            old_pistol_bool = false;
           
            assault57_bool = false;



            // leaving all weapons
            exit_weapons();

            active_fireaxe.SetActive(true);
            Icon_fireaxe.SetActive(true);
            // restarting animator
            animator_obj.SetActive(false);
            animator_obj.SetActive(true);


        }

        if (active_old_pistol.activeSelf)
        {


            old_pistol_bool = false;
           
            assault57_bool = false;


            old_pistol_bool = true;


            exit_weapons();
            active_old_pistol.SetActive(true);
            active_old_pistol.GetComponent<old_pistol>().Start();
            Icon_old_pistol.SetActive(true);
            // restarting animator
            animator_obj.SetActive(false);
            animator_obj.SetActive(true);


        }
        





    }


    public float vertical_float_spread;
    public float horizontal_float_spread;

    public int player_health;


    public TextMesh ammo_gui;
    public TextMesh health_gui;



    void FixedUpdate()
    {

        walk_status();
        walk_execute();
        jumping();

        
        if (Input.GetKeyDown(KeyCode.E))
        {
            action_execute();
        }

        // To disable the aim point, while aimming
        if (Input.GetButton("Fire2"))
        {
            aim_point.SetActive(false);
        }
        else
        {
            aim_point.SetActive(true);
        }

        // Player health status
        health_gui.text = "HP : " + player_health;


        // Current magazine count

        if (active_assault57.activeSelf)
        {
            ammo_gui.text = active_assault57.GetComponent<assault57>().magazine_current + " / " + active_assault57.GetComponent<assault57>().stored_bullets;
        }
        
        if (active_fireaxe.activeSelf)
        {
            ammo_gui.text = " & ";
        }
      
        if (active_old_pistol.activeSelf)
        {
            ammo_gui.text = active_old_pistol.GetComponent<old_pistol>().magazine_current + " / " + active_old_pistol.GetComponent<old_pistol>().stored_bullets;
        }
      



        if (Input.GetKeyDown(KeyCode.L))
        {
            Cursor.lockState = CursorLockMode.Locked;

        }
        if (Input.GetKeyDown(KeyCode.O))
        {

            Cursor.lockState = CursorLockMode.None;
        }





    }






    private void LateUpdate()
    {

        aimming();
    }




    public GameObject vertical_aim_bone_default;

    public float min_angle;
    public float max_angle;

    public float vertical_speed;
    public float horizontal_speed;

    public float saved_roation_X;

    public void aimming()
    {


        float add_speed_aim;

        if (Input.GetButton("Fire2"))
        {

            // we slowing down the aimming speed, if the press the aim button Fire2
            add_speed_aim = 0.5f;
        }
        else
        {

            add_speed_aim = 1f;
        }



        // adding recoil movement to the aim

        float add_hor = horizontal_float_spread;
        horizontal_float_spread = 0;


        float add_ver = vertical_float_spread;
        vertical_float_spread = 0;




        transform.eulerAngles = new Vector3(transform.eulerAngles.x, (transform.eulerAngles.y + add_hor) + (Input.GetAxis("Mouse X") * add_speed_aim) * Time.deltaTime * horizontal_speed, transform.eulerAngles.z);



        Vector3 rot = new Vector3((saved_roation_X + add_ver) - (Input.GetAxis("Mouse Y") * add_speed_aim) * Time.deltaTime * vertical_speed, vertical_aim_bone_default.transform.eulerAngles.y, vertical_aim_bone_default.transform.eulerAngles.z);

        // limit of rotation for the aimming
        rot.x = Mathf.Clamp(rot.x, min_angle, max_angle);


        // Here gets the current roation value saved to reuse it, that we don't begin at zero, because the animation overplays all
        saved_roation_X = rot.x;



        vertical_aim_bone_default.transform.eulerAngles = rot;




    }





    bool is_toggled;




    public Animator ani;
    public float walk_speed;
    public float run_speed;
    public float Duck_walk_speed;

    public CharacterController controller;

    float forward_back;
    float right_left;

    Vector3 moveDirection;



    public bool in_jump;


    public float jump_speed;

    public bool walking;
    public bool running;
    public bool walking_side;

    bool changed_state;
    public string state;
    public string old_state;


    public AudioSource walk_sound;

    public AudioClip walk_clip;
    public AudioClip run_clip;
    public AudioClip walk_side_clip;
    public AudioClip walk_duck_clip;


    public void walk_execute()
    {
        walking = false;
        running = false;
        walking_side = false;

        if (idle)
        {
            state = "idle";

            ani.SetInteger("legs", 0);
            walk_speed = 0;
            forward_back = 0;
            right_left = 0;

            walking = false;
            running = false;
        }


        if (forward)
        {
            state = "walk";

            ani.SetFloat("legs_speed", 1);
            ani.SetInteger("legs", 1);
            walk_speed = 250f;

            forward_back = 1.2f;
            right_left = 0;

            walking = true;
            running = false;
        }
        if (back)
        {
            state = "walk";

            ani.SetFloat("legs_speed", -1);
            ani.SetInteger("legs", 1);
            walk_speed = 250f;

            forward_back = -1.2f;
            right_left = 0;

            walking = true;
            running = false;
        }





        if (right)
        {
            state = "side_walk";

            ani.SetFloat("legs_speed", 1);
            ani.SetInteger("legs", 2);
            walk_speed = 150f;

            right_left = 1;

            walking_side = true;
            walking = true;
            running = false;
        }
        if (left)
        {
            state = "side_walk";

            ani.SetFloat("legs_speed", -1);
            ani.SetInteger("legs", 2);
            walk_speed = 150f;
            right_left = -1;

            walking_side = true;
            walking = true;
            running = false;
        }





        if (forward_right)
        {
            state = "walk";

            ani.SetFloat("legs_speed", 2);
            ani.SetInteger("legs", 2);
            walk_speed = 150f;

            forward_back = 1;
            right_left = 1;

            walking = true;
            running = false;
        }
        if (forward_left)
        {
            state = "walk";

            ani.SetFloat("legs_speed", 2);
            ani.SetInteger("legs", 2);
            walk_speed = 150f;

            forward_back = 1;
            right_left = -1;

            walking = true;
            running = false;
        }





        if (back_right)
        {
            state = "walk";

            ani.SetFloat("legs_speed", 2);
            ani.SetInteger("legs", 2);
            walk_speed = 150f;

            forward_back = -1;
            right_left = 1;

            walking = true;
            running = false;
        }
        if (back_left)
        {
            state = "walk";

            ani.SetFloat("legs_speed", 2);
            ani.SetInteger("legs", 2);
            walk_speed = 150f;

            forward_back = -1;
            right_left = -1;

            walking = true;
            running = false;
        }

        if (run)
        {
            state = "run";

            ani.SetInteger("legs", 3);
            walk_speed = 400f;

            forward_back = 1.2f;
            right_left = 0;

            walking = false;
            running = true;
        }

        if (duck)
        {
            state = "idle";

            ani.SetInteger("legs", 5);
            ani.SetFloat("legs_speed", 0);
            walk_speed = 100;

            forward_back = 0;
            right_left = 0;

            walking = false;
            running = false;
        }

        if (duck_walk)
        {
            state = "duck_walk";

            ani.SetInteger("legs", 5);
            ani.SetFloat("legs_speed", 1);
            walk_speed = 100;

            forward_back = 1;
            right_left = 0;

            walking = true;
            running = false;
        }



        // checking, if the state of the "walking" has changed, if yes, we change the sound 

        if (state == "idle" && old_state != "idle")
        {
            old_state = "idle";



            walk_sound.time = 0;
            walk_sound.Stop();
        }

        if (state == "walk" && old_state != "walk")
        {
            old_state = "walk";


            walk_sound.clip = walk_clip;

            walk_sound.time = 0;
            walk_sound.Play();


        }

        if (state == "run" && old_state != "run")
        {
            old_state = "run";



            walk_sound.clip = run_clip;
            walk_sound.time = 0;
            walk_sound.Play();



        }

        if (state == "side_walk" && old_state != "side_walk")
        {
            old_state = "side_walk";



            walk_sound.clip = walk_side_clip;
            walk_sound.time = 0;
            walk_sound.Play();



        }

        if (state == "duck_walk" && old_state != "duck_walk")
        {
            old_state = "duck_walk";



            walk_sound.clip = walk_duck_clip;
            walk_sound.time = 0;
            walk_sound.Play();



        }

        moveDirection = new Vector3(Vector3.forward.x * Time.deltaTime * forward_back * walk_speed, 0, Vector3.right.z * Time.deltaTime * forward_back * walk_speed);

        Vector3 FB = transform.TransformDirection(Vector3.forward * forward_back * walk_speed * Time.deltaTime);
        Vector3 RL = transform.TransformDirection(Vector3.right * right_left * walk_speed * Time.deltaTime);




        controller.SimpleMove(FB + RL);








    }

    public void jumping()
    {
        // The drag is always negative, that the player falls down, with pressing the jump key, we add force, which moves the player jump
        if (jump && controller.isGrounded)
        {
            


            jump_speed = 0.3f;

            transform.position = transform.position + new Vector3(0, 0.1f, 0);


        }

        if (!controller.isGrounded && jump)
        {


            jump_speed -= 0.01f;

            transform.Translate(new Vector3(0, jump_speed, 0));


            if (jump_speed < -0.5f)
            {
                jump_speed = -0.5f;
            }
        }
    }



    bool already_toggles_cam;
    public void walk_status()
    {


        
        right = false;
        left = false;
        forward = false;
        forward_right = false;
        forward = false;
        forward_left = false;
        back = false;
        back_right = false;
        back = false;
        back_left = false;
        duck = false;
        duck_walk = false;
        jump = false;
        reload = false;
        run = false;


        if (Input.GetKeyDown(KeyCode.Q))
        {

            if (!cam_toggled && !already_toggles_cam)
            {
                cam_toggled = true; already_toggles_cam = true;
            }

            if (cam_toggled && !already_toggles_cam)
            {
                cam_toggled = false; already_toggles_cam = true;
            }


        }
        already_toggles_cam = false;



        // determine input from keys
        if (Input.GetKey(KeyCode.W))
        {
            Key_w = true;
        }
        else
        {
            Key_w = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            Key_s = true;
        }
        else
        {
            Key_s = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Key_d = true;
        }
        else
        {
            Key_d = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            Key_a = true;
        }
        else
        {
            Key_a = false;
        }

        if (Input.GetKey(KeyCode.R))
        {
            key_reload = true;
        }
        else
        {
            key_reload = false;
        }

        if (Input.GetKey(KeyCode.C))
        {
            key_duck = true;
        }
        else
        {
            key_duck = false;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            key_jump = true;
        }
        else
        {
            key_jump = false;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            key_run = true;
        }
        else
        {
            key_run = false;
        }







        // determine the walk status
        if (Key_w)
        {
            forward = true;
        }
        if (Key_s)
        {
            back = true;
        }
        if (Key_d)
        {
            right = true;
        }
        if (Key_a)
        {
            left = true;
        }
        if (key_jump)
        {
            jump = true;
        }


        if (key_reload)
        {
            reload = true;
        }
        if (key_duck)
        {
            duck = true;
        }


        if (Key_w && Key_d)
        {
            forward = false;
            forward_right = true;
        }
        if (Key_w && Key_a)
        {
            forward = false;
            forward_left = true;
        }



        if (Key_s && Key_d)
        {
            back = false;

            back_right = true;
        }
        if (Key_s && Key_a)
        {
            back = false;
            back_left = true;
        }



        if (key_duck && !Key_w && !key_run)
        {
            right = false;
            left = false;


            forward = false;
            forward_right = false;

            forward = false;
            forward_left = false;

            back = false;
            back_right = false;

            back = false;
            back_left = false;
            key_run = false;

            duck = true;
            duck_walk = false;
        }
        if (key_duck && Key_w && !key_run)
        {
            right = false;
            left = false;


            forward = false;
            forward_right = false;

            forward = false;
            forward_left = false;

            back = false;
            back_right = false;

            back = false;
            back_left = false;

            duck = false;
            key_run = false;

            duck_walk = true;

        }
        if (!key_duck && !duck && !forward && !back && !right && !left && !forward_right && !forward_left && !back_right && !back_left)
        {
            right = false;
            left = false;


            forward = false;
            forward_right = false;

            forward = false;
            forward_left = false;

            back = false;
            back_right = false;

            back = false;
            back_left = false;

            duck = false;

            duck_walk = false;

            key_run = false;

            idle = true;

        }

        if (key_run && !Key_s && !Key_d && !Key_a)
        {
            right = false;
            left = false;


            forward = false;
            forward_right = false;

            forward = false;
            forward_left = false;

            back = false;
            back_right = false;

            back = false;
            back_left = false;

            duck = false;

            duck_walk = false;

            key_run = false;

            idle = false;

            run = true;
        }









    }



    public GameObject cam;


    public bool _3rd;

    public GameObject[] heads;

    public void head_3rd_status()
    {
        // turning off the heads for each character

        if (_3rd)
        {
            foreach (GameObject g in heads)
            {
                g.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject g in heads)
            {
                g.SetActive(true);
            }
        }
    }

    public Transform skin_folder;

    int active_skin_ID;
    public void action_execute()
    {
        // Here we shoot a 
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {

         
            if (hit.collider.tag == "eq")
            {
                destroy = false;
                picking_up(hit.transform.GetComponent<equipment>().ID);



                if (destroy)
                {
                    Destroy(hit.transform.gameObject);
                }
            }
         




        }




    }







    bool destroy;

    // objects to drop

    public GameObject lamp_a;
    public GameObject lamp_laser_a;
    public GameObject lamp_laser_b;
    public GameObject laser_a;

    public GameObject red_dot_a;
    public GameObject red_dot_b;
    public GameObject red_dot_c;
    public GameObject red_dot_d;
    public GameObject red_dot_e;

    public GameObject scope_a;
    public GameObject scope_b;
    public GameObject scope_c;

    public GameObject suppressor_a;
    public GameObject suppressor_clustersub;
    public GameObject suppressor_c;
    public GameObject suppressor_d;



    // turning on active weapon and setting up custom adjustments

    public GameObject drop_assault57;

    public GameObject drop_fireaxe;

    public GameObject drop_old_pistol;



    // accessing all weapon scripts to controll the change

    public GameObject active_assault57;

    public GameObject active_fireaxe;

    public GameObject active_old_pistol;



    // Icons of the weapons

    public GameObject Icon_assault57;

    public GameObject Icon_fireaxe;

    public GameObject Icon_old_pistol;






    public GameObject assault57_obj;
    public bool assault57_bool;




    public GameObject old_pistol_obj;
    public bool old_pistol_bool;





 

    public AudioClip click;
    public GameObject animator_obj;
    public void picking_up(string which)
    {
       
        if (assault57_bool)
        {


            // Here we check, what we picked up and turning on the states on the gun




            // Lamp_a and lamp_laser_b are on the right side of the holder on the assault57 rifle
            if (which == "lamp_a")
            {

                AudioSource.PlayClipAtPoint(click, transform.position);
                assault57_obj.GetComponent<assault57>().Equipment_holder_b_bool = true;
                destroy = true;

                assault57_obj.GetComponent<assault57>().lamp_a_bool = true;

              

                if (assault57_obj.GetComponent<assault57>().lamp_laser_b_bool)
                {
                    Drop("lamp_laser_b");
                }


                assault57_obj.GetComponent<assault57>().lamp_laser_b_bool = false;

                assault57_obj.GetComponent<assault57>().change_equipment();


            }
            if (which == "lamp_laser_b")
            {
                AudioSource.PlayClipAtPoint(click, transform.position);
                assault57_obj.GetComponent<assault57>().Equipment_holder_b_bool = true;
                destroy = true;

                assault57_obj.GetComponent<assault57>().lamp_laser_b_bool = true;

               

                if (assault57_obj.GetComponent<assault57>().lamp_a_bool)
                {
                    Drop("lamp_a");

                    assault57_obj.GetComponent<assault57>().lamp_a_bool = false;
                }




                assault57_obj.GetComponent<assault57>().change_equipment();


            }

            // laser_a and lamp_laser_a are on the left side of the holder on the assault57 rifle
            if (which == "laser_a")
            {
                AudioSource.PlayClipAtPoint(click, transform.position);
                assault57_obj.GetComponent<assault57>().Equipment_holder_b_bool = true;
                destroy = true;

                assault57_obj.GetComponent<assault57>().laser_a_bool = true;



                if (assault57_obj.GetComponent<assault57>().lamp_laser_a_bool)
                {
                    Drop("lamp_laser_a");

                    assault57_obj.GetComponent<assault57>().lamp_laser_a_bool = false;
                }




                assault57_obj.GetComponent<assault57>().change_equipment();



            }
            if (which == "lamp_laser_a")
            {
                AudioSource.PlayClipAtPoint(click, transform.position);
                assault57_obj.GetComponent<assault57>().Equipment_holder_b_bool = true;
                destroy = true;

                assault57_obj.GetComponent<assault57>().lamp_laser_a_bool = true;



                if (assault57_obj.GetComponent<assault57>().laser_a_bool)
                {
                    Drop("lamp_laser_a");

                    assault57_obj.GetComponent<assault57>().laser_a_bool = false;
                }




                assault57_obj.GetComponent<assault57>().change_equipment();



            }





           
            if (which == "red_dot_a")
            {
                AudioSource.PlayClipAtPoint(click, transform.position);

                destroy = true;

                


                if (assault57_obj.GetComponent<assault57>().red_dot_a_bool)
                {
                    Drop("red_dot_a");
                    assault57_obj.GetComponent<assault57>().red_dot_a_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_b_bool)
                {
                    Drop("red_dot_b");
                    assault57_obj.GetComponent<assault57>().red_dot_b_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_c_bool)
                {
                    Drop("red_dot_c");
                    assault57_obj.GetComponent<assault57>().red_dot_c_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_d_bool)
                {
                    Drop("red_dot_d");
                    assault57_obj.GetComponent<assault57>().red_dot_d_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_e_bool)
                {
                    Drop("red_dot_e");
                    assault57_obj.GetComponent<assault57>().red_dot_e_bool = false;
                }



                if (assault57_obj.GetComponent<assault57>().scope_a_bool)
                {
                    Drop("scope_a");
                    assault57_obj.GetComponent<assault57>().scope_a_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().scope_b_bool)
                {
                    Drop("scope_b");
                    assault57_obj.GetComponent<assault57>().scope_b_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().scope_c_bool)
                {
                    Drop("scope_c");
                    assault57_obj.GetComponent<assault57>().scope_c_bool = false;
                }

                Debug.Log("worked");

                assault57_obj.GetComponent<assault57>().Equipment_holder_a_bool = true;

                assault57_obj.GetComponent<assault57>().red_dot_a_bool = true;





                assault57_obj.GetComponent<assault57>().change_equipment();


            }
            if (which == "red_dot_b")
            {
                AudioSource.PlayClipAtPoint(click, transform.position);

                destroy = true;

              


                if (assault57_obj.GetComponent<assault57>().red_dot_a_bool)
                {
                    Drop("red_dot_a");
                    assault57_obj.GetComponent<assault57>().red_dot_a_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_b_bool)
                {
                    Drop("red_dot_b");
                    assault57_obj.GetComponent<assault57>().red_dot_b_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_c_bool)
                {
                    Drop("red_dot_c");
                    assault57_obj.GetComponent<assault57>().red_dot_c_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_d_bool)
                {
                    Drop("red_dot_d");
                    assault57_obj.GetComponent<assault57>().red_dot_d_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_e_bool)
                {
                    Drop("red_dot_e");
                    assault57_obj.GetComponent<assault57>().red_dot_e_bool = false;
                }



                if (assault57_obj.GetComponent<assault57>().scope_a_bool)
                {
                    Drop("scope_a");
                    assault57_obj.GetComponent<assault57>().scope_a_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().scope_b_bool)
                {
                    Drop("scope_b");
                    assault57_obj.GetComponent<assault57>().scope_b_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().scope_c_bool)
                {
                    Drop("scope_c");
                    assault57_obj.GetComponent<assault57>().scope_c_bool = false;
                }


                assault57_obj.GetComponent<assault57>().Equipment_holder_a_bool = true;

                assault57_obj.GetComponent<assault57>().red_dot_b_bool = true;





                assault57_obj.GetComponent<assault57>().change_equipment();


            }
            if (which == "red_dot_c")
            {
                AudioSource.PlayClipAtPoint(click, transform.position);

                destroy = true;

               


                if (assault57_obj.GetComponent<assault57>().red_dot_a_bool)
                {
                    Drop("red_dot_a");
                    assault57_obj.GetComponent<assault57>().red_dot_a_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_b_bool)
                {
                    Drop("red_dot_b");
                    assault57_obj.GetComponent<assault57>().red_dot_b_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_c_bool)
                {
                    Drop("red_dot_c");
                    assault57_obj.GetComponent<assault57>().red_dot_c_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_d_bool)
                {
                    Drop("red_dot_d");
                    assault57_obj.GetComponent<assault57>().red_dot_d_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_e_bool)
                {
                    Drop("red_dot_e");
                    assault57_obj.GetComponent<assault57>().red_dot_e_bool = false;
                }



                if (assault57_obj.GetComponent<assault57>().scope_a_bool)
                {
                    Drop("scope_a");
                    assault57_obj.GetComponent<assault57>().scope_a_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().scope_b_bool)
                {
                    Drop("scope_b");
                    assault57_obj.GetComponent<assault57>().scope_b_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().scope_c_bool)
                {
                    Drop("scope_c");
                    assault57_obj.GetComponent<assault57>().scope_c_bool = false;
                }


                assault57_obj.GetComponent<assault57>().Equipment_holder_a_bool = true;

                assault57_obj.GetComponent<assault57>().red_dot_c_bool = true;





                assault57_obj.GetComponent<assault57>().change_equipment();


            }
            if (which == "red_dot_d")
            {
                AudioSource.PlayClipAtPoint(click, transform.position);

                destroy = true;

               

                if (assault57_obj.GetComponent<assault57>().red_dot_a_bool)
                {
                    Drop("red_dot_a");
                    assault57_obj.GetComponent<assault57>().red_dot_a_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_b_bool)
                {
                    Drop("red_dot_b");
                    assault57_obj.GetComponent<assault57>().red_dot_b_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_c_bool)
                {
                    Drop("red_dot_c");
                    assault57_obj.GetComponent<assault57>().red_dot_c_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_d_bool)
                {
                    Drop("red_dot_d");
                    assault57_obj.GetComponent<assault57>().red_dot_d_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_e_bool)
                {
                    Drop("red_dot_e");
                    assault57_obj.GetComponent<assault57>().red_dot_e_bool = false;
                }



                if (assault57_obj.GetComponent<assault57>().scope_a_bool)
                {
                    Drop("scope_a");
                    assault57_obj.GetComponent<assault57>().scope_a_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().scope_b_bool)
                {
                    Drop("scope_b");
                    assault57_obj.GetComponent<assault57>().scope_b_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().scope_c_bool)
                {
                    Drop("scope_c");
                    assault57_obj.GetComponent<assault57>().scope_c_bool = false;
                }


                assault57_obj.GetComponent<assault57>().Equipment_holder_a_bool = true;

                assault57_obj.GetComponent<assault57>().red_dot_d_bool = true;





                assault57_obj.GetComponent<assault57>().change_equipment();


            }
            if (which == "red_dot_e")
            {
                AudioSource.PlayClipAtPoint(click, transform.position);

                destroy = true;

               


                if (assault57_obj.GetComponent<assault57>().red_dot_a_bool)
                {
                    Drop("red_dot_a");
                    assault57_obj.GetComponent<assault57>().red_dot_a_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_b_bool)
                {
                    Drop("red_dot_b");
                    assault57_obj.GetComponent<assault57>().red_dot_b_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_c_bool)
                {
                    Drop("red_dot_c");
                    assault57_obj.GetComponent<assault57>().red_dot_c_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_d_bool)
                {
                    Drop("red_dot_d");
                    assault57_obj.GetComponent<assault57>().red_dot_d_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_e_bool)
                {
                    Drop("red_dot_e");
                    assault57_obj.GetComponent<assault57>().red_dot_e_bool = false;
                }



                if (assault57_obj.GetComponent<assault57>().scope_a_bool)
                {
                    Drop("scope_a");
                    assault57_obj.GetComponent<assault57>().scope_a_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().scope_b_bool)
                {
                    Drop("scope_b");
                    assault57_obj.GetComponent<assault57>().scope_b_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().scope_c_bool)
                {
                    Drop("scope_c");
                    assault57_obj.GetComponent<assault57>().scope_c_bool = false;
                }


                assault57_obj.GetComponent<assault57>().Equipment_holder_a_bool = true;

                assault57_obj.GetComponent<assault57>().red_dot_e_bool = true;





                assault57_obj.GetComponent<assault57>().change_equipment();


            }
            if (which == "scope_a")
            {
                AudioSource.PlayClipAtPoint(click, transform.position);

                destroy = true;

         


                if (assault57_obj.GetComponent<assault57>().red_dot_a_bool)
                {
                    Drop("red_dot_a");
                    assault57_obj.GetComponent<assault57>().red_dot_a_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_b_bool)
                {
                    Drop("red_dot_b");
                    assault57_obj.GetComponent<assault57>().red_dot_b_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_c_bool)
                {
                    Drop("red_dot_c");
                    assault57_obj.GetComponent<assault57>().red_dot_c_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_d_bool)
                {
                    Drop("red_dot_d");
                    assault57_obj.GetComponent<assault57>().red_dot_d_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_e_bool)
                {
                    Drop("red_dot_e");
                    assault57_obj.GetComponent<assault57>().red_dot_e_bool = false;
                }



                if (assault57_obj.GetComponent<assault57>().scope_a_bool)
                {
                    Drop("scope_a");
                    assault57_obj.GetComponent<assault57>().scope_a_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().scope_b_bool)
                {
                    Drop("scope_b");
                    assault57_obj.GetComponent<assault57>().scope_b_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().scope_c_bool)
                {
                    Drop("scope_c");
                    assault57_obj.GetComponent<assault57>().scope_c_bool = false;
                }


                assault57_obj.GetComponent<assault57>().Equipment_holder_a_bool = true;

                assault57_obj.GetComponent<assault57>().scope_a_bool = true;





                assault57_obj.GetComponent<assault57>().change_equipment();


            }
            if (which == "scope_b")
            {
                AudioSource.PlayClipAtPoint(click, transform.position);

                destroy = true;

             


                if (assault57_obj.GetComponent<assault57>().red_dot_a_bool)
                {
                    Drop("red_dot_a");
                    assault57_obj.GetComponent<assault57>().red_dot_a_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_b_bool)
                {
                    Drop("red_dot_b");
                    assault57_obj.GetComponent<assault57>().red_dot_b_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_c_bool)
                {
                    Drop("red_dot_c");
                    assault57_obj.GetComponent<assault57>().red_dot_c_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_d_bool)
                {
                    Drop("red_dot_d");
                    assault57_obj.GetComponent<assault57>().red_dot_d_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_e_bool)
                {
                    Drop("red_dot_e");
                    assault57_obj.GetComponent<assault57>().red_dot_e_bool = false;
                }



                if (assault57_obj.GetComponent<assault57>().scope_a_bool)
                {
                    Drop("scope_a");
                    assault57_obj.GetComponent<assault57>().scope_a_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().scope_b_bool)
                {
                    Drop("scope_b");
                    assault57_obj.GetComponent<assault57>().scope_b_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().scope_c_bool)
                {
                    Drop("scope_c");
                    assault57_obj.GetComponent<assault57>().scope_c_bool = false;
                }


                assault57_obj.GetComponent<assault57>().Equipment_holder_a_bool = true;

                assault57_obj.GetComponent<assault57>().scope_b_bool = true;





                assault57_obj.GetComponent<assault57>().change_equipment();


            }
            if (which == "scope_c")
            {
                AudioSource.PlayClipAtPoint(click, transform.position);

                destroy = true;

              


                if (assault57_obj.GetComponent<assault57>().red_dot_a_bool)
                {
                    Drop("red_dot_a");
                    assault57_obj.GetComponent<assault57>().red_dot_a_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_b_bool)
                {
                    Drop("red_dot_b");
                    assault57_obj.GetComponent<assault57>().red_dot_b_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_c_bool)
                {
                    Drop("red_dot_c");
                    assault57_obj.GetComponent<assault57>().red_dot_c_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_d_bool)
                {
                    Drop("red_dot_d");
                    assault57_obj.GetComponent<assault57>().red_dot_d_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().red_dot_e_bool)
                {
                    Drop("red_dot_e");
                    assault57_obj.GetComponent<assault57>().red_dot_e_bool = false;
                }



                if (assault57_obj.GetComponent<assault57>().scope_a_bool)
                {
                    Drop("scope_a");
                    assault57_obj.GetComponent<assault57>().scope_a_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().scope_b_bool)
                {
                    Drop("scope_b");
                    assault57_obj.GetComponent<assault57>().scope_b_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().scope_c_bool)
                {
                    Drop("scope_c");
                    assault57_obj.GetComponent<assault57>().scope_c_bool = false;
                }


                assault57_obj.GetComponent<assault57>().Equipment_holder_a_bool = true;

                assault57_obj.GetComponent<assault57>().scope_c_bool = true;





                assault57_obj.GetComponent<assault57>().change_equipment();


            }



            if (which == "suppressor_a")
            {
                AudioSource.PlayClipAtPoint(click, transform.position);


                destroy = true;


                if (assault57_obj.GetComponent<assault57>().suppressor_a_bool)
                {
                    Drop("suppressor_a");

                    assault57_obj.GetComponent<assault57>().suppressor_a_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().suppressor_c_bool)
                {
                    Drop("suppressor_c");

                    assault57_obj.GetComponent<assault57>().suppressor_c_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().suppressor_d_bool)
                {
                    Drop("suppressor_d");

                    assault57_obj.GetComponent<assault57>().suppressor_d_bool = false;
                }

                assault57_obj.GetComponent<assault57>().suppressor_a_bool = true;

                assault57_obj.GetComponent<assault57>().change_equipment();



            }
          
            if (which == "suppressor_c")
            {
                AudioSource.PlayClipAtPoint(click, transform.position);




                destroy = true;

                if (assault57_obj.GetComponent<assault57>().suppressor_a_bool)
                {
                    Drop("suppressor_a");

                    assault57_obj.GetComponent<assault57>().suppressor_a_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().suppressor_c_bool)
                {
                    Drop("suppressor_c");

                    assault57_obj.GetComponent<assault57>().suppressor_c_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().suppressor_d_bool)
                {
                    Drop("suppressor_d");

                    assault57_obj.GetComponent<assault57>().suppressor_d_bool = false;
                }

                assault57_obj.GetComponent<assault57>().suppressor_c_bool = true;

                assault57_obj.GetComponent<assault57>().change_equipment();



            }
            if (which == "suppressor_d")
            {
                AudioSource.PlayClipAtPoint(click, transform.position);


                destroy = true;


                if (assault57_obj.GetComponent<assault57>().suppressor_a_bool)
                {
                    Drop("suppressor_a");

                    assault57_obj.GetComponent<assault57>().suppressor_a_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().suppressor_c_bool)
                {
                    Drop("suppressor_c");

                    assault57_obj.GetComponent<assault57>().suppressor_c_bool = false;
                }

                if (assault57_obj.GetComponent<assault57>().suppressor_d_bool)
                {
                    Drop("suppressor_d");

                    assault57_obj.GetComponent<assault57>().suppressor_d_bool = false;
                }

                assault57_obj.GetComponent<assault57>().suppressor_d_bool = true;

                assault57_obj.GetComponent<assault57>().change_equipment();



            }

        }

        if (old_pistol_bool)
        {


            // Here we check, what we picked up and turning on the states on the gun




            if (which == "lamp_laser_b")
            {
                AudioSource.PlayClipAtPoint(click, transform.position);





                // same position, turning lamp_laser_b off

                if (old_pistol_obj.GetComponent<old_pistol>().laser_a_bool)
                {
                    Drop("lamp_laser_a");
                }
                if (old_pistol_obj.GetComponent<old_pistol>().lamp_laser_a_bool)
                {
                    Drop("laser_a");
                }
                if (old_pistol_obj.GetComponent<old_pistol>().lamp_laser_a_bool)
                {
                    Drop("lamp_laser_b");
                }
                if (old_pistol_obj.GetComponent<old_pistol>().lamp_laser_a_bool)
                {
                    Drop("lamp_a");
                }

                old_pistol_obj.GetComponent<old_pistol>().lamp_laser_a_bool = false;
                old_pistol_obj.GetComponent<old_pistol>().laser_a_bool = false;

                old_pistol_obj.GetComponent<old_pistol>().lamp_laser_b_bool = true;

                old_pistol_obj.GetComponent<old_pistol>().change_equipment();

                destroy = true;
            }
            if (which == "laser_a")
            {
                AudioSource.PlayClipAtPoint(click, transform.position);







                if (old_pistol_obj.GetComponent<old_pistol>().laser_a_bool)
                {
                    Drop("lamp_laser_a");
                }
                if (old_pistol_obj.GetComponent<old_pistol>().lamp_laser_a_bool)
                {
                    Drop("laser_a");
                }
                if (old_pistol_obj.GetComponent<old_pistol>().lamp_laser_a_bool)
                {
                    Drop("lamp_laser_b");
                }
                if (old_pistol_obj.GetComponent<old_pistol>().lamp_laser_a_bool)
                {
                    Drop("lamp_a");
                }


                old_pistol_obj.GetComponent<old_pistol>().lamp_laser_a_bool = false;
                old_pistol_obj.GetComponent<old_pistol>().lamp_laser_b_bool = false;


                old_pistol_obj.GetComponent<old_pistol>().laser_a_bool = true;

                old_pistol_obj.GetComponent<old_pistol>().change_equipment();

                destroy = true;

            }
            if (which == "lamp_laser_a")
            {
                AudioSource.PlayClipAtPoint(click, transform.position);







                if (old_pistol_obj.GetComponent<old_pistol>().laser_a_bool)
                {
                    Drop("lamp_laser_a");
                }
                if (old_pistol_obj.GetComponent<old_pistol>().lamp_laser_a_bool)
                {
                    Drop("laser_a");
                }
                if (old_pistol_obj.GetComponent<old_pistol>().lamp_laser_a_bool)
                {
                    Drop("lamp_laser_b");
                }
                if (old_pistol_obj.GetComponent<old_pistol>().lamp_laser_a_bool)
                {
                    Drop("lamp_a");
                }


                old_pistol_obj.GetComponent<old_pistol>().laser_a_bool = false;
                old_pistol_obj.GetComponent<old_pistol>().lamp_laser_b_bool = false;


                old_pistol_obj.GetComponent<old_pistol>().lamp_laser_a_bool = true;

                old_pistol_obj.GetComponent<old_pistol>().change_equipment();

                destroy = true;

            }


           
            if (which == "red_dot_a")
            {
                AudioSource.PlayClipAtPoint(click, transform.position);

                destroy = true;

                


                if (old_pistol_obj.GetComponent<old_pistol>().red_dot_a_bool)
                {
                    Drop("red_dot_a");
                    old_pistol_obj.GetComponent<old_pistol>().red_dot_a_bool = false;
                }




                old_pistol_obj.GetComponent<old_pistol>().red_dot_a_bool = true;





                old_pistol_obj.GetComponent<old_pistol>().change_equipment();


            }





            if (which == "suppressor_a")
            {
                AudioSource.PlayClipAtPoint(click, transform.position);


                destroy = true;


                if (old_pistol_obj.GetComponent<old_pistol>().suppressor_a_bool)
                {
                    Drop("suppressor_a");

                    old_pistol_obj.GetComponent<old_pistol>().suppressor_a_bool = false;
                }

                if (old_pistol_obj.GetComponent<old_pistol>().suppressor_c_bool)
                {
                    Drop("suppressor_c");

                    old_pistol_obj.GetComponent<old_pistol>().suppressor_c_bool = false;
                }

                if (old_pistol_obj.GetComponent<old_pistol>().suppressor_d_bool)
                {
                    Drop("suppressor_d");

                    old_pistol_obj.GetComponent<old_pistol>().suppressor_d_bool = false;
                }

                old_pistol_obj.GetComponent<old_pistol>().suppressor_a_bool = true;

                old_pistol_obj.GetComponent<old_pistol>().change_equipment();



            }
            if (which == "suppressor_c")
            {
                AudioSource.PlayClipAtPoint(click, transform.position);




                destroy = true;

                if (old_pistol_obj.GetComponent<old_pistol>().suppressor_a_bool)
                {
                    Drop("suppressor_a");

                    old_pistol_obj.GetComponent<old_pistol>().suppressor_a_bool = false;
                }

                if (old_pistol_obj.GetComponent<old_pistol>().suppressor_c_bool)
                {
                    Drop("suppressor_c");

                    old_pistol_obj.GetComponent<old_pistol>().suppressor_c_bool = false;
                }

                if (old_pistol_obj.GetComponent<old_pistol>().suppressor_d_bool)
                {
                    Drop("suppressor_d");

                    old_pistol_obj.GetComponent<old_pistol>().suppressor_d_bool = false;
                }

                old_pistol_obj.GetComponent<old_pistol>().suppressor_c_bool = true;

                old_pistol_obj.GetComponent<old_pistol>().change_equipment();



            }
            if (which == "suppressor_d")
            {
                AudioSource.PlayClipAtPoint(click, transform.position);


                destroy = true;


                if (old_pistol_obj.GetComponent<old_pistol>().suppressor_a_bool)
                {
                    Drop("suppressor_a");

                    old_pistol_obj.GetComponent<old_pistol>().suppressor_a_bool = false;
                }

                if (old_pistol_obj.GetComponent<old_pistol>().suppressor_c_bool)
                {
                    Drop("suppressor_c");

                    old_pistol_obj.GetComponent<old_pistol>().suppressor_c_bool = false;
                }

                if (old_pistol_obj.GetComponent<old_pistol>().suppressor_d_bool)
                {
                    Drop("suppressor_d");

                    old_pistol_obj.GetComponent<old_pistol>().suppressor_d_bool = false;
                }

                old_pistol_obj.GetComponent<old_pistol>().suppressor_d_bool = true;

                old_pistol_obj.GetComponent<old_pistol>().change_equipment();



            }

        }
  



        // checking, which weapon we picked up 

        if (which == "Assault57")
        {


            old_pistol_bool = false;
     
            assault57_bool = false;


            assault57_bool = true;

            // Here we give the access, to destory the intem, which we picked up
            destroy = true;


            check_weapon_drop();


            exit_weapons();

            active_assault57.SetActive(true);
            Icon_assault57.SetActive(true);
            active_assault57.GetComponent<assault57>().Start();

            // restarting the animator
            animator_obj.SetActive(false);
            animator_obj.SetActive(true);





        }
      
        if (which == "axe")
        {


            old_pistol_bool = false;
            
            assault57_bool = false;


            // Here we give the access, to destory the intem, which we picked up
            destroy = true;

           
            check_weapon_drop();

            
            exit_weapons();

            active_fireaxe.SetActive(true);
            Icon_fireaxe.SetActive(true);
            // restarting the animator
            animator_obj.SetActive(false);
            animator_obj.SetActive(true);


        }

        if (which == "old_pistol")
        {


          
            old_pistol_bool = false;
     
            assault57_bool = false;


            old_pistol_bool = true;

            // Here we give the access, to destory the intem, which we picked up
            destroy = true;

            check_weapon_drop();

            exit_weapons();
            active_old_pistol.SetActive(true);
            active_old_pistol.GetComponent<old_pistol>().Start();
            Icon_old_pistol.SetActive(true);
            // restarting the animator
            animator_obj.SetActive(false);
            animator_obj.SetActive(true);


        }
       




    }


    public void exit_weapons()
    {

        // leaving all weapon scripts


        active_assault57.GetComponent<assault57>().turn_off_weapon();
       
        active_fireaxe.GetComponent<axe>().turn_off_weapon();
     
        active_old_pistol.GetComponent<old_pistol>().turn_off_weapon();
     

        // turning all weapons off + the script on it

        active_assault57.SetActive(false);
        
        active_fireaxe.SetActive(false);
       
        active_old_pistol.SetActive(false);
      

        // turning all weapon icons off

        Icon_assault57.SetActive(false);
      
        Icon_fireaxe.SetActive(false);
       
        Icon_old_pistol.SetActive(false);
       

    }




    public void check_weapon_drop()
    {
        // checking, which weapon is active, to drop, because it's getting replaced


        if (active_assault57.activeSelf)
        {
            Drop("Assault57");
        }
     
        if (active_fireaxe.activeSelf)
        {
            Drop("axe");
        }
        
        if (active_old_pistol.activeSelf)
        {
            Drop("old_pistol");
        }
        

    }




    
    public void Drop(string which)
    {


        // dropping a intem, if we replace it with an new one

        if (which == "lamp_a")
        {

            Instantiate(lamp_a, transform.position, transform.rotation);

        }
        if (which == "lamp_laser_b")
        {

            Instantiate(lamp_laser_b, transform.position, transform.rotation);

        }
        if (which == "laser_a")
        {

            Instantiate(laser_a, transform.position, transform.rotation);

        }
        if (which == "lamp_laser_a")
        {

            Instantiate(lamp_laser_a, transform.position, transform.rotation);

        }


        if (which == "red_dot_a")
        {

            Instantiate(red_dot_a, transform.position, transform.rotation);

        }
        if (which == "red_dot_b")
        {

            Instantiate(red_dot_b, transform.position, transform.rotation);

        }
        if (which == "red_dot_c")
        {

            Instantiate(red_dot_c, transform.position, transform.rotation);

        }
        if (which == "red_dot_d")
        {

            Instantiate(red_dot_d, transform.position, transform.rotation);

        }
        if (which == "red_dot_e")
        {

            Instantiate(red_dot_e, transform.position, transform.rotation);

        }
        if (which == "scope_a")
        {

            Instantiate(scope_a, transform.position, transform.rotation);

        }
        if (which == "scope_b")
        {

            Instantiate(scope_b, transform.position, transform.rotation);

        }
        if (which == "scope_c")
        {

            Instantiate(scope_c, transform.position, transform.rotation);

        }


        if (which == "suppressor_a")
        {

            Instantiate(suppressor_a, transform.position, transform.rotation);

        }
        if (which == "suppressor_b")
        {

            Instantiate(suppressor_clustersub, transform.position, transform.rotation);

        }
        if (which == "suppressor_c")
        {

            Instantiate(suppressor_c, transform.position, transform.rotation);

        }
        if (which == "suppressor_d")
        {

            Instantiate(suppressor_d, transform.position + new Vector3(0, 2, 0), transform.rotation);

        }


        // The same for the weapon, if we replace it with an new one


        if (which == "Assault57")
        {
            Instantiate(drop_assault57, transform.position + new Vector3(0, 2, 0), transform.rotation);
        }
       
        if (which == "axe")
        {
            Instantiate(drop_fireaxe, transform.position + new Vector3(0, 2, 0), transform.rotation);
        }



        if (which == "old_pistol")
        {
            Instantiate(drop_old_pistol, transform.position + new Vector3(0, 2, 0), transform.rotation);
        }
       
    }







}
