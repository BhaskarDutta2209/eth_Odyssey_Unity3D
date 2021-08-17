using UnityEngine;

public class destroy_object : MonoBehaviour
{



    public int strenght;
    public int glass_strenght;

    public void Input_damage(int value, bool is_glass)
    {


        if (!is_glass)
        {
            strenght -= value;
        }
        else
        {
            glass_strenght -= value;
        }


        if (strenght < 0 && !is_glass)
        {

            Destroy_object();

        }
        if (glass_strenght < 0 && is_glass)
        {

            Destroy_glass();

        }


    }


    public bool is_door;
    public bool break_able;
    public bool connected_glass;


    public GameObject[] break_pieces;
    public GameObject[] to_disaper;

    public void Destroy_object()
    {

        if (is_door)
        {
            if (break_able)
            {





                foreach (GameObject g in to_disaper)
                {
                    if (g.GetComponent<Collider>())
                    {
                        Destroy(g.GetComponent<Collider>());
                    }


                    Destroy(g.GetComponent<MeshRenderer>());

                    if (g.GetComponent<HingeJoint>())
                    {
                        Destroy(g.GetComponent<HingeJoint>());
                    }
                }


                foreach (GameObject g in break_pieces)
                {
                    g.SetActive(true);
                }

                if (connected_glass)
                {
                    Destroy_glass();
                }

                Destroy(door_without_glass);
            }
            else // Door isn't destroyable, but knocks out
            {


                Destroy(gameObject.GetComponent<HingeJoint>());

                if (gameObject.transform.tag == "wood_door")
                {
                    gameObject.transform.tag = "wood";
                }
                if (gameObject.transform.tag == "metall_door")
                {
                    gameObject.transform.tag = "metall";
                }






            }



        }






    }



    public GameObject[] break_pieces_glass;
    public GameObject[] to_disaper_glass;

    public GameObject door_without_glass;

    public AudioClip glass_breaks;


    public void Destroy_glass()
    {




        if (is_door)
        {

            AudioSource.PlayClipAtPoint(glass_breaks, transform.position);

            foreach (GameObject g in to_disaper_glass)
            {

                if ((g.GetComponent<Collider>() && g.GetComponent<Collider>().gameObject.name != "left") && g.GetComponent<Collider>() && g.GetComponent<Collider>().gameObject.name != "right")
                {

                    Destroy(g.GetComponent<Collider>());
                }




                Destroy(g.GetComponent<MeshRenderer>());


            }


            foreach (GameObject g in break_pieces_glass)
            {
                g.SetActive(true);
            }


            door_without_glass.SetActive(true);

        }
    }




}
