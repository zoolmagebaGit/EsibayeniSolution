using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using EsibayeniSolution.Models;
[assembly: OwinStartupAttribute(typeof(EsibayeniSolution.Startup))]
namespace EsibayeniSolution
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //CreateUserAndRoles();
            CreateRole();
            CreateUser();
        }
        //public void CreateUserAndRoles()
        //{
        //    ApplicationDbContext context = new ApplicationDbContext();
        //    var roleMananger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
        //    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>());
        //    if (!roleMananger.RoleExists("SuperAadmin"))
        //    {//****create superadmin role
        //        var role = new IdentityRole("SuperAadmin");
        //        roleMananger.Create(role);
        //        //create default user
        //        var user = new ApplicationUser();
        //        user.UserName = "mageba123@gmail.com";
        //        user.Email = "mageba123@gmail.com";
        //        string pwd = "Password@2019";

        //        var newuser = userManager.Create(user, pwd);
        //        if (newuser.Succeeded)
        //        {
        //            userManager.AddToRole(user.Id, "SuperAadmin");
        //        }

        //    }


        //}
    }
}
