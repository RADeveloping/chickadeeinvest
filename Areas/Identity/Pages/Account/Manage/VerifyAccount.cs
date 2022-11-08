// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using chickadee.Enums;
using chickadee.Models;
using Duende.IdentityServer.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        
        [TempData]
        public string UnitId { get; set; }
        
        
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
     
        private async Task LoadAsync(Tenant user)
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
            if (_context.Tenant != null)
            {
                var user =  _context.Tenant.FirstOrDefault(t => t.Id == User.GetSubjectId());
                ViewData["tenant"] = user;

                await LoadAsync(user);
            }

            if (_context.Property == null) return Page();
            List<SelectListItem> li = new List<SelectListItem> { new() { Text = "", Value = "" } };
            foreach (var property in _context.Property.Where(p=>p.Units.Any()))
            {
                li.Add(new SelectListItem { Text = property.Address, Value = property.PropertyId });
            }
            ViewData["properties"] = li;
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // foreach (var (key, value) in Request.Form)
            // {
            //     Console.WriteLine("key = {key}");
            //     Console.WriteLine("Value = {value}");
            //
            // }

            Console.WriteLine(Request.Form["UnitId"] == "");
            Console.WriteLine(Request.Form["IdPhoto"] == "");
            Console.WriteLine(Request.Form["leasePhoto"] == "");


            if (Request.Form["UnitId"] == "")
            {
                ModelState.AddModelError(string.Empty, "Unit selection is missing.");
            };
            
            if (Request.Form["IdPhoto"] == "")
            {
                ModelState.AddModelError(string.Empty, "ID photo is required and needs to be uploaded.");
            }
            
            if (Request.Form["leasePhoto"] == "")
            {
                ModelState.AddModelError(string.Empty, "Lease agreement photo is required and needs to be uploaded.");
            }

            if (Request.Form["UnitId"] == "" || Request.Form["IdPhoto"] == "" || Request.Form["leasePhoto"] == "")
            {
                return await OnGetAsync();
            }

            if (_context.Tenant != null)
            {
                var user = _context.Tenant.FirstOrDefault(u=>u.Id == User.GetSubjectId());
                if (user == null || _context.VerificationDocuments == null) return NotFound();

                var photoIdDocumentList = _context.VerificationDocuments
                    .Where(m => m.TenantId == user.Id)
                    .Where(m => m.DocumentType == DocumentType.PhotoIdentification)
                    .ToList();

                var leaseDocumentList = _context.VerificationDocuments
                    .Where(m => m.TenantId == user.Id)
                    .Where(m => m.DocumentType == DocumentType.LeaseAgreement)
                    .ToList();
            

                if (user.IsIdVerified)
                {
                    StatusMessage = "Your account is already verified.";
                    return RedirectToPage();
                }
            
            
                if (Request.Form.Files.Count == 2)
                {
                
                    IFormFile idPhoto = Request.Form.Files.GetFile("IdPhoto");
                    IFormFile leasePhoto = Request.Form.Files.GetFile("leasePhoto");
                    ;
                
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


                    try
                    {
                        user.UnitId = Request.Form["UnitId"];
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        Console.WriteLine("DB ERROR");
                    }
                
                
                }
            }

            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
