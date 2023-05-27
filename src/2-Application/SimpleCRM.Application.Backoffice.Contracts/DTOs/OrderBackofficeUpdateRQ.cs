using SimpleCRM.Application.Common.Contracts.DTOs;
using SimpleCRM.Domain.Common.Enums;

namespace SimpleCRM.Application.Backoffice.Contracts.DTOs;

public class OrderBackofficeUpdateRQ : BaseRQ
{
    public OrderState OrderState { get; set; }
}