using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace grpc_server
{
    public class BirthdayGreeterService : BirthdayGreeter.BirthdayGreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public BirthdayGreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<BirthdayMessage> GreetWithBirthdayMessage(PersonInfo request, ServerCallContext context)
        {
            var message = "";
            DateTime birthday = default(DateTime);
            DateTime today = DateTime.Now.Date;
            if (DateTime.TryParse(request.Birthday, out birthday))
            {
                var birthdayDate = birthday.Date;
                var yearsBetweenDates = today.Year - birthdayDate.Year;
                message = "Hello " + request.Name + "!";
                if (birthdayDate.AddYears(yearsBetweenDates).CompareTo(today) == 0)
                {
                    message += " Happy birthday!!!";
                }
                else
                {
                    
                    var daysBetweenDates = birthdayDate.AddYears(yearsBetweenDates).Subtract(today).TotalDays;
                    var daysUntilBirthday = daysBetweenDates < 0 ? today.AddYears(1).DayOfYear + daysBetweenDates : daysBetweenDates;
                    message += " Can't wait to wish you a happy birthday in " + daysUntilBirthday;
                    message += daysUntilBirthday > 1 ? " days!" : " day!";
                }
            }
            else
            {
                message = "Oops! Couldn't figure out your birthday " + request.Name + "!";
            }
            
            return Task.FromResult(new BirthdayMessage
            {
                Message = message
            });
        }
    }
}
