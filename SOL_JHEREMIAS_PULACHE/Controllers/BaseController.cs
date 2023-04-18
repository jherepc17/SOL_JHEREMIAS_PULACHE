using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static SOL_JHEREMIAS_PULACHE.Models.EnumAlert;

namespace SOL_JHEREMIAS_PULACHE.Controllers
{
    public static class Alerts
    {
        public static void Alert(Controller controller, string message, NotificationType notificationType)
        {
            var msg = "<script language='javascript'>Swal.fire('" + notificationType.ToString().ToUpper() + "', '" + message + "','" + notificationType + "')" + "</script>";
            controller.TempData["notification"] = msg;
        }

        public static void Message(Controller controller, string message, NotificationType notifyType)
        {
            controller.TempData["Notification2"] = message;

            switch (notifyType)
            {
                case NotificationType.success:
                    controller.TempData["NotificationCSS"] = "alert-box success";
                    break;
                case NotificationType.error:
                    controller.TempData["NotificationCSS"] = "alert-box errors";
                    break;
                case NotificationType.warning:
                    controller.TempData["NotificationCSS"] = "alert-box warning";
                    break;

                case NotificationType.info:
                    controller.TempData["NotificationCSS"] = "alert-box notice";
                    break;
            }
        }
    }

}