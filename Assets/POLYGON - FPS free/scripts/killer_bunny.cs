using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class killer_bunny : MonoBehaviour
{

    public GameObject player;

    public NavMeshAgent agent;





    public float walk_speed;

    public float hit_range;

    public Transform head;

    private void Start()
    {

        player = GameObject.Find("Polygon_FPS_prefab");


        GameObject g = GameObject.Find("props_active");



        g.GetComponent<find_destory_able_props>().objs_7.Add(head);




        sound_next_ticks = UnityEngine.Random.Range(250, 1000);


        agent.GetComponent<NavMeshAgent>().avoidancePriority = UnityEngine.Random.Range(0, 100);

        target_source = GameObject.FindGameObjectWithTag("targets");

        StartCoroutine(Target_update());

    }

    int random_head_move;

    bool in_walk;

    public int health;
    public bool in_hitting;
    public int walking_stuck_ticks;


    bool already_died;
    public bool hit_ready;


    public GameObject[] Target_list;
    public List<GameObject> Target_list_active_check = new List<GameObject>();
    public GameObject target_source;
    void Update()
    {

        if (already_died)
        {


            return;
        }


        if (health < 1)
        {
            StopAllCoroutines();
        }

        sound_next_ticks -= 1;
        walking_stuck_ticks -= 1;










        if (sound_next_ticks < 0 && !in_bunny_sound)
        {

            ani.SetInteger("mouth", 1);
            in_bunny_sound = true;

            StartCoroutine(mouth_behavior());



        }

        if (Target_list.Length > 0)
        {
            if (Vector3.Distance(transform.position, Target_list[0].transform.position) < hit_range)
            {
                // hit 
                agent.speed = 0;
                ani.SetInteger("corp", 2);
                ani.SetInteger("legs", 0);
                in_hitting = true;
                in_walk = false;
                hit_ready = true;
            }
            else
            {
                // move to the player


                agent.SetDestination(Target_list[0].transform.position);



                agent.speed = walk_speed;
                in_hitting = false;
                in_walk = true;
                hit_ready = false;
                ani.SetInteger("legs", 1);


                // choosing between 2 walk animation, to make the killybunny less static in walking
                if (!in_corp_walk_determining)
                {
                    in_corp_walk_determining = true;
                    corp_walk_animation_r = StartCoroutine(corp_walk_animation());
                }

            }

        }




    }



    public IEnumerator Target_update()
    {
        yield return new WaitForSeconds(0.1f);


        Target_list = target_source.GetComponent<targets_for_bunny>().bunny_targets.ToArray();



        Target_list = Target_list.OrderBy(point => Vector3.Distance(transform.position, point.transform.position)).ToArray();

        Target_list_active_check.Clear();

        foreach (GameObject g in Target_list)
        {
            if (g.activeSelf)
            {
                Target_list_active_check.Add(g);
            }


        }

        Target_list = Target_list_active_check.ToArray();




        StartCoroutine(Target_update());
    }







    public void receive_dmg(int dmg, bool headshot)
    {



        health -= dmg;


        if (health < 0)
        {

            dead();
        }







    }








    public GameObject[] bones;
    List<GameObject> bunny_sounds = new List<GameObject>();
    public void dead()
    {

        StopAllCoroutines();
        foreach (GameObject g in bones)
        {
            g.GetComponent<bunny_receive_dmg>().dead = true;
        }

        Destroy(ani);

        already_died = true;



        foreach (GameObject g in bunny_sounds)
        {
            Destroy(g);
        }


        foreach (GameObject g in bones)
        {
            g.GetComponent<Rigidbody>().isKinematic = false;
        }



        Destroy(GetComponent<NavMeshAgent>());
        gameObject.AddComponent<timer_destroy>().ticks = 1200;
        Destroy(fire_burning);

    }


    public Animator ani;



    public IEnumerator hit()
    {
        yield return new WaitForSeconds(0);
    }




    public GameObject Audio_spawn;
    public AudioClip Bunny_sound;

    public int sound_next_ticks;
    public bool in_bunny_sound;


    bool in_corp_walk_determining;
    Coroutine corp_walk_animation_r;

    public IEnumerator corp_walk_animation()
    {
        yield return new WaitForSeconds(0);

        in_corp_walk_determining = true;




        int r = UnityEngine.Random.Range(1, 4);
        float l = 0.411f;

        if (r == 0)
        {
            ani.SetInteger("corp", 0);
        }
        if (r == 1)
        {
            ani.SetInteger("corp", 0);
        }
        if (r == 2)
        {
            ani.SetInteger("corp", 0);
        }
        if (r == 3)
        {
            // playing extra animation instead of walking arms to make the bunny more alive
            ani.SetInteger("corp", 1);
        }

        if (in_walk == false)
        {
            StopCoroutine(corp_walk_animation_r);
        }

        yield return new WaitForSeconds(l);

        in_corp_walk_determining = false;


    }






    public IEnumerator mouth_behavior()
    {

        yield return new WaitForSeconds(0);


        if (!already_died)
        {



            random_head_move = UnityEngine.Random.Range(0, 4);


            // adding random head movement, while screaming
            if (random_head_move == 0)
            {
                ani.SetInteger("head", 1);
            }
            if (random_head_move == 1)
            {
                ani.SetInteger("head", 2);
            }
            if (random_head_move == 2)
            {
                ani.SetInteger("head", 3);
            }
            if (random_head_move == 3)
            {
                ani.SetInteger("head", 3);
            }


            // open mouth
            ani.SetInteger("mouth", 1);

            GameObject AB = Instantiate(Audio_spawn, transform.position, transform.rotation);

            AB.GetComponent<AudioSource>().clip = Bunny_sound;
            AB.GetComponent<AudioSource>().maxDistance = 25;
            AB.GetComponent<AudioSource>().loop = false;
            AB.GetComponent<AudioSource>().Play();
            AB.transform.parent = gameObject.transform;
            AB.GetComponent<timer_destroy>().ticks = 2500;

            bunny_sounds.Add(AB);


            yield return new WaitForSeconds(5.067f);
            // close mouth
            ani.SetInteger("head", 0);
            ani.SetInteger("mouth", 0);
            // determine new timer for bunny sound
            sound_next_ticks = UnityEngine.Random.Range(200, 700);

            in_bunny_sound = false;



        }



    }

    public bool in_burning;
    public GameObject fire_burning;
    public void start_burning()
    {


        if (!in_burning && !already_died)
        {
            fire_burning.SetActive(true);
            in_burning = true;
            StartCoroutine(burning());



            main_skin.GetComponent<Renderer>().materials[0].mainTexture = burning_skin;


        }
    }


    public GameObject main_skin;
    public GameObject withoutHead_skin;
    public GameObject[] pieces_skin;



    public Texture burning_skin;
    public IEnumerator burning()
    {




        yield return new WaitForSeconds(0);

        health -= 5;

        if (health < 0 && !already_died)
        {
            dead();
        }


        yield return new WaitForSeconds(0.1f);
        if (!already_died)
        {
            StartCoroutine(burning());
        }

    }














}
