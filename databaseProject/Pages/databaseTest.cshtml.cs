using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using databaseProject;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Cryptography.X509Certificates;

namespace databaseProject.Pages
{
    public class databaseTestModel : PageModel
    {
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;
        public List<User> Users { get; set; }

        [BindProperty]
        public User NewUser { get; set; }

        public databaseTestModel(UserService userService,IConfiguration configuration)
        {
            _configuration = configuration;
            _userService = userService;
        }

        // ��ܸ��
        public void OnGet()
        {
            //OnPostExecuteSql();
            User fuckuser = new User();
            fuckuser.Name = "samuel";
            fuckuser.Email = "fier.sun.god@gmail.com";
            _userService.AddUser(fuckuser);
            Users = _userService.GetUsers();
        }

        // �s�W���
        //[HttpPost]
        //public async Task<IActionResult> OnPostAddUser([FromBody] User user)
        //{
        //    if (user != null && !string.IsNullOrEmpty(user.Name) && !string.IsNullOrEmpty(user.Email))
        //    {
        //        await AddUserAsync(user);

        //        return new JsonResult(new { message = "User added successfully", user = user })
        //        {
        //                    StatusCode = 200  // ���w���A�X�� 200
        //        }; ;  // ��^���\���A
        //    }

        //    return BadRequest();  // �Y��Ƥ����T�A��^���Ѫ��A
        //}
        //private async Task AddUserAsync(User user)
        //{
        //    using (var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        //    {
        //        await connection.OpenAsync();

        //        string query = "INSERT INTO Users (Name, Email) VALUES (@Name, @Email)";
        //        using (var command = new MySqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@Name", user.Name);
        //            command.Parameters.AddWithValue("@Email", user.Email);
        //            await command.ExecuteNonQueryAsync();
        //        }
        //    }
        //}
        public IActionResult OnPostAddUser()
        {
            if (!ModelState.IsValid)
            {
                Users = _userService.GetUsers();
                return Page();
            }

            _userService.AddUser(NewUser);
            return RedirectToPage();
        }

        // ���� SQL �ɮ�
        public IActionResult OnPostExecuteSql()
        {
            _userService.ExecuteSqlFile();
            return RedirectToPage();
        }
    }
}
