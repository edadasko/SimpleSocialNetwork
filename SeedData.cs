using System;
using System.Collections.Generic;
using System.Linq;
using SimpleSocialNetwork.Models;
namespace SimpleSocialNetwork
{
    public static class SeedData
    {
        public static void Initialize(UsersContext context)
        {
            var users = new List<User>
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

            var posts = new List<Post>
            {
                new Post
                {
                    Date = DateTime.Now,
                    Text = "Информативный пост",
                    Owner = users[0]
                },

                new Post
                {
                    Date = DateTime.Now,
                    Text = "Не менее информативный пост",
                    Owner = users[1]
                },

                new Post
                {
                    Date = DateTime.Now,
                    Text = "Не банальный пост",
                    Owner = users[2]
                },

                new Post
                {
                    Date = DateTime.Now,
                    Text = "Наинтереснейший пост",
                    Owner = users[1]
                }
            };


            var mainPhotosPosts = new List<Post>();

            for (int i = 0; i < 3; i++)
            {
                mainPhotosPosts.Add(
                    new Post
                    {
                        Type = PostType.MainPhoto,
                        Owner = users[i],
                        Date = DateTime.Now
                    });
            }

            var mainPhotos = new List<Photo>();

            for (int i = 0; i < 3; i++)
            {
                mainPhotos.Add(
                    new Photo
                    {
                        Image = "~/images/no_photo.png",
                        Post = mainPhotosPosts[i]
                    });
            }

            var photos = new List<Photo>
            {
                new Photo
                {
                    Image = "img1.jpg",
                    Post = posts[0]
                }
            };

            photos.AddRange(mainPhotos);
            posts.AddRange(mainPhotosPosts);

            var comments = new List<Comment>
            {
                new Comment
                {
                    Date = DateTime.Now,
                    Text = "Какой интересный пост",
                    Post = posts[0],
                    Owner = users[2],
                },

                new Comment
                {
                    Date = DateTime.Now,
                    Text = "Какой не банальный пост",
                    Post = posts[2],
                    Owner = users[0],
                }
            };


            var likes = new List<Like>
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
                },

                new Like
                {
                    Owner = users[0],
                    Post = posts[1],
                },

                new Like
                {
                    Owner = users[1],
                    Post = posts[2],
                },

                new Like
                {
                    Owner = users[0],
                    Post = posts[3],
                },

                new Like
                {
                    Owner = users[1],
                    Post = posts[3],
                },
            };


            var friendships = new List<Friendship>
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
            if (!context.Users.Any())
            {
                context.Users.AddRange(users);
                context.SaveChanges();
                context.Posts.AddRange(posts);
                context.SaveChanges();
                context.Photos.AddRange(photos);
                context.SaveChanges();
                context.Comments.AddRange(comments);
                context.SaveChanges();
                context.Likes.AddRange(likes);
                context.SaveChanges();
                context.Friendships.AddRange(friendships);
                context.SaveChanges();
            }


            var dialogs = new List<Dialog>
            {
                new Dialog
                {
                    User1Id = context.Users.ToList()[0].UserId,
                    User2Id = context.Users.ToList()[1].UserId
                },

                new Dialog
                {
                    User1Id = context.Users.ToList()[0].UserId,
                    User2Id = context.Users.ToList()[2].UserId
                }
            };

            var messages = new List<Message>
            {
                new Message
                {
                    Date = new DateTime(2019, 9, 19, 20, 1, 5),
                    Text = "Привет",
                    UserFrom = users[0],
                    UserTo = users[1],
                    Dialog = dialogs[0]
                },

                new Message
                {
                    Date = new DateTime(2019, 9, 19, 20, 2, 10),
                    Text = "Привет",
                    UserFrom = users[1],
                    UserTo = users[0],
                    Dialog = dialogs[0]
                },

                new Message
                {
                    Date = new DateTime(2019, 9, 19, 20, 2, 20),
                    Text = "Как дела?",
                    UserFrom = users[0],
                    UserTo = users[1],
                    Dialog = dialogs[0]
                },

                new Message
                {
                    Date = new DateTime(2019, 9, 19, 20, 2, 30),
                    Text = "Хорошо",
                    UserFrom = users[1],
                    UserTo = users[0],
                    Dialog = dialogs[0]
                },

                new Message
                {
                    Date = new DateTime(2019, 9, 19, 20, 2, 20),
                    Text = "Привет",
                    UserFrom = users[0],
                    UserTo = users[2],
                    Dialog = dialogs[1]
                },

                new Message
                {
                    Date = new DateTime(2019, 9, 19, 20, 2, 20),
                    Text = "Привет",
                    UserFrom = users[2],
                    UserTo = users[0],
                    Dialog = dialogs[1]
                },

            };

            if(!context.Messages.Any())
            {
                context.Dialogs.AddRange(dialogs);
                context.SaveChanges();
                context.Messages.AddRange(messages);
                context.SaveChanges();
            }
        }
    }
}
