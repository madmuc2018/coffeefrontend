namespace coffeefrontend
{
    public class ErrorResp : ToStringBase
    {
        public string message { get; set; }

        public override string ToString()
        {
            return message;
        }
    }
}