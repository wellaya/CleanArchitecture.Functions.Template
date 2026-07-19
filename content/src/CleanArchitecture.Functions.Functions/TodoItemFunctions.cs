using System.Net;
using CleanArchitecture.Functions.Application.TodoItems.Commands.CreateTodoItem;
using CleanArchitecture.Functions.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace CleanArchitecture.Functions.Functions;

public class TodoItemFunctions(ISender sender)
{
    [Function(nameof(CreateTodoItem))]
    public async Task<HttpResponseData> CreateTodoItem(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "todo-items")] HttpRequestData req,
        CancellationToken ct)
    {
        var command = await req.ReadFromJsonAsync<CreateTodoItemCommand>(ct)
            ?? throw new ArgumentException("Invalid request body");

        var id = await sender.Send(command, ct);

        var res = req.CreateResponse(HttpStatusCode.Created);
        res.Headers.Add("Location", $"/api/todo-items/{id}");
        await res.WriteAsJsonAsync(new { id }, ct);
        return res;
    }

    [Function(nameof(GetTodoItems))]
    public async Task<HttpResponseData> GetTodoItems(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "todo-lists/{listId}/todo-items")] HttpRequestData req,
        int listId, CancellationToken ct)
    {
        var query = new GetTodoItemsWithPaginationQuery { ListId = listId };
        var result = await sender.Send(query, ct);

        var res = req.CreateResponse(HttpStatusCode.OK);
        await res.WriteAsJsonAsync(result, ct);
        return res;
    }
}