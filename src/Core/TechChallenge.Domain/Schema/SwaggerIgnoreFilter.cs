﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.Json.Serialization;

namespace TechChallenge.Domain.Schema;

public class SwaggerIgnoreFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext schemaFilterContext)
    {
        if (schema.Properties.Count == 0)
        {
            return;
        }
        var properties = schemaFilterContext.Type.GetProperties();
        var excludedList = properties
            .Where(m => m.GetCustomAttribute<SwaggerIgnoreAttribute>() is not null)
            .Select(m => m.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? m.Name.ToCamelCase());
        foreach (var excludedName in excludedList)
        {
            schema.Properties.Remove(excludedName);
        }
    }
}
