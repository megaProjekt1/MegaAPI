using MegaProjekt.Core.DTO;

namespace MegaProjekt.Core.Entities
{
    public class UserManagerResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public AuthenticationResponse AuthenticationResponse { get; set; }
    }
}
