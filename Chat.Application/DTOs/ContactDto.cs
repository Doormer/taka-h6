namespace Chat.Application.DTOs;

/// <summary>
/// Data Transfer Object for contact information.
/// Represents the essential contact data for client-side consumption.
/// </summary>
public record ContactDto(Guid UserId, Guid ContactUserId, bool IsArchived); 