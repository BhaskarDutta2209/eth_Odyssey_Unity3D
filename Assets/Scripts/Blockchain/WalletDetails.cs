using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
//using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;

public class WalletDetails : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string GetAccount();

    [SerializeField]
    private TextMeshProUGUI textMesh;

    [SerializeField]
    private TMP_Dropdown GunA_Dropdown;
    [SerializeField]
    private TMP_Dropdown GunB_Dropdown;

    [SerializeField]
    private GameObject noNFTWarning;

    [SerializeField]
    private GameObject self;

    private string playerAccountAddress;

    public class WeaponResponse
    {
        public string weaponType;
        public string damage;
    }

    public static string gunA_damage = "";
    public static string gunB_damage = "";

    private List<string> gunAOptions = new List<string>();
    private List<string> gunBOptions = new List<string>();

    //private NFTQuerryFunctions nftQuerryObject;

    // Start is called before the first frame update
    async void Start()
    {
        WeaponResponse weaponResponse = new WeaponResponse();

        playerAccountAddress = GetAccount();
        //playerAccountAddress = "0x6ef91a90C46201da680430B49583Bf3b47FfAc26";
        //playerAccountAddress = "0xd7a06850a729AB6bBA042562C0cB8a0EE39Ea464";
        //playerAccountAddress = "0x4B94dB342358526e8c15174AE9f3e39f64F1B91a";

        textMesh.text = playerAccountAddress;

        GunA_Dropdown.ClearOptions();
        GunB_Dropdown.ClearOptions();

        gunA_damage = "";
        gunB_damage = "";

        //nftQuerryObject = new NFTQuerryFunctions();
        //await PopulateDropDown();

        // Find the number of tokens owned by the player

        int balance = await ERC721.BalanceOf(BlockchainConstants.CHAIN,
            BlockchainConstants.NETWORK_NAME,
            BlockchainConstants.ERC721_CONTRACT_ADDRESS,
            playerAccountAddress
            );

        if (balance == 0)
        {
            Debug.Log("Shit is printed");
            noNFTWarning.SetActive(true);
            self.SetActive(false);
            return;
        }

        for (int i=0; i<balance; i++)
        {
            // Find token id owned by the player

            string[] obj = { playerAccountAddress, i.ToString() };
            string args = JsonConvert.SerializeObject(obj);

            var res = await EVM.Call(
                BlockchainConstants.CHAIN,
                BlockchainConstants.NETWORK_NAME,
                BlockchainConstants.ERC721_CONTRACT_ADDRESS,
                ContractConstants.ABI,
                "tokenOfOwnerByIndex",
                args
                );

            Debug.Log(res);

            // Find type and damage of each weapon
            // Add weapon to respective list

            string[] obj2 = { res.ToString() };
            args = JsonConvert.SerializeObject(obj2);

            res = await EVM.Call(
                BlockchainConstants.CHAIN,
                BlockchainConstants.NETWORK_NAME,
                BlockchainConstants.ERC721_CONTRACT_ADDRESS,
                ContractConstants.ABI,
                "tokenIdToWeapon",
                args
                );

            weaponResponse = JsonUtility.FromJson<WeaponResponse>(res.ToString());

            //Debug.Log("Weapons => " + res);
            //Debug.Log(res.GetType());

            string option = "Damage = " + weaponResponse.damage;
            //Debug.Log(option);

            if(weaponResponse.weaponType == "0")
            {
                Debug.Log("Gun A => " + weaponResponse.weaponType);
                gunAOptions.Add(option);
            }
            else if(weaponResponse.weaponType == "1")
            {
                Debug.Log("Gun B => " + weaponResponse.weaponType);
                gunBOptions.Add(option);
            }
            else
            {
                Debug.Log("Something else is happening");
            }
        }

        // Set the options
        GunA_Dropdown.AddOptions(gunAOptions);
        GunB_Dropdown.AddOptions(gunBOptions);

    }

    // Update is called once per frame
    void Update()
    {
        // Get the value selected and store it
        if(gunAOptions.Count > 0)
            gunA_damage = gunAOptions[GunA_Dropdown.value].Substring(9);
        if (gunBOptions.Count > 0)
            gunB_damage = gunBOptions[GunB_Dropdown.value].Substring(9);
    }
}
