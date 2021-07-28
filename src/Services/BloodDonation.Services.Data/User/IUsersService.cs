namespace BloodDonation.Services.Data.User
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BloodDonation.Data.Models;

    public interface IUsersService
    {
        ApplicationUser GetUserByUsername(string username);

        Task AddQuestionsAnswersToUser(QuestionAnswer questionAnswer, ApplicationUser user);

        bool AddUserToRole(string username, string role);

        bool RemoveUserFromRole(string name, string role);

        Recipient GetCurrentSignedInRecipient(string username);

        IEnumerable<ApplicationUser> GetUsersByRole(string role);
    }
}
