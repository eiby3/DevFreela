using DevFreela.Core.IntegrationEvents;
using DevFreela.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DevFreela.Application.Consumers
{
    public class PaymentsApprovedConsumer : BackgroundService
    {
        private const string PAYMENTS_APPROVED_QUEUE = "PaymentsApproved";


        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;

        public PaymentsApprovedConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
               queue: PAYMENTS_APPROVED_QUEUE,
               durable: false,
               exclusive: false,
               autoDelete: false,
               arguments: null
               );
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, eventArgs) =>
            {
                var paymentApprovedBytes = eventArgs.Body.ToArray();
                var paymentApprovedJson = Encoding.UTF8.GetString(paymentApprovedBytes);

                var paymentApprovedIntegrationEvent = JsonSerializer.Deserialize<PaymentsApprovedIntegrationEvent>(paymentApprovedJson);

                await FinishProject(paymentApprovedIntegrationEvent.IdProject);

                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };
            _channel.BasicConsume(PAYMENTS_APPROVED_QUEUE, false, consumer);

            return Task.CompletedTask;
        }

        private async Task FinishProject(int id)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var projectRepository = scope.ServiceProvider.GetRequiredService<IProjectRepository>();
                var project = await projectRepository.GetByIdAsync(id);

                project.Finish();

                await projectRepository.SaveChangesAsync();
            }
        }
    }
}
