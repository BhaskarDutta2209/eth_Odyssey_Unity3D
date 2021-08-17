using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject warningMenu;

    [SerializeField]
    private GameObject self;

    public void PlayGame()
    {
        // Check if weapon is selected
        if (WalletDetails.gunA_damage.Length > 0 || WalletDetails.gunB_damage.Length > 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
        {
            Debug.Log("Printing Stuff");

            Debug.Log(self.name);

            self.SetActive(false);
            warningMenu.SetActive(true);
        }
            

    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
