using SimpleCRM.Application.Common.Contracts.DTOs;
using SimpleCRM.Domain.Common.Enums;

namespace SimpleCRM.Application.Backoffice.Contracts.DTOs;

public class OrderSearchRQ : BaseSearchRQ
{
    public OrderState OrderState { get; set; }
}