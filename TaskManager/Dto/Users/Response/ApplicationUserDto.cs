namespace TaskManager.Dto.Users.Response
{
    /// <summary>
    /// Dto class for hold part information about user for return on page
    /// </summary>
    public class ApplicationUserDto
    {
        public Guid Id { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
