using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ICustomerService customerService;

        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        // GET: api/Customers
        [HttpGet]
        public IActionResult Get()
        {
            List<CustomerModel> customerModels = new List<CustomerModel>();
            List<Customer> customers = customerService.GetList();

            foreach (var customer in customers)
            {
                CustomerModel customerModel = new CustomerModel();
                customerModel.Id = customer.Id;
                customerModel.Note = customer.Note;
                customerModel.Title = customer.Title;
                customerModel.Address = customer.Address;
                customerModel.CurrentCode = customer.CurrentCode;

                customerModels.Add(customerModel);
            }

            return Ok(customerModels);
        }

        // GET: api/Customers/id/5
        [HttpGet("id/{id}")]
        public IActionResult Get(Guid id)
        {
            if (id != Guid.Empty)
            {
                Customer customer = customerService.Get(id);
                CustomerModel customerModel = new CustomerModel { Title = customer.Title, Address = customer.Address, Note = customer.Note, CurrentCode = customer.CurrentCode, Id = customer.Id };

                return Ok(customerModel);
            }

            return BadRequest();
        }

        // GET: api/Customers/currentCode/5
        [HttpGet("currentCode/{currentCode}")]
        public IActionResult Get(string currentCode)
        {
            if (currentCode != string.Empty)
            {
                Customer customer = customerService.Get(currentCode);
                Customer customerModel = new Customer { Title = customer.Title, Address = customer.Address, Note = customer.Note, CurrentCode = customer.CurrentCode, Id = customer.Id };

                return Ok(customerModel);
            }

            return BadRequest();
        }

        // POST: api/Customers                    
        [HttpPost]
        public IActionResult Post([FromBody] CustomerModel customerModel)
        {

            if (ModelState.IsValid)
            {
               
                Customer customer = new Customer { Title = customerModel.Title, Address = customerModel.Address, Note = customerModel.Note, CurrentCode = customerModel.CurrentCode };
                customerService.Add(customer);

                return Ok(new { status = "success" });
            }

            return BadRequest();
        }

        // PUT: api/Customers/5
        [HttpPut("{currentCode}")]
        public IActionResult Put([FromBody] CustomerModel customerModel)
        {
            if (ModelState.IsValid)
            {
                
                Customer customer = customerService.Get(customerModel.CurrentCode);
                customer.CurrentCode = customerModel.CurrentCode;
                customer.Address = customerModel.Address;
                customer.Note = customerModel.Note;
                customer.Title = customerModel.Title;
                customer.Id = customer.Id;
                customerService.Update(customer);

                return Ok(new { status = "success" });
            }

            return BadRequest();
        }

        // DELETE: api/Customers/5
        [HttpDelete("{currentCode}")]
        public IActionResult Delete(string currentCode)
        {
            if (currentCode != string.Empty)
            {
                customerService.Delete(currentCode);
                return Ok(new { status = "success" });
            }

            return BadRequest();
        }
    }
}