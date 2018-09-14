using MaxovCQRS.Common.Primitives;

namespace MaxovCQRS.Sample.Commands
{
    public class CreateUserCommand : ICommand
    {
        public int Id { get; set; }

        public string Login { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string MiddleName { get; private set; }

        public string Password { get; private set; }

        public CreateUserCommand(string login, string firstName, string middleName, string lastName, string password)
        {
            /* validation */

            Login = login;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Password = password;
        }
    }
}