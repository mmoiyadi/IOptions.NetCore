using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Options.NetCore.Services.Interfaces
{
    public interface IEmailService
    {
        void Send(string report);
        void SendAdmin(string text);
    }
}
