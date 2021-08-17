using UnityEngine;

public class spawn_blood : MonoBehaviour
{

    public GameObject recyle_particles_performance;

    public GameObject[] blood_boxes;




    public float angle;



    public float power;




    void Start()
    {


        recyle_particles_performance = GameObject.FindGameObjectWithTag("rycle");
    }




    public void blood_spawining()
    {



        foreach (GameObject g in blood_boxes)
        {
            g.transform.localPosition = new Vector3(0, 0, 0);


            float x = Random.Range(-0.05f, 0.05f);
            float y = Random.Range(-0.05f, 0.05f);
            float z = Random.Range(-0.05f, 0.05f);


            g.transform.position += new Vector3(x, y, z);


            g.GetComponent<Rigidbody>().velocity = transform.position;


            g.SetActive(true);



            float p = UnityEngine.Random.Range(0, power);

            g.GetComponent<Rigidbody>().AddForce(transform.TransformDirection(Vector3.forward + new Vector3(UnityEngine.Random.Range(-angle, angle), UnityEngine.Random.Range(-angle, angle), UnityEngine.Random.Range(-angle, angle))) * p, ForceMode.Impulse);
        }
















    }


}
