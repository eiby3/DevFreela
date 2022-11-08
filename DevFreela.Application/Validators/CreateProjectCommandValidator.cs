using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.UpdateProject;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Validators
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(_ => _.Description)
                .MaximumLength(255)
                .WithMessage("O tamanho máximo de Descrição é de 255 caracteres!");

            RuleFor(_ => _.Title)
                .MinimumLength(30)
                .WithMessage("O tamanho máximo de Titulo é de 30 caracteres!");
        }
    }
}
