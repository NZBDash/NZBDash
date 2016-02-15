#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: EmailSender.cs
//   Created By: Jamie Rees
//  
//   Permission is hereby granted, free of charge, to any person obtaining
//   a copy of this software and associated documentation files (the
//   "Software"), to deal in the Software without restriction, including
//   without limitation the rights to use, copy, modify, merge, publish,
//   distribute, sublicense, and/or sell copies of the Software, and to
//   permit persons to whom the Software is furnished to do so, subject to
//   the following conditions:
//  
//   The above copyright notice and this permission notice shall be
//   included in all copies or substantial portions of the Software.
//  
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//   EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//   MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//   NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//   LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//   OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//   WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ************************************************************************/
#endregion
using System.Net;
using System.Net.Mail;

using HtmlAgilityPack;

using NZBDash.Common.Interfaces;
using NZBDash.Core.Interfaces;
using NZBDash.Services.HardwareMonitor.Email_Templates;
using NZBDash.Services.HardwareMonitor.Interfaces;

using RazorEngine;
using RazorEngine.Templating;

namespace NZBDash.Services.HardwareMonitor.Notification
{
    public class EmailSender
    {
        public EmailSender(IFile file, ILogger logger, ISmtpClient client)
        {
            File = file;
            Logger = logger;
            Client = client;
        }
        private IFile File { get; set; }
        private ILogger Logger { get; set; }
        private ISmtpClient Client { get; set; }
        public void SendEmail(EmailModel model)
        {
            var body = GenerateHtmlTemplate(model);
            var message = new MailMessage
            {
                To = { model.Address },
                From = new MailAddress("nzbdash@nzbdash.com", "NZBDash StartAlert"),
                IsBodyHtml = true,
                Body = body,
                Subject = $"NZBDash Monitor {model.BreachType} Alert!" };
            var creds = new NetworkCredential(model.Username, model.Password);
            Client.Send(model.Host, model.Port, message, creds);
            Logger.Info("Email: { To: {0}, Subject: {1}, Host: {2}, Port: {3} };", model.Address, message.Subject, model.Host, model.Port);
        }

        private string GenerateHtmlTemplate(EmailModel model)
        {
            var template = string.Empty;
            template = File.ReadAllText(EmailResource.Email);
            var document = new HtmlDocument();
            document.LoadHtml(template);

            template = document.DocumentNode.OuterHtml;

            template = RemoveLineBreaks(template);

            template = Engine.Razor.RunCompile(template, model.BreachType, null, model);

            return template;
        }

        private static string RemoveLineBreaks(string text)
        {
            var newRegex = new System.Text.RegularExpressions.Regex("\\r\\n");
            text = newRegex.Replace(text, string.Empty);
            return text.Replace("\\", "\"");
        }
    }
}
