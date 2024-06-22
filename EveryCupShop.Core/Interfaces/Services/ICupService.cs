﻿using EveryCupShop.Core.Models;

namespace EveryCupShop.Core.Interfaces.Services;

public interface ICupService
{
    Task<CupAttachment> CreateAttachment(string name, string description, decimal price, int amount);

    Task<CupShape> CreateShape(string name, string description, decimal price, int amount);
}