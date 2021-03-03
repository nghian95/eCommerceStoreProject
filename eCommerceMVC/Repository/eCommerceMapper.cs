using eCommerceClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace eCommerceMVC.Repository
{
    public class eCommerceMapper : Profile
    {
        public eCommerceMapper()
        {
            CreateMap<Products, Models.Products>();
            CreateMap<Models.Products, Products>();
            CreateMap<Categories, Models.Categories>();
            CreateMap<Models.Categories, Categories>();
        }
    }
}
