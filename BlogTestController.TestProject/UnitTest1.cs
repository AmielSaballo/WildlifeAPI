using System.Drawing.Text;
using Moq;
using WildlifeAPI.Controllers;
using WildlifeAPI.Data;
using WildlifeAPI.Models;
using WildlifeAPI_Prod.Controllers;
using WildlifeAPI_Prod.Data.Services;

namespace BlogTestController.TestProject
{
    public class UnitTest1
    {
        

        private Mock<IBlogsService> _services;

        public UnitTest1()
        {
            _services = new Mock<IBlogsService>();
        }

        [Fact]
        public async void GetBlogById_Success_ReturnsNotNull()
        {
            //arrange
            var blogList = new Blogs() { id = 1, blogTitle = "Test", blogSummary = "This is a", blogContent = "This is a test", blogAuthor = "Amiel", postedDate = "3 March 2023" };
            _services.Setup(x => x.GetById(1)).Returns(Task.Run(() => blogList)); 
            var blogsController = new TestBlogsController(_services.Object);

            var result = blogsController.GetBlogsById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public async void GetAllBlogs_Success_ReturnsNotNull()
        {
            //arrange
            var blogList = CreateBlogsList();
            _services.Setup(x => x.GetAll()).Returns(Task.Run(() => blogList));
            var blogsController = new TestBlogsController(_services.Object);

            var result = blogsController.GetAllBlogs();

            Assert.NotNull(result);
        }

        public IEnumerable<Blogs> CreateBlogsList()
        {
            IEnumerable<Blogs> blogData = new List<Blogs>();
            {
                new Blogs() { id = 1, blogTitle = "Test", blogSummary = "This is a", blogContent = "This is a test", blogAuthor = "Amiel", postedDate = "3 March 2023", };
            }
            return blogData;
        }
    }
}