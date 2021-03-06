﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

using SofiaDayAndNight.Common.Enums;
using SofiaDayAndNight.Data.Models;
using SofiaDayAndNight.Web.Infrastructure;

namespace SofiaDayAndNight.Web.Models
{
    public class ImageViewModel : IMapFrom<Image>
    {
        public ImageViewModel()
        {
            this.Id = Guid.NewGuid();
            this.Privacy = Privacy.OnlyFriends;
        }

        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string ContentType { get; set; }

        [Required]
        public byte[] Data { get; set; }

        public Privacy Privacy { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        //public Event Event { get; set; }

        //public virtual Individual Individual { get; set; }

        //public virtual Organization Organization { get; set; }

        //public virtual Multimedia Multimedia { get; set; }

        //public  IEnumerable<CommentViewModel> Comments { get; set; }
    }
}