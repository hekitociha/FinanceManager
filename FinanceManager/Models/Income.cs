namespace FinanceManager.Models
{
    public class Income
    {
        public int Id { get; set; }
        public string FromWhere { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
    }
}
