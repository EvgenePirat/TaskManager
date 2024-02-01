namespace BusinessLayer.Models.Users.Response
{
    /// <summary>
    /// Model class for hold part information about user for output on screen
    /// </summary>
    public class ApplicationUserModel
    {
        public Guid Id { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
