using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : ControllerBase
    {
        private IResturantService _resturantService;
        public RestaurantController(IResturantService resturantService)
        {
            _resturantService = resturantService;
        }

        [HttpGet("AllRestaurants")]
        public ActionResult<IEnumerable<RestaurantDto>> GetAllRestaurants()
        {
            ActionResult returnResult = NotFound();
            var restaurantsDots = _resturantService.GetAllRestaurants();
            if (restaurantsDots is not null)
            {
                returnResult = Ok(restaurantsDots);
            }
            return returnResult;
        }

        [HttpGet("RestaurantByName/{restaurantName}")]
        public ActionResult<IEnumerable<RestaurantDto>> GetRestaurantByName([FromRoute] string restaurantName)
        {

            var restaurant = _resturantService.GetByName(restaurantName);
            if (restaurant is not null)
            {
                return Ok(restaurant);
            }

            return NotFound("The database is empty");
        }

        [HttpGet("RestaurantByCategory/{categoryName}")]
        public ActionResult<IEnumerable<RestaurantDto>> GetRestaurantByCategory([FromRoute] string categoryName)
        {
            var restaurant = _resturantService.GetByCategory(categoryName);
            if (restaurant is not null)
            {
                return Ok(restaurant);
            }

            return NotFound("The database is empty");
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<RestaurantDto>> Get([FromRoute] int id)
        {
            var restaurant = _resturantService.GetById(id);
            return  Ok(restaurant);
         }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto restaurantDto)
        {
         
            int createdRestaurantID = _resturantService.CreateRestaurant(restaurantDto);

            return Created($"/api/restaurant/{createdRestaurantID}", null);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateRestaurant([FromRoute] int id, [FromBody] UpdateRestaurantDto restaurantDto)
        {
           
            _resturantService.UpdateRestaurant(id, restaurantDto);

            return Ok($"/api/restaurant/{id}");
        }


        [HttpDelete("{id}")]
        public ActionResult DeleteRestaurant([FromRoute] int id)
        {
            _resturantService.DeleteRestaurant(id);
            return NoContent();
        }
    }
}
