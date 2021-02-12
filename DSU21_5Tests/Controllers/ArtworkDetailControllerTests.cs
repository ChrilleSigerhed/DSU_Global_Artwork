using DSU21_5.Controllers;
using DSU21_5.Data;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DSU21_5Tests.Controllers
{
    //public class ArtworkDetailControllerTests
    //{
        //    private MockRepository mockRepository;

        //    private Mock<IArtRepository> mockArtRepository;
        //    private Mock<IMemberRepository> mockMemberRepository;

        //    public ArtworkDetailControllerTests()
        //    {
        //        this.mockRepository = new MockRepository(MockBehavior.Strict);

        //        this.mockArtRepository = this.mockRepository.Create<IArtRepository>();
        //        this.mockMemberRepository = this.mockRepository.Create<IMemberRepository>();
        //    }

        //    private ArtworkDetailController CreateArtworkDetailController()
        //    {
        //        return new ArtworkDetailController(
        //            this.mockArtRepository.Object,
        //            this.mockMemberRepository.Object);
        //    }

        //    [Fact]
        //    public async Task Index_NonExistingId_ExpectedBehavior()
        //    {
        //        // Arrange
        //        var artworkDetailController = this.CreateArtworkDetailController();
        //        int Id = 0;

        //        // Act
        //        var result = await artworkDetailController.Index(Id);

        //        // Assert
        //        Assert.True(false);
        //        this.mockRepository.VerifyAll();
        //    }
    //}
}
