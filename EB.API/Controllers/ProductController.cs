using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EB.API.Input;
using EB.Domain.Interface;
using EB.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EB.API.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductDomain _productDomain;
        private IMapper _mapper;
        
        public ProductController(IProductDomain productDomain, IMapper mapper)
        {
            _productDomain = productDomain;
            _mapper = mapper;
        }

        // GET: api/product
        [HttpGet(Name = "Get All Products")]
        public IEnumerable<Product> Get()
        {
            return _productDomain.GetAll();
        }

        // GET: api/product/5
        [HttpGet("{id}", Name = "Get Product By Id")]
        public ProductResponse Get(int id)
        {
            return _productDomain.GetById(id);
        }

        // POST: api/product
        [HttpPost (Name = "Create Product")]
        public Product Post([FromBody] ProductInput value)
        {
            return _productDomain.Create(value);
        }
    }
}
