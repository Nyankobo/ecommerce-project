﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace orders_library.Services
{
    public class ProductManager
    {

        //GET COUNT
        public int GetProductCount()
        {
            using (var context = DbContextFactory.Create())
            {
                var query = context.Product;

                var recordcount = query.Count();

                return recordcount;
            }
        }

        //RETRIEVE--------
        public IEnumerable<Product> GetProducts(Instructions instructions)
        {
            //view amt
            //start val
            //category


            using (var context = DbContextFactory.Create())  {

                var query = context.Product.ToList();

                Console.WriteLine(instructions.category);

                switch (instructions.category)
                {
                    case "all":
                        query = context.Product
                                       .OrderBy(n => n.Id).Skip(instructions.startVal).Take(instructions.viewAmt).ToList();
                        break;
                    default:
                        query = context.Product
                                       .OrderBy(n => n.Id).Where(c => c.Category == instructions.category)
                                       .Skip(instructions.startVal).Take(instructions.viewAmt).ToList();
                        break;
                }

                return query;
            }
        }

        /*
         * 
        //UPDATE--------
        public void UpdateProduct(Product prod)
        {
            using (var context = DbContextFactory.Create())   //no 'NEW' b/c of static class
            {
                try
                {
                    var prquery = context.Product.FirstOrDefault(a => a.Id == prod.Id);

                    Console.WriteLine($"Passed Address ID {prod.Id}");
                    Console.WriteLine($"Queried Address ID {prquery.Id}");

                    prquery.Name = prod.Name;
                    prquery.Price = prod.Price;
   

                    if (prquery.Id == prod.Id)
                    {
                        context.Entry(prquery).State = EntityState.Modified;
                        context.SaveChanges();
                        Console.WriteLine($"Product ID# {prod.Id} updated successfully.");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Unable to update product.");
                }
            }
        }

        //CREATE--------
        public void CreateProduct(Product prod)
        {
            using (var context = DbContextFactory.Create())   //no 'NEW' b/c of static class
            {
                try
                {
                    context.Product.Add(prod);
                    context.SaveChanges();
                    Console.WriteLine($"New product {prod.Name} added.");
                }
                catch(Exception)
                {
                    Console.WriteLine("Unable to add product.");
                }
            }
        }

        //REMOVE--> has line constraints ************************************
        public void RemoveProduct(int id)
        {
            using (var context = DbContextFactory.Create())   //no 'NEW' b/c of static class
            {
                try
                {
                    var prod = context.Product.FirstOrDefault(p => p.Id == id);
                    if (prod != null)
                    {
                        //locate lines that have this product Id:
                        var lineDel = from LineItem a in context.LineItem
                                      where a.ProductDetails.Id == prod.Id
                                      select a;

                        //Theoretically, will have to delete lines that have this product...
                        //Loop through these lines and remove
                        //foreach line found, if id == prodnum, remove it from context
                        /*
                        foreach (LineItem line in lineDel)
                        {
                            if (line.ProductDetails.Id == id)
                                context.LineItem.Remove(line);
                        }


                        context.Product.Remove(prod);
                        context.SaveChanges();

                        Console.WriteLine($"Product ID {id} removed successfully");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Unable to delete product.");
                }
            }
        }
        */
    }
}

