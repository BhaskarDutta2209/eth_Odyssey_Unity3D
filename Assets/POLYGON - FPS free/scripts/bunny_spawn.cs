using UnityEngine;

public class bunny_spawn : MonoBehaviour
{




    public bool start_bunny_spawn;

    public GameObject killer_bunny;

    public int spawn_ticks;
    public int spawn_ticks_current;
    bool in_spawn;


    void FixedUpdate()
    {


        spawn_ticks_current -= 1;

        if (start_bunny_spawn && !in_spawn && spawn_ticks_current < 0)
        {
            spawn_ticks_current = spawn_ticks;


            int Child_count = transform.childCount;


            Instantiate(killer_bunny, transform.GetChild(UnityEngine.Random.Range(0, Child_count)).position, transform.rotation);







        }

    }
}
