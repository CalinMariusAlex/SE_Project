using SE_Project.Models;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UnitProject.Tests
{
    public class PlaylistTests
    {
        [Fact]
        public void Playlist_Creation_SetsCorrectDefaults()
        {
            // Arrange
            var playlist = new Playlist
            {
                Name = "Test Playlist",
                Description = "Test Desc",
                DateCreated = DateTime.Now,
                LastModified = DateTime.Now,
                IsPublic = true
            };

            // Act & Assert
            Assert.Equal("Test Playlist", playlist.Name);
            Assert.True(playlist.IsPublic);
        }
    }
}
