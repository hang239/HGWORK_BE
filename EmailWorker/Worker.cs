using HGWork.Model;
using HGWork.Service.Interfaces;
using System.Xml.Linq;
using Task = System.Threading.Tasks.Task;

namespace EmailWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public Worker(ILogger<Worker> logger, ITaskService taskService, IUserService userService, IEmailService emailService)
        {
            _logger = logger;
            _taskService = taskService;
            _userService = userService;
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var tasks = await _taskService.GetTaskEndDate();
                var userIDs = tasks.Select(task => task.UserId).ToList();
                var resUsers = await _userService.GetAll();

                if (resUsers.StatusCode == 200)
                {
                    var users = resUsers.Data;
                    if (users != null && users.Count > 0)
                    {
                        var usersReceiveEmails = users.Where(user => userIDs.Contains(user.Id)).ToList();
                        if (usersReceiveEmails != null && usersReceiveEmails.Count > 0)
                        {
                            foreach (var user in usersReceiveEmails)
                            {
                                var taskForUser = tasks.Where(task => task.UserId == user.Id).Select(task => task.Name).ToList();
                                string contentEmail = string.Format(@"
                                <p>Thông báo công việc sắp hết hạn từ HGWork</p>
                                <p>Hệ thống thông báo HGWork<p>
                                <p>Các task sắp đến hạn: <b>{0}</b></p>
                                <p>Chúng tôi gửi thông báo này tới bạn để thông báo cho bạn các công việc sắp hết hạn.</p>
                                ", string.Join(",", taskForUser));

                                var email = new Email()
                                {
                                    From = "",
                                    To = user.Email,
                                    EmailContent = contentEmail,
                                    Subject = "HGWork thông báo công việc sắp đến hạn hoàn thành"
                                };

                                _ = _emailService.SendMail(email);
                            }
                        }
                    }
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}