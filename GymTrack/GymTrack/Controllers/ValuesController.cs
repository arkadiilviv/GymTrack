using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GymTrack.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GymTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        ApplicationContext appContext;

        public ValuesController(ApplicationContext context)
        {
            appContext = context;
        }



        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public IEnumerable<object> Get(int id = -1)
        {
            if (id > -1)
            {
                var result = GetWorkoutData(id);
                return result;
            }
            else
            {
                var result = appContext.Exercises.Where(x => x.Owner.Login == User.Identity.Name).Select(x => new { x.id, x.ExerciseName }).ToList();
                return result;
            }
         
        }

     
        public IEnumerable<object> GetWorkoutData(int id)
        {
            var result = appContext.Workouts.Where(x => x.Exercise_Workout.id == id && x.Owner.Login == User.Identity.Name).Select(x=> new {x.Weight, x.Date,x.Exercise_Workout.id }).ToList();
            return result;
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut]
        public IActionResult Put(String exerciseName)
        {
            try
            {
                if (!String.IsNullOrEmpty(exerciseName))
                {
                    Person user = appContext.Users.FirstOrDefault(x => x.Login == User.Identity.Name);
                    appContext.Exercises.Add(new Exercise() { ExerciseName = exerciseName, Owner = user });
                    appContext.SaveChanges();
                    return Ok();
                }
                else
                    return BadRequest(new { errorText = "Exercise name empty" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorText = ex.Message });

            }
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public IActionResult Post([FromBody]WorkoutModel workout)
        {
            try
            {
                Person user = appContext.Users.FirstOrDefault(x => x.Login == User.Identity.Name);
                Exercise exercise = appContext.Exercises.FirstOrDefault(x => x.id == workout.Exercise);
                Workout item = new Workout();
                item.Weight = workout.Weight;
                item.Owner = user;
                item.Exercise_Workout = exercise;
                item.Date = workout.Date;
                appContext.Workouts.Add(item);
                appContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorText = ex.Message });
            }
        }

    }
}
