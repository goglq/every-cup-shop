﻿using System.Text;

namespace EveryCupShop.Core.Configs;

public class JwtConfig
{
    public string SigningKey { get; init; }
    
    public TimeSpan Lifetime { get; init; }
    
    public string Audience { get; init; }
    
    public string Issuer { get; init; }

    public byte[] SigningKeyBytes => Encoding.UTF8.GetBytes(SigningKey);
}