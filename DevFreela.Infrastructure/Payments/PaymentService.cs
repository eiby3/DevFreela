using DevFreela.Core.DTOs;
using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IMessageBusService _messageBus;
        private const string QUEUE_NAME = "Payments";

        public PaymentService(IMessageBusService messageBus)
        {
            _messageBus = messageBus;
        }

        public void Process(PaymentInfoDTO paymentInfoDTO)
        {
            var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);
            //transformar infoJson em bytes
            var paymentsInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);

            _messageBus.Publish(QUEUE_NAME, paymentsInfoBytes);
        }
    }
}
