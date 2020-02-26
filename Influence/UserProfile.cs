using AutoMapper;
using Influence.Data.Models;
using Influence.Domain.Entities;
using Influence.Models;


namespace Influence.Data
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<User, UserModel>()
                .ReverseMap();

            this.CreateMap<Post, PostModel>()
                .ReverseMap();


            this.CreateMap<Comment, CommentModel>()
                .ReverseMap();
            this.CreateMap<Like, LikeModel>()
               .ReverseMap();
        }
    }
}
