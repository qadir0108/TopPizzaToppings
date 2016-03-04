using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TopPizzaToppings
{
    class Program
    {
        public static void Main(string[] args)
        {
            FindFrequentToppings();
        }

        /// <summary>
        /// It finds top 20 pizza toppings
        /// </summary>
        public static void FindFrequentToppings()
        {
            var json = File.ReadAllText(@"pizzas.json");
            var pizzaConfigs = JsonConvert.DeserializeObject<PizzaConfiguration[]>(json);
            var orderedConfigs = pizzaConfigs.Select(x => string.Join(",", x.Toppings.OrderBy(y => y)));
            var groups = orderedConfigs.GroupBy(x => x);

            Dictionary<string, int> dicToppingCounts = new Dictionary<string, int>();
            foreach (var g in groups)
            {
                dicToppingCounts.Add(g.Key, g.Count());
            }

            var orderedToppingCounts = dicToppingCounts.OrderByDescending(x => x.Value);

            var Top20ToppingCounts = orderedToppingCounts.Take(20);

            Console.WriteLine("=====================================");
            foreach (var t in Top20ToppingCounts)
            {
                var topping = t.Key;
                Console.WriteLine("Toping : " + string.Join(",", topping) + "\t Frequency : " + t.Value);
            }
            Console.ReadLine();
        }
    }

    /// <summary>
    /// Pizza Configuration
    /// </summary>
    public class PizzaConfiguration
    {
        [JsonProperty("toppings")]
        public string[] Toppings { get; set; }
    }
}