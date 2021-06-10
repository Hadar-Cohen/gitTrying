using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ex2.Models;
using Ex2.Models.DAL;

namespace Ex2.Controllers
{
    public class UsersController : ApiController
    {
        public HttpResponseMessage Get(string email, string password)
        {
            User us = new User();
            User u= us.checkLogin(email, password);

            if (u !=null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, u);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "user not found");
            }
        }

        // POST api/<controller>
        public int Post([FromBody] User user)
        {
            return user.Insert(); //return 1
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}