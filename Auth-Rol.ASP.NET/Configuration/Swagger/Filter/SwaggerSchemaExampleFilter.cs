﻿using Auth_Rol.ASP.NET.Configuration.Swagger.Attributes;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Auth_Rol.ASP.NET.Configuration.Swagger.Filter
{
    public class SwaggerSchemaExampleFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.MemberInfo != null)
            {
                var schemaAttribute = context.MemberInfo.GetCustomAttributes<SwaggerSchemaExampleAttribute>().FirstOrDefault();

                if (schemaAttribute != null) ApplySchemaAttribute(schema, schemaAttribute);
            }
        }

        private void ApplySchemaAttribute(OpenApiSchema schema, SwaggerSchemaExampleAttribute schemaAtribute)
        {
            schema.Example = new Microsoft.OpenApi.Any.OpenApiString(schemaAtribute.Example);
        }
    }
}
