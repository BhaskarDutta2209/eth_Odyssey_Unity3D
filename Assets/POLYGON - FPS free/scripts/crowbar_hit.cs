using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crowbar_hit : MonoBehaviour
{


    public int dmg;




    // reusing particles
    public GameObject recyle_particles_performance;

    void Start()
    {
        recyle_particles_performance = GameObject.FindGameObjectWithTag("rycle");
    }




        public void OnCollisionEnter(Collision collision)
        {

     


            if (collision.collider.tag == "body")
            {



              

               if(collision.collider.gameObject.GetComponent<bunny_receive_dmg>().is_head)
               {
               // bunny receiving 4x more damage, if you hit the head
                collision.collider.gameObject.GetComponent<bunny_receive_dmg>().take_dmg(dmg*4);
                }
               else
               {
              
                collision.collider.gameObject.GetComponent<bunny_receive_dmg>().take_dmg(dmg);
                }


          
         
            recyle_particles_performance.GetComponent<recyle_inst>().blood_particle_new(collision.contacts[0].point, transform.position);





        }








            
            if (collision.collider.tag == "petrol")
            {
        
            recyle_particles_performance.GetComponent<recyle_inst>().metall_particle_new(collision.contacts[0].point, transform.position);

        
            collision.collider.gameObject.GetComponent<petrol_can>().explosion_on();



               


                
                if (collision.collider.GetComponent<Rigidbody>())
                {
                  collision.collider.GetComponent<Rigidbody>().AddExplosionForce(dmg, collision.contacts[0].point, 10);
                }

           
            }




            if (collision.collider.tag == "metall")
            {
           
                recyle_particles_performance.GetComponent<recyle_inst>().metall_particle_new(collision.contacts[0].point, transform.position);
 

         


           
                if (collision.collider.GetComponent<Rigidbody>())
                {
                    collision.collider.GetComponent<Rigidbody>().AddExplosionForce(dmg, collision.contacts[0].point, 10);
                }

            }
            if (collision.collider.tag == "stone")
            {
            
            recyle_particles_performance.GetComponent<recyle_inst>().stone_particle_new(collision.contacts[0].point, transform.position);


        }

            // Dirt & Wood using the same particle
            if (collision.collider.tag == "dirt")
            {

            
            recyle_particles_performance.GetComponent<recyle_inst>().wood_particle_new(collision.contacts[0].point, transform.position);


             }

            if (collision.collider.tag == "wood")
            {

           
            recyle_particles_performance.GetComponent<recyle_inst>().wood_particle_new(collision.contacts[0].point, transform.position);

           
            if (collision.collider.gameObject.GetComponent<destory_simple>())
                {
                    collision.collider.GetComponent<Rigidbody>().AddExplosionForce(dmg, collision.contacts[0].point, 10);
                    collision.collider.gameObject.GetComponent<destory_simple>().Receive_DMG(dmg, false);
                }

              
            }







    }



}
