using BasketService.Api.Core.Application.Repository;
using BasketService.Api.Core.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            this._basketRepository = basketRepository;
        }

        public async Task<ActionResult<CustomerBasket>> GetBasketAsync(string customerId)
        {
            var basket = await _basketRepository.GetBasketAsync(customerId);
            return Ok(basket);
        }

        public async Task<ActionResult<bool>> DeleteBasketAsync(string id)
        {
            return Ok(await _basketRepository.DeleteBasketAsync(id));
        }


        public async Task<ActionResult<CustomerBasket>> UpdateBasketAsync(CustomerBasket cb)
        {
            return Ok(await _basketRepository.UpdateBasketAsync(cb));
        }

        public async Task<ActionResult<CustomerBasket>> AddBasketAsync(BasketItem basketIem)
        {
            var basketValue = await _basketRepository.GetBasketAsync(basketIem.Id);

            if (basketValue==null)
            {
                await _basketRepository.UpdateBasketAsync(basketValue);
            }

            return Ok(await _basketRepository.UpdateBasketAsync(basketValue));
        }
    }
}
