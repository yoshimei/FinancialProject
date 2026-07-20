using Microsoft.Data.SqlClient;

namespace FinancialProject.Data.Infrastructure;

public interface ISqlConnectionFactory
{
    SqlConnection CreateConnection();
}
