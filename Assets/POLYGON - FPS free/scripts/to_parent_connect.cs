using UnityEngine;

public class to_parent_connect : MonoBehaviour
{
    public GameObject aim_object;


    public int dmg;
    public void hitted_glass(int dmg)
    {

        aim_object.GetComponent<destroy_object>().Input_damage(dmg, true);



    }


}
