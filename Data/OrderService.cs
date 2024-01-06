using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bislerium.Data
{
    public class OrderService
    {
        public static List<Orders> GetAllOrders()
        {
            string ordersFilePath = Utils.GetAppAddInsFilePath();
            if (!File.Exists(ordersFilePath))
            {
                return new List<Orders>();
            }

            var json = File.ReadAllText(ordersFilePath);

            return JsonSerializer.Deserialize<List<Orders>>(json);

        }

        public static int getOrderCount(string customerPhone) 
        {
            List <Orders> orders = GetAllOrders();
            int count = 0;
            foreach (var order in orders)
            {
                if (order.customerPhone.Equals(customerPhone)) {
                    count++;
                }

            }
            return count;
        }

    }
}
