using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces; 
using AidManager.Collaborate.Domain.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers.CommandHandlers
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, PostDto>
    {
        private readonly IPostRepository _postRepository;
        private readonly IProfilesClient _profilesClient;

        public CreatePostCommandHandler(
            IPostRepository postRepository,
            IProfilesClient profilesClient)
        {
            _postRepository = postRepository;
            _profilesClient = profilesClient;
        }

        public async Task<PostDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = new Post
            {
                Title = request.Title,
                Subject = request.Subject,
                Description = request.Description,
                CompanyId = request.CompanyId,
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow,
                Rating = 0, // Initial rating
                PostImages = request.Images
                    .Select(url => new PostImage { ImageUrl = url })
                    .ToList()
            };

            var createdPost = await _postRepository.AddAsync(post);

            var user = await _profilesClient.GetUserByIdAsync(createdPost.UserId, cancellationToken);

            var userName  = user?.Name       ?? "Unknown";
            var userImage = user?.ProfileImg ?? "UserImagePlaceholder.png";
            var email     = user?.Email      ?? string.Empty;

            return new PostDto(
                createdPost.Id,
                createdPost.Title,
                createdPost.Subject,
                createdPost.Description,
                createdPost.CreatedAt,
                createdPost.CompanyId,
                createdPost.UserId,
                userName,
                userImage,
                email,
                createdPost.Rating,
                createdPost.PostImages.Select(img => img.ImageUrl).ToList(),
                new List<CommentDto>() // Empty comments list initially
            );
        }
    }
}