using System;
using System.Collections.Generic;
using System.Text;


namespace ConsoleApp
{   
    //Class responsible for processing string input into a list of floats.
    public class DataProcessor
    {
        public static List<float> StringToList(string dailyOpeningPrices)
        {
            var dailyOpeningPricesProcessed = new List<float> { };
            var partialValue = new StringBuilder();

            // Append characters to the StringBuilder
            // When ',' encountered, parse made string as a float to the list.
            for (int i = 0; i < dailyOpeningPrices.Length; i++)
            {
                if (dailyOpeningPrices[i] != ',')
                {
                    partialValue.Append(dailyOpeningPrices[i]);
                }
                else
                {
                    dailyOpeningPricesProcessed.Add(float.Parse(partialValue.ToString()));
                    partialValue.Clear();
                }
            }
            // Add to the list left value as input is not ended with a ','.
            dailyOpeningPricesProcessed.Add(float.Parse(partialValue.ToString()));
            return dailyOpeningPricesProcessed;
        }
    }

    //Class responsible to calculate Best possible trade form the input
    //It iterates only once throught the list while keeping track 
    //Of days,prices and the best profit during the period 
    public class TheBestTrade
    { 
        public static string TradeOfMonth(List<float> dailyPrices)
        {           
            int buyDayOfMonth = 1;
            int sellDayOfMonth = 1;
            float profit = 0;
            float minPrice = dailyPrices[0];
            float maxPrice = 0;
            int tempBuyDayIndex = 1;
            float tempMinPrice = dailyPrices[0];

            for (int i = 1; i < dailyPrices.Count; i++)
            {
                if (dailyPrices[i] < minPrice)
                {
                    tempMinPrice = dailyPrices[i];
                    tempBuyDayIndex = i;
                }
                else              
                {
                    if (dailyPrices[i] - tempMinPrice > profit)
                    {
                        profit = dailyPrices[i] - tempMinPrice;
                        minPrice = tempMinPrice;
                        maxPrice = dailyPrices[i];
                        buyDayOfMonth = tempBuyDayIndex + 1;
                        sellDayOfMonth = i + 1;
                    }

                }            
            }
            return ($"{buyDayOfMonth}({minPrice}),{sellDayOfMonth}({maxPrice})");
        }
    }
    public class Program
    {
        static void Main()
        {
            // Take monthly input as a string
            string dailyOpeningPrices = Console.ReadLine();
            // Process input to retrive daily prices
            List<float> dailyPricesList = DataProcessor.StringToList(dailyOpeningPrices);
            // Return calculated the best trade
            Console.WriteLine(TheBestTrade.TradeOfMonth(dailyPricesList));            
        }
    }
}
