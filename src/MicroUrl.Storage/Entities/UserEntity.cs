namespace MicroUrl.Storage.Entities
{
    using MicroUrl.Storage.Abstractions;
    using System;

    [EntityName("users")]
    internal class UserEntity
    {
        [Key(KeyType.StringId)]
        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public DateTime Created { get; set; }
    }
}
