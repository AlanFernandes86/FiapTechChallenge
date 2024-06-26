﻿namespace TechChallenge.Domain.Entities;

public class ProductOnOrder
{
    public int? Id { get; set; }
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }
}
