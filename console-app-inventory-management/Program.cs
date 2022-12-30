using System;
using Telnyx;
using dotenv.net;
using Telnyx.net.Entities;

namespace console_app_inventory_management
{
    class Program
    {
        static void Main(string[] args)
        {
            DotEnv.Config();
            string TELNYX_API_KEY = System.Environment.GetEnvironmentVariable("TELNYX_API_KEY");
            TelnyxConfiguration.SetApiKey(TELNYX_API_KEY);

            var service = new NumberOrderService();
            NumberOrderListOptions numberOrderListOptions = new NumberOrderListOptions()
            {
                Status = "success",
                PageSize = 250
            };
            var requestOptions = new RequestOptions();
            TelnyxList<Telnyx.NumberOrder> orders = service.List(numberOrderListOptions, requestOptions);
            foreach (NumberOrder order in orders)
            {
                Console.WriteLine(order);
            }

        }
    }
}
