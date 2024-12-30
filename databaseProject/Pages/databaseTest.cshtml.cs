using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using databaseProject;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using Mysqlx.Crud;

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

        // 顯示資料
        public void OnGet()
        {
            //OnPostExecuteSql();
            //User fuckuser = new User();
            //fuckuser.Name = "samuel";
            //fuckuser.Email = "fier.sun.god@gmail.com";
            //_userService.AddUser(fuckuser);
            Users = _userService.GetUsers();
        }

        // 新增資料
        //[HttpPost]
        public IActionResult OnPostAddUser([FromBody] User user)
        {
            if (user != null && !string.IsNullOrEmpty(user.Name) && !string.IsNullOrEmpty(user.Email))
            {
                _userService.AddUser(user);

                return new JsonResult(new { success = true, message = "User added successfully" });
            }

            return BadRequest(new { success = false, message = "無效的資料" });  // 若資料不正確，返回失敗狀態
        }
        //public IActionResult OnPostAddUser()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        Users = _userService.GetUsers();
        //        return Page();
        //    }

        //    _userService.AddUser(NewUser);
        //    return RedirectToPage();
        //}

        // 執行 SQL 檔案
        public IActionResult OnGetExecuteSql()
        {
            _userService.ExecuteSqlFile();
            return new JsonResult(new { success = true });
        }
        //public IActionResult OnPostExecuteSql()
        //{
        //    _userService.ExecuteSqlFile();
        //    return RedirectToPage();
        //}

        public IActionResult OnPostRefreshSql()
        {
            Users = _userService.GetUsers();
            return new JsonResult(new { success = true, message = "User added successfully" });
        }
    }
}
