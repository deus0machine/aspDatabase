// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using aspDatabase.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace aspDatabase.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<aspDatabaseUser> _signInManager;
        private readonly UserManager<aspDatabaseUser> _userManager;
        private readonly IUserStore<aspDatabaseUser> _userStore;
        private readonly IUserEmailStore<aspDatabaseUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        // Удаляем IEmailSender
        // private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel(
            UserManager<aspDatabaseUser> userManager,
            IUserStore<aspDatabaseUser> userStore,
            SignInManager<aspDatabaseUser> signInManager,
            ILogger<RegisterModel> logger,
            // Удаляем IEmailSender из конструктора
            // IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            // _emailSender = emailSender;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Имя")]
            public string FirstName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Фамилия")]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Адрес почты")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Повторите пароль")]
            [Compare("Password", ErrorMessage = "Пароль и пароль для подтверждения не совпадают.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Роль")]
            public string Role { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                user.FirstName = Input.FirstName;
                user.LastName = Input.LastName;

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Пользователь создал новую учетную запись с паролем.");

                    // Добавление ролей, если их нет
                    if (!await _roleManager.RoleExistsAsync("admin"))
                        await _roleManager.CreateAsync(new IdentityRole("admin"));
                    if (!await _roleManager.RoleExistsAsync("coach"))
                        await _roleManager.CreateAsync(new IdentityRole("coach"));
                    if (!await _roleManager.RoleExistsAsync("guest"))
                        await _roleManager.CreateAsync(new IdentityRole("guest"));

                    var selectedRole = Input.Role;
                    if (!await _roleManager.RoleExistsAsync(selectedRole))
                    {
                        ModelState.AddModelError(string.Empty, "Selected role does not exist.");
                        return Page();
                    }
                    await _userManager.AddToRoleAsync(user, selectedRole);

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Если мы зашли сюда, значит что-то пошло не так, повторно отобразим форму
            return Page();
        }
        private aspDatabaseUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<aspDatabaseUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(aspDatabaseUser)}'. " +
                    $"Ensure that '{nameof(aspDatabaseUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<aspDatabaseUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<aspDatabaseUser>)_userStore;
        }
    }
}
