using System;
using System.Collections.Generic;

namespace SimpleSocialNetwork.Models
{
    public class FakeUsersRepository : IUsersRepository
    {
        private List<User> users;
        private List<Message> messages;
        private List<Post> posts;
        private List<Photo> photos;
        private List<Comment> comments;
        private List<Like> likes;
        private List<Friendship> friendships;


        public FakeUsersRepository()
        {
            users = new List<User>
            {
                new User
                {
                    Name = "Эдуард",
                    Surname = "Адасько",
                    BirthDay = new DateTime(2000, 3, 30),
                    Country = "Беларусь",
                    City = "Барановичи",
                    Address = "Жукова 18/2 - 24",
                    School = "СШ 19",
                    University = "БГУИР",
                },

                new User
                {
                    Name = "Полина",
                    Surname = "Ушакова",
                    BirthDay = new DateTime(2000, 9, 22),
                    Country = "Беларусь",
                    City = "Минск",
                    Address = "Восточая 26 - 35",
                    School = "СШ 19",
                    University = "БГУИР",
                },

                new User
                {
                    Name = "Дмитрий",
                    Surname = "Ширко",
                    BirthDay = new DateTime(2000, 2, 5),
                    Country = "Беларусь",
                    City = "Барановичи",
                    Address = "Наконечникова",
                    School = "СШ 19",
                    University = "МГЛУ",
                }
            };

            messages = new List<Message>
            {
                new Message
                {
                    Date = new DateTime(2019, 9, 19, 20, 1, 5),
                    Text = "Привет",
                    UserFrom = users[0],
                    UserTo = users[1]
                },

                new Message
                {
                    Date = new DateTime(2019, 9, 19, 20, 2, 10),
                    Text = "Привет!!!",
                    UserFrom = users[1],
                    UserTo = users[0]
                },

                new Message
                {
                    Date = new DateTime(2019, 9, 19, 20, 2, 20),
                    Text = "Как дела?",
                    UserFrom = users[0],
                    UserTo = users[2]
                },

                 new Message
                {
                    Date = new DateTime(2019, 9, 19, 20, 2, 30),
                    Text = "Хорошо",
                    UserFrom = users[2],
                    UserTo = users[0]
                },

                 new Message
                {
                    Date = new DateTime(2019, 9, 19, 18, 2, 30),
                    Text = "лолололол",
                    UserFrom = users[0],
                    UserTo = users[1]
                },

                new Message
                {
                    Date = new DateTime(2019, 9, 19, 18, 2, 45),
                    Text = "хахахаххааха",
                    UserFrom = users[1],
                    UserTo = users[0]
                },

            };

            posts = new List<Post>
            {
                new Post
                {
                    Date = DateTime.Now,
                    Text = "ЛЛАЛАЛАЛАЛАЛАЛА",
                    Owner = users[0]
                }
            };

            photos = new List<Photo>
            {
                new Photo
                {
                    Image = "img1.jpg",
                    Post = posts[0]
                }
            };

            comments = new List<Comment>
            {
                new Comment
                {
                    Date = DateTime.Now,
                    Text = "First comment",
                    Post = posts[0],
                    Owner = users[2],
                },

                new Comment
                {
                    Date = DateTime.Now,
                    Text = "Second comment",
                    Post = posts[0],
                    Owner = users[0],
                }
            };


            likes = new List<Like>
            {
                new Like
                {
                    Owner = users[2],
                    Post = posts[0],
                },

                new Like
                {
                    Owner = users[0],
                    Post = posts[0],
                },

                new Like
                {
                    Owner = users[1],
                    Post = posts[0],
                }
            };


            friendships = new List<Friendship>
            {
                new Friendship()
                {
                    RequestFrom = users[0],
                    RequestTo = users[1],
                    Status = FriendshipStatus.Confirmed
                },

                new Friendship()
                {
                    RequestFrom = users[1],
                    RequestTo = users[2],
                    Status = FriendshipStatus.Confirmed
                },

                new Friendship()
                {
                    RequestFrom = users[2],
                    RequestTo = users[0],
                    Status = FriendshipStatus.Confirmed
                },
            };

        }
        public IEnumerable<User> Users => users;
        public IEnumerable<Message> Messages => messages;
        public IEnumerable<Post> Posts => posts;
        public IEnumerable<Like> Likes => likes;
        public IEnumerable<Comment> Comments => comments;
        public IEnumerable<Photo> Photos => photos;
        public IEnumerable<Friendship> Friendships => friendships;

        public Post GetUserMainPhoto(User user) => user.MainPhoto;
        public List<User> GetUsersFriends(User user) => user.Friends;
        public List<Post> GetUsersPhotos(User user) => user.Photos;
        public List<Post> GetUsersPosts(User user) => (List<Post>)user.Posts;

        public Dictionary<User, List<Message>> GetUsersMessages(User user) => throw new NotImplementedException();
        public List<Post> GetUsersNews(User user) => throw new NotImplementedException();
    }
}
