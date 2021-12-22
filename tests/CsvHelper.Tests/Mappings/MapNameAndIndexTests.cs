// ----------------------------------------------------------------------------------------------------
// <copyright file="MapNameAndIndexTests.cs" company="Federal Aviation Administration">
//     Federal Aviation Administration.
// </copyright>
// ----------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

using CsvHelper.Configuration;
using CsvHelper.Tests.Mocks;

using Xunit;

namespace CsvHelper.Tests.Mappings;

public class MapNameAndIndexTests
{
	[Fact]
	public async Task MapNameAndIndexNoHeaderTest()
	{
		var parser = new ParserMock
		{
			new [] { "1", "one" },
			new [] { "2", "two" },
			null
		};

		CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = false };
		var records = new List<NameAndIndex>();

		using (var csvReader = new CsvReader(parser))
		{
			await csvReader.ReadAsync();
			csvReader.ReadHeader();
			while (await csvReader.ReadAsync())
			{
				records.Add(csvReader.GetRecord<NameAndIndex>());
			}
		}
	}

	private class NameAndIndexMap : ClassMap<NameAndIndex>
	{
		public NameAndIndexMap()
		{
			Parameter("id").Name("id").Index(0);
			Parameter("name").Name("name").Index(1);

			Map(mapped => mapped.Id).Name("Id").Index(0);
			Map(mapped => mapped.Name).Name("Name").Index(1);
		}
	}

	private class NameAndIndex
	{
		public NameAndIndex(int id, string name)
		{
			Id = id;
			Name = name;
		}

		public int Id { get; private set; }

		public string Name { get; private set; }
	}
}
