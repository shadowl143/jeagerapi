// <summary>
// <copyright file="AutoMapperProfile.cs" company="Axity">
// This source code is Copyright Axity and MAY NOT be copied, reproduced,
// published, distributed or transmitted to or stored in any manner without prior
// written consent from Axity (www.axity.com).
// </copyright>
// </summary>

namespace Axity.Users.Services.Mapping
{
    using AutoMapper;
    using Axity.Users.Dtos.User;
    using Axity.Users.Entities.Model.User;

    /// <summary>
    /// class AutoMapperProfile.
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMapperProfile"/> class.
        /// </summary>
        public AutoMapperProfile()
        {
            this.CreateMap<UserModel, UserDto>();
            this.CreateMap<UserModel, UserTableDto>()
                .ForMember(e => e.NameUser, mo => mo.MapFrom(src => $"{src.Name} {src.SurName}"));
            this.CreateMap<UserDto, UserModel>();
        }
    }
}
