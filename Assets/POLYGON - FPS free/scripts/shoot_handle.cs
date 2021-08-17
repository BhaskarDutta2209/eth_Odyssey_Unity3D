using System.Collections.Generic;
using UnityEngine;

public class shoot_handle : MonoBehaviour
{
    public GameObject recyle_particles_performance;


    public void Start()
    {

        recyle_particles_performance = GameObject.FindGameObjectWithTag("rycle");
    }


    public struct add_shoot
    {
        public Vector3 pos;
        public Vector3 rot;
        public int dmg;

        public add_shoot(Vector3 pos, Vector3 rot, int dmg)
        {
            this.pos = pos;
            this.rot = rot;
            this.dmg = dmg;
        }
    }


    private List<add_shoot> added_shoots = new List<add_shoot>();

    void Update()
    {
        foreach (add_shoot s in added_shoots)
        {

            shoot(s.pos, s.rot, s.dmg);
        }


        added_shoots.Clear();

    }


    public void register_shoot(Vector3 pos, Vector3 rot, int dmg)
    {
        added_shoots.Add(new add_shoot(pos, rot, dmg));
    }




    public void shoot(Vector3 pos, Vector3 rot, int dmg)
    {




        RaycastHit hit;

       

        if (Physics.Raycast(pos, rot, out hit))
        {


            if (hit.collider.tag == "body")
            {



               
                hit.collider.gameObject.GetComponent<bunny_receive_dmg>().take_dmg(dmg);


               
                recyle_particles_performance.GetComponent<recyle_inst>().blood_particle_new(hit.point, (pos - hit.point));




            }





           
            if (hit.collider.tag == "petrol")
            {
                recyle_particles_performance.GetComponent<recyle_inst>().metall_particle_new(hit.point, (pos - hit.point));



                
                hit.collider.gameObject.GetComponent<petrol_can>().explosion_on();



            }




            if (hit.collider.tag == "metall")
            {
                recyle_particles_performance.GetComponent<recyle_inst>().metall_particle_new(hit.point, (pos - hit.point));


                
                if (hit.collider.GetComponent<Rigidbody>())
                {
                    hit.collider.GetComponent<Rigidbody>().AddExplosionForce(dmg / 10, hit.point, 10);
                }


            }



            if (hit.collider.tag == "stone" || hit.collider.tag == "Untagged")
            {

                recyle_particles_performance.GetComponent<recyle_inst>().stone_particle_new(hit.point, (pos - hit.point));





            }

          
            if (hit.collider.tag == "dirt")
            {

                recyle_particles_performance.GetComponent<recyle_inst>().dirt_particle_new(hit.point, (pos - hit.point));


            }

            if (hit.collider.tag == "wood")
            {

                recyle_particles_performance.GetComponent<recyle_inst>().wood_particle_new(hit.point, (pos - hit.point));


                
                if (hit.collider.gameObject.GetComponent<destory_simple>())
                {
                    hit.collider.GetComponent<Rigidbody>().AddExplosionForce(dmg, hit.point, 10);
                    hit.collider.gameObject.GetComponent<destory_simple>().Receive_DMG(dmg, false);
                }


            }






        }



    }








}
