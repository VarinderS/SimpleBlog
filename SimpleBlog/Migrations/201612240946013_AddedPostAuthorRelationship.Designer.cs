// <auto-generated />
namespace SimpleBlog.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
    public sealed partial class AddedPostAuthorRelationship : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(AddedPostAuthorRelationship));
        
        string IMigrationMetadata.Id
        {
            get { return "201612240946013_AddedPostAuthorRelationship"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}