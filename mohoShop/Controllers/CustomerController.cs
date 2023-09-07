using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using mohoShop.Dto;
using mohoShop.Helpers;
using mohoShop.Interfaces;
using mohoShop.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace mohoShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper, IConfiguration configuration)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("SignUp")]
        public IActionResult SignUp(CustomerDto customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_customerRepository.AccountExists(customer.Gmail))
            {
                ModelState.AddModelError("", "This account already exists");
                return StatusCode(422, ModelState);
            }

            var mappedCustomer = _mapper.Map<Customer>(customer);

            // Encrypting password
            var encryptPass = HashPassword.ConvertToEncrypt(customer.Password);
            mappedCustomer.Password = encryptPass;

            if (!_customerRepository.SignUp(mappedCustomer))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created your account!");
        }

        [HttpPost("SignIn")]
        public IActionResult SignIn(string gmail, string password)
        {
            if (string.IsNullOrEmpty(gmail) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Both gmail and password are required.");
                return BadRequest(ModelState);
            }

            if (!_customerRepository.AccountExists(gmail))
            {
                ModelState.AddModelError("", "Check your gmail and password again");
                return StatusCode(422, ModelState);
            }

            var encryptedPass = _customerRepository.GetPassword(gmail);

            var decryptedPass = HashPassword.ConvertToDecrypt(encryptedPass);

            if (decryptedPass != password)
            {
                ModelState.AddModelError("", "Wrong password");
                return StatusCode(422, ModelState);
            }    

            var customer = _customerRepository.GetCustomer(gmail);

            // Create token and return
            string token = CreateToken(customer);

            return Ok(token);
        }

        private string CreateToken(Customer customer)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, customer.FullName),
                new Claim(ClaimTypes.Email, customer.Gmail)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
