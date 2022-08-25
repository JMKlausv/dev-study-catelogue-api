using Application.Languages.Commands.CreateLanguage;
using FluentAssertions;
using Application.Common.Exceptions;
using Domain.Entities;

namespace Application.IntegrationTests.Languages.Commands
{
    using static Testing;
    public class CreateLanguageCommandTests : BaseTestFixture
    {
        [Test]
        public async Task ShouldRequireNameField()
        {

            var command = new CreateLanguageCommand();
            await FluentActions.Invoking(() =>
                 sendAsync(command))
                .Should().ThrowAsync<ValidationException>();

        }
        [Test]
        public async Task ShouldCreateLanguage()
        {
            var command = new CreateLanguageCommand()
            {
                Name = "sample name"
            };
            var result = await sendAsync(command);
            result.Should().NotBe(null);

            var savedLanguage = await FindAsync<Language>(result);
            savedLanguage.Should().NotBeNull().And.BeOfType<Language>();
            command.Name.Should().BeEquivalentTo(savedLanguage?.Name);
            
        }
    }
}
