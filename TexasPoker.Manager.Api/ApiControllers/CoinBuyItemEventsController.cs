﻿using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using GameCloud.Database.Adapters;
using GameCloud.UCenter.Common.Settings;
using GameCloud.UCenter.Web.Common.Modes;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TexasPoker.Database;
using TexasPoker.Database.Entities;

namespace Manager.TexasPoker.ApiControllers
{
    /// <summary>
    /// Provide a controller for events.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CoinBuyItemEventsController : ApiControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventsController" /> class.
        /// </summary>
        /// <param name="database">Indicating the database context.</param>
        /// <param name="settings">Indicating the settings.</param>
        [ImportingConstructor]
        public CoinBuyItemEventsController(TexasPokerDatabaseContext database, Settings settings)
            : base(database, settings)
        {
        }

        /// <summary>
        /// Get event list.
        /// </summary>
        /// <param name="token">Indicating the cancellation token.</param>
        /// <param name="keyword">Indicating the keyword.</param>
        /// <param name="orderby">Indicating the order by name.</param>
        /// <param name="page">Indicating the page number.</param>
        /// <param name="count">Indicating the count.</param>
        /// <returns>Async return event list.</returns>
        [Route("api/events/CoinBuyItem")]
        public async Task<PaginationResponse<CoinBuyItemEventEntity>> Get(
            CancellationToken token,
            string keyword = null,
            string orderby = null,
            int page = 1,
            int count = 1000)
        {
            Expression<Func<CoinBuyItemEventEntity, bool>> filter = null;

            if (!string.IsNullOrEmpty(keyword))
            {
                filter = e => e.BuyPlayerEtGuid == keyword;
            }

            var total = await this.Database.CoinBuyItemEvents.CountAsync(filter, token);

            IQueryable<CoinBuyItemEventEntity> queryable = this.Database.CoinBuyItemEvents.Collection.AsQueryable();
            if (filter != null)
            {
                queryable = queryable.Where(filter);
            }

            var result = queryable.Skip((page - 1) * count).Take(count).ToList();

            // todo: add orderby support.
            var model = new PaginationResponse<CoinBuyItemEventEntity>
            {
                Page = page,
                PageSize = count,
                Raws = result,
                Total = total
            };

            return model;
        }

        /// <summary>
        /// Get single user details.
        /// </summary>
        /// <param name="id">Indicating the user id.</param>
        /// <param name="token">Indicating the cancellation token.</param>
        /// <returns>Async return user details.</returns>
        public async Task<CoinBuyItemEventEntity> Get(string id, CancellationToken token)
        {
            var result = await this.Database.CoinBuyItemEvents.GetSingleAsync(id, token);

            return result;
        }
    }
}
