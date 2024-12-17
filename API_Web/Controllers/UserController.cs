using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using API_Web.Models;
using API_Web.DTO;
using API_Web.Mappers;

namespace API_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        [HttpPost]
        [Route("sign-in")]
        public IActionResult SignIn([FromBody] UserDTO dto)
        {
            using (var db = new BaseProjectContext())
            {
                var user = db.UserLists.FirstOrDefault(u => u.Login == dto.Login && u.Password == dto.Password);
                return (user != null) ? Ok(UserMapper.ConvertToDTO(user)) : NotFound("User not found");
            }
        }
        private void SendMail(string to, string name, string patronomyc, string pass)
        {
            var fromAddress = new MailAddress("nikita.kalentiev@mail.ru", "Nikita");
            var toAddress = new MailAddress(to);
            const string fromPassword = "4yAuRdQE9de9HnUVs90w";
            const string smtpServer = " smtp.mail.ru";
            const int smtpPort = 587;

            var client = new SmtpClient
            {
                Host = smtpServer,
                Port = smtpPort,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),

            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = $"Спасибо за регистрацию, {name} {patronomyc}!",
                Body = $"Вы получаете это сообщение, потому что недавно зарегистрировали учетную запись в системе [ООО 'Система'].\r\n\r\nЕсли это были не вы, то, пожалуйста, обратитесь в нашу службу поддержки!\r\n\r\nВаш логин: [{to}]\nВаш пароль: [{pass}]",
            })
            {
                client.Send(message);
            }
        }
        [HttpPost]
        [Route("sign-up")]
        public IActionResult SignUp([FromBody] UserDTO dto)
        {
            using (var db = new BaseProjectContext())
            {
                try
                {
                    var user = db.UserLists.FirstOrDefault(u => u.Login == dto.Login);
                    if (user != null)
                    {
                        return BadRequest("User with this login already exists");
                    }
                    int id = db.UserLists.OrderBy(u => u.UserId).Last().UserId + 1;
                    user = UserMapper.ConvertToModel(dto);
                    user.UserId = id;
                    db.UserLists.Add(user);
                    db.SaveChanges();
                    SendMail(user.Login, user.Name, user.Patronymic, user.Password);
                    return Ok(user);
                }
                catch (Exception ex)
                {
                    return BadRequest($"Something is wrong! Try again later! [{ex.Message}]");
                }
            }
        }
        [HttpPut]
        [Route("edit")]
        public IActionResult Edit([FromBody] UserDTO dto)
        {
            using (var db = new BaseProjectContext())
            {
                try
                {
                    var user = db.UserLists.FirstOrDefault(u => u.UserId == dto.UserId);
                    user.Login = dto.Login;
                    user.Password = dto.Password;
                    user.Surname = dto.Surname;
                    user.Patronymic = dto.Patronymic;
                    user.Name = dto.Name;
                    db.SaveChanges();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
