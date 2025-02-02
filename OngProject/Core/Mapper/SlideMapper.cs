﻿using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace OngProject.Core.Mapper
{
    public static class SlideMapper
    {
        public static SlideDTO ToDTO(this Slide slide)
        {
            var dto = new SlideDTO() {ImageUrl = slide.ImageUrl, Order = slide.Order };
            return dto;
        }

        public static IEnumerable<SlideDTO>  ToDTO(IEnumerable<Slide> slides)
        {
            return slides.Select(s => ToDTO(s));
        }

        public static SlidePublicDTO ToPublicDTO(Slide slide)
        {
            SlidePublicDTO dto = new SlidePublicDTO()
            {
                ImageUrl = slide.ImageUrl,
                Order = slide.Order,
                OrganizationId = slide.OrganizationId,
                Text = slide.Text
            };
            return dto;
        }
    }
}
