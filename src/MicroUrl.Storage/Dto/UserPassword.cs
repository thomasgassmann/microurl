namespace MicroUrl.Storage.Dto
{
    public class UserPassword
    {
        public byte[] Hash { get; set; }

        public byte[] Salt { get; set; }

        public int Iterations { get; set; }
    }
}
