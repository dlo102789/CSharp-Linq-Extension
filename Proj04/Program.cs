// Derek Lo
// CSC470
// Project 04

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj04
{
    class Program
    {
        static void Main(string[] args)
        {
            // Populating arrays by reading data from database
            var suppliers = new[] {
                new {SN = 1, SName = "Smith", Status = 20, City = "London"},
                new {SN = 2, SName = "Jones", Status = 10, City = "Paris" },
                new {SN = 3, SName = "Blake", Status = 30, City = "Paris" },
                new {SN = 4, SName = "Clark", Status = 20, City = "London"},
                new {SN = 5, SName = "Adams", Status = 30, City = "Athens"}
            };

            var parts = new[] {
                new {PN = 1, PName = "Nut", Color = "Red", Weight = 12, City = "London"},
                new {PN = 2, PName = "Bolt", Color = "Green", Weight = 17, City = "Paris"},
                new {PN = 3, PName = "Screw", Color = "Blue", Weight = 17, City = "Rome"},
                new {PN = 4, PName = "Screw", Color = "Red", Weight = 14, City = "London"},
                new {PN = 5, PName = "Cam", Color = "Blue", Weight = 12, City = "Paris"},
                new {PN = 6, PName = "Cog", Color = "Red", Weight = 19, City = "London"}
            };

            var shipments = new[] {
                new {SN = 1, PN = 1, Qty = 300},
                new {SN = 1, PN = 2, Qty = 200},
                new {SN = 1, PN = 3, Qty = 400},
                new {SN = 1, PN = 4, Qty = 200},
                new {SN = 1, PN = 5, Qty =100},
                new {SN = 1, PN = 6, Qty = 100},
                new {SN = 2, PN = 1, Qty = 300},
                new {SN = 2, PN = 2, Qty = 400},
                new {SN = 3, PN = 2, Qty =200},
                new {SN = 4, PN = 2, Qty = 200},
                new {SN = 4, PN = 4, Qty = 300},
                new {SN = 4, PN = 5, Qty = 400}
            };

            Console.WriteLine("List of suppliers:");
            foreach (var supplier in suppliers)
            {
                Console.WriteLine($"SN = {supplier.SN}, SName = {supplier.SName}, Status = {supplier.Status}, City = {supplier.City}");
            }

            Console.WriteLine("\nList of parts:");
            foreach (var part in parts)
            {
                Console.WriteLine($"PN = {part.PN}, PName = {part.PName}, Color = {part.Color}, Weight = {part.Weight}, City = {part.City}");
            }

            Console.WriteLine("\nList of shipments:");
            foreach (var shipment in shipments)
            {
                Console.WriteLine($"SN = {shipment.SN}, PN = {shipment.PN}, Qty = {shipment.Qty}");
            }

            // 2) Prompting user for part color input and displaying all cities with that color
            Console.Write("\n2) Please enter a color to query: ");
            string input = Console.ReadLine();

            var subParts = parts
                .Where(part => part.Color.ToUpper() == input.ToUpper())
                .GroupBy(g => g.City)
                .Select(part => part.First())
                .ToArray();

            if (subParts.Count() == 0)
            {
                Console.WriteLine("Color not found");
            }
            else
            {
                Console.WriteLine($"{input} parts can be found in these cities: ");
                foreach (var part in subParts)
                {
                    Console.WriteLine(part.City);
                }
            }

            // 3) Querying suppliers data and displaying only names in ascending order.
            Console.WriteLine("\n3) Supplier names in ascending order:");
            var subSuppliers = suppliers
                .OrderBy(s => s.SName)
                .Select(supp => supp.SName)
                .ToArray();

            foreach (var supplier in subSuppliers)
            {
                Console.WriteLine(supplier);
            }

            // 4) Prompting user for supplier number and displaying corresponding part names and quantities.
            Console.Write($"\n4) Please enter a supplier number (1 - {suppliers.Count()}): ");
            input = Console.ReadLine();
            var subShipments = shipments
                .Join(parts, s => s.PN, p => p.PN, (s, p) => new { s.SN, s.PN, p.PName, s.Qty })
                .Where(s => s.SN == int.Parse(input))
                .ToArray();

            foreach (var shipment in subShipments)
            {
                Console.WriteLine($"Name: {shipment.PName}, Qty: {shipment.Qty}");
            }
        }

    }
}
