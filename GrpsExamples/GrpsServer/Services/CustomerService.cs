using Grpc.Core;

namespace GrpsServer.Services;

public sealed class CustomerService : Customer.CustomerBase
{
    private readonly ILogger<CustomerService> _logger;

    public CustomerService(ILogger<CustomerService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override Task<CustomerDataModel> GetCustomerInfo(CustomerFindModel request, ServerCallContext context)
    {
        CustomerDataModel result = new CustomerDataModel();

        // This is a sample code for demo
        // in real life scenarios this information should be fetched from the database
        // no data should be hardcoded in the application
        if (request.UserId == 1)
        {
            result.FirstName = "Mohamad";
            result.LastName = "Lawand";
        }
        else if (request.UserId == 2)
        {
            result.FirstName = "Richard";
            result.LastName = "Feynman";
        }
        else if (request.UserId == 3)
        {
            result.FirstName = "Bruce";
            result.LastName = "Wayne";
        }
        else
        {
            result.FirstName = "James";
            result.LastName = "Bond";
        }

        return Task.FromResult(result);
    }

    public override async Task GetAllCustomers(Unit request, IServerStreamWriter<CustomerDataModel> responseStream, ServerCallContext context)
    {
        var allCustomers = new List<CustomerDataModel>();

        var c1 = new CustomerDataModel();
        c1.FirstName = "Mohamad";
        c1.LastName = "Lawand";
        allCustomers.Add(c1);

        var c2 = new CustomerDataModel();
        c2.FirstName = "Richard";
        c2.LastName = "Feynman";
        allCustomers.Add(c2);

        var c3 = new CustomerDataModel();
        c3.FirstName = "Bruce";
        c3.LastName = "Wayne";
        allCustomers.Add(c3);

        var c4 = new CustomerDataModel();
        c4.FirstName = "James";
        c4.LastName = "Bond";
        allCustomers.Add(c4);

        foreach (var item in allCustomers)
        {
            await responseStream.WriteAsync(item);
        }
    }
}
