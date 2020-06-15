using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName = "admin",
                        UserName = "admin",
                        Email = "admin@test.com"
                    },
                    new AppUser
                    {
                        DisplayName = "Bob",
                        UserName = "bob",
                        Email = "bob@test.com"
                    },
                    new AppUser
                    {
                        DisplayName = "Tom",
                        UserName = "tom",
                        Email = "tom@test.com"
                    },
                    new AppUser
                    {
                        DisplayName = "Jane",
                        UserName = "jane",
                        Email = "jane@test.com"
                    }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }

            if (!context.Categories.Any())
            {
                var cats = new List<Category>
                 {
                    //new Category {Id = Guid.NewGuid(), Title = "Beauty" },
                    //new Category {Id = Guid.NewGuid(), Title = "Electronics"},
                    //new Category {Id = Guid.NewGuid(), Title = "Cloth" },
                    //new Category {Id = Guid.NewGuid(), Title = "Kitchen"}
                 };

                context.Categories.AddRange(cats);
                context.SaveChanges();
            }

            if (!context.AppConfig.Any())
            {
                var appCon = new List<AppConfig>
                 {
                    //new AppConfig {Id = Guid.NewGuid(), Category = "Color", Title="WHITE", Details1="#FFFFFF", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Color", Title="WHITE", Details1="#FFFFFF", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Color", Title="SILVER", Details1="#C0C0C0", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Color", Title="GRAY", Details1="#808080", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Color", Title="BLACK", Details1="#000000", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Color", Title="RED", Details1="#FF0000", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Color", Title="MAROON", Details1="#800000", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Color", Title="YELLOW", Details1="#FFFF00", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Color", Title="OLIVE", Details1="#808000", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Color", Title="LIME", Details1="#00FF00", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Color", Title="GREEN", Details1="#008000", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Color", Title="AQUA", Details1="#00FFFF", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Color", Title="TEAL", Details1="#008080", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Color", Title="BLUE", Details1="#0000FF", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Color", Title="NAVY", Details1="#000080", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Color", Title="FUCHSIA", Details1="#FF00FF", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Color", Title="PURPLE", Details1="#800080", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Size", Title="XS", Details1="", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Size", Title="S", Details1="", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Size", Title="M", Details1="", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Size", Title="L", Details1="", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Size", Title="XL", Details1="", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Size", Title="XXL", Details1="", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Age", Title="8-9 YRS", Details1="", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Age", Title="9-10 YRS", Details1="", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Age", Title="11-12 YRS", Details1="", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Age", Title="13-14 YRS", Details1="", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Age", Title="15-16 YRS", Details1="", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Footwear", Title="40", Details1="", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Footwear", Title="41", Details1="", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Footwear", Title="42", Details1="", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Footwear", Title="43", Details1="", Details2="", Details3="" },
                    new AppConfig {Id = Guid.NewGuid(), Category = "Footwear", Title="44", Details1="", Details2="", Details3="" }

                 };

                context.AppConfig.AddRange(appCon);
                context.SaveChanges();
            }

            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();
            var id4 = Guid.NewGuid();
            if (!context.Categories.Any())
            {
                var values = new List<Category>
                {
                    new Category {Id =id1, ParentId = id1, Title="Clothing" ,icon="" , SizeType="Size"},
                    new Category {Id =id2, ParentId = id2, Title="Home & Furniture" ,icon="" , SizeType="DefaultSize"},
                    new Category {Id =id3, ParentId = id3, Title="Footwear" ,icon="" , SizeType="Footwear"},
                    new Category {Id =id4, ParentId = id4, Title="Baby & Kids" ,icon="" , SizeType="Age"},

                    new Category {Id = Guid.NewGuid(), ParentId = id1, Title="Western Wear" ,icon="" , SizeType="Size"},
                    new Category {Id = Guid.NewGuid(), ParentId = id1, Title="Tops, T-Shirts & Shirts" ,icon="" , SizeType="Size"},
                    new Category {Id = Guid.NewGuid(), ParentId = id1, Title="Jeans" ,icon="" , SizeType="Size"},
                    new Category {Id = Guid.NewGuid(), ParentId = id1, Title="Shorts" ,icon="" , SizeType="Size"},
                    new Category {Id = Guid.NewGuid(), ParentId = id1, Title="Skirts" ,icon="" , SizeType="Size"},
                    new Category {Id = Guid.NewGuid(), ParentId = id1, Title="Leggings & Jeggings" ,icon="" , SizeType="Size"},
                    new Category {Id = Guid.NewGuid(), ParentId = id1, Title="Trousers & Capris" ,icon="" , SizeType="Size"},
                    new Category {Id = Guid.NewGuid(), ParentId = id1, Title="Lingerie & Sleepwear" ,icon="" , SizeType="Size"},
                    new Category {Id = Guid.NewGuid(), ParentId = id1, Title="Shapewear" ,icon="" , SizeType="Size"},
                    new Category {Id = Guid.NewGuid(), ParentId = id1, Title="Camisoles & Slips" ,icon="" , SizeType="Size"},
                    new Category {Id = Guid.NewGuid(), ParentId = id2, Title="Kitchen, Cookware & Serveware" ,icon="" , SizeType="DefaultSize"},
                    new Category {Id = Guid.NewGuid(), ParentId = id2, Title="Pans" ,icon="" , SizeType="DefaultSize"},
                    new Category {Id = Guid.NewGuid(), ParentId = id2, Title="Tawas" ,icon="" , SizeType="DefaultSize"},
                    new Category {Id = Guid.NewGuid(), ParentId = id2, Title="Pressure Cookers" ,icon="" , SizeType="DefaultSize"},
                    new Category {Id = Guid.NewGuid(), ParentId = id2, Title="Kitchen tools" ,icon="" , SizeType="DefaultSize"},
                    new Category {Id = Guid.NewGuid(), ParentId = id2, Title="Gas Stoves" ,icon="" , SizeType="DefaultSize"},
                    new Category {Id = Guid.NewGuid(), ParentId = id3, Title="Sports Shoes" ,icon="" , SizeType="Footwear"},
                    new Category {Id = Guid.NewGuid(), ParentId = id3, Title="Casual Shoes" ,icon="" , SizeType="Footwear"},
                    new Category {Id = Guid.NewGuid(), ParentId = id3, Title="Formal Shoes" ,icon="" , SizeType="Footwear"},
                    new Category {Id = Guid.NewGuid(), ParentId = id3, Title="Sandals & Floaters" ,icon="" , SizeType="Footwear"},
                    new Category {Id = Guid.NewGuid(), ParentId = id3, Title="Flip- Flops" ,icon="" , SizeType="Footwear"},
                    new Category {Id = Guid.NewGuid(), ParentId = id3, Title="Loafers" ,icon="" , SizeType="Footwear"},
                    new Category {Id = Guid.NewGuid(), ParentId = id3, Title="Boots" ,icon="" , SizeType="Footwear"},
                    new Category {Id = Guid.NewGuid(), ParentId = id4, Title="Kids Clothing" ,icon="" , SizeType="Age"},
                    new Category {Id = Guid.NewGuid(), ParentId = id4, Title="Boys' Clothing" ,icon="" , SizeType="Age"},
                    new Category {Id = Guid.NewGuid(), ParentId = id4, Title="Polos & T-Shirts" ,icon="" , SizeType="Age"},
                    new Category {Id = Guid.NewGuid(), ParentId = id4, Title="Ethnic Wear" ,icon="" , SizeType="Age"},
                    new Category {Id = Guid.NewGuid(), ParentId = id4, Title="Shorts & 3/4ths" ,icon="" , SizeType="Age"},
                    new Category {Id = Guid.NewGuid(), ParentId = id4, Title="Girls' Clothing" ,icon="" , SizeType="Age"},
                    new Category {Id = Guid.NewGuid(), ParentId = id4, Title="Dresses & Skirts" ,icon="" , SizeType="Age"},
                    new Category {Id = Guid.NewGuid(), ParentId = id4, Title="Ethnic Wear" ,icon="" , SizeType="Age"}

                };

                context.Categories.AddRange(values);
                context.SaveChanges();
            }

            // if (!context.Values.Any())
            // {
            //     var values = new List<Value>
            //      {
            //         new Value {Id = 1, Name = "Value 101"},
            //         new Value {Id = 2, Name = "Value 102"},
            //         new Value {Id = 3, Name = "Value 103"}
            //      };

            //     context.Values.AddRange(values);
            //     context.SaveChanges();
            // }



        }

        public static async Task CreateRoles(IServiceProvider serviceProvider, string RoleName, string defaultUserEmail = "" )
        {            
            var UserManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            IdentityResult roleResult;
           
            var roleCheck = await RoleManager.RoleExistsAsync(RoleName);
            if (!roleCheck)
            {
                //here in this line we are creating admin role and seed it to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole(RoleName));

                if (!string.IsNullOrEmpty(defaultUserEmail))
                {
                    AppUser user = await UserManager.FindByEmailAsync(defaultUserEmail);
                    var User = new IdentityUser();
                    await UserManager.AddToRoleAsync(user, RoleName);
                }
            }

           
            
        }

      
    }
}