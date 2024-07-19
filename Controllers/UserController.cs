using ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace ecommerce.Controllers
{
    public class UserController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        IPasswordHasher<ApplicationUser> _passwordHasher;
        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
        }


        public async Task<IActionResult> Profile()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                var role = await _userManager.GetRolesAsync(user);
                ViewData["FirstName"] = user.FirstName;
                ViewBag.Role = role[0];
                return View(user);
            }
            else
            {
                return RedirectToAction("Profile");
            }
        }
        public async Task<IActionResult> DeleteAccount(string password)
        {
            var user = await _userManager.GetUserAsync(User);
            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (passwordVerificationResult == PasswordVerificationResult.Success)
            {
                // Delete the user from the database
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    // sign out 
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Profile");
                }
            }
            else
            {
                TempData["DeleteWrongPasswordMessage"] = "Wrong Password";
                TempData["Color"] = "warning";
                return RedirectToAction("Profile");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveNewPassword(string newPassword2, string confirmNewPassword)
        {
            if (newPassword2 == confirmNewPassword)
            {
                var user = await _userManager.GetUserAsync(User);
                // hash new password to insert it in db
                var newPasswordHash = _passwordHasher.HashPassword(user, newPassword2);
                // Update the user password hash
                user.PasswordHash = newPasswordHash;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    TempData["VerificationMessage2"] = "new Password saved successfully";
                    TempData["Color"] = "success";
                }
                else
                {
                    TempData["VerificationMessage"] = "Error saving password.";
                    TempData["Color"] = "danger";
                }
                return RedirectToAction("Profile");
            }
            else
            {
                TempData["ShowNewPassword"] = "show";
                TempData["ShowNewPassword2"] = "show";
                TempData["Color"] = "warning";
                return RedirectToAction("Profile");
            }
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string currentPassword, string newPassword, string confirmPassword)
        {
            var user = await _userManager.GetUserAsync(User);

            if (currentPassword != null)
            {
                // Compare with the hashed password stored in the database
                var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, currentPassword);

                if (passwordVerificationResult == PasswordVerificationResult.Success)
                {
                    if (newPassword != confirmPassword)
                    {
                        TempData["VerificationMessage"] = "new password and confirm password are not match.";
                        TempData["Color"] = "warning";
                    }
                    else
                    {
                        // hash new password to insert it in db
                        var newPasswordHash = _passwordHasher.HashPassword(user, newPassword);
                        // Update the user password hash
                        user.PasswordHash = newPasswordHash;
                        var result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            TempData["VerificationMessage"] = "Password saved successfully";
                            TempData["Color"] = "success";
                        }
                        else
                        {
                            TempData["VerificationMessage"] = "Error saving password.";
                            TempData["Color"] = "danger";
                        }
                    }
                }
                else
                {
                    TempData["VerificationMessage"] = "Wrong Password";
                    TempData["Color"] = "warning";
                }
            }
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePhone(string phoneChange)
        {
            var user = await _userManager.GetUserAsync(User);
            user.PhoneNumber = phoneChange;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                // Update successful, handle accordingly
                TempData["AlertMessage3"] = "Phone updated successfully.";
                TempData["Color"] = "success";
            }
            else
            {
                TempData["AlertMessage3"] = "Errror updating phone";
                TempData["Color"] = "warning";
            }
            return RedirectToAction("Profile");
        }
        [HttpPost]
        public async Task<IActionResult> ChangeLocation(string locationChange)
        {
            var user = await _userManager.GetUserAsync(User);
            user.Location = locationChange;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                // Update successful, handle accordingly
                TempData["AlertMessage2"] = "Location updated successfully.";
                TempData["Color"] = "success";
            }
            else
            {
                TempData["AlertMessage2"] = "Errror updating location";
                TempData["Color"] = "warning";
            }
            return RedirectToAction("Profile");
        }
        public async Task<IActionResult> ForgetPasswordUserCode(string userCode, string Rcode)
        {
            if (userCode == Rcode)
            {
                TempData["ShowNewPassword"] = "show";
                TempData["Color"] = "warning";
            }
            else
            {
                TempData["AlertMessage5"] = "Wrong Code";
                TempData["Color"] = "warning";
            }
            return RedirectToAction("Profile");
        }
        [HttpPost]
        public async Task<IActionResult> SendCode(string emailChange)
        {
            var user = await _userManager.GetUserAsync(User);

            string fromMail = "nadertarek781@gmail.com";
            string fromPassword = "ccqrpqchbfidcrvt";
            Random rand = new Random();
            int randomNumber = rand.Next(10000, 100000);

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = $"Welcome from {user.FirstName}";
            message.To.Add(new MailAddress(emailChange));
            message.Body = $"<html><body> Your verification code is : <span style='color: red;'>{randomNumber}</span></body></html>";
            message.IsBodyHtml = true;
            TempData["Code"] = randomNumber;
            TempData["NewEmail"] = emailChange;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true
            };

            smtpClient.Send(message);
            return RedirectToAction("Profile");
        }
        public async Task<IActionResult> ForgetPasswordSendCode(string forgetEmail)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user.Email == forgetEmail)
            {
                string fromMail = "nadertarek781@gmail.com";
                string fromPassword = "ccqrpqchbfidcrvt";
                Random rand = new Random();
                int randomNumber = rand.Next(10000, 100000);

                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromMail);
                message.Subject = $"Welcome from {user.FirstName}";
                message.To.Add(new MailAddress(forgetEmail));
                message.Body = $"<html><body> Your verification code is : <span style='color: red;'>{randomNumber}</span></body></html>";
                message.IsBodyHtml = true;
                TempData["Code2"] = randomNumber;
                TempData["NewEmail2"] = forgetEmail;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true
                };

                smtpClient.Send(message);
            }
            else
            {
                TempData["AlertMessage4"] = "wrong email";
                TempData["Color"] = "warning";
            }
            return RedirectToAction("Profile");
        }
        [HttpPost]
        public async Task<IActionResult> ChangeEmail(string emailChange, string code, string RCode)
        {
            emailChange = TempData["NewEmail"].ToString();
            if (RCode == code)
            {
                var user = await _userManager.GetUserAsync(User);
                var existingUser = await _userManager.FindByEmailAsync(emailChange);
                if (user.Email == emailChange)
                {
                    TempData["AlertMessage"] = "Email is already the same as current.";
                    TempData["Color"] = "warning";
                }
                else if (existingUser != null)
                {
                    TempData["AlertMessage"] = "Email is already taken.";
                    TempData["Color"] = "warning";
                }
                else
                {
                    // Update the user email
                    user.UserName = emailChange;
                    var token = await _userManager.GenerateChangeEmailTokenAsync(user, emailChange);
                    var result = await _userManager.ChangeEmailAsync(user, emailChange, token);

                    if (!result.Succeeded)
                    {
                        TempData["AlertMessage"] = "Error updating email.";
                        TempData["Color"] = "warning";
                    }
                    else
                    {
                        TempData["AlertMessage"] = "Email updated successfully.";
                        TempData["Color"] = "success";
                    }
                }
            }
            else
            {
                TempData["AlertMessage"] = "Wrong code";
                TempData["Color"] = "warning";
            }
            return RedirectToAction("Profile");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            Response.Cookies.Delete("Cart");
            return Redirect("~/");
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View(new UserModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginModel user)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState)
                {
                    if (error.Value.Errors.Count > 0)
                        ModelState.AddModelError(string.Empty, error.Value.Errors[0].ErrorMessage);
                }
                return View("Login", user);
            }

            ApplicationUser appUser = new ApplicationUser()
            {
                Email = user.Email,
                UserName = user.Email
            };

            var FindEmail = await _userManager.FindByEmailAsync(user.Email);
            if (FindEmail == null)
            {
                ModelState.AddModelError(string.Empty, "Email does not exist");
                return View(user);
            }
            else
            {
                try
                {
                    var loginResult = await _signInManager.PasswordSignInAsync(user.Email, user.Password, true, true);
                    if (loginResult.Succeeded)
                    {
                        //if (string.IsNullOrEmpty(user.ReturnUrl))
                        return Redirect("~/");
                        //else
                        //    return Redirect(user.ReturnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid email or password."); // Add error message
                        return View(user);
                    }

                }
                catch (Exception ex)
                {

                }
            }
            return View(new UserModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserModel user, IFormFile imageFile)
        {
            //user.ReturnUrl = "~/";
            if (!ModelState.IsValid)
                return View("Register", user);

            byte[] imageData;

            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                imageData = memoryStream.ToArray();
            }

            ApplicationUser appUser = new ApplicationUser()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.Email,
                Location = user.Location,
                PhoneNumber = user.PhoneNumber,
                ImageData = imageData
            };

            try
            {
                var result = await _userManager.CreateAsync(appUser, user.Password);
                if (result.Succeeded)
                {
                    var loginResult = await _signInManager.PasswordSignInAsync(appUser, user.Password, true, true);
                    var myUser = await _userManager.FindByEmailAsync(appUser.Email);
                    await _userManager.AddToRoleAsync(myUser, "Customer");
                    if (loginResult.Succeeded)
                    {
                        //if (string.IsNullOrEmpty(user.ReturnUrl))
                        return Redirect("~/");
                        //else
                        //    return Redirect(user.ReturnUrl);
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        if (error.Code == error.Code)
                        {
                            ModelState.AddModelError(string.Empty, " | " + error.Description);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View(new UserModel());
        }
    }
}
