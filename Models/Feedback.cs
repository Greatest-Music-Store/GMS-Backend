using System.Net.Http.Headers;
using System.Reflection.Metadata;

namespace GMS_Backend.Models;

public class Feedback
{
    public Guid Id { get; set;}
    public Guid UserId { get; set;}
    public string Description { get; set; }
    public int Rating { get; set; }
    public Guid ProductId { get; set; }
}