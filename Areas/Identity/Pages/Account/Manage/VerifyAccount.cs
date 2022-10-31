// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using chickadee.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace chickadee.Areas.Identity.Pages.Account.Manage
{
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class VerifyAccountModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public VerifyAccountModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;

        }
        
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }
        
        
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Key]
            [Display(Name = "Photo ID")]
            public byte[] IdPhoto { get; set; }

            [Display(Name = "Lease Agreement")]
            public byte[] LeasePhoto { get; set; }
            
            public bool IsIdVerified { get; set; }

        }

        private async Task LoadAsync(ApplicationUser user)
        {
            
            var idPhoto = Array.Empty<byte>();
            var leasePhoto = Array.Empty<byte>();
            
            if (_context.Document == null)
            {
                Console.WriteLine("No Table Name Documents");
                return;
            }

            var document = await _context.Document.FirstOrDefaultAsync(m => m.UserId == user.Id);
            
            if(document == null)
            {
            var res =await _context.Document.AddAsync(new Document()
            {
            DocumentId = Guid.NewGuid().ToString(),
            IdPhoto = idPhoto,
            LeasePhoto = leasePhoto,
            IsIdVerified = false,
            UserId = user.Id
            });
            await _context.SaveChangesAsync();
            }
            
            var documentResult = await _context.Document.FirstOrDefaultAsync(m => m.UserId == user.Id);

            Input = new InputModel
            {
                IdPhoto = documentResult?.IdPhoto,
                LeasePhoto = documentResult?.LeasePhoto,
                IsIdVerified = documentResult.IsIdVerified
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            
            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            var user = await _userManager.GetUserAsync(User);
            var document = await _context.Document.FirstOrDefaultAsync(d => d.UserId == user.Id);

            if (document.IsIdVerified)
            {
                StatusMessage = "Your account is already verified.";
                return RedirectToPage();
            }

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                Console.WriteLine(message);
                return Page();
            }

            if (Request.Form.Files.Count > 0)
            {
             
                IFormFile idPhoto = Request.Form.Files.GetFile("IdPhoto");
                IFormFile leasePhoto = Request.Form.Files.GetFile("leasePhoto");

                foreach (var file in Request.Form.Files)
                {
                    Console.WriteLine(file.Name);
                };
                using (var idPhotoStream = new MemoryStream())
                {
                    if(idPhoto != null){
                        await idPhoto.CopyToAsync(idPhotoStream);
                        document!.IdPhoto = idPhotoStream.ToArray();
                        
                    }
                }
                
                using (var leasePhotoStream = new MemoryStream())
                {
                    if(leasePhoto != null){
                        await leasePhoto.CopyToAsync(leasePhotoStream);
                        document!.LeasePhoto = leasePhotoStream.ToArray();
                    }

                }
                await _context.SaveChangesAsync();
                
            }

            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
