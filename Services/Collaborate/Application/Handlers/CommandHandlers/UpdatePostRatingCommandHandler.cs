using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces; // For IPostRepository, ILikedPostRepository, ICommentRepository
using AidManager.Collaborate.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AidManager.Collaborate.Application.Handlers.CommandHandlers;

// This handler simulates a user liking a post, which updates the post's rating.
public class UpdatePostRatingCommandHandler : IRequestHandler<UpdatePostRatingCommand, PostDto?>
{
    private readonly IPostRepository _postRepository;
    private readonly ILikedPostRepository _likedPostRepository;
    private readonly ICommentRepository _commentRepository; // For DTO population

    public UpdatePostRatingCommandHandler(
        IPostRepository postRepository,
        ILikedPostRepository likedPostRepository,
        ICommentRepository commentRepository)
    {
        _postRepository = postRepository;
        _likedPostRepository = likedPostRepository;
        _commentRepository = commentRepository;
    }

    public async Task<PostDto?> Handle(UpdatePostRatingCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.PostId);
        if (post == null)
        {
            return null; // Post not found
        }

        var existingLike = await _likedPostRepository.GetAsync(request.UserId, request.PostId);
        if (existingLike == null)
        {
            // User hasn't liked this post yet, so add a like
            await _likedPostRepository.AddAsync(new LikedPost { UserId = request.UserId, PostId = request.PostId });
        }
        else
        {
            // User has already liked this post, so remove the like (toggle behavior)
            await _likedPostRepository.DeleteAsync(request.UserId, request.PostId);
        }

        // Update post rating - count of likes
        post.Rating = await _likedPostRepository.GetLikesCountAsync(request.PostId);
        await _postRepository.UpdateAsync(post); // Save the updated rating

        // Fetch comments for DTO
        var comments = await _commentRepository.GetByPostIdAsync(post.Id);
        var commentDtos = comments.Select(c => new CommentDto(
            c.Id, c.Content, c.UserId,
            "UserNamePlaceholder", "user@example.com", "userimage.png", // Placeholders
            c.PostId, c.TimeOfComment)).ToList();

        // Fetch user details (placeholders for now)

        return new PostDto(
            post.Id,
            post.Title,
            post.Subject,
            post.Description,
            post.CreatedAt,
            post.CompanyId,
            post.UserId,
            "UserNamePlaceholder",
            "UserImagePlaceholder",
            "user@example.com",
            post.Rating,
            post.PostImages.Select(img => img.ImageUrl).ToList(),
            commentDtos
        );
    }
}
