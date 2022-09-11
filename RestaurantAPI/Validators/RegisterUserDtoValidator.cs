using FluentValidation;
using RestaurantAPI.Models;

namespace RestaurantAPI.Validators
{
    public class RegisterUserDtoValidator :AbstractValidator<RegistereUserDto>
    {
        public RegisterUserDtoValidator(RestaurantDbContextcs restaurantDb)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password).MinimumLength(6);

            RuleFor(x => x.ConfirmPassword).Equal(e=>e.Password);

            RuleFor(x => x.Email).Custom((value, context) =>
              {
                 var emailInUse =  restaurantDb.Users.Any(u=>u.Email == value);
                  if (emailInUse)
                  {
                      context.AddFailure("Email", "That email is taken");
                  }
              });
        }
    }
}
