namespace SkillUpAcademy.IntegrationTests;

/// <summary>
/// Colección que agrupa todos los tests de integración para compartir la factory
/// y evitar race conditions de paralelismo de xUnit.
/// </summary>
[CollectionDefinition("Integration")]
public class IntegrationTestCollection : ICollectionFixture<CustomWebApplicationFactory>
{
}
