using GemBox.Spreadsheet;
using SendEmailService.Helper;
using SendEmailService.Models;
using System.Reflection;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace SendEmailService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ServiceSettings _serviceSettings;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        private InfoPcFileHelper _infoPcFileHelper;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, IHostApplicationLifetime lifetime)
        {
            _logger = logger;
            _serviceSettings = configuration.GetSection("ServiceSettings").Get<ServiceSettings>();
            _hostApplicationLifetime = lifetime;

            _infoPcFileHelper = new InfoPcFileHelper(_logger);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var sendMailHelper = new SendMailHelper();
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Try to set info pc..");

                try
                {
                    var reprotFilePath = _infoPcFileHelper.CreatExcelFileReportInfo();

                    //TODO: SF Create a order with your exel file in pdx
                    //Send mail with mail kit
                    var isMailsend = sendMailHelper.SendMailSmtp(_serviceSettings.NameFrom,
                                                             _serviceSettings.NameTo,
                                                             _serviceSettings.ToEmail,
                                                             _serviceSettings.FromEmail,
                                                             _serviceSettings.Password,
                                                             _serviceSettings.Subject,
                                                             _serviceSettings.BodyText,
                                                             _serviceSettings.HostServer,
                                                             reprotFilePath);

                    if (isMailsend == false)
                    {
                        _logger.LogError("Error: Mail was not send");
                    }
                    else
                    {
                        _logger.LogInformation("Mail was send succesfull");
                        _hostApplicationLifetime.StopApplication();
                        _logger.LogInformation("The worker service stop");
                    }
                }
                catch (Exception)
                {

                    throw;
                }

                await Task.Delay(TimeSpan.FromMinutes(_serviceSettings.IntervalSetting), stoppingToken);
            }
        }
    }
}