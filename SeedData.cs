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
                    Gender = Gender.Male,
                    IsLogin = true
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
                    Gender = Gender.Female,
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
                    Gender = Gender.Male,
                    University = "МГЛУ",
                },

                new User
                {
                    Name = "Никита",
                    Surname = "Яцик",
                    BirthDay = new DateTime(2002, 2, 26),
                    Country = "Россия",
                    City = "Иваново",
                    Gender = Gender.Male,
                },

                new User
                {
                    Name = "Иван",
                    Surname = "Иванов",
                    BirthDay = new DateTime(1990, 2, 5),
                    Country = "Россия",
                    City = "Москва",
                    Gender = Gender.Male,
                },

                new User
                {
                    Name = "Илья",
                    Surname = "Мороз",
                    BirthDay = new DateTime(2000, 2, 26),
                    Country = "Россия",
                    City = "Санкт-Петербург",
                    Gender = Gender.Male,
                },

                new User
                {
                    Name = "Екатерина",
                    Surname = "Петрушкевич",
                    BirthDay = new DateTime(1999, 7, 10),
                    Country = "Россия",
                    City = "Москва",
                    Gender = Gender.Female,
                },

                new User
                {
                    Name = "Илья",
                    Surname = "Иванов",
                    BirthDay = new DateTime(1998, 5, 1),
                    Country = "Россия",
                    City = "Санкт-Петербург",
                    Gender = Gender.Male,
                },

                new User
                {
                    Name = "Илья",
                    Surname = "Иванов",
                    BirthDay = new DateTime(2000, 12, 23),
                    Country = "Россия",
                    City = "Москва",
                    Gender = Gender.Male,
                },

                new User
                {
                    Name = "Илья",
                    Surname = "Холод",
                    BirthDay = new DateTime(1980, 9, 20),
                    Country = "Украина",
                    City = "Киев",
                    Gender = Gender.Male,
                },

                new User
                {
                    Name = "Илья",
                    Surname = "Цукенберг",
                    BirthDay = new DateTime(2000, 3, 10),
                    Country = "Беларусь",
                    City = "Гродно",
                    Gender = Gender.Male,
                },

                new User
                {
                    Name = "Артем",
                    Surname = "Барташ",
                    BirthDay = new DateTime(1997, 8, 10),
                    Country = "Беларусь",
                    City = "Гродно",
                    Gender = Gender.Male,
                },

                new User
                {
                    Name = "Илья",
                    Surname = "Барташ",
                    BirthDay = new DateTime(1999, 10, 10),
                    Country = "Беларусь",
                    City = "Гродно",
                    Gender = Gender.Male,
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
                    Text = "Не банальный пост 2",
                    Owner = users[2]
                },

                new Post
                {
                    Date = DateTime.Now,
                    Text = "Новый пост",
                    Owner = users[1]
                },

                new Post
                {
                    Date = DateTime.Now,
                    Text = "Еще один новый пост",
                    Owner = users[1]
                },

                new Post
                {
                    Date = DateTime.Now,
                    Text = "Интересный пост",
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

            for (int i = 0; i < 13; i++)
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

            mainPhotos.Add(
                new Photo
                {
                    Image = "~/images/no_photo.png",
                    Post = mainPhotosPosts[0]
                });

            mainPhotos.Add(
                new Photo
                {
                    Image = "~/usersPhotos/polina.jpg",
                    Post = mainPhotosPosts[1]
                });

            mainPhotos.Add(
                new Photo
                {
                    Image = "~/usersPhotos/dima.jpg",
                    Post = mainPhotosPosts[2]
                });

            mainPhotos.Add(
                new Photo
                {
                    Image = "~/usersPhotos/yatsik.jpg",
                    Post = mainPhotosPosts[3]
                });

            for  (int i = 4; i < users.Count; i ++)
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
            };


            var friendships = new List<Friendship>
            {
                new Friendship()
                {
                    RequestFrom = users[1],
                    RequestTo = users[0],
                    Status = FriendshipStatus.Waiting
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
                    Status = FriendshipStatus.Waiting
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
        }
    }
}
