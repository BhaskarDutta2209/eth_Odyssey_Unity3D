using UnityEngine;

public class recyle_inst : MonoBehaviour
{

    // saving performance and reusing the spawned objtecs, because instantiate is expensive and laggy

    public GameObject[] stone_particles;
    public int stone_particles_count;

    public GameObject[] dirt_particles;
    public int dirt_particles_count;

    public GameObject[] wood_particles;
    public int wood_particles_count;

    public GameObject[] metall_particles;
    public int metall_particles_count;

    public GameObject[] glass_particles;
    public int glass_particles_count;

    public GameObject[] blood_decals;
    public int blood_decals_count;





    public GameObject[] blood_particle;
    public int blood__particle_count;



    // sending hit point to this script, to place the particle system on the point and restarting the effect + sound
    public void stone_particle_new(Vector3 position_, Vector3 rotation_)
    {


        if (stone_particles_count < stone_particles.Length)
        {

            stone_particles[stone_particles_count - 1].transform.position = position_;
            stone_particles[stone_particles_count - 1].transform.rotation = Quaternion.LookRotation(rotation_);

            stone_particles[stone_particles_count - 1].GetComponent<ParticleSystem>().Play();
            stone_particles[stone_particles_count - 1].GetComponent<AudioSource>().Play();

            stone_particles_count += 1;
        }

        if (stone_particles_count == stone_particles.Length)
        {

            stone_particles[stone_particles_count - 1].transform.position = position_;
            stone_particles[stone_particles_count - 1].transform.rotation = Quaternion.LookRotation(rotation_);

            stone_particles[stone_particles_count - 1].GetComponent<ParticleSystem>().Play();
            stone_particles[stone_particles_count - 1].GetComponent<AudioSource>().Play();

            stone_particles_count = 1;

        }



    }
    public void dirt_particle_new(Vector3 position_, Vector3 rotation_)
    {


        if (dirt_particles_count < dirt_particles.Length)
        {

            dirt_particles[dirt_particles_count - 1].transform.position = position_;
            dirt_particles[dirt_particles_count - 1].transform.rotation = Quaternion.LookRotation(rotation_);

            dirt_particles[dirt_particles_count - 1].GetComponent<ParticleSystem>().Play();
            dirt_particles[dirt_particles_count - 1].GetComponent<AudioSource>().Play();

            dirt_particles_count += 1;
        }

        if (dirt_particles_count == dirt_particles.Length)
        {

            dirt_particles[dirt_particles_count - 1].transform.position = position_;
            dirt_particles[dirt_particles_count - 1].transform.rotation = Quaternion.LookRotation(rotation_);

            dirt_particles[dirt_particles_count - 1].GetComponent<ParticleSystem>().Play();
            dirt_particles[dirt_particles_count - 1].GetComponent<AudioSource>().Play();

            dirt_particles_count = 1;

        }



    }

    public void wood_particle_new(Vector3 position_, Vector3 rotation_)
    {


        if (wood_particles_count < wood_particles.Length)
        {

            wood_particles[wood_particles_count - 1].transform.position = position_;
            wood_particles[wood_particles_count - 1].transform.rotation = Quaternion.LookRotation(rotation_);

            wood_particles[wood_particles_count - 1].GetComponent<ParticleSystem>().Play();
            wood_particles[wood_particles_count - 1].GetComponent<AudioSource>().Play();

            wood_particles_count += 1;
        }

        if (wood_particles_count == wood_particles.Length)
        {

            wood_particles[wood_particles_count - 1].transform.position = position_;
            wood_particles[wood_particles_count - 1].transform.rotation = Quaternion.LookRotation(rotation_);

            wood_particles[wood_particles_count - 1].GetComponent<ParticleSystem>().Play();
            wood_particles[wood_particles_count - 1].GetComponent<AudioSource>().Play();

            wood_particles_count = 1;

        }



    }

    public void metall_particle_new(Vector3 position_, Vector3 rotation_)
    {


        if (metall_particles_count < metall_particles.Length)
        {

            metall_particles[metall_particles_count - 1].transform.position = position_;
            metall_particles[metall_particles_count - 1].transform.rotation = Quaternion.LookRotation(rotation_);

            metall_particles[metall_particles_count - 1].GetComponent<ParticleSystem>().Play();
            metall_particles[metall_particles_count - 1].GetComponent<AudioSource>().Play();

            metall_particles_count += 1;
        }

        if (metall_particles_count == metall_particles.Length)
        {

            metall_particles[metall_particles_count - 1].transform.position = position_;
            metall_particles[metall_particles_count - 1].transform.rotation = Quaternion.LookRotation(rotation_);

            metall_particles[metall_particles_count - 1].GetComponent<ParticleSystem>().Play();
            metall_particles[metall_particles_count - 1].GetComponent<AudioSource>().Play();

            metall_particles_count = 1;

        }



    }

    public void glass_particle_new(Vector3 position_, Vector3 rotation_)
    {


        if (glass_particles_count < glass_particles.Length)
        {

            glass_particles[glass_particles_count - 1].transform.position = position_;
            glass_particles[glass_particles_count - 1].transform.rotation = Quaternion.LookRotation(rotation_);

            glass_particles[glass_particles_count - 1].GetComponent<ParticleSystem>().Play();
            glass_particles[glass_particles_count - 1].GetComponent<AudioSource>().Play();

            glass_particles_count += 1;
        }

        if (glass_particles_count == glass_particles.Length)
        {

            glass_particles[glass_particles_count - 1].transform.position = position_;
            glass_particles[glass_particles_count - 1].transform.rotation = Quaternion.LookRotation(rotation_);

            glass_particles[glass_particles_count - 1].GetComponent<ParticleSystem>().Play();
            glass_particles[glass_particles_count - 1].GetComponent<AudioSource>().Play();

            glass_particles_count = 1;

        }



    }

    public void blood_particle_new(Vector3 position_, Vector3 rotation_)
    {


        if (blood__particle_count < blood_particle.Length)
        {

            blood_particle[blood__particle_count - 1].transform.position = position_;
            blood_particle[blood__particle_count - 1].transform.rotation = Quaternion.LookRotation(rotation_);

            blood_particle[blood__particle_count - 1].GetComponent<ParticleSystem>().Play();
            blood_particle[blood__particle_count - 1].GetComponent<AudioSource>().Play();

            blood__particle_count += 1;
        }

        if (blood__particle_count == blood_particle.Length)
        {

            blood_particle[blood__particle_count - 1].transform.position = position_;
            blood_particle[blood__particle_count - 1].transform.rotation = Quaternion.LookRotation(rotation_);

            blood_particle[blood__particle_count - 1].GetComponent<ParticleSystem>().Play();
            blood_particle[blood__particle_count - 1].GetComponent<AudioSource>().Play();

            blood__particle_count = 1;

        }



    }





    public void blood_decal_new(Vector3 position_, Vector3 rotation_)
    {


        if (blood_decals_count < blood_decals.Length)
        {

            blood_decals[blood_decals_count - 1].transform.position = position_;
            blood_decals[blood_decals_count - 1].transform.rotation = Quaternion.LookRotation(rotation_);



            blood_decals_count += 1;
        }

        if (blood_decals_count == blood_decals.Length)
        {

            blood_decals[blood_decals_count - 1].transform.position = position_;
            blood_decals[blood_decals_count - 1].transform.rotation = Quaternion.LookRotation(rotation_);


            blood_decals_count = 1;

        }



    }



}
