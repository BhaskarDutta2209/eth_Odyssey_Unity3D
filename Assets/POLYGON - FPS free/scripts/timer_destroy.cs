using UnityEngine;

public class timer_destroy : MonoBehaviour
{
    public int ticks;


    private void Start()
    {
        Destroy(gameObject, ticks / 100);


        if (multipy_timer)
        {
            foreach (GameObject g in to_destory)
            {
                Destroy(g, ticks / 100);
            }
        }
    }



    public GameObject[] to_destory;
    public bool multipy_timer;




}
