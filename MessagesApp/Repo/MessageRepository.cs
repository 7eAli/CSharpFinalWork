using AutoMapper;
using MessagesApp.Db;
using MessagesApp.Dto;

namespace MessagesApp.Repo
{
    public class MessageRepository : IMessageRepository
    {
        private IMapper _mapper;
        private AppDbContext _dbContext;

        public MessageRepository (IMapper mapper, AppDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public IEnumerable<MessageDto> ReadAllMessages(Guid receiverId)
        {
            using (_dbContext)
            {
                var messages = _dbContext.Messages.Where(m => m.ReceiverId == receiverId).ToList();
                
                if (messages.Count == 0)
                {
                    throw new Exception("Сообщений нет");
                }
                var messageDtos = new List<MessageDto>();                
                foreach (var message in messages)
                {
                    messageDtos.Add(_mapper.Map<MessageDto>(message));

                    if (message.IsRead == false)
                    {
                        message.IsRead = true;
                    }
                }

                _dbContext.SaveChanges();

                return messageDtos;
            }
        }

        public IEnumerable<MessageDto> ReadSentMessages(Guid senderId)
        {
            using (_dbContext)
            {
                var messages = _dbContext.Messages.Where(m => m.SenderId == senderId).ToList();

                if (messages.Count == 0)
                {
                    throw new Exception("Сообщений нет");
                }

                var messageDtos = new List<MessageDto>();
                foreach (var message in messages)
                {
                    messageDtos.Add(_mapper.Map<MessageDto>(message));
                }

                _dbContext.SaveChanges();

                return messageDtos;
            }
        }

        public IEnumerable<MessageDto> ReadUnreadMessages(Guid receiverId)
        {
            using (_dbContext)
            {
                var messages = _dbContext.Messages.Where(m => m.ReceiverId == receiverId && m.IsRead == false).ToList();

                if (messages.Count == 0)
                {
                    throw new Exception("Сообщений нет");
                }

                var messageDtos = new List<MessageDto>();
                foreach (var message in messages)
                {
                    messageDtos.Add(_mapper.Map<MessageDto>(message));

                    message.IsRead = true;
                }

                _dbContext.SaveChanges();

                return messageDtos;
            }
        }

        public string SendMessage(MessageDto messageDto)
        {
            using (_dbContext)
            {
                var message = _mapper.Map<Message>(messageDto);

                _dbContext.Messages.Add(message);
                _dbContext.SaveChanges();

                return message.Id.ToString();
            }
        }
    }
}
