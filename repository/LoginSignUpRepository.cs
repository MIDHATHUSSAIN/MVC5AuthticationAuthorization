using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using webAPIandMVC.DBContext;
using webAPIandMVC.Models;
namespace webAPIandMVC.repository
{
    public class LoginSignUpRepository
    {
        readonly LoginSignUpDB_Context db = new LoginSignUpDB_Context();
        private readonly string secretKey = "your_super_secret_key_16chars_minimumSS"; 
        public bool creatingUser(LgoinSignUp credentials)
        {
            if (credentials != null)
            {
                string encryptedPassword = BCrypt.Net.BCrypt.HashPassword(credentials.password);
                var newUser = new LgoinSignUp
                {
                    email = credentials.email,
                    password = encryptedPassword
                };
                db.userData.Add(newUser);
                db.SaveChanges();
            }
            return true;
        }


        public string loginn(LgoinSignUp credentials)
        {
            //string encryptedPassword = BCrypt.Net.BCrypt.HashPassword(credentials.password);
            var FindUser = db.userData.FirstOrDefault(user => user.email == credentials.email);

            if (FindUser == null)
            {
                return ("Please signUP first");
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(credentials.password, FindUser.password);
            if (!isPasswordValid)
            {
                return ("InCorrect Password");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
            {
                //new Claim(ClaimTypes.Email, FindUser.email),
                new Claim("UserId", FindUser.ID.ToString())
                 }),
                Expires = DateTime.UtcNow.AddHours(1), 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }








    }
}