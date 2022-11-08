// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using chickadee.Enums;
using chickadee.Models;
using Duende.IdentityServer.Extensions;
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
        public VerificationDocument InputPhotoId { get; set; }

        [BindProperty]
        public VerificationDocument InputLeaseAgreement { get; set; }

        
        [TempData]
        public string StatusMessage { get; set; }
        
        
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
     
        private async Task LoadAsync(ApplicationUser user)
        {
            
            if (_context.VerificationDocuments == null || user == null)
            {
                Console.WriteLine("No Table Name Documents");
                return;
            }

            var photoIdDocumentList = _context.VerificationDocuments
                .Where(m => m.TenantId == user.Id)
                .Where(m => m.DocumentType == DocumentType.PhotoIdentification)
                .ToList();

            var leaseDocumentList = _context.VerificationDocuments
                .Where(m => m.TenantId == user.Id)
                .Where(m => m.DocumentType == DocumentType.LeaseAgreement)
                .ToList();

            if (!photoIdDocumentList.IsNullOrEmpty())
            {
                var photoIdDocument = photoIdDocumentList.First();
                InputPhotoId = new VerificationDocument()
                {
                    VerificationDocumentId = photoIdDocument.VerificationDocumentId,
                    Data = photoIdDocument.Data,
                    DocumentType = photoIdDocument.DocumentType,
                    ResponseMessage = photoIdDocument.ResponseMessage,
                    TenantId = photoIdDocument.TenantId
                }; 
            }
            else
            {
                InputPhotoId = new VerificationDocument();

            }


            if (!leaseDocumentList.IsNullOrEmpty())
            {
                var leaseDocument = leaseDocumentList.First();
                InputLeaseAgreement = new VerificationDocument()
                {
                    VerificationDocumentId = leaseDocument.VerificationDocumentId,
                    Data = leaseDocument.Data,
                    DocumentType = leaseDocument.DocumentType,
                    ResponseMessage = leaseDocument.ResponseMessage,
                    TenantId = leaseDocument.TenantId
                };
            }
            else
            {
                InputLeaseAgreement = new VerificationDocument();

            }
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
            Console.WriteLine("ON POST");

            var user = (Tenant)await _userManager.GetUserAsync(User);
            if (user == null || _context.VerificationDocuments == null) return NotFound();

            var photoIdDocumentList = _context.VerificationDocuments
                .Where(m => m.TenantId == user.Id)
                .Where(m => m.DocumentType == DocumentType.PhotoIdentification)
                .ToList();

            var leaseDocumentList = _context.VerificationDocuments
                .Where(m => m.TenantId == user.Id)
                .Where(m => m.DocumentType == DocumentType.LeaseAgreement)
                .ToList();

            Console.WriteLine(photoIdDocumentList);
            Console.WriteLine(leaseDocumentList);

            
            if (user.IsIdVerified)
            {
                StatusMessage = "Your account is already verified.";
                return RedirectToPage();
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
                    if(idPhoto != null)
                    {
                        await idPhoto.CopyToAsync(idPhotoStream);
                        if (photoIdDocumentList.Count > 0)
                        {
                            photoIdDocumentList.First().Data = idPhotoStream.ToArray();
                            photoIdDocumentList.First().ResponseMessage = null;
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            _context.VerificationDocuments.Add(new VerificationDocument()
                            {
                                Data = idPhotoStream.ToArray(),
                                DocumentType = DocumentType.PhotoIdentification,
                                TenantId = user.Id
                            });
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                
                using (var leasePhotoStream = new MemoryStream())
                {
                    if(leasePhoto != null)
                    {
                        await leasePhoto.CopyToAsync(leasePhotoStream);
                        if (leaseDocumentList.Count > 0)
                        {
                            leaseDocumentList.First().Data = leasePhotoStream.ToArray();
                            leaseDocumentList.First().ResponseMessage = null;
                            await _context.SaveChangesAsync();

                        }
                        else
                        {
                            _context.VerificationDocuments.Add(new VerificationDocument()
                            {
                                Data = leasePhotoStream.ToArray(),
                                DocumentType = DocumentType.LeaseAgreement,
                                TenantId = user.Id
                            });
                            await _context.SaveChangesAsync();

                        }
                    }
                }
                
            }

            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
