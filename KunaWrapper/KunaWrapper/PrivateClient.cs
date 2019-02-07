﻿using System.Threading.Tasks;
using System.Collections.Generic;
using KunaWrapper.DataLayer.ReciveData;
using KunaWrapper.DataLayer.RequestData;

using static KunaWrapper.DataLayer.ServiceTypes;

namespace KunaWrapper
{
    public sealed class PrivateClient : BaseClient
    {
        public PrivateClient(string apiKey, string apiSec) : base(apiKey, apiSec) { }

        public async Task<Holder> GetHolderInfoAsync() =>
                await GetJsonAsync<Holder>(new RequestHolderInfo(authData));

        /// <summary>
        /// Holders active orders
        /// </summary>
        /// <param name="pairId">ticker of trade pair</param>
        /// <param name="state">order state: "wait"(default), "done", "cancel"</param>
        /// <returns>list of orders</returns>
        public async Task<List<Order>> GetHolderOrdersAsync(string pairId, string state = _wait) =>
                await GetJsonAsync<List<Order>>(new RequestHolderOrders(authData, pairId, state));

        public async Task<List<Trade>> GetHolderTradesAsync(string pairId) =>
                await GetJsonAsync<List<Trade>>(new RequestHolderTrades(authData, pairId));

        public async Task<DepositAddresses> GetDepositAddressesAsync(string currenyId) =>
                await GetJsonAsync<DepositAddresses>(new RequestDepositAddresses(authData, currenyId));

        public async Task<DepositAddress> CreateAddressAsync(string currensyId) =>
                await GetJsonAsync<DepositAddress>(new RequestCreateDepositAddress(authData, currensyId));

        /// <summary>
        /// Return list of all deposits
        /// </summary>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="full">full or not full answer</param>
        /// <param name="currecyId">optionaly currency name</param>
        /// <param name="depositAddressesIds">coma separated deposit addresses Ids</param>
        /// <param name="payInIds">coma separated pay in Ids</param>
        /// <returns>object (full or not full) AllDeposits</returns>
        public async Task<AllDeposits> GetAllDepositsAsync(ushort page, ushort perPage, bool full = false, string currecyId = null, string depositAddressesIds = null, string payInIds = null) =>
                await GetJsonAsync<AllDeposits>(new RequestAllDeposits(authData, page, perPage, full, currecyId, depositAddressesIds, payInIds));


        /// <summary>
        /// Place Order
        /// </summary>
        /// <param name="ordeType"> _buy or _sell</param>
        /// <param name="pairId">Currecy pair identifikator</param>
        /// <param name="volume">Base Volume</param>
        /// <param name="price">Base Price</param>
        /// <returns>Order</returns>
        public async Task<Order> PlaceOrderAsync(string ordeType, string pairId, decimal volume, decimal price) =>
                await PostJsonAsync<Order>(new RequestPlaceOrder(authData, ordeType, pairId, volume, price));

        public async Task<Order> CancelOrderAsync(uint orderId) =>
                await PostJsonAsync<Order>(new RequestCancelOrder(authData, orderId));
    }
}
