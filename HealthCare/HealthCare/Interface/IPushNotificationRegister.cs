using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare.Interface
{
    public interface IPushNotificationRegister
    {
        void ExtractTokenAndRegister();

    }
    public interface IPushNotificationService
    {
        void ExtractTokenAndRegister();
    }
    public interface ILogger
    {

    }
}
