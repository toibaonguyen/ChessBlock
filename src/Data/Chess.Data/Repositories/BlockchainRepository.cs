using Chess.Data.Common.Models;
using Chess.Data.Common.Repositories;
using Chess.Data.Models;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Chess.Data.Repositories
{
    public class BlockchainRepository<TEntity> : IBlockchainRepository<TEntity> where TEntity : class, IBlockEntity
    {
        private readonly Web3 _web3;
        private readonly string _abi;
        //replace by info in secret.txt
        private readonly string _contractAddress = "0x6a7aA9b882d50Bb7bc5Da1a244719C99f12F06a3";
        private readonly string _privateKey = "1a0ef7687d9f1d83144d35f1a9c20289478c920d74470d22afb4cb9c851a982c";
        private readonly string _url = "https://rpc.sepolia.org";
        public BlockchainRepository()
        {
            this._abi = File.ReadAllText("../../ABI.json");
            this._web3 = new Web3(this._url);
        }

        public async Task AddAsync(TEntity entity)
        {
            try
            {
                string entityName = entity.GetType().Name;
                Console.WriteLine($"Entity moi duoc tao la: {entityName}");
                string functionNameToCall = "create" + entityName.Substring(0, entityName.IndexOf("Entity"));
                var account = new Nethereum.Web3.Accounts.Account(this._privateKey);
                var web3 = new Web3(account, this._url);
                var contract = web3.Eth.GetContract(this._abi, this._contractAddress);
                var function = contract.GetFunction(functionNameToCall);
                TransactionInput input = null;
                switch (entityName)
                {
                    case "GameEntity":
                        GameEntity game = (GameEntity)(IBlockEntity)entity;
                        input = function.CreateTransactionInput(account.Address, game.Id, game.PlayerOneName, game.PlayerOneUserId, game.PlayerTwoName, game.PlayerTwoUserId);
                        input.Gas = await web3.Eth.Transactions.EstimateGas.SendRequestAsync(input);
                        var hashed= await function.SendTransactionAsync(input);
                        Console.WriteLine($"transaction hash:: {hashed}");
                        break;
                    case "MoveEntity":
                        MoveEntity move = (MoveEntity)(IBlockEntity)entity;
                        input = function.CreateTransactionInput(account.Address, move.Id, move.UserId, move.GameId, move.Notation);
                        input.Gas = await web3.Eth.Transactions.EstimateGas.SendRequestAsync(input);
                        hashed = await function.SendTransactionAsync(input);
                        Console.WriteLine($"transaction hash:: {hashed}");
                        break;
                    case "StatisticEntity":
                        StatisticEntity statistic = (StatisticEntity)(IBlockEntity)entity;
                        input = function.CreateTransactionInput(account.Address, statistic.Id, statistic.Played, statistic.Won, statistic.Drawn, statistic.Lost, statistic.EloRating, statistic.UserId);
                        input.Gas = await web3.Eth.Transactions.EstimateGas.SendRequestAsync(input);
                        hashed = await function.SendTransactionAsync(input);
                        Console.WriteLine($"transaction hash:: {hashed}");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                throw;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            try
            {
                string entityName = nameof(TEntity);
                Console.WriteLine($"Entity duoc update la: {entityName}");
                string functionNameToCall = "update" + entityName.Substring(0, entityName.IndexOf("Entity"));
                var account = new Nethereum.Web3.Accounts.Account(this._privateKey);
                var web3 = new Web3(account);
                var contract = web3.Eth.GetContract(this._abi, this._contractAddress);
                var function = contract.GetFunction(functionNameToCall);
                TransactionInput input = null;
                switch (entityName)
                {
                    case "GameEntity":
                        GameEntity game = (GameEntity)(IBlockEntity)entity;
                        input = function.CreateTransactionInput(account.Address, game.Id, game.PlayerOneName, game.PlayerOneUserId, game.PlayerTwoName, game.PlayerTwoUserId);
                        input.Gas = await web3.Eth.Transactions.EstimateGas.SendRequestAsync(input);
                        var hashed = await function.SendTransactionAsync(input);
                        Console.WriteLine($"transaction hash:: {hashed}");
                        break;
                    case "MoveEntity":
                        MoveEntity move = (MoveEntity)(IBlockEntity)entity;
                        input = function.CreateTransactionInput(account.Address, move.Id, move.UserId, move.GameId, move.Notation);
                        input.Gas = await web3.Eth.Transactions.EstimateGas.SendRequestAsync(input);
                        hashed = await function.SendTransactionAsync(input);
                        Console.WriteLine($"transaction hash:: {hashed}");
                        break;
                    case "StatisticEntity":
                        StatisticEntity statistic = (StatisticEntity)(IBlockEntity)entity;
                        input = function.CreateTransactionInput(account.Address, statistic.Id, statistic.Played, statistic.Won, statistic.Drawn, statistic.Lost, statistic.EloRating, statistic.UserId);
                        input.Gas = await web3.Eth.Transactions.EstimateGas.SendRequestAsync(input);
                        hashed = await function.SendTransactionAsync(input);
                        Console.WriteLine($"transaction hash:: {hashed}");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
