using Microsoft.AspNetCore.Mvc;

namespace VL.Challenge.API.Requests;

public class IdRequest
{
    [FromRoute(Name = "id")]
    public int Id { get; set; }
}
