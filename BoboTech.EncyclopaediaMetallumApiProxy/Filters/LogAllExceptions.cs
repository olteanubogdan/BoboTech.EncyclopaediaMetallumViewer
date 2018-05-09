﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using static System.FormattableString;

namespace BoboTech.EncyclopaediaMetallumApiProxy.Filters
{
    public class LogAllExceptions : IExceptionFilter
    {
        ILogger<LogAllExceptions> _logger;

        public LogAllExceptions(ILogger<LogAllExceptions> logger) => _logger = logger;

        public void OnException(ExceptionContext context)
        {
            var errorId = Invariant($"{DateTime.Now:yyyyMMddHHmmss}");
            _logger.LogError($"ErrorId is {errorId}. {context.Exception}");
            context.ExceptionHandled = true;
            context.HttpContext.Response.Headers.Add("ErrorId", errorId);
            context.HttpContext.Response.StatusCode = 500;
        }
    }
}