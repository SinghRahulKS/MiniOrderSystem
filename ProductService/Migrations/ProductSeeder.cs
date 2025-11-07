using Microsoft.EntityFrameworkCore;
using ProductService.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductService.Repository.Seed
{
    public static class ProductSeeder
    {
        public static async Task SeedAsync(ProductDbContext context)
        {
            await context.Database.MigrateAsync();
            // ---------- Seed Categories (Unsplash – Super Reliable) ----------
            if (!context.Categories.Any())
            {
                var categories = new[]
                {
                    new Category { Id = Guid.NewGuid(), Name = "Mobile",  Description = "Smartphones and accessories", Image = "https://images.unsplash.com/photo-1512941937669-90a6b94e7e62?auto=format&fit=crop&w=1200&q=80" },
                    new Category { Id = Guid.NewGuid(), Name = "Laptop", Description = "Laptops and notebooks",       Image = "https://images.unsplash.com/photo-1496181133206-80ce9b88a0f2?auto=format&fit=crop&w=1200&q=80" },
                    new Category { Id = Guid.NewGuid(), Name = "Tablet", Description = "Tablets and iPads",           Image = "https://images.unsplash.com/photo-1544244012-2c75f8e0c68b?auto=format&fit=crop&w=1200&q=80" },
                    new Category { Id = Guid.NewGuid(), Name = "Camera", Description = "Cameras and photography gear",Image = "https://images.unsplash.com/photo-1502920917128-1fc03a9c0db5?auto=format&fit=crop&w=1200&q=80" },
                    new Category { Id = Guid.NewGuid(), Name = "Others", Description = "Miscellaneous electronics",   Image = "https://images.unsplash.com/photo-1518770660439-4636190af475?auto=format&fit=crop&w=1200&q=80" }
                };
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            // ---------- Seed Products (Two Unique Images Each) ----------
            if (!context.Products.Any())
            {
                var categories = context.Categories.ToList();
                var products = new List<Product>();

                foreach (var category in categories)
                {
                    switch (category.Name)
                    {
                        case "Mobile":
                            products.AddRange(new[]
                            {
                                CreateProduct("iPhone 15", "Apple flagship phone", 79999, 74999, category.Id,
                                    "https://images.unsplash.com/photo-1603898037225-3aa3e17b8b05?auto=format&fit=crop&w=800&q=80",  // iPhone front
                                    "https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?auto=format&fit=crop&w=800&q=80"), // iPhone side

                                CreateProduct("Samsung Galaxy S24", "Latest Samsung flagship phone", 69999, 65999, category.Id,
                                    "https://images.unsplash.com/photo-1603791440384-56cd371ee9a7?auto=format&fit=crop&w=800&q=80",  // Galaxy closeup
                                    "https://images.unsplash.com/photo-1603797219268-248a3ccdcd59?auto=format&fit=crop&w=800&q=80"),

                                CreateProduct("OnePlus 12", "Performance Android phone", 55999, 52999, category.Id,
                                    "https://images.unsplash.com/photo-1616129942194-dc57c6d4eb6d?auto=format&fit=crop&w=800&q=80",  // Android performance
                                    "https://images.unsplash.com/photo-1603797219268-248a3ccdcd59?auto=format&fit=crop&w=800&q=80"),

                                CreateProduct("Google Pixel 8", "Best Android camera phone", 67999, 64999, category.Id,
                                    "https://images.unsplash.com/photo-1603898037225-3aa3e17b8b05?auto=format&fit=crop&w=800&q=80",  // Pixel camera
                                    "https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?auto=format&fit=crop&w=800&q=80"),

                                CreateProduct("Redmi Note 13 Pro", "Budget-friendly phone", 18999, 16999, category.Id,
                                    "https://images.unsplash.com/photo-1606813902844-dbf3b1a24c7d?auto=format&fit=crop&w=800&q=80",  // Budget phone
                                    "https://images.unsplash.com/photo-1603797219268-248a3ccdcd59?auto=format&fit=crop&w=800&q=80")
                            });
                            break;

                        case "Laptop":
                            products.AddRange(new[]
                            {
                                CreateProduct("Dell XPS 13", "Premium ultrabook", 109999, 99999, category.Id,
                                    "https://images.unsplash.com/photo-1496181133206-80ce9b88a0f2?auto=format&fit=crop&w=800&q=80",  // Ultrabook open
                                    "https://images.unsplash.com/photo-1581091226825-a6a2a5aee158?auto=format&fit=crop&w=800&q=80"),

                                CreateProduct("MacBook Air M3", "Apple thin laptop", 119999, 114999, category.Id,
                                    "https://images.unsplash.com/photo-1517336714731-489689fd1ca8?auto=format&fit=crop&w=800&q=80",  // MacBook thin
                                    "https://images.unsplash.com/photo-1581091226825-a6a2a5aee158?auto=format&fit=crop&w=800&q=80"),

                                CreateProduct("HP Spectre x360", "Convertible laptop", 99999, 94999, category.Id,
                                    "https://images.unsplash.com/photo-1519389950473-47ba0277781c?auto=format&fit=crop&w=800&q=80",  // Convertible
                                    "https://images.unsplash.com/photo-1496181133206-80ce9b88a0f2?auto=format&fit=crop&w=800&q=80"),

                                CreateProduct("Lenovo ThinkPad X1", "Professional laptop", 129999, 124999, category.Id,
                                    "https://images.unsplash.com/photo-1559163499-413811fb2344?auto=format&fit=crop&w=800&q=80",  // Professional
                                    "https://images.unsplash.com/photo-1581091226825-a6a2a5aee158?auto=format&fit=crop&w=800&q=80"),

                                CreateProduct("Asus ROG Zephyrus G14", "Gaming laptop", 139999, 129999, category.Id,
                                    "https://images.unsplash.com/photo-1593642532973-d31b6557fa68?auto=format&fit=crop&w=800&q=80",  // Gaming
                                    "https://images.unsplash.com/photo-1496181133206-80ce9b88a0f2?auto=format&fit=crop&w=800&q=80")
                            });
                            break;

                        case "Tablet":
                            products.AddRange(new[]
                            {
                                CreateProduct("iPad Pro 12.9", "Apple high-end tablet", 99999, 94999, category.Id,
                                    "https://images.unsplash.com/photo-1593642634443-44adaa06623a?auto=format&fit=crop&w=800&q=80",  // iPad Pro
                                    "https://images.unsplash.com/photo-1587831990711-23ca6441447b?auto=format&fit=crop&w=800&q=80"),

                                CreateProduct("Samsung Galaxy Tab S9", "Flagship Android tablet", 65999, 62999, category.Id,
                                    "https://images.unsplash.com/photo-1587574293340-e0011c1a8ecf?auto=format&fit=crop&w=800&q=80",  // Galaxy Tab
                                    "https://images.unsplash.com/photo-1616401784845-43e1a3d2f6be?auto=format&fit=crop&w=800&q=80"),

                                CreateProduct("Lenovo Tab P11 Pro", "Mid-range tablet", 34999, 32999, category.Id,
                                    "https://images.unsplash.com/photo-1587831990711-23ca6441447b?auto=format&fit=crop&w=800&q=80",
                                    "https://images.unsplash.com/photo-1593642634443-44adaa06623a?auto=format&fit=crop&w=800&q=80"),

                                CreateProduct("Xiaomi Pad 6", "Value-for-money tablet", 29999, 27999, category.Id,
                                    "https://images.unsplash.com/photo-1616401784845-43e1a3d2f6be?auto=format&fit=crop&w=800&q=80",
                                    "https://images.unsplash.com/photo-1587574293340-e0011c1a8ecf?auto=format&fit=crop&w=800&q=80"),

                                CreateProduct("Amazon Fire HD 10", "Affordable multimedia tablet", 14999, 13999, category.Id,
                                    "https://images.unsplash.com/photo-1616401784845-43e1a3d2f6be?auto=format&fit=crop&w=800&q=80",
                                    "https://images.unsplash.com/photo-1587831990711-23ca6441447b?auto=format&fit=crop&w=800&q=80")
                            });
                            break;

                        case "Camera":
                            products.AddRange(new[]
                            {
                                CreateProduct("Canon EOS 1500D", "Entry-level DSLR", 42999, 39999, category.Id,
                                    "https://images.unsplash.com/photo-1504215680853-026ed2a45def?auto=format&fit=crop&w=800&q=80",  // DSLR entry
                                    "https://images.unsplash.com/photo-1519183071298-a2962be96c94?auto=format&fit=crop&w=800&q=80"),

                                CreateProduct("Nikon D5600", "DSLR for beginners", 49999, 46999, category.Id,
                                    "https://images.unsplash.com/photo-1504215680853-026ed2a45def?auto=format&fit=crop&w=800&q=80",
                                    "https://images.unsplash.com/photo-1526170375885-4d8ecf77b99f?auto=format&fit=crop&w=800&q=80"),

                                CreateProduct("Sony Alpha A7 III", "Mirrorless camera", 149999, 139999, category.Id,
                                    "https://images.unsplash.com/photo-1502920917128-1fc03a9c0db5?auto=format&fit=crop&w=800&q=80",  // Mirrorless
                                    "https://images.unsplash.com/photo-1519183071298-a2962be96c94?auto=format&fit=crop&w=800&q=80"),

                                CreateProduct("Fujifilm X-T30 II", "Compact mirrorless camera", 84999, 79999, category.Id,
                                    "https://images.unsplash.com/photo-1526170375885-4d8ecf77b99f?auto=format&fit=crop&w=800&q=80",
                                    "https://images.unsplash.com/photo-1502920917128-1fc03a9c0db5?auto=format&fit=crop&w=800&q=80"),

                                CreateProduct("GoPro Hero 12", "Action camera", 45999, 42999, category.Id,
                                    "https://images.unsplash.com/photo-1581291519195-ef11498d1cf5?auto=format&fit=crop&w=800&q=80",  // Action cam
                                    "https://images.unsplash.com/photo-1502920917128-1fc03a9c0db5?auto=format&fit=crop&w=800&q=80")
                            });
                            break;

                        case "Others":
                            products.AddRange(new[]
                            {
                                CreateProduct("Sony WH-1000XM5", "Noise cancelling headphones", 29999, 24999, category.Id,
                                    "https://images.unsplash.com/photo-1526170375885-4d8ecf77b99f?auto=format&fit=crop&w=800&q=80",  // Premium headphones
                                    "https://images.unsplash.com/photo-1579710039144-85d6bdffddc0?auto=format&fit=crop&w=800&q=80"), // Headphones detail

                                CreateProduct("Apple Watch Series 9", "Smartwatch", 41999, 39999, category.Id,
                                    "https://images.unsplash.com/photo-1523275335684-37898b6baf30?auto=format&fit=crop&w=800&q=80",  // Apple Watch front
                                    "https://images.unsplash.com/photo-1561169895-b30051f7da23?auto=format&fit=crop&w=800&q=80"), // Watch side

                                CreateProduct("Logitech MX Master 3S", "Ergonomic mouse", 9999, 8999, category.Id,
                                    "https://images.unsplash.com/photo-1587829741301-dc798b83add3?auto=format&fit=crop&w=800&q=80",  // Ergonomic mouse
                                    "https://images.unsplash.com/photo-1613427623952-4e0c4a7a896d?auto=format&fit=crop&w=800&q=80"), // Mouse closeup

                                CreateProduct("Amazon Echo Dot 5th Gen", "Smart home speaker", 5499, 4999, category.Id,
                                    "https://images.unsplash.com/photo-1587654780291-39c9404d53c3?auto=format&fit=crop&w=800&q=80",  // Smart speaker
                                    "https://images.unsplash.com/photo-1617172236257-1f0f4deda8a0?auto=format&fit=crop&w=800&q=80"),

                                CreateProduct("JBL Flip 6", "Bluetooth speaker", 12999, 10999, category.Id,
                                    "https://images.unsplash.com/photo-1583225419948-5d5d7a4e4865?auto=format&fit=crop&w=800&q=80",  // Bluetooth speaker
                                    "https://images.unsplash.com/photo-1617172236257-1f0f4deda8a0?auto=format&fit=crop&w=800&q=80")
                            });
                            break;
                    }
                }

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }

        // Helper: Appends Unsplash params once (no duplicates)
        private static Product CreateProduct(string name, string description, decimal price, decimal sellPrice,
                                            Guid categoryId, string mainImageBase, string image1Base)
        {
            return new Product
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                Price = price,
                SellPrice = sellPrice,
                Currency = "INR",
                CurrencyCode = "₹",
                CategoryId = categoryId,
                MainImage = mainImageBase,  // Already has ?auto=...&w=800
                Image1 = image1Base,     // Already has ?auto=...&w=800
                CreatedBy = "system",
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}