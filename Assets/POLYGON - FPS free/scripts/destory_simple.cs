using System.Collections;
using UnityEngine;

public class destory_simple : MonoBehaviour
{


    public GameObject[] To_delte;
    public GameObject[] To_drop;
    public GameObject[] active_skins;
    public GameObject[] destroy_colliders;
    public AudioClip break_sound;
    public Texture Burned;


    public void Start()
    {
        health = current_health;
    }

    public int health;
    public int current_health;
    bool already_fire;
    public void Receive_DMG(int dmg, bool fire)
    {

        current_health -= dmg;

        if (current_health < 0)
        {
            Destroyy();
        }




        if (fire && !already_fire)
        {
            already_fire = true;
            StartCoroutine(firee());
        }
    }


    public bool burn_able;


    public void Destroyy()
    {
        AudioSource.PlayClipAtPoint(break_sound, transform.position);




        foreach (GameObject g in To_delte)
        {
            if (g.GetComponent<MeshRenderer>())
            {
                Destroy(g.GetComponent<MeshRenderer>());
            }
        }

        foreach (GameObject g in To_drop)
        {
            if (g != null)
            {
                g.SetActive(true);
            }
        }



        foreach (GameObject g in To_drop)
        {
            if (already_fire)
            {
                g.GetComponent<MeshRenderer>().materials[0].mainTexture = Burned;
                g.GetComponent<MeshRenderer>().materials[1].mainTexture = Burned;
            }
            g.transform.parent = null;
        }



        foreach (GameObject g in destroy_colliders)
        {
            if (g.GetComponent<Collider>())
            {
                Destroy(g.GetComponent<Collider>());
            }
        }

        Destroy(gameObject);

    }



    public GameObject fire_on_object;

    public IEnumerator firee()
    {
        yield return new WaitForSeconds(0);
        fire_on_object.SetActive(true);


        current_health -= 100;


        if (current_health < (health / 2))
        {

            foreach (GameObject g in active_skins)
            {

                g.GetComponent<MeshRenderer>().materials[0].mainTexture = Burned;
                g.GetComponent<MeshRenderer>().materials[1].mainTexture = Burned;
            }

        }



        yield return new WaitForSeconds(1);

        if (current_health < 0 || current_health == 0)
        {

            Destroyy();
        }

        if (current_health > 0 || current_health == 0)
        {
            StartCoroutine(firee());
        }




    }


}
