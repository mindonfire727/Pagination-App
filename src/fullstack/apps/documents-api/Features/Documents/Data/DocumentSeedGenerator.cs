using Bogus;
using Fullstack.Contracts.Documents;
using Fullstack.DocumentsApi.Features.Documents.Models;

namespace Fullstack.DocumentsApi.Features.Documents.Data;

public static class DocumentSeedGenerator
{
  public static IEnumerable<Document> Seed(int count = 1000)
  {
    Randomizer.Seed = new Random(2023);

    var faker = new Faker<Document>("de")
      .CustomInstantiator(f => new Document(f.Commerce.Ean13(), f.Date.PastOffset(3), f.Name.FullName(), f.PickRandom<DocumentType>()));

    return faker.Generate(count);
  }
}
