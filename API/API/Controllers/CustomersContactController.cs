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
    public class CustomersContactController : ControllerBase
    {
        private ICustomerContactService customerContactService;

        public CustomersContactController(ICustomerContactService customerContactService)
        {
            this.customerContactService = customerContactService;
        }

        // GET: api/CustomersContact
        [HttpGet]
        public IActionResult Get()
        {
            List<CustomerContactModel> customerContactModels = new List<CustomerContactModel>();
            List<CustomerContact> customersContact = customerContactService.GetList();

            foreach (var customerContact in customersContact)
            {
                CustomerContactModel customerContactModel = new CustomerContactModel();
                customerContactModel.Id = customerContact.Id;
                customerContactModel.CustomerId = customerContact.CustomerId;
                customerContactModel.Department = customerContact.Department;
                customerContactModel.FirstName = customerContact.FirstName;
                customerContactModel.LastName = customerContact.LastName;
                customerContactModel.InternalNumber = customerContact.InternalNumber;
                customerContactModel.MailAddress = customerContact.MailAddress;
                customerContactModel.PhoneNumber = customerContact.PhoneNumber;
                customerContactModel.Title = customerContact.Title;
                
                customerContactModels.Add(customerContactModel);
            }

            return Ok(customerContactModels);
        }

        // GET: api/CustomersContact/5
        [HttpGet("{customerId}")]
        public IActionResult Get(Guid customerId)
        {
            if (customerId != Guid.Empty)
            {
                CustomerContact customerContact = customerContactService.Get(customerId);
                if (customerContact != null)
                {
                    CustomerContactModel customerContactModel = new CustomerContactModel { Title = customerContact.Title, Department = customerContact.Department, FirstName = customerContact.FirstName, LastName = customerContact.LastName, Id = customerContact.Id, CustomerId = customerContact.CustomerId, InternalNumber = customerContact.InternalNumber, MailAddress = customerContact.MailAddress, PhoneNumber = customerContact.PhoneNumber };

                    return Ok(customerContactModel);
                }

                return BadRequest();
            }

            return BadRequest();
        }

        // POST: api/CustomersContact                    
        [HttpPost]
        public IActionResult Post([FromBody] CustomerContactModel customerContactModel)
        {

            if (ModelState.IsValid)
            {

                CustomerContact customerContact = new CustomerContact { Title = customerContactModel.Title, Department = customerContactModel.Department, FirstName = customerContactModel.FirstName, LastName = customerContactModel.LastName, CustomerId = customerContactModel.CustomerId, InternalNumber = customerContactModel.InternalNumber, MailAddress = customerContactModel.MailAddress, PhoneNumber = customerContactModel.PhoneNumber };
                customerContactService.Add(customerContact);

                return Ok(new { status = "success" });
            }

            return BadRequest();
        }

        // PUT: api/CustomersContact/5
        [HttpPut("{customerId}")]
        public IActionResult Put([FromBody] CustomerContactModel customerContactModel)
        {
            if (ModelState.IsValid)
            {

                CustomerContact customerContact = customerContactService.Get(customerContactModel.CustomerId);
                customerContact.Department = customerContactModel.Department;
                customerContact.FirstName = customerContactModel.FirstName;
                customerContact.LastName = customerContactModel.LastName;
                customerContact.InternalNumber = customerContactModel.InternalNumber;
                customerContact.MailAddress = customerContactModel.MailAddress;
                customerContact.PhoneNumber = customerContactModel.PhoneNumber;
                customerContact.Title = customerContactModel.Title;
                customerContact.Id = customerContact.Id;
                customerContact.CustomerId = customerContact.CustomerId;
                customerContactService.Update(customerContact);

                return Ok(new { status = "success" });
            }

            return BadRequest();
        }

        // DELETE: api/CustomersContact/5
        [HttpDelete("{currentId}")]
        public IActionResult Delete(Guid currentId)
        {
            if (currentId != Guid.Empty)
            {
                customerContactService.Delete(currentId);
                return Ok(new { status = "success" });
            }

            return BadRequest();
        }
    }
}