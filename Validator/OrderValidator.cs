﻿using FluentValidation;
using ToTable.Contract;
using ToTable.Models;

namespace ToTable.Validator;

public class OrderValidator : AbstractValidator<OrderDto>
{
    public OrderValidator()
    {
        RuleFor(x => x.OrderTime).NotNull();
        RuleFor(x => x.OrderStatus).IsInEnum().WithMessage("Put 0-New or 1-InProgress or 2-Done");
        RuleFor(x => x.WaiterId).Must(x => x is int or null).WithMessage("Must be int or null");
        RuleFor(x => x.TableId).NotNull().GreaterThan(0).Must(x => x is int).WithMessage("Must be int");
        RuleFor(x => x.RestaurantId).NotNull().GreaterThan(0).Must(x => x is int).WithMessage("Must be int");
        RuleFor(x => x.OrderComment).Must(x => x is string or null).WithMessage("Must be string or null");
        RuleFor(x => x.PaymentMethod).Must(x => x is string or null).WithMessage("Must be string or null");
        

        
    }
}