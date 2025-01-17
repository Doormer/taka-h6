using System;

/// <summary>
/// Represents errors that occur during domain rule validation.
/// Used to encapsulate domain-specific error conditions.
/// </summary>
public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
} 