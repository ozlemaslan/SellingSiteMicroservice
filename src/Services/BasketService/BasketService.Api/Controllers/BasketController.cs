using BasketService.Api.Core.Application.Repository;
using BasketService.Api.Core.Application.Services;
using BasketService.Api.Core.Domain;
using BasketService.Api.IntegrationEvents.Events;
using EventBus.Base.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BasketService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IEventBus _eventBus;
        private readonly IIdentityService _identityService;

        public BasketController(IBasketRepository basketRepository, IEventBus eventBus, IIdentityService identityService)
        {
            this._basketRepository = basketRepository;
            this._eventBus = eventBus;
            this._identityService = identityService;
        }

        [HttpGet("{customerId}")]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerBasket>> GetBasketAsync(string customerId)
        {
            var basket = await _basketRepository.GetBasketAsync(customerId);
            return Ok(basket);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteBasketAsync(string id)
        {
            return Ok(await _basketRepository.DeleteBasketAsync(id));
        }

        [HttpPost]
        [Route("update")]
        public async Task<ActionResult<CustomerBasket>> UpdateBasketAsync(CustomerBasket cb)
        {
            return Ok(await _basketRepository.UpdateBasketAsync(cb));
        }

        [HttpPost]
        [Route("addBasket")]
        public async Task<ActionResult<CustomerBasket>> AddBasketItemAsync(BasketItem basketIem)
        {
            var userId = _identityService.GetUserName();

            if (userId == null)
            {
                return null;
            }

            CustomerBasket cusBasket = await _basketRepository.GetBasketAsync(userId);

            if (cusBasket == null)
            {
                cusBasket = new CustomerBasket(userId);
            }

            cusBasket.Items.Add(basketIem);

            await _basketRepository.UpdateBasketAsync(cusBasket);

            return Ok();

        }

        [HttpPost]
        [Route("checkout")]
        public async Task<ActionResult<CustomerBasket>> BasketCheckoutAsync(BasketCheckout basketCheckout)
        {
            var userId = basketCheckout.Buyer;

            if (userId==null)
            {
                return null;
            }

            var cusBasketValue = await _basketRepository.GetBasketAsync(userId);

            CustomerBasket customerBasket;

            if (cusBasketValue == null)
            {
                customerBasket = new CustomerBasket();
                return customerBasket;
            }

            OrderCreatedIntegrationEvent orderCreatedEvent = new OrderCreatedIntegrationEvent(basketCheckout.City,basketCheckout.Street,basketCheckout.State,basketCheckout.Country,basketCheckout.ZipCode,basketCheckout.CardNumber,basketCheckout.CardHolderName,basketCheckout.CardExpiration,basketCheckout.CardSecurityNumber,basketCheckout.CardTypeId, userId,cusBasketValue);

            try
            {
                _eventBus.Publish(orderCreatedEvent);
            }
            catch (Exception)
            {
                throw new Exception("event publish edilirken hata alındı.");
            }
            return Ok();
        }
    }
}
