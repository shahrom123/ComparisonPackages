using Dapper;
using Domain.Entities;
using Infrastructure.Data;
using Npgsql;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Infrastructre.Services
{
    public class ProductService
    {
        private readonly DataContext _context;
        private string _connectionString = "Server=Localhost;Port=5432;Database=DB;User Id=postgres;Password=233223;";

        public ProductService(DataContext context)
        {
            _context = context;
        }
        public List<Product> GetProductsWithEntityF()
        {
            
            var sw = new Stopwatch();
            sw.Start();
            var eF =  _context.Products.Select(x => new Product(x.Id, x.Name, x.Price)).ToList();
            sw.Stop();
            Console.WriteLine($"Elapsed Times with EF /  {sw.ElapsedMilliseconds}");
            return eF.ToList();
        }
        
        public List<Product> GetProductWithDapper()
        {
            var sw = new Stopwatch();
            sw.Start();       
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    string sql = "SELECT * FROM \"Products\" ";
                    var result = connection.Query<Product>(sql);
                    sw.Stop();
                    Console.WriteLine($"Elapsed Times with dapper /  {sw.ElapsedMilliseconds}");
                    return result.ToList();
                }
        }

        public void Add()
        {
            var list = new List<Product>();
            for (int i = 0; i < 10; i++)
            {
                var t = new Product()
                {
                    Name = $"test" + i,
                    Price = 333,
                };
                list.Add(t);
            }
            _context.Products.AddRange(list);
            _context.SaveChanges();
        }
     
        public List<Product> GetProductsWithoutPackage()
        {
            string sql = "SELECT * FROM \"Products\" ";
            var Products = new List<Product>();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sw = new Stopwatch();
                sw.Start();
                connection.Open();
           
                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while ( reader.Read())
                            {
                                var Product = new Product()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                                    Name = reader.GetString(reader.GetOrdinal("name")),
                                    Price = reader.GetInt32(reader.GetOrdinal("price")),
                                };
                                Products.Add(Product);
                            }
                        } 
                    }
                sw.Stop();
               Console.WriteLine($"Elapsed Times without Packages /  {sw.ElapsedMilliseconds}");
                connection.Close();
            }
            return Products;
        }
    }
}
