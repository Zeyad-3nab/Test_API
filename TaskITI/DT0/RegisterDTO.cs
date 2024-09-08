using System.ComponentModel.DataAnnotations;

namespace TaskITI.DT0
{
    public class RegisterDTO
    {
        [MaxLength(30)]
        public string FirstName { get; set; } = null!;


        [MaxLength(30)]
        public string LastName { get; set; } = null!;

        [MaxLength(60)]
        public string UserName { get; set; } = null!;


        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;



        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
