using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoseShop.InterfaceRepositories
{
    public interface IEmailSender
    {
        void SendEmail(string email, string subject, string message);
    }
}
