namespace BusinessManagement.Entities.DTOs
{
    public class AccountGetByAccountGroupCodesDto
    {
        public int BusinessId { get; set; }

        public string[] AccountGroupCodes { get; set; }
    }
}
