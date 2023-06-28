using clickfree_Maui.Contracts.Services;
using clickfree_Maui.Helpers;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace clickfree_Maui;

public partial class App : Application
{
   
    public App(INavigationService navigationService)
    {
        InitializeComponent();
        MainPage = new NavigationPage();
        navigationService.NavigateToMainPage();
    //    AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
       // this.Dispatcher.Dispatch. += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(Application_DispatcherUnhandledException);
    }

    private void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
    {
        try
        {
            
            var smtpServer = ConfigurationManager.AppSettings["SmtpId"];
            var port = ConfigurationManager.AppSettings["Port"];
            var enableSsl = true;
            var smtpUsername = ConfigurationManager.AppSettings["UserId"];
            var smtpPassword = ConfigurationManager.AppSettings["Password"];

            var smtpClient = new SmtpClient(smtpServer, Convert.ToInt32(ConfigurationManager.AppSettings["Port"]));
            //smtpClient.Timeout = 60000;
            smtpClient.EnableSsl = enableSsl;
            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

            // Create a MailMessage object
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserId"]);
            mailMessage.To.Add(ConfigurationManager.AppSettings["receiver"]);
            mailMessage.CC.Add(ConfigurationManager.AppSettings["receiver2"]);
            //mailMessage.CC.Add(ConfigurationManager.AppSettings["receiver3"]);
            // mailMessage.CC.Add("michaelw@clearview.tv");
            mailMessage.Subject = "Error occurred on ClickFree Maui";
            //mailMessage.Body = exceptionMessage;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = @"<!DOCTYPE html><html>
                     <head><HTML Codes by Quackit.com-- ><title></title>
                         <meta name = 'viewport' content = 'width=device-width, initial-scale=1'>  
                      <style>   body { background -background-repeat:no-repeat;background-position:0 left;background-attachment:fixed;}
                             h4{ font - family:Arial, sans - serif; color:#000000;background-color:#ffffff;} 
                             p{ font - family:Georgia, serif; font - size:14px; font - style:Italic; font - weight:normal}
                       </style>
                     </head>
                        <body>
                                <h4>Exception Message</h4> <p> " + e.Exception.Message + " </p> </br>   <h4> Details: There is an error on  : "+ e.Exception.Source +"-</h4>   </br> <p> Thanks </p> " +
                            "</body></html>";



            smtpClient.Send(mailMessage);



        }
        catch (Exception ex)
        {
           // MessageBox.Show("Failed to send email notification. Exception Message: " + ex.Message);
        }

        
    }

 
    protected override Window CreateWindow(IActivationState activationState)
    {
        bool ifDrive = DriveManager.HasUsbDrives;
        var window = base.CreateWindow(activationState);

        if (window != null)
        {
          
            window.Title = "Click Free";
           
        }
        
        window.Destroying += Window_Destroying; 
        return window;
        
    }

    private void Window_Destroying(object sender, EventArgs e)
    {
        var DiskInfo = DriveManager.GetAvailableDisks().FirstOrDefault();
        var drives = DriveInfo.GetDrives().Where(d => d.IsReady & d.DriveType == DriveType.Removable);
        if (DiskInfo != null)
        {
            string status = DriveManager.DriverWindow.EjectPenDrive(Convert.ToChar(drives.First().Name.Replace(":\\", "")));

        }
    }

    //protected override Window 
  
}
