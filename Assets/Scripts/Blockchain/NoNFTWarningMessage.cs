using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoNFTWarningMessage : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI contractAddress;

    // Start is called before the first frame update
    void Start()
    {

        contractAddress.text = "Contract Address = " + BlockchainConstants.ERC721_CONTRACT_ADDRESS;
        
    }
}
