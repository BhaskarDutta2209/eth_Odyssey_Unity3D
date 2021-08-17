using System.Collections;
using UnityEngine;

public class axe : MonoBehaviour
{



    private void OnEnable()
    {
        _3rd_view = player.GetComponent<polygon_fps_controller>()._3rd;
    }
    

    public AudioClip axe_hit_sound;

    public GameObject player;

    bool running;
    bool walking;
    bool duck_walk;
    bool reload;

    public bool _3rd_view;
    public Vector3 _3rd_view_cam;

    void Update()
    {
        Input_Status();

        running = player.GetComponent<polygon_fps_controller>().running;
        walking = player.GetComponent<polygon_fps_controller>().walking;
        duck_walk = player.GetComponent<polygon_fps_controller>().duck_walk;
        cam_toggled = player.GetComponent<polygon_fps_controller>().cam_toggled;






        if (button_shoot && !in_shoot && !running)
        {
            in_shoot = true;
            shooting = StartCoroutine(hit());
        }




        // idle
        if (!button_aim && !button_shoot && !in_shoot)
        {
            ani.SetInteger("axe", 0);
        }


        if (walking && !in_shoot)
        {


            //  walk
            ani.SetInteger("axe", 1);

            // slow down duck walk
            if (duck_walk)
            {
                ani.SetFloat("weapon_speed", 0.5f);
            }
            else
            {
                ani.SetFloat("weapon_speed", 1);
            }

        }


        // run 
        if (running && !in_shoot)
        {

            ani.SetInteger("axe", 2);

        }




        cam_equipment();
    }


    public bool button_shoot;
    public bool button_aim;
    public bool key_reload;
    public bool cam_toggled;


    public void Input_Status()
    {
        if (Input.GetButton("Fire1"))
        {
            button_shoot = true;
        }
        else
        {
            button_shoot = false;
        }

        if (cam_toggled)
        {
            _3rd_view = true;
            player.GetComponent<polygon_fps_controller>()._3rd = false;
            player.GetComponent<polygon_fps_controller>().head_3rd_status();
        }
        else
        {
            _3rd_view = false;
            player.GetComponent<polygon_fps_controller>()._3rd = true;
            player.GetComponent<polygon_fps_controller>().head_3rd_status();
        }


        if (Input.GetButton("Fire2"))
        {
            button_aim = true;
        }
        else
        {
            button_aim = false;
        }


        if (Input.GetKey(KeyCode.R))
        {
            key_reload = true;
        }
        else
        {
            key_reload = false;
        }

        if (_3rd_view)
        {
            Cam.transform.localPosition = Vector3.Lerp(Cam.transform.localPosition, _3rd_view_cam, 15 * Time.deltaTime);

        }
    }








    bool in_shoot;
    Coroutine shooting;







    public GameObject hit_collider;
    public GameObject Shoot_start_point;

    public IEnumerator hit()
    {

        yield return new WaitForSeconds(0);

        ani.SetInteger("axe", 3);


        GameObject g = Instantiate(Clip_on_point, Shoot_start_point.transform.position, transform.rotation);
        g.GetComponent<AudioSource>().clip = axe_hit_sound;
        g.GetComponent<AudioSource>().maxDistance = 10;
        g.GetComponent<AudioSource>().Play();
        g.transform.parent = Shoot_start_point.transform;

        yield return new WaitForSeconds(0.1f);

        hit_collider.SetActive(true);

        yield return new WaitForSeconds(0.576975f);
        hit_collider.SetActive(false);

        if (!button_shoot)
        {
            in_shoot = false;
            StopCoroutine(hit());
        }
        else
        {

            StopCoroutine(hit());
            shooting = StartCoroutine(hit());
        }


    }





    public GameObject Clip_on_point;




    public GameObject Cam;
    public Animator ani;

    public Vector3 idle_cam;
    public Vector3 aim_cam;
    public Vector3 run_cam;




    public void cam_equipment()
    {



        if (_3rd_view)
        {
            Cam.transform.localPosition = Vector3.Lerp(Cam.transform.localPosition, _3rd_view_cam, 15 * Time.deltaTime);

            return;
        }



        // different cam positions, idle, aim, 3rd


        if (!button_aim && !running) //idle
        {

            Cam.transform.localPosition = Vector3.Lerp(Cam.transform.localPosition, idle_cam, 15 * Time.deltaTime);


        }
        if (button_aim && !running) //aim
        {
            Cam.transform.localPosition = Vector3.Lerp(Cam.transform.localPosition, aim_cam, 15 * Time.deltaTime);

        }
        if (running)
        {
            Cam.transform.localPosition = Vector3.Lerp(Cam.transform.localPosition, run_cam, 15 * Time.deltaTime);
        }








    }


    public void turn_off_weapon()
    {
        // reset all, also the animation to -1, that none animations from an different gun, runs into the animation/with the current one, or the result would be awful and not wished

        ani.SetInteger("axe", -1);



    }
















}
