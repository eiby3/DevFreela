using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System.Text.Json.Nodes;

namespace DevFreela.Application.Commands.FinishProject
{
    public class FinishProjectCommandHandler : IRequestHandler<FinishProjectCommand, ServiceInfoDTO>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IPaymentService _paymentService;

        public FinishProjectCommandHandler(IProjectRepository projectRepository, IPaymentService paymentService)
        {
            _projectRepository = projectRepository;
            _paymentService = paymentService;
        }
        public async Task<ServiceInfoDTO> Handle(FinishProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id);
            project.Finish();

            var paymentInfoDTO = new PaymentInfoDTO(request.Id, 
                request.CreditCardNumber, 
                request.Cvv, 
                request.ExpiresAt, 
                request.FullName,
                request.Amount);

            var result = await _paymentService.Process(paymentInfoDTO);

            if (!result.sucess)
                project.SetPaymentPeding();
            await _projectRepository.ChangeStatusAsync(project);
            

            return result;
        }
    }
}
