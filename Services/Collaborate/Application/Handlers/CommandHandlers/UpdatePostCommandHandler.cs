using MediatR;
using AidManager.Collaborate.Application.Commands;
using AidManager.Collaborate.Application.DTOs;
using AidManager.Collaborate.Application.Interfaces; // For IPostRepository, ICommentRepository
using AidManager.Collaborate.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace AidManager.Collaborate.Application.Handlers.CommandHandlers;

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, PostDto?>
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository; // To fetch comments for the DTO

    public UpdatePostCommandHandler(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
    }

    public async Task<PostDto?> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.Id);
        if (post == null)
        {
            return null; // Or throw NotFoundException
        }

        post.Title = request.Title;
        post.Subject = request.Subject;
        post.Description = request.Description;
        post.CompanyId = request.CompanyId;
        // post.UserId should generally not change for an existing post.

        // Handle images update: This could be complex (delete old, add new, keep existing)
        // For simplicity, let's assume it replaces all images.
        // First, remove existing images (if any) - this might need specific repo methods
        // For now, clearing and adding:
        post.PostImages.Clear(); // This assumes EF Core change tracking will handle deletions
        foreach (var imageUrl in request.Images)
        {
            post.PostImages.Add(new PostImage { ImageUrl = imageUrl, PostId = post.Id });
        }

        await _postRepository.UpdateAsync(post);

        // Fetch comments to populate the DTO
        var comments = await _commentRepository.GetByPostIdAsync(post.Id);
        var commentDtos = comments.Select(c => new CommentDto(
            c.Id, c.Content, c.UserId,
            "UserNamePlaceholder", "user@example.com", "userimage.png", // Placeholders
            c.PostId, c.TimeOfComment)).ToList();

        return new PostDto(
            post.Id,
            post.Title,
            post.Subject,
            post.Description,
            post.CreatedAt,
            post.CompanyId,
            post.UserId,
            "UserNamePlaceholder", // Placeholder for user details
            "UserImagePlaceholder", // Placeholder for user details
            "user@example.com",    // Placeholder for user details
            post.Rating,
            post.PostImages.Select(img => img.ImageUrl).ToList(),
            commentDtos
        );
    }
}
