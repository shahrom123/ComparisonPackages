using Domain.Entities;
using Infrastructre.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private ProductService _productService;
    public ProductController(ProductService productService)
    {
        _productService = productService;
    }
    [HttpGet("GetProductWithEntityFramwork")]
    public List<Product> GetEF()
    {
        return  _productService.GetProductsWithEntityF();
       
       
    }
    [HttpGet("GetProductWithDapper")]
    public List<Product> GetDapper()
    {
        return _productService.GetProductWithDapper();
    }
    [HttpGet("GetProductWithoutPackages")]
    public List<Product> Get()
    {
        return _productService.GetProductsWithoutPackage();
    }

    [HttpPost("Add")]
    public void Add() 
    {
        _productService.Add();
    }
}
