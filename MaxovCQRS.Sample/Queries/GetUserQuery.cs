using MaxovCQRS.Common.Primitives;
using MaxovCQRS.Sample.Dto;

namespace MaxovCQRS.Sample.Queries
{
    public class GetUserQuery : IQuery<UserDto>
    {
        public int Id { get; private set; }

        public GetUserQuery(int id)
        {
            /* validation */
            Id = id;
        }
    }
}