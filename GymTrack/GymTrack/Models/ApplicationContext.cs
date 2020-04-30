using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymTrack.Models;
using Microsoft.EntityFrameworkCore;

namespace GymTrack.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Person> Users { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Workout> Workouts { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}