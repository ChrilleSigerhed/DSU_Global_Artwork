using DSU21_5.Data;
using DSU21_5.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DSU21_5Tests.Data
{
    //public class RelationshipRepositoryTests
    //{
    //    private MockRepository mockRepository;

    //    private Mock<DSU21_5.Data.ImageDbContext> mockImageDbContext;

    //    public RelationshipRepositoryTests()
    //    {
    //        this.mockRepository = new MockRepository(MockBehavior.Strict);

    //        this.mockImageDbContext = this.mockRepository.Create<DSU21_5.Data.ImageDbContext>();
    //    }

    //    private RelationshipRepository CreateRelationshipRepository()
    //    {
    //        return new RelationshipRepository(new MockImageDbContext());
    //    }

    //    [Fact]
    //    public async Task CheckIfRelationshipAlreadyExists_NullInput_ReturnFalse()
    //    {
    //        // Arrange
    //        var relationshipRepository = this.CreateRelationshipRepository(); // Skapa ett objekt som ska testas
    //        Relationship relationship = null; // Input-data att testa metoden på

    //        // Act
    //        var result = await relationshipRepository.CheckIfRelationshipAlreadyExists(relationship); // Kör metoden med input-datat

    //        // Assert
    //        Assert.False(result); // Verifiera att resultatet är false
    //        //this.mockRepository.VerifyAll();
    //    }

    //    [Fact]
    //    public async Task CheckIfRelationshipAlreadyExists_NonExistingRelationship_ReturnFalse()
    //    {
    //        // Arrange
    //        var relationshipRepository = this.CreateRelationshipRepository(); // Skapa ett objekt som ska testas
    //        Relationship relationship = new Relationship
    //        {
    //            Requestee = "1",
    //            Requester = "2"
    //        }; // Input-data att testa metoden på

    //        // Act
    //        var result = await relationshipRepository.CheckIfRelationshipAlreadyExists(relationship); // Kör metoden med input-datat

    //        // Assert
    //        Assert.False(result); // Verifiera att resultatet är false
    //        //this.mockRepository.VerifyAll();
    //    }

    //    [Fact]
    //    public async Task Create_StateUnderTest_ExpectedBehavior()
    //    {
    //        // Arrange
    //        var relationshipRepository = this.CreateRelationshipRepository();
    //        Relationship relationship = null;

    //        // Act
    //        var result = await relationshipRepository.Create(
    //            relationship);

    //        // Assert
    //        Assert.True(false);
    //        this.mockRepository.VerifyAll();
    //    }

    //    [Fact]
    //    public async Task GetRelationship_StateUnderTest_ExpectedBehavior()
    //    {
    //        // Arrange
    //        var relationshipRepository = this.CreateRelationshipRepository();
    //        string id = null;
    //        string id2 = null;

    //        // Act
    //        var result = await relationshipRepository.GetRelationship(
    //            id,
    //            id2);

    //        // Assert
    //        Assert.True(false);
    //        this.mockRepository.VerifyAll();
    //    }

    //    [Fact]
    //    public async Task GetPendingRelationship_StateUnderTest_ExpectedBehavior()
    //    {
    //        // Arrange
    //        var relationshipRepository = this.CreateRelationshipRepository();
    //        string id = null;

    //        // Act
    //        var result = await relationshipRepository.GetPendingRelationship(
    //            id);

    //        // Assert
    //        Assert.True(false);
    //        this.mockRepository.VerifyAll();
    //    }

    //    [Fact]
    //    public async Task GetRelationshipsByUserId_StateUnderTest_ExpectedBehavior()
    //    {
    //        // Arrange
    //        var relationshipRepository = this.CreateRelationshipRepository();
    //        string id = null;

    //        // Act
    //        var result = await relationshipRepository.GetRelationshipsByUserId(
    //            id);

    //        // Assert
    //        Assert.True(false);
    //        this.mockRepository.VerifyAll();
    //    }

    //    [Fact]
    //    public async Task AcceptRelationshipRequest_StateUnderTest_ExpectedBehavior()
    //    {
    //        // Arrange
    //        var relationshipRepository = this.CreateRelationshipRepository();
    //        string id1 = null;
    //        string id2 = null;

    //        // Act
    //        var result = await relationshipRepository.AcceptRelationshipRequest(
    //            id1,
    //            id2);

    //        // Assert
    //        Assert.True(false);
    //        this.mockRepository.VerifyAll();
    //    }

    //    [Fact]
    //    public async Task DenyRelationshipRequest_StateUnderTest_ExpectedBehavior()
    //    {
    //        // Arrange
    //        var relationshipRepository = this.CreateRelationshipRepository();
    //        string id1 = null;
    //        string id2 = null;

    //        // Act
    //        var result = await relationshipRepository.DenyRelationshipRequest(
    //            id1,
    //            id2);

    //        // Assert
    //        Assert.True(false);
    //        this.mockRepository.VerifyAll();
    //    }
    //    }
}
