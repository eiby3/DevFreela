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
        private readonly IHttpClientFactory _httpClient;
        private readonly string _paymentsBaseUrl;
        public PaymentService(IHttpClientFactory httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _paymentsBaseUrl = configuration.GetSection("Services:Payments").Value;
        }
        public async Task<ServiceInfoDTO> Process(PaymentInfoDTO paymentInfoDTO)
        {
            var url = $"{_paymentsBaseUrl}/api/payments";
            var paymentInfoJson = JsonSerializer.Serialize(paymentInfoDTO);
            var paymentInfoContent = new StringContent(
                paymentInfoJson,
                Encoding.UTF8,
                "application/json");

            var httpClient = _httpClient.CreateClient("Payments");
            var response = await httpClient.PostAsync(url, paymentInfoContent);

            var responseJson = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<ServiceInfoDTO>(responseJson);

            return responseObject;
        }
    }
}
