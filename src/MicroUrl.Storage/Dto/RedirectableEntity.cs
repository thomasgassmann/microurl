namespace MicroUrl.Storage.Dto
{
    using System;

    public abstract class RedirectableEntity
    {
        public string Key { get; set; }

        public DateTime Created { get; set; }

        public bool Enabled { get; set; }
    }
}