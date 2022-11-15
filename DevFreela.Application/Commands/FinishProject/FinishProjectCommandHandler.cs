using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System.Text.Json.Nodes;

namespace DevFreela.Application.Commands.FinishProject
{
    public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, bool>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IPaymentService _paymentService;

        public FinishProjectCommandHandler(IProjectRepository projectRepository, IPaymentService paymentService)
        {
            _projectRepository = projectRepository;
            _paymentService = paymentService;
        }
        public async Task<bool> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);

            var paymentInfoDTO = new PaymentInfoDTO(request.Id, 
                request.CreditCardNumber, 
                request.Cvv, 
                request.ExpiresAt, 
                request.FullName,
                request.Amount);
            
            _paymentService.Process(paymentInfoDTO);
            
            project.SetPaymentPeding();
            await _projectRepository.ChangeStatusAsync(project);
            
            return true;
        }
    }
}
