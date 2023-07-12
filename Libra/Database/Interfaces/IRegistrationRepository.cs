using Libra.Models;
using Libra.Models.RegistrationModels;

namespace Libra.Database.Interfaces;

public interface IRegistrationRepository
{
    public Registration CreateRegistration(Registration registration);
    public Registration? GetByUserId(Guid id);
}