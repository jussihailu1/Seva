using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Seva.Factory.DAL;
using Seva.Interface.IContextModels;
using Seva.Interface.IContexts;
using Seva.Interface.ILogicModels;
using Seva.Interface.ILogics;
using Seva.Logic.Models;

namespace Seva.Logic.Logics
{
    public class ImageLogic : IImageLogic
    {
        private readonly IImageContext _imageContext;
        private readonly IOfferedServiceContext _offeredServiceContext;

        public ImageLogic()
        {
            _imageContext = ContextFactory.CreateImageContext();
            _offeredServiceContext = ContextFactory.CreateOfferedServiceContext();
        }

        public void UploadImages(List<IFormFile> images, int currentUserId)
        {
            var latestOfferedServiceId = _offeredServiceContext.GetLatestOfferedServiceByProviderId(currentUserId).Id;

            foreach (var imageFile in images)
            {
                byte[] imageAsBytes;
                int isHeadImage = 0;

                using (var rs = imageFile.OpenReadStream())
                using (var ms = new MemoryStream())
                {
                    rs.CopyTo(ms);
                    imageAsBytes = ms.ToArray();
                }

                if (images.IndexOf(imageFile) == 0)
                {
                    isHeadImage = 1;
                }

                var imageDTO = DTOFactory.CreateImageDTO(imageAsBytes, isHeadImage, latestOfferedServiceId);

                _imageContext.AddImage(imageDTO);
            }
        }

        private IImage ConvertImage(IImageDTO imageDTO)
        {
            return new Image()
            {
                Id = imageDTO.Id, OfferedServiceId = imageDTO.OfferedServiceId,
                ImageAsString = "data:image/jpeg;base64," + Convert.ToBase64String(imageDTO.Image, 0, imageDTO.Image.Length),
                IsHeadImage = Convert.ToBoolean(imageDTO.IsHeadImage)
            };
        }

        public IEnumerable<IImage> GetImagesByOfferedServiceId(int offeredServiceId)
        {
            var imageDTOs = _imageContext.GetImagesByOfferedServiceId(offeredServiceId);
            var imagesToReturn = imageDTOs.Select(ConvertImage);

            return imagesToReturn;
        }
    }
}
