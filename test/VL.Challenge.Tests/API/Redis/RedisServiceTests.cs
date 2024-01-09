using Moq.AutoMock;
using VL.Challenge.Domain.Entities;
using StackExchange.Redis;
using Moq;
using VL.Challenge.Domain.Tests.Helpers;
using System.Text.Json;
using VL.Challenge.Common.CRUD;
using VL.Challenge.API.Redis;

namespace VL.Challenge.Tests.API.Redis;

public class RedisServiceTests : IDisposable
{
    private readonly AutoMocker _mocker;
    private RedisService<User>? _cache;

    public RedisServiceTests()
    {
        _mocker = new AutoMocker();
    }

    public void Dispose()
    {
        _cache = null;
    }

    private RedisService<User> Cache
    {
        get
        {
            _cache ??= _mocker.CreateInstance<RedisService<User>>();
            return _cache;
        }
    }


    [Fact]
    public async Task Get_WhenFindsKey_ShouldNotQueryDatabase()
    {
        var id = 5;
        var type = typeof(User);
        var redisKey = $"{type.Name}_{id}";
        var expectedUser = UserHelper.Create();

        _mocker.GetMock<IDatabase>()
            .Setup(x => x.StringGetAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
            .ReturnsAsync(new RedisValue(JsonSerializer.Serialize(expectedUser)));

        var user = await Cache.Get(id);

        Assert.NotNull(user);
        Assert.Equal(expectedUser, user);
        _mocker.GetMock<IRead<User>>()
            .Verify(x => x.Read(It.IsAny<int>()), Times.Never());
    }

    [Fact]
    public async Task Get_WhenDoesNotFindKey_ShouldQueryDatabase()
    {
        var id = 5;
        var type = typeof(User);
        var redisKey = $"{type.Name}_{id}";
        var expectedUser = UserHelper.Create(id);

        _mocker.GetMock<IRead<User>>()
            .Setup(x => x.Read(It.IsAny<int>()))
            .ReturnsAsync(expectedUser);

        var user = await Cache.Get(id);

        Assert.Equal(expectedUser, user);
        _mocker.GetMock<IRead<User>>()
            .Verify(x => x.Read(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public async Task Get_WHenDoesNotFindKeyAndEntityDoesNotExistInDatabase_ShouldThrow()
    {
        var action = () => Cache.Get(1337);

        await Assert.ThrowsAsync<ApplicationException>(action);
    }

    [Fact]
    public async Task Invalidate_ShouldDeleteKey()
    {
        var id = 5;
        var type = typeof(User);
        var expectedKey = $"{type.Name}_{id}";

        await Cache.Invalidate(id);

        _mocker.GetMock<IDatabase>()
            .Verify(x => x.KeyDeleteAsync(expectedKey, It.IsAny<CommandFlags>()), Times.Once);
    }
}
