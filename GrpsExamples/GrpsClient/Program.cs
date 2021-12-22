using Grpc.Core;
using Grpc.Net.Client;
using GrpsClient;

// We create a channel that represents the connection from client to the server
// The URL that we add is provided from Kestrel in the server
var channel = GrpcChannel.ForAddress("https://localhost:7042");

// this the strongly typed client that was create for us from code generation
// when we added the .proto file
var greeterClient = new Greeter.GreeterClient(channel);

var greeterResponse = await greeterClient.SayHelloAsync(new HelloRequest { Name = "Ivan" });

Console.WriteLine($"The response from server: '{greeterResponse}'");

var customerClient = new Customer.CustomerClient(channel);

var customerResponse = await customerClient.GetCustomerInfoAsync(new CustomerFindModel { UserId = 1 });

Console.WriteLine($"The response from server: First Name is '{customerResponse.FirstName}', Last Name is '{customerResponse.LastName}'");

var customerCall = customerClient.GetAllCustomers(new Unit());

Console.WriteLine("Making a stream request to CustomerService...");

await foreach (var customer in customerCall.ResponseStream.ReadAllAsync())
{
    Console.WriteLine($"Customer: {customer}");
}

Console.ReadLine();