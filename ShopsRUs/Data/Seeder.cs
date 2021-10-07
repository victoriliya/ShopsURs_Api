using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopsRUs.Helpers;
using ShopsRUs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShopsRUs.Data
{
    public static class Seeder
    {
        private static string basePath = Environment.CurrentDirectory;
        private static string relativePath = "./Data/Data.Json/";

        private static string path = Path.GetFullPath(relativePath, basePath);

        public static async Task Seed(IApplicationBuilder app)
        {

            //Get the Db context
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (context.Users.Any())
            {
                return;
            }
            else
            {

                //Seed items
                var userTypeList = GetSampleData<UserType>(File.ReadAllText(path + "UserType.json"));
                await context.UserTypes.AddRangeAsync(userTypeList);

                //Seed Discounta
                var discountList = GetSampleData<Discount>(File.ReadAllText(path + "Discount.json"));
                await context.Discounts.AddRangeAsync(discountList);

                //Seed User
                var usersList = GetSampleData<User>(File.ReadAllText(path + "User.json"));
                foreach (var user in usersList)
                {
                    if (Utilities.IsRegularUser(user))
                    {
                        user.UserTypeId = "a063a2ec-3d0e-4f75-b724-2c471ced2268";
                    }
                }
                await context.Users.AddRangeAsync(usersList);



                //Seed Order
                var orderList = GetSampleData<Order>(File.ReadAllText(path + "Order.json"));
                await context.Orders.AddRangeAsync(orderList);


                //Seed items
                var itemList = GetSampleData<Item>(File.ReadAllText(path + "Item.json"));
                await context.Items.AddRangeAsync(itemList);



                await context.SaveChangesAsync();

            }
        }

        //Get sample data from json files
        private static List<T> GetSampleData<T>(string location)
        {
            var output = JsonSerializer.Deserialize<List<T>>(location, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return output;
        }


        public static async Task GenerateInvoice(IApplicationBuilder app)
        {

            //Get the Db context
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
            // var users  = await context.Users.Include(u => u.Orders).ToListAsync();

            if (context.Invoices != null)
            {
                return;
            }
            else
            {
                var discounts = await context.Discounts.ToListAsync();

                var orders = await context.Orders.Include(o => o.Items).Include(o => o.User).ThenInclude(u => u.UserType).ThenInclude(u => u.Discount).Where(u => u.UserId != null).ToListAsync();

                List<Invoice> newInvoices = new List<Invoice>();

                foreach (var order in orders)
                {
                    decimal amount = 0;


                    foreach (var item in order.Items)
                    {
                        amount += (item.Price * item.Quantity);
                    }



                    var discountRate = order.User.UserType.Discount.DiscountRate;

                    decimal percentageTotalDiscount = amount * (discountRate / 100);

                    decimal fixedDiscount = Math.Floor(amount / 100) * 5m;

                    Invoice newInvoice = new Invoice
                    {
                        UserId = order.UserId,
                        OrderId = order.Id,
                        TotalAmount = amount,
                        DiscountRate = discountRate,
                        GrandTotal = amount - (percentageTotalDiscount + fixedDiscount)
                    };

                    newInvoices.Add(newInvoice);
                }

                await context.AddRangeAsync(newInvoices);

                await context.SaveChangesAsync();

            }
        }

    }
}
