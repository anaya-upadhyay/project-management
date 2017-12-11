using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManagement.Api.Commands;
using ProjectManagement.Api.Handlers.Core;
using ProjectManagement.Dal;
using ProjectManagement.Domain;

namespace ProjectManagement.Api.Handlers
{
    /// <summary>
    ///     Validate the Create Project command before is executed
    /// </summary>
    public sealed class CanCreateProject : IValidationHandler<CreateProjectCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public CanCreateProject(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<ValidationResult> Validate(CreateProjectCommand command)
        {
            var repository = unitOfWork.CreateRepository();
            var donor = repository.Get<DonorAggregate>()
                .SingleOrDefault(x => x.Id == command.DonorId);
            var analyst = repository.Get<Analyst>()
                .SingleOrDefault(x => x.Id == command.AnalystId);

            if (donor == null)
            {
                yield return new ValidationResult("Donor", "The Donor does not exists");
            }

            if (analyst == null)
            {
                yield return new ValidationResult("Analyst", "The Analyst does not exist");
            }
        }
    }
}