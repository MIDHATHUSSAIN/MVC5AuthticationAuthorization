using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using webAPIandMVC.Models;
using webAPIandMVC.repository;
namespace webAPIandMVC.Controllers
{
   
    public class LoginSignUpController : ApiController
    {
        readonly LoginSignUpRepository rp = new LoginSignUpRepository();

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/LoginSignUp/CreateUserr")]
        public IHttpActionResult CreateUserrr([FromBody] LgoinSignUp credentials)
        {
            if (credentials == null || string.IsNullOrEmpty(credentials.email) || string.IsNullOrEmpty(credentials.password))
            {

                return BadRequest("Please Provide all information");

            }

            //var abc = rp.creatingUser(credentials);

            if (rp.creatingUser(credentials))
            {
                return Ok("user created successfully");

            }

            return BadRequest("please use other email");


        }
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/LoginSignUp/login")]
        public IHttpActionResult Login([FromBody] LgoinSignUp credentialss)
        {
            if (credentialss == null || string.IsNullOrEmpty(credentialss.email) || string.IsNullOrEmpty(credentialss.password))
            {

                return BadRequest("Please Provide all information");

            }
            var abc = rp.loginn(credentialss);
            if(abc == "InCorrect Password" || abc == "Please signUP first")
            {
                return BadRequest("Password or Email is incorrect");
            }

            return Ok(abc);


        }


    }
}