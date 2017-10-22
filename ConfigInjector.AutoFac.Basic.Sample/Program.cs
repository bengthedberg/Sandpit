using Autofac;
using ConfigInjector.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ConfigInjector.AutoFac.Basic.Sample
{
    // This will give us a strongly-typed string setting.
    public class SmtpHostConfigurationSetting : ConfigurationSetting<string>
    {
    }

    // This will give us a strongly-typed int setting.
    public class SmtpPortConfigurationSetting : ConfigurationSetting<int>
    {
        protected override IEnumerable<string> ValidationErrors(int value)
        {
            if (value <= 0) yield return "TCP port numbers cannot be negative.";
            if (value > 65535) yield return "TCP port numbers cannot be greater than 65535.";
        }
    }

    public class EmailSender : IEmailSender
    {
        private readonly SmtpHostConfigurationSetting _smtpHost;
        private readonly SmtpPortConfigurationSetting _smtpPort;

        public EmailSender(SmtpHostConfigurationSetting smtpHost,
                           SmtpPortConfigurationSetting smtpPort)
        {
            _smtpHost = smtpHost;
            _smtpPort = smtpPort;
        }

        public void Send(MailMessage message)
        {
            // NOTE the way we can use our strongly-typed settings directly as a string and int respectively
            using (var client = new SmtpClient(_smtpHost, _smtpPort))
            {
                try
                {
                    Console.WriteLine($"SmtpHostConfigurationSetting {_smtpHost} SmtpPortConfigurationSetting {_smtpPort}");
                    client.Send(message);
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }

    public interface IEmailSender
    {
        void Send(MailMessage message);
    }

    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EmailSender>();

            ConfigurationConfigurator.RegisterConfigurationSettings()
                .FromAssemblies(System.Reflection.Assembly.GetExecutingAssembly())
                .RegisterWithContainer(configSetting => builder.RegisterInstance(configSetting)
                                                                                    .AsSelf()
                                                                                    .SingleInstance())
                .DoYourThing();

            Container = builder.Build();

            SendMail();
        }

        public static void SendMail()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var mailer = scope.Resolve<EmailSender>();
                mailer.Send(new MailMessage(@"From@mail.com", @"To@mail.com",@"Subject", @"Body"));

                var port = scope.Resolve<SmtpPortConfigurationSetting>();
                var host = scope.Resolve<SmtpHostConfigurationSetting>();
                Console.WriteLine($"SmtpHostConfigurationSetting {host} SmtpPortConfigurationSetting {port}");
            }
        }
    }
}
