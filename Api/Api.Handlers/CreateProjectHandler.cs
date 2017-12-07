using System;
using System.Diagnostics;
using System.Linq;
using ProjectManagement.Api.Commands;
using ProjectManagement.Api.Handlers.Core;
using ProjectManagement.Dal;
using ProjectManagement.Domain;
using ProjectManagement.Utils.Extensions;

namespace ProjectManagement.Api.Handlers
{
    /// <summary>
    ///     Handles the Create Project command by validating the command and creating a new Project
    /// </summary>
    public sealed class CreateProjectHandler : ICommandHandler<CreateProjectCommand>
    {
        /// <summary>
        ///     The current Unit of Work used to persist the data
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        ///     Create a new Command Handler by injecting an Unit of Work
        /// </summary>
        /// <param name="unitOfWork">An instance of IUnitOfWork</param>
        public CreateProjectHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     Create a new Project Root Aggregate
        /// </summary>
        /// <param name="command">The Command used to provide information about the Project that will be created</param>
        /// <returns>Returns an <see cref="ICommandResult" /> according to the execution result</returns>
        public ICommandResult Execute(CreateProjectCommand command)
        {
            using (var tx = unitOfWork.BeginTransaction())
            {
                try
                {
                    // hydrate the domain
                    var repository = unitOfWork.CreateRepository();
                    var donor = repository.Get<DonorAggregate>().Single(x => x.Id == command.DonorId);
                    var analyst = repository.Get<Analyst>().Single(x => x.Id == command.AnalystId);
                    var typeOfProject = command.ProjectType.ToEnum(TypeOfProject.None);
                    var typeOfTenderProcess = command.TenderProcessType.ToEnum(TypeOfTenderProcess.None);

                    var project = new ProjectAggregate(donor, analyst, command.Acronym, typeOfProject,
                        typeOfTenderProcess, command.StartDate);
                    repository.Add(project);

                    tx.Commit();

                    return new SimpleCommandResult(true);
                }
                catch (Exception ex)
                {
                    tx.Rollback();

                    Trace.TraceError("An error occurred while executing CreateProjectCommand. {0}", ex);
                    return new SimpleCommandResult(false, ex.Message);
                }
            }
        }
    }
}