using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;


//public class Program
//{
//    // This example demonstrates how to get the balance for an ERC721 (non-fungible) token,
//    // and also how to find out who the owner is for a given token.
//    // The ERC721 standard is here: https://eips.ethereum.org/EIPS/eip-721
//    // The ERC721 token we use in the example is for the "Gods Unchained" game. It is
//    // deployed on mainnet here:
//    // https://etherscan.io/address/0x6EbeAf8e8E946F0716E6533A6f2cefc83f60e8Ab

//    // The balance function message definition    
//    [Function("balanceOf", "uint256")]
//    public class BalanceOfFunction : FunctionMessage
//    {
//        [Parameter("address", "owner", 1)]
//        public string Owner { get; set; }
//    }

//    // The owner function message definition
//    [Function("ownerOf", "address")]
//    public class OwnerOfFunction : FunctionMessage
//    {
//        [Parameter("uint256", "tokenId", 1)]
//        public BigInteger TokenId { get; set; }
//    }

//    //async Task Main to enable async methods
//    private static async Task Main(string[] args)
//    {
//        // Connect to Ethereum mainnet using Infura
//        var web3 = new Web3("https://rinkeby.infura.io/v3/c5a798dfd7184a27990ed8744003cc61");

//        // The ERC721 contract we will be querying
//        var erc721TokenContractAddress = "0x30e57a3b36B8290E5514A043f67dcAF4e9EF4971";

//        // Example 1. Get balance of an account. This is the count of tokens that an account 
//        // has from a specified contract (in this case "Gods Unchained").  
//        var accountWithSomeTokens = "0x6ef91a90C46201da680430B49583Bf3b47FfAc26";
//        // You can check the token balance of the above account on etherscan:
//        // https://etherscan.io/token/0x6ebeaf8e8e946f0716e6533a6f2cefc83f60e8ab?a=0x5a4d185c590c5815a070ed62c278e665d137a0d9#inventory

//        // Send query to contract, to find out how many tokens the owner has
//        var balanceOfMessage = new BalanceOfFunction() { Owner = accountWithSomeTokens };
//        var queryHandler = web3.Eth.GetContractQueryHandler<BalanceOfFunction>();
//        var balance = await queryHandler
//            .QueryAsync<BigInteger>(erc721TokenContractAddress, balanceOfMessage)
//            .ConfigureAwait(false);
//        Console.WriteLine($"Address: {accountWithSomeTokens} holds: {balance}");


//        // Example 2. Find a token's owner.             
//        var sampleTokenId = 0;
//        // You can check the token owner of the above tokenId on etherscan:
//        // https://etherscan.io/token/0x6ebeaf8e8e946f0716e6533a6f2cefc83f60e8ab?a=441588#inventory

//        var ownerOfMessage = new OwnerOfFunction() { TokenId = sampleTokenId };
//        var queryHandler2 = web3.Eth.GetContractQueryHandler<OwnerOfFunction>();
//        var tokenOwner = await queryHandler2
//            .QueryAsync<string>(erc721TokenContractAddress, ownerOfMessage)
//            .ConfigureAwait(false);
//        Console.WriteLine($"Address: {tokenOwner} owns token id: {sampleTokenId}");

//        // In a real project, you could use the NuGet Nethereum.StandardNonFungibleTokenERC721
//        // which simplifies things by defining all the function messages and wrapping it all 
//        // up into a class. For example, the balance check shown above would become:
//        //   var tokenService = new ERC721Service(web3, erc721TokenContractAddress);
//        //   var balance = await tokenService.BalanceOfQueryAsync(accountWithSomeTokens);
//    }
//}

public class QueryWeaponsNFT : MonoBehaviour
{

    [Function("balanceOf", "uint256")]
    public class BalanceOfFunction : FunctionMessage
    {
        [Parameter("address", "owner", 1)]
        public string Owner { get; set; }
    }

    // The owner function message definition
    [Function("ownerOf", "address")]
    public class OwnerOfFunction : FunctionMessage
    {
        [Parameter("uint256", "tokenId", 1)]
        public BigInteger TokenId { get; set; }
    }

    [Function("tokenIdToWeapon","uint256")]
    public class TokenIdToWeaponFunction : FunctionMessage
    {
        [Parameter("uint256", "tokenId", 1)]
        public BigInteger TokenId { get; set; }
    }

    [Function("tokenOfOwnerByIndex", "uint256")]
    public class TokenOfOwnerByIndexFunction : FunctionMessage
    {
        [Parameter("address", "owner", 1)]
        public string Owner { get; set; }
        [Parameter("uint256", "index")]
        public BigInteger Index { get; set; }
    }

    [FunctionOutput]
    public class GetTokenToWeaponOutputDTO: IFunctionOutputDTO
    {
        [Parameter("uint8", "weaponType",1)]
        public BigInteger WeaponType { get; set; }
        [Parameter("uint256", "damage", 2)]
        public BigInteger Damage { get; set; }
    }

