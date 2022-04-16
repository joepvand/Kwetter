using CommentService.Data;
using CommentService.DomainModels;
using Mapster;

namespace CommentService.Application
{
    public class CommentApp
    {
        private readonly ICommentRepository _repository;

        public CommentApp(ICommentRepository repository)
        {
            _repository = repository;
        }
        public async Task<Comment> GetByIdAsync(Guid id)
        {
            return (await this._repository.GetByIdAsync(id)).Adapt<Comment>();
        }

        public List<Comment> GetByUser(Guid id)
        {
            return this._repository.GetByUser(id).ProjectToType<Comment>().ToList();
        }

        public List<Comment> GetByTweet(Guid id)
        {
            return this._repository.GetByTweet(id).ProjectToType<Comment>().ToList();
        }

        public List<Comment> GetAll()
        {
            return this._repository.GetAll().ProjectToType<Comment>().ToList();
        }

        public Task DeleteByIdAsync(Guid id)
        {
            return this._repository.DeleteByIdAsync(id);
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            return (await this._repository.CreateAsync(comment)).Adapt<Comment>(); ;
        }

        public List<Comment> GetFiltered(string tweetId, string userId)
        {
            var tweetNull = string.IsNullOrWhiteSpace(tweetId);
            var userNull = string.IsNullOrWhiteSpace(userId);

            if (tweetNull && userNull)
                return this.GetAll();
            if (tweetNull && !userNull)
            {
                return GetByTweet(Guid.Parse(userId));
            }
            if (!tweetNull && userNull)
            {
                return GetByUser(Guid.Parse(tweetId));
            }

            return GetByUser(Guid.Parse(userId))
                .Where(x => x.TweetId == Guid.Parse(tweetId)).ToList();

        }
    }
}