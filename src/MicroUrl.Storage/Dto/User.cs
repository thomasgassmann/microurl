namespace MicroUrl.Storage.Dto
{
    using System;

    public class User
    {
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public DateTime Created { get; set; }
    }
}
