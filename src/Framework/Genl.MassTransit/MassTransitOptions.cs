﻿namespace Genl.MassTransit;

public class MassTransitOptions
{
    public string? Host { get; set; }
    public string? VirtualHost { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? ServiceName { get; set; }
}