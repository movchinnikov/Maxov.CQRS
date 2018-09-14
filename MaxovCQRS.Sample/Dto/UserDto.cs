namespace MaxovCQRS.Sample.Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public override string ToString()
        {
            return $"{LastName} {FirstName} {MiddleName} (id: {Id})";
        }
    }
}