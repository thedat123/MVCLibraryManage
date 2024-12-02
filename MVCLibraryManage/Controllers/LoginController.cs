using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Identity.Client;
using MVCLibraryManage.Models.Entity;
using MVCLibraryManage.Service;
using System.Collections.Generic;

namespace MVCLibraryManage.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IBorrowerService borrowerService;
        private readonly IStaffService staffService;

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public LoginController(IAccountService accountService, IBorrowerService borrowerService, IStaffService staffService)
        {
            this.accountService = accountService;
            this.borrowerService = borrowerService;
            this.staffService = staffService;
        }

        public IActionResult LoginRegister()
        {
            ViewBag.List_Borrowers = borrowerService.GetAllBorrower();
            ViewBag.List_Staffs = staffService.GetAllStaffs();
            return View();
        }

        [HttpPost]
        public IActionResult LoginRegister(string email, string password)
        {
            var user = accountService.GetAccount(email, password);
            if (user == null)
            {
                ViewData["Message"] = "* Tài khoản/ Mật khẩu không đúng!";
                ViewBag.List_Borrowers = borrowerService.GetAllBorrower();
                ViewBag.List_Staffs = staffService.GetAllStaffs();
                return View();
            }
            else
            {
                // Log to ensure session is set properly
                System.Diagnostics.Debug.WriteLine("User session set: " + email);

                HttpContext.Session.SetString("EmailUser", email);
                HttpContext.Session.SetString("Id", Convert.ToString(user.AccountID));
                HttpContext.Session.SetString("isStaff", user.isStaff ? "Staff" : "Borrower");
                HttpContext.Session.SetString("Name", accountService.GetNamebyID(user.AccountID, user.isStaff));

                if (HttpContext.Session.GetString("isStaff") == "Staff")
                {
                    return RedirectToAction("ManageLibraryItem", "Staff");
                }
                else
                {
                    return RedirectToAction("Menu", "Borrower");
                }
            }
        }

        [HttpPost]
        public IActionResult RegisterFunc(string email, string password, string confirmPassword, string name, string address)
        {
            if (accountService.GetIDAccountByMail(email) != null)
            {
                ViewData["Message"] = "* Email đã tồn tại!";
                return RedirectToAction("LoginRegister", "Login");
            }

            if (password != confirmPassword)
            {
                ViewData["Message"] = "* Mật khẩu xác nhận không khớp!";
                return View("LoginRegister", "Login");
            }

            accountService.AddNewAccount(email, password, name, address);
            ViewData["Message"] = "Đăng ký thành công! Vui lòng đăng nhập.";
            return View("LoginRegister", "Login");
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
