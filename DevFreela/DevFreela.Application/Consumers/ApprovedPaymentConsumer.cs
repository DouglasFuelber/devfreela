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
using System.Threading;
using System.Threading.Tasks;

namespace DevFreela.Application.Consumers
{
    public class ApprovedPaymentConsumer : BackgroundService
    {
        private const string APPROVED_PAYMENTS_QUEUE = "ApprovedPayments";

        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;

        public ApprovedPaymentConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var _factory = new ConnectionFactory { HostName = "127.0.0.1" };

            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                        queue: APPROVED_PAYMENTS_QUEUE,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, eventArgs) =>
            {
                var approvedPaymentBytes = eventArgs.Body.ToArray();
                var approvedPaymentJson = Encoding.UTF8.GetString(approvedPaymentBytes);

                var approvedPayment = JsonSerializer.Deserialize<ApprovedPaymentIntegrationEvent>(approvedPaymentJson);

                await FinishProject(approvedPayment.ProjectId);

                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(APPROVED_PAYMENTS_QUEUE, false, consumer);

            return Task.CompletedTask;
        }

        private async Task FinishProject(int projectId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var projectRepository = scope.ServiceProvider.GetRequiredService<IProjectRepository>();

                var project = await projectRepository.GetByIdAsync(projectId);

                project.Finish();

                await projectRepository.SaveChangesAsync();
            }
        }
    }
}
