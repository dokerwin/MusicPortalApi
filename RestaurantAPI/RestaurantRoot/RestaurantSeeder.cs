using Microsoft.EntityFrameworkCore;
using RestaurantAPI.RestaurantRoot.Entities;

namespace RestaurantAPI.RestaurantRoot
{
    public class RestaurantSeeder
    {
        private RestaurantDbContextcs _dbContext;
        public RestaurantSeeder(RestaurantDbContextcs dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Restaurants.Any())
                {
                    _dbContext.Restaurants.AddRange(GetStaticRestaurants());
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Roles.Any())
                {
                    _dbContext.Roles.AddRange(GetRoles());
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            return new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };
        }

        private IEnumerable<Restaurant> GetStaticRestaurants()
        {
            return new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "Mc Donalds",
                    Description = "FastFood",
                    Category = "FastFood",
                    HasDelivery = true,
                    ContactEmail = "mcdonalds@gmail.com",
                    ContactPhone = "+4853323522",
                    Address = new Address
                    {
                        City = "Warsaw",
                        Street = "Ciolka 22",
                        PostalCode = "05585"
                    },
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Burger",
                            Description = "Standart burger",
                            Price = 22
                        },
                           new Dish()
                        {
                            Name = "Cease burger",
                            Description = "Burger with cheese",
                            Price = 22
                        }
                    }

                }

            };
        }
    }
}
