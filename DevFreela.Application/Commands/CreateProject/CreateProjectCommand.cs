using MediatR;

namespace DevFreela.Application.Commands.CreateProject
{
    public class CreateProjectCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int idClient { get; set; }
        public int idFreelancer { get; set; }
        public decimal TotalCost { get; set; }
    }
}