    // Start is called before the first frame update
    async void Start()
    {
        //await PrintNFTDetails();
        //await FindTokenOwner();
        //await FindWeapon();
        string ret = await GetListOfWeapons();
        Debug.Log(ret);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async Task<string> PrintNFTDetails()
    {
        var web3 = new Web3("https://rinkeby.infura.io/v3/c5a798dfd7184a27990ed8744003cc61");
        var erc721TokenContractAddress = "0x30e57a3b36B8290E5514A043f67dcAF4e9EF4971";
        var accountWithSomeTokens = "0x6ef91a90C46201da680430B49583Bf3b47FfAc26";

        var balanceOfMessage = new BalanceOfFunction() { Owner = accountWithSomeTokens };
        var queryHandler = web3.Eth.GetContractQueryHandler<BalanceOfFunction>();
        var balance = await queryHandler
            .QueryAsync<BigInteger>(erc721TokenContractAddress, balanceOfMessage)
            .ConfigureAwait(false);

        Debug.Log($"Address: {accountWithSomeTokens} holds: {balance}");

        return null;

    }

    async Task FindTokenOwner()
    {
        var web3 = new Web3("https://rinkeby.infura.io/v3/c5a798dfd7184a27990ed8744003cc61");
        var erc721TokenContractAddress = "0x30e57a3b36B8290E5514A043f67dcAF4e9EF4971";

        var sampleTokenId = 0;
        var ownerOfMessage = new OwnerOfFunction() { TokenId = sampleTokenId };
        var queryHandler2 = web3.Eth.GetContractQueryHandler<OwnerOfFunction>();
        var tokenOwner = await queryHandler2
            .QueryAsync<string>(erc721TokenContractAddress, ownerOfMessage)
            .ConfigureAwait(false);

        Debug.Log($"Address: {tokenOwner} owns token id: {sampleTokenId}");

    }

    async Task FindWeapon()
    {
        var web3 = new Web3("https://rinkeby.infura.io/v3/c5a798dfd7184a27990ed8744003cc61");
        var erc721TokenContractAddress = "0x30e57a3b36B8290E5514A043f67dcAF4e9EF4971";

        var sampleTokenId = 0;
        var toWeaponMessage = new TokenIdToWeaponFunction() { TokenId = sampleTokenId };
        var queryHandler = web3.Eth.GetContractQueryHandler<TokenIdToWeaponFunction>();
        var response = await queryHandler
            .QueryDeserializingToObjectAsync<GetTokenToWeaponOutputDTO>(toWeaponMessage, erc721TokenContractAddress)
            .ConfigureAwait(false);

        Debug.Log(response.Damage);
    }

    async Task<string> GetListOfWeapons()
    {
        //Debug.Log("Getting Weapons");

        var web3 = new Web3("https://rinkeby.infura.io/v3/c5a798dfd7184a27990ed8744003cc61");
        var erc721TokenContractAddress = "0x30e57a3b36B8290E5514A043f67dcAF4e9EF4971";
        var accountWithSomeTokens = "0x6ef91a90C46201da680430B49583Bf3b47FfAc26";

        var balanceOfMessage = new BalanceOfFunction() { Owner = accountWithSomeTokens };
        
        var queryHandler = web3.Eth.GetContractQueryHandler<BalanceOfFunction>();
        var queryHandler2 = web3.Eth.GetContractQueryHandler<TokenOfOwnerByIndexFunction>();
        var queryHanfler3 = web3.Eth.GetContractQueryHandler<TokenIdToWeaponFunction>();

        var balance = await queryHandler
            .QueryAsync<BigInteger>(erc721TokenContractAddress, balanceOfMessage)
            .ConfigureAwait(false);

        //Debug.Log("Total weapons you have => " + balance);

        List<GetTokenToWeaponOutputDTO> weaponsDamages = new List<GetTokenToWeaponOutputDTO>();

        for (int i=0; i<balance; i++)
        {
            Debug.Log("Loop running with value" + i);

            var tokenOfOwnerByIndexMessage = new TokenOfOwnerByIndexFunction() { Owner = accountWithSomeTokens, Index = i };
            var tokenId = await queryHandler2
                .QueryAsync<BigInteger>(erc721TokenContractAddress, tokenOfOwnerByIndexMessage)
                .ConfigureAwait(false);

            var toWeaponMessage = new TokenIdToWeaponFunction() { TokenId = tokenId };
            var weapon = await queryHanfler3
                .QueryDeserializingToObjectAsync<GetTokenToWeaponOutputDTO>(toWeaponMessage, erc721TokenContractAddress)
                .ConfigureAwait(false);

            weaponsDamages.Add(weapon);
        }

        //Debug.Log("Total elements in the list => " + weaponsDamages.Count);

        weaponsDamages.ForEach(weapon => Debug.Log(" " + weapon.WeaponType + " has damage => " + weapon.Damage));

        return "Apple";
    }

}
