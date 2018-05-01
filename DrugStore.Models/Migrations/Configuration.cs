namespace DrugStore.Model.Migrations
{
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DrugContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DrugContext context)
        {
            Database.SetInitializer<DrugContext>(new CreateDatabaseIfNotExists<DrugContext>());
        }
    }
}
