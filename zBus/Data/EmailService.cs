using System;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using zBus.GLobal;

public class EmailService
{
    public async Task SendRegistrationEmailAsync(string to, string username)
    {
        string GmailAddress = "winformapp010@gmail.com";
        string AppSpecificPassword = "qdsqwsnazpxajhes";
        string subject = "Welcome to ZBUS!";

        StringBuilder bodyBuilder = new StringBuilder();
        bodyBuilder.AppendLine("<img src=\"cid:photo\" alt=\"Photo\" /><br/><br/>"); // Image as a label
        bodyBuilder.AppendLine($"Dear {username},<br/><br/>");
        bodyBuilder.AppendLine("Thank you for registering with ZBUS. Your account has been successfully created.<br/><br/>");
        bodyBuilder.AppendLine("Best regards,<br/><br/>");
        bodyBuilder.AppendLine("ZBUS - Sherif Elglaly - Cgmoreda<br/><br/>");
        bodyBuilder.AppendLine("01026386402<br/><br/>");

        string body = bodyBuilder.ToString();
        try
        {
            using (var client = new SmtpClient("smtp.gmail.com"))
            {
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(GmailAddress, AppSpecificPassword);

                var message = new MailMessage(GmailAddress, to, subject, body);
                message.IsBodyHtml = true;

                // Attach the image as a linked resource
                LinkedResource imageResource = new LinkedResource("C:/Users/sheri/OneDrive/سطح المكتب/ZBUS/zBus/wwwroot/imgs/bus.png", "image/png");
                imageResource.ContentId = "photo";
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                htmlView.LinkedResources.Add(imageResource);
                message.AlternateViews.Add(htmlView);

                await client.SendMailAsync(message);
            }
        }
        catch (Exception ex)
        {
            // Log or handle the exception
            Console.WriteLine($"Failed to send registration email: {ex.Message}");
            throw; // Rethrow the exception or handle it as needed
        }
    }

    public async Task SendPasswordChangeEmailAsync(string to, string username)
    {
        string GmailAddress = "winformapp010@gmail.com";
        string AppSpecificPassword = "qdsqwsnazpxajhes";
        Random random = new Random();
        GlobalVariables.Code = random.Next(1000, 10000).ToString();
        string subject = "Password Change Confirmation";

        // Create the linked resource
        LinkedResource busImage = new LinkedResource("C:/Users/sheri/OneDrive/سطح المكتب/ZBUS/zBus/wwwroot/imgs/bus.png", "image/png");
        busImage.ContentId = "photo"; // Content-ID reference used in the HTML body

        // Create the HTML body
        StringBuilder bodyBuilder = new StringBuilder();
        bodyBuilder.AppendLine("<div style=\"font-size: 14px; font-family: Arial, sans-serif; line-height: 1.6;\">"); // Apply font size, font family, and line height
        bodyBuilder.AppendLine($"<img src=\"cid:photo\" alt=\"Photo\" /><br/><br/>"); // Image as a label
        bodyBuilder.AppendLine($"Dear {username},<br/><br/>");
        bodyBuilder.AppendLine("We hope this email finds you well. Your request to change the password for your account on the ZBUS app has been received.<br/><br/>");
        bodyBuilder.AppendLine($"To proceed with the password change, please use the following authentication code:<br/><br/>");
        bodyBuilder.AppendLine($"<span style=\"color: red;\">Authentication Code: {GlobalVariables.Code}</span><br/><br/>");
        bodyBuilder.AppendLine("Please enter this code within the ZBUS app to verify your identity and initiate the password change process.<br/><br/>");
        bodyBuilder.AppendLine("If you did not make this request, or if you have any concerns regarding your account security, please contact our support team immediately at winformapp010@gmail.com.<br/><br/>");
        bodyBuilder.AppendLine("Thank you for choosing ZBUS. We are committed to ensuring the security of your account.<br/><br/>");
        bodyBuilder.AppendLine("Best regards,<br/><br/>");
        bodyBuilder.AppendLine("ZBUS - Sherif Elglaly - cgmoreda<br/><br/>");
        bodyBuilder.AppendLine("01026386402<br/><br/>");
        bodyBuilder.AppendLine("</div>");


        string body = bodyBuilder.ToString();

        try
        {
            using (var client = new SmtpClient("smtp.gmail.com"))
            {
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(GmailAddress, AppSpecificPassword);

                var message = new MailMessage(GmailAddress, to, subject, body);

                // Add the linked resource to the alternate views
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                htmlView.LinkedResources.Add(busImage);
                message.AlternateViews.Add(htmlView);

                await client.SendMailAsync(message);
            }
        }
        catch (Exception ex)
        {
            // Log or handle the exception
            Console.WriteLine($"Failed to send password change email: {ex.Message}");
            throw; // Rethrow the exception or handle it as needed
        }
    }


}

