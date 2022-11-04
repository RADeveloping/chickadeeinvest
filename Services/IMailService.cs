using System;
using chickadee.Models;

namespace chickadee.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}

