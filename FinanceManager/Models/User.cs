namespace FinanceManager.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        List <Income> Incomes { get; set; }
        List <Expense> Expenses { get; set; }
    }
}
