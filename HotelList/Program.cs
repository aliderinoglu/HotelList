using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace HotelList
{
    class Program
    {
        struct Hotels
        {
            public int hotelID;
            public string review;
        };

        static void Main(string[] args)
        {
            string fileName = "in.txt";

            if (File.Exists(fileName))
            {
                StreamReader sr = new StreamReader(fileName);
                
                string[] words = sr.ReadLine().ToLower().Split(' ');
                
                int numOfReviews = Convert.ToInt32(sr.ReadLine());
                Hotels[] hotels = new Hotels[numOfReviews];

                for (int i = 0; i < numOfReviews; i++)
                {
                    hotels[i].hotelID = Convert.ToInt32(sr.ReadLine());
                    hotels[i].review = sr.ReadLine().Replace(".", "").Replace(",", "").Replace("!", "").ToLower();
                }

                Dictionary<int, int> results = new Dictionary<int, int>();

                foreach (string word in words)
                {
                    for (int i = 0; i < numOfReviews; i++)
                    {
                        if (!results.ContainsKey(hotels[i].hotelID))
                            results.Add(hotels[i].hotelID, 0);

                        results[hotels[i].hotelID] += Regex.Matches(hotels[i].review, word).Count;
                    }
                }

                results = results.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
                results = results.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

                foreach (int hotelID in results.Keys)
                    Console.Write(hotelID + " ");
                
                sr.Close();
                sr.Dispose();
            }
            else
                Console.WriteLine("file not found! {0}", fileName);
        }
    }
}
