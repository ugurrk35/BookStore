﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BookStore.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context =new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any())
                {
                    return;
                }
                context.Books.AddRange(
            new Book
            {
             //   Id = 1,
                Title = "Learn Startup",
                GenreId = 1,
                PageCount = 134,
                PublishDate = new DateTime(2001, 06, 12)

            },
             new Book
             {
             //    Id = 2,
                 Title = "Herland",
                 GenreId = 2,
                 PageCount = 343,
                 PublishDate = new DateTime(2012, 06, 22)

             },
                new Book
                {
                //    Id = 3,
                    Title = "Dune",
                    GenreId = 2,
                    PageCount = 543,
                    PublishDate = new DateTime(2011, 06, 22)

                }
                );
                context.SaveChanges();
            }
        }
    }
}
