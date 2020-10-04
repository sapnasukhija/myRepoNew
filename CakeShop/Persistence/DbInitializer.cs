using CakeShop.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace CakeShop.Persistence
{
    public static class DbInitializer
    {
        public static void SeedDatabase(
            CakeShopDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            System.Console.WriteLine("Seeding... - Start");

            var menues = new List<Category>
            {
                new Category { Name = "Categories"},
                new Category { Name = "Occasions" },
                new Category { Name = "Price Range"}
            };

            var cakes = new List<Cake>
            {
                new Cake
                {
                    Name ="Strawberry Cake",
                    Price = 200.00M,
                    ShortDescription ="Yammy Sweet & Testy",
                    LongDescription ="Icing carrot cake jelly-o cheesecake. tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake.dragée gummies.",
                    Category = menues[0],
                    ImageUrl ="/img/vanilla-cake2.jpg",
                    IsCakeOfTheWeek = true,
                },
                new Cake
                {
                    Name ="Dark Chocolate Cake",
                    Price =245.50M,
                    ShortDescription ="Yammy! Dark Chocolate Flavour",
                    LongDescription ="Icing carrot cake jelly-o cheesecake. tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake.dragée gummies.",
                    Category = menues[0],
                    ImageUrl ="/img/chocolate-cake4.jpg",
                    IsCakeOfTheWeek = false,
                },
                new Cake
                {
                    Name ="Special Chocolate Cake",
                    Price = 240.50M,
                    ShortDescription ="Taste Our Special Chocolates",
                    LongDescription ="Icing carrot cake jelly-o cheesecake. tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake.dragée gummies.",
                    Category = menues[0],
                    ImageUrl ="/img/chocolate-cake3.jpg",
                    IsCakeOfTheWeek = false,
                },
                new Cake
                {
                    Name ="Red Velvet Cake",
                    Price = 242.50M,
                    ShortDescription ="Our Special Cake",
                    LongDescription ="Icing carrot cake jelly-o cheesecake. tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake.dragée gummies.",
                    Category = menues[0],
                    ImageUrl ="/img/vanilla-cake4.jpg",
                    IsCakeOfTheWeek = true,
                },
                new Cake
                {
                    Name ="Mixed Fruit Cake",
                    Price = 255.50M,
                    ShortDescription ="Fruity & Testy",
                    LongDescription ="Icing carrot cake jelly-o cheesecake. tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake.caramels.",
                    Category = menues[0],
                    ImageUrl ="/img/fruit-cake.jpg",
                    IsCakeOfTheWeek =true,
                },
                  new Cake
                {
                    Name ="Anniversary and Party",
                                        Price = 255.50M,
                    ShortDescription ="Gift your loved ones",
                    LongDescription ="Here we book cakes for the anniversary Events",
                    Category = menues[1],
                    ImageUrl ="/img/happy-anniversary-cake-578x563.png",
                },
                    new Cake
                {
                    Name ="Birthday Occasion",
                                                            Price = 275.50M,

                    ShortDescription ="The Customised Photo Cakes",
                    LongDescription ="You can order the photo cakes to show your love for looved ones",
                    Category = menues[1],
                    ImageUrl ="/img/HappyBday.jpg",
                    IsCakeOfTheWeek =true,
                },
                      new Cake
                {
                    Name ="Mixed Fruit Cake",
                    Price = 245 ,
                    ShortDescription ="Fruity & Tasty",
                    LongDescription ="Icing carrot cake jelly-o cheesecake. tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake.caramels.",
                    Category = menues[2],
                    ImageUrl ="/img/fruit-cake.jpg",
                    IsCakeOfTheWeek =true,
                },
                      new Cake
                {
                    Name ="The New Cake",
                    Price = 256 ,
                    ShortDescription ="Fruity & Testy",
                    LongDescription ="Icing carrot cake jelly-o cheesecake. tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake.caramels.",
                    Category = menues[2],
                    ImageUrl ="/img/fruit-cake.jpg",
                    IsCakeOfTheWeek =true,
                },
                      new Cake
                {
                    Name ="The newest Cake",
                    Price = 284 ,
                    ShortDescription ="Fruity & Testy",
                    LongDescription ="Icing carrot cake jelly-o cheesecake. tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake.caramels.",
                    Category = menues[2],
                    ImageUrl ="/img/fruit-cake.jpg",
                    IsCakeOfTheWeek =true,
                }

            };





            if (!context.Categories.Any() && !context.Cakes.Any())
            {
                context.Categories.AddRange(menues);
                context.Cakes.AddRange(cakes);
                context.SaveChanges();
            }

          
            IdentityUser usr = null;
            string userEmail = configuration["Admin:Email"] ?? "admin@admin.com";
            string userName = configuration["Admin:Username"] ?? "admin";
            string password = configuration["Admin:Password"] ?? "Passw@rd!123";

            if (!context.Users.Any())
            {
                usr = new IdentityUser
                {
                    Email = userEmail,
                    UserName = userName
                };
                userManager.CreateAsync(usr, password);
            }

            if (!context.UserRoles.Any())
            {
                roleManager.CreateAsync(new IdentityRole("Admin"));

            }
            if (usr == null)
            {
                usr = userManager.FindByEmailAsync(userEmail).Result;
            }
            if (!userManager.IsInRoleAsync(usr, "Admin").Result)
            {
                userManager.AddToRoleAsync(usr, "Admin");
            }

            context.SaveChanges();

            System.Console.WriteLine("Seeding... - End");
        }

    }
}