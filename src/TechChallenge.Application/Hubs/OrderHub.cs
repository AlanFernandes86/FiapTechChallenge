using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using TechChallenge.Application.Common.UseCase.Models;
using TechChallenge.Domain.Enums;
using TechChallenge.Domain.Options;
using TechChallenge.Domain.Repositories;

namespace TechChallenge.Application.Hubs;

public class OrderHub : Hub
{
    public OrderHub()
    {
    }    
}
