namespace Discussion.BLL.Services.Interfaces;

public interface IEmailService
{
    /// <summary>
    /// Send an Email that confirm's the Registration was successful.
    /// </summary>
    /// <param name="to">Email of the User to whom we send registration confirmation.</param>
    void SendRegistrationEmail(string to);
}
