using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class TestEVM : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        string[] obj = { "0x6ef91a90C46201da680430B49583Bf3b47FfAc26", "2" };
        string args = JsonConvert.SerializeObject(obj);

        var res = await EVM.Call("ethereum","rinkeby",BlockchainConstants.ERC721_CONTRACT_ADDRESS, ContractConstants.ABI, "tokenOfOwnerByIndex",args);
        Debug.Log(res);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
