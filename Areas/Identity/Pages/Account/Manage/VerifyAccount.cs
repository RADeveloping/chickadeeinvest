// // Licensed to the .NET Foundation under one or more agreements.
// // The .NET Foundation licenses this file to you under the MIT license.
// #nullable disable
//
// using System;
// using System.ComponentModel.DataAnnotations;
// using System.Text.Encodings.Web;
// using System.Threading.Tasks;
// using chickadee.Models;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.RazorPages;
//
// namespace chickadee.Areas.Identity.Pages.Account.Manage
// {
//     using Data;
//     using Microsoft.EntityFrameworkCore;
//
//     public class VerifyAccountModel : PageModel
//     {
//         private readonly UserManager<ApplicationUser> _userManager;
//         private readonly SignInManager<ApplicationUser> _signInManager;
//         private readonly ApplicationDbContext _context;
//
//         public VerifyAccountModel(
//             UserManager<ApplicationUser> userManager,
//             SignInManager<ApplicationUser> signInManager,
//             ApplicationDbContext context)
//         {
//             _userManager = userManager;
//             _signInManager = signInManager;
//             _context = context;
//
//         }
//         
//         /// <summary>
//         ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
//         ///     directly from your code. This API may change or be removed in future releases.
//         /// </summary>
//         [BindProperty]
//         public VerificationDocument Input { get; set; }
//
//         [TempData]
//         public string StatusMessage { get; set; }
//         
//         
//         /// <summary>
//         ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
//         ///     directly from your code. This API may change or be removed in future releases.
//         /// </summary>
//      
//
//         private async Task LoadAsync(ApplicationUser user)
//         {
//             
//             
//             if (_context.VerificationDocuments == null || user == null)
//             {
//                 Console.WriteLine("No Table Name Documents");
//                 return;
//             }
//
//             var document = await _context.VerificationDocuments.FirstOrDefaultAsync(m => m.TenantId == user.Id);
//             
//             if (document == null) return;
//             
//             Input = new VerificationDocument()
//             {
//                 VerificationDocumentId = document.VerificationDocumentId,
//                 data = document.data,
//                 DocumentType = document.DocumentType,
//                 ResponseMessage = document.ResponseMessage,
//                 TenantId = document.TenantId
//             };
//         }
//
//         public async Task<IActionResult> OnGetAsync()
//         {
//             var user = await _userManager.GetUserAsync(User);
//             if (user == null)
//             {
//                 return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
//             }
//             
//             await LoadAsync(user);
//             return Page();
//         }
//
//         public async Task<IActionResult> OnPostAsync()
//         {
//
//             var user = await _userManager.GetUserAsync(User) as Tenant;
//             if (user == null || _context.VerificationDocuments == null) return NotFound();
//             
//             var document = await _context.VerificationDocuments.FirstOrDefaultAsync(d => d.TenantId == user.Id);
//
//             if (user.IsIdVerified)
//             {
//                 StatusMessage = "Your account is already verified.";
//                 return RedirectToPage();
//             }
//             
//             if (!ModelState.IsValid)
//             {
//                 await LoadAsync(user);
//                 var message = string.Join(" | ", ModelState.Values
//                     .SelectMany(v => v.Errors)
//                     .Select(e => e.ErrorMessage));
//                 Console.WriteLine(message);
//                 return Page();
//             }
//
//             if (Request.Form.Files.Count > 0)
//             {
//              
//                 IFormFile idPhoto = Request.Form.Files.GetFile("IdPhoto");
//                 IFormFile leasePhoto = Request.Form.Files.GetFile("leasePhoto");
//
//                 foreach (var file in Request.Form.Files)
//                 {
//                     Console.WriteLine(file.Name);
//                 };
//                 using (var idPhotoStream = new MemoryStream())
//                 {
//                     if(idPhoto != null){
//                         await idPhoto.CopyToAsync(idPhotoStream);
//                         document!.IdPhoto = idPhotoStream.ToArray();
//                         
//                     }
//                 }
//                 
//                 using (var leasePhotoStream = new MemoryStream())
//                 {
//                     if(leasePhoto != null){
//                         await leasePhoto.CopyToAsync(leasePhotoStream);
//                         document!.LeasePhoto = leasePhotoStream.ToArray();
//                     }
//
//                 }
//                 await _context.SaveChangesAsync();
//                 
//             }
//
//             StatusMessage = "Your profile has been updated";
//             return RedirectToPage();
//         }
//     }
// }
