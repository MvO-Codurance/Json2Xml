using System;

namespace GenerateJson.Benchmarks;

public record struct Person(string FirstName, string LastName, DateTime DateOfBirth, string Nationality);