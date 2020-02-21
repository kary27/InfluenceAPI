using AutoMapper;
using Influence.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Influence.Data
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            this.CreateMap<UserProfile, UserModel>();
        }
    }
}
