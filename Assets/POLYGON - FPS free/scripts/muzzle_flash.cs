using UnityEngine;

public class muzzle_flash : MonoBehaviour
{

    public void Start()
    {


        scale_increase = gameObject.transform.localScale + new Vector3(step_size_increase, step_size_increase, step_size_increase);
        scale_decrease = gameObject.transform.localScale + new Vector3(step_size_decrease, step_size_decrease, step_size_decrease);

    }
    public GameObject origin;

    public float increase_time;
    public float decrease_time;
    public float step_size_increase;
    public float step_size_decrease;
    bool max_size_reached;

    Vector3 scale_increase, scale_decrease;

    void Update()
    {





        transform.position = origin.transform.position;
        transform.rotation = origin.transform.rotation;


        transform.Rotate(0, 0, Random.Range(0, 90));


        if (increase_time > 0)
        {

            increase_time -= step_size_increase;
            gameObject.transform.localScale = scale_increase;
            scale_increase = gameObject.transform.localScale + new Vector3(step_size_increase, step_size_increase, step_size_increase);
        }
        else
        {

            max_size_reached = true;
        }


        if (decrease_time > 0 && max_size_reached)
        {

            decrease_time -= step_size_decrease;
            gameObject.transform.localScale = scale_decrease;
            scale_decrease = gameObject.transform.localScale - new Vector3(step_size_decrease, step_size_decrease, step_size_decrease);
        }
        if (decrease_time < 0 && max_size_reached || decrease_time == 0)
        {
            Destroy(gameObject);
        }




    }
}
