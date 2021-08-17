using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using System.Threading.Tasks;
using Nethereum.Web3;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

public class NFTQuerryFunctions
{

    private Web3 web3 = new Web3(BlockchainConstants.RINKEBY_INFURA_GATEWAY);

    // ------------------ Function Messages used to send Querry ------------------

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

    [Function("tokenIdToWeapon", "uint256")]
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

    // ------------------ Function Output for Non-Premitive return types ------------------

    [FunctionOutput]
    public class GetTokenToWeaponOutputDTO : IFunctionOutputDTO
    {
        [Parameter("uint8", "weaponType", 1)]
        public BigInteger WeaponType { get; set; }
        [Parameter("uint256", "damage", 2)]
        public BigInteger Damage { get; set; }
    }


    // ------------------ Public Callable Functions ------------------

    public async Task<BigInteger> GetAccountBalance(string accountAddress)
    {
        var balanceOfMessage = new BalanceOfFunction() { Owner = accountAddress };
        var queryHandler = web3.Eth.GetContractQueryHandler<BalanceOfFunction>();
        var balance = await queryHandler
            .QueryAsync<BigInteger>(BlockchainConstants.ERC721_CONTRACT_ADDRESS, balanceOfMessage)
            .ConfigureAwait(false);

        return balance;
    }

    public async Task<string> FindTokenOwner(int tokenId)
    {
        var ownerOfMessage = new OwnerOfFunction() { TokenId = tokenId };
        var queryHandler = web3.Eth.GetContractQueryHandler<OwnerOfFunction>();
        var tokenOwner = await queryHandler
            .QueryAsync<string>(BlockchainConstants.ERC721_CONTRACT_ADDRESS, ownerOfMessage)
            .ConfigureAwait(false);

        return tokenOwner;
    }

    public async Task<GetTokenToWeaponOutputDTO> FindWeaponFromTokenId(BigInteger tokenId)
    {
        var toWeaponMessage = new TokenIdToWeaponFunction() { TokenId = tokenId };
        var queryHandler = web3.Eth.GetContractQueryHandler<TokenIdToWeaponFunction>();
        var weapon = await queryHandler
            .QueryDeserializingToObjectAsync<GetTokenToWeaponOutputDTO>(toWeaponMessage, BlockchainConstants.ERC721_CONTRACT_ADDRESS)
            .ConfigureAwait(false);

        return weapon;
    }

    public async Task<List<GetTokenToWeaponOutputDTO>> GetListOfWeapon(string accountAddress)
    {
        List<GetTokenToWeaponOutputDTO> weapons = new List<GetTokenToWeaponOutputDTO>();
        var queryHandler = web3.Eth.GetContractQueryHandler<TokenOfOwnerByIndexFunction>();

        var balance = await GetAccountBalance(accountAddress);

        for(int i=0; i<balance; i++)
        {
            var tokenOfOwnerByIndexMessage = new TokenOfOwnerByIndexFunction() { Owner = accountAddress, Index = i };
            var tokenId = await queryHandler
                .QueryAsync<BigInteger>(BlockchainConstants.ERC721_CONTRACT_ADDRESS, tokenOfOwnerByIndexMessage)
                .ConfigureAwait(false);

            var weapon = await FindWeaponFromTokenId(tokenId);

            weapons.Add(weapon);
        }

        return weapons;
    }
}
