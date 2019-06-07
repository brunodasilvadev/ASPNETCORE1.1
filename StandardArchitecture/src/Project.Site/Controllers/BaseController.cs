using Architecture.Domain.Core.Interfaces;
using Architecture.Domain.Core.Notifications;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Project.Site.Controllers
{
    public class BaseController : Controller
    {
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;
        private readonly IUser _user;
        public Guid ClienteId { get; set; }

        public BaseController(IDomainNotificationHandler<DomainNotification> notifications,
                              IUser user)
        {
            _notifications = notifications;
            _user = user;

            if (_user.IsAuthenticated())
            {
                ClienteId = _user.GetUserId();
            }
        }

        protected bool OperacaoValida()
        {
            return (!_notifications.HasNotifications());
        }
    }
}
