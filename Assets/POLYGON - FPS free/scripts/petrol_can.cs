using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class petrol_can : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject fire_ball;
    public float force;
    public int fireball_count;
    public AudioClip explosion;
    public GameObject Audio_on;

    List<GameObject> f_balls = new List<GameObject>();


    public bool Straight_start;


    public void Start()
    {
        if (Straight_start)
        {
            StartCoroutine(Execute_explosion_little_delay());
        }
    }

    public IEnumerator Execute_explosion_little_delay()
    {
        yield return new WaitForSeconds(0.05f);


        explosion_on();
    }



    public void explosion_on()
    {

        for (int i = 0; i < fireball_count; i++)
        {


            // putting fireballs in random position to have a random spread, with AddExplosionForce
            float x = Random.Range(-0.5f, 0.5f);
            float y = Random.Range(0.3f, 0.5f);
            float z = Random.Range(-0.5f, 0.5f);

            GameObject f_b = Instantiate(fire_ball, transform.position + new Vector3(x, y, z), transform.rotation);



            f_balls.Add(f_b);





        }

        foreach (GameObject g in f_balls)
        {
            float random_force = Random.Range(0, force);




            g.GetComponent<timer_destroy>().ticks = 200;


            g.transform.Find("Sphere").GetComponent<Rigidbody>().AddExplosionForce(random_force, transform.position, 3);
        }

        GameObject a = Instantiate(Audio_on, transform.position, transform.rotation);

        a.GetComponent<AudioSource>().clip = explosion;
        a.GetComponent<AudioSource>().volume = 30;
        a.GetComponent<AudioSource>().Play();

        if (!Straight_start)
        {
            Destroy(gameObject);
        }

    }
}
