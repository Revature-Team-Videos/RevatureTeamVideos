using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StoringApi.Service.Models;

namespace StoringApi.Service
{
  public class VWFContext : DbContext
  {
    public DbSet<User> Users;

    public DbSet<Room> Rooms;

    public DbSet<Message> Messages;

    public DbSet<Video> Videos;

    public VWFContext(DbContextOptions<VWFContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<User>().HasKey(user => user.EntityID);
      builder.Entity<User>().HasMany(user => user.Friends);
      builder.Entity<User>().HasMany(user => user.BlockedUsers);

      builder.Entity<ChatBox>().HasKey(chatBox => chatBox.EntityID);
      builder.Entity<ChatBox>().HasMany(chatBox => chatBox.Chat);

      builder.Entity<Video>().HasKey(video => video.EntityID);
      builder.Entity<Video>().HasMany(video => video.Viewers);

      builder.Entity<Room>().HasKey(room => room.EntityID);
      builder.Entity<Room>().HasMany(room => room.Party);
      builder.Entity<Room>().HasOne(room => room.RoomChat);

      builder.Entity<Message>().HasKey(message => message.EntityID);
      builder.Entity<Message>().HasOne(message => message.User);

      SeedUserData(builder);
    }

    private void SeedUserData(ModelBuilder builder)
    {
      builder.Entity<User>().HasData(new List<User>()
      {
        new User() { Username = "TestJim", EntityID = 1, Email = "test@jim.com"},
        new User() { Username = "TestZach", EntityID = 2, Email = "test@zach.com"},
        new User() { Username = "TestYiChen", EntityID = 3, Email = "test@yichen.com"}
      });
    }
  }
}
