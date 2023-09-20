namespace Discussion.BLL.Services.Interfaces;

public interface IEmailService
{
    /// <summary>
    /// Send an Email that confirm's the Registration was successful.
    /// </summary>
    /// <param name="to">Email of the User to whom we send registration confirmation.</param>
    void SendRegistrationEmail(string to); 

    /// <summary>
    /// Send an Email that confirm's the Account has been deleted successfully.
    /// </summary>
    /// <param name="to">Email of the User to whom we send the confirmation.</param>
    void SendAccountDeleteEmail(string to);
}
