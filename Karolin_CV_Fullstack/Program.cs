using Karolin_CV_Fullstack.Data;
using Karolin_CV_Fullstack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Karolin_CV_Fullstack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<CV_DbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseAuthorization();

            // CREATE TECH SKILLS
            app.MapPost("/Tech-skills", async (Tech_skills techSkill, CV_DbContext db) =>
            {
                try
                {
                    db.TechSkills.Add(techSkill);
                    await db.SaveChangesAsync();
                    return Results.Ok(techSkill);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fel vid databasuppdatering: {ex.Message}");
                    return Results.Problem("Kunde inte spara till databasen.");
                }
            });


            // GET ALL TECH SKILLS
            app.MapGet("/Tech-skills", async (CV_DbContext db) =>
            {
                var skills = await db.TechSkills.ToListAsync();
                return Results.Ok(skills);
            });

            // GET BY ID TECH 
            app.MapGet("/Tech-skills/{id}", async (int id, CV_DbContext db) =>
            {
                var skill = await db.TechSkills.FindAsync(id);

                if (skill == null)
                {
                    return Results.NotFound($"Tech skill with ID {id} not found.");
                }

                return Results.Ok(skill);
            });

            // UPDATE TECH
            app.MapPut("/Tech-skills/{id}", async (int id, Tech_skills updateSkill, CV_DbContext db) =>
            {
                var existingSkill = await db.TechSkills.FindAsync(id);

                if (existingSkill == null)
                {
                    return Results.NotFound($"Tech skill with ID {id} not found.");
                }

                existingSkill.Name = updateSkill.Name;
                existingSkill.Years = updateSkill.Years;
                existingSkill.Skill_level = updateSkill.Skill_level;

                await db.SaveChangesAsync();

                return Results.Ok(existingSkill);
            });

            // DELETE TECH
            app.MapDelete("/Tech-skills/{id}", async (int id, CV_DbContext db) =>
            {
                var skillToDelete = await db.TechSkills.FindAsync(id);

                if (skillToDelete == null)
                {
                    return Results.NotFound($"Tech skill with ID {id} not found.");
                }

                db.TechSkills.Remove(skillToDelete);
                await db.SaveChangesAsync();

                return Results.Ok($"Tech skill with ID {id} has been deleted.");
            });

            // CREATE PROJECTS
            app.MapPost("/Projects", async ([FromBody] Projects project, CV_DbContext db) =>
            {
                db.Projects.Add(project);
                await db.SaveChangesAsync();
                return Results.Ok(project);
            });

            // GET ALL PROJECTS
            app.MapGet("/Projects", async (CV_DbContext db) =>
            {
                var projects = await db.Projects.ToListAsync();  
                return Results.Ok(projects);
            });


            // GET BY ID 

            app.MapGet("/Projects/{id}", async (int id, CV_DbContext db) =>
            {
                var project = await db.Projects.FindAsync(id);

                if (project == null)
                {
                    return Results.NotFound($"Project with ID {id} not found.");
                }

                return Results.Ok(project);
            });

            // UPDATE 

            app.MapPut("/Projects/{id}", async (int id, Projects updateProject, CV_DbContext db) =>
            {
                var existingProject = await db.Projects.FindAsync(id);

                if (existingProject == null)
                {
                    return Results.NotFound($"Project with ID {id} not found.");
                }

                existingProject.Name = updateProject.Name;
                existingProject.Description = updateProject.Description;

                await db.SaveChangesAsync();

                return Results.Ok(existingProject);
            });

            // DELETE

            app.MapDelete("/Projects/{id}", async (int id, CV_DbContext db) =>
            {
                var projectToDelete = await db.Projects.FindAsync(id);

                if (projectToDelete == null)
                {
                    return Results.NotFound($"Project with ID {id} not found.");
                }

                db.Projects.Remove(projectToDelete);
                await db.SaveChangesAsync();

                return Results.Ok($"Project with ID {id} has been deleted.");
            });

            app.Run();
        }
    }
}
