using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces; // For IPostRepository
using AidManager.Collaborate.Domain.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AidManager.Collaborate.Application.Handlers.CommandHandlers;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, PostDto>
{
    private readonly IPostRepository _postRepository;
    // private readonly IUserRepository _userRepository; // For user details

    public CreatePostCommandHandler(IPostRepository postRepository /*, IUserRepository userRepository*/)
    {
        _postRepository = postRepository;
        // _userRepository = userRepository;
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
            PostImages = request.Images.Select(url => new PostImage { ImageUrl = url }).ToList()
        };

        var createdPost = await _postRepository.AddAsync(post);

        // Fetch user details for UserName, UserImage, Email (placeholders for now)
        // var user = await _userRepository.GetByIdAsync(createdPost.UserId);

        return new PostDto(
            createdPost.Id,
            createdPost.Title,
            createdPost.Subject,
            createdPost.Description,
            createdPost.CreatedAt,
            createdPost.CompanyId,
            createdPost.UserId,
            "UserNamePlaceholder", // user?.Username ?? "N/A",
            "UserImagePlaceholder.png", // user?.ProfileImageUrl ?? "N/A",
            "user@example.com", // user?.Email ?? "N/A",
            createdPost.Rating,
            createdPost.PostImages.Select(img => img.ImageUrl).ToList(),
            new List<CommentDto>() // Empty comments list initially
        );
    }
}
