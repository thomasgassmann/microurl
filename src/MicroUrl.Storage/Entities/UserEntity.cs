namespace MicroUrl.Storage.Entities
{
    using System;
    using MicroUrl.Storage.Abstractions;

    [EntityName("users")]
    internal class UserEntity
    {
        [Key(KeyType.StringId)]
        public string UserName { get; set; }

        [ExcludeFromIndexes]
        public byte[] PasswordHash { get; set; }

        [ExcludeFromIndexes]
        public byte[] PasswordSalt { get; set; }

        public int PasswordIterations { get; set; }

        public DateTime Created { get; set; }
    }
}
