﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var bookCtx = new BookContext();
            long count = 0;

            Console.WriteLine("Loading Books ...");

            foreach (var item in GetBookData())
            {
                var book = new Book()
                {
                    ISBN = item[0],
                    Title = item[1],
                    Author = item[2],
                    PublicationYear = int.Parse(item[3]),
                    Publisher = item[4],
                    ImageUrlSmall = item[5],
                    ImageUrlMedium = item[6],
                    ImageUrlLarge = item[7],
                };

                bookCtx.Books.InsertOne(book);
                count++;

                Console.WriteLine("{0,-10} - {1}", count,book.Title);
            }

            Console.WriteLine("Finished!");
            
        }

        public static IEnumerable<string[]> GetBookData()
        {
            string filename = @"D:\Temp\BX-CSV-Dump\BX-Books.csv";
            var bookLines = File.ReadLines(filename).Take(100);
            return bookLines.Skip(1).Select(s => s.Replace("\"", "").Replace("&amp;", "&").Split(';'));
        }

    }
}
