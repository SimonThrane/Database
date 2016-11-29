namespace GDL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CharacteristicContainers",
                c => new
                    {
                        CharacteristicContainerId = c.Int(nullable: false, identity: true),
                        timestamp = c.DateTime(nullable: false),
                        version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CharacteristicContainerId);
            
            CreateTable(
                "dbo.Appartmentcharacteristics",
                c => new
                    {
                        AppartmentId = c.Int(nullable: false, identity: true),
                        No = c.Int(nullable: false),
                        Size = c.Single(nullable: false),
                        Floor = c.Int(nullable: false),
                        CharacteristicContainer_CharacteristicContainerId = c.Int(),
                    })
                .PrimaryKey(t => t.AppartmentId)
                .ForeignKey("dbo.CharacteristicContainers", t => t.CharacteristicContainer_CharacteristicContainerId)
                .Index(t => t.CharacteristicContainer_CharacteristicContainerId);
            
            CreateTable(
                "dbo.Sensorcharacteristics",
                c => new
                    {
                        sensorId = c.Int(nullable: false, identity: true),
                        calibrationCoeff = c.String(),
                        description = c.String(),
                        calibrationDate = c.DateTime(nullable: false),
                        externalRef = c.String(),
                        unit = c.String(),
                        calibrationEquation = c.String(),
                        CharacteristicContainer_CharacteristicContainerId = c.Int(),
                    })
                .PrimaryKey(t => t.sensorId)
                .ForeignKey("dbo.CharacteristicContainers", t => t.CharacteristicContainer_CharacteristicContainerId)
                .Index(t => t.CharacteristicContainer_CharacteristicContainerId);
            
            CreateTable(
                "dbo.ReadingContainers",
                c => new
                    {
                        ReadingContainerId = c.Int(nullable: false, identity: true),
                        version = c.Int(nullable: false),
                        timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ReadingContainerId);
            
            CreateTable(
                "dbo.Readings",
                c => new
                    {
                        ReadingId = c.Int(nullable: false, identity: true),
                        sensorId = c.Int(nullable: false),
                        appartmentId = c.Int(nullable: false),
                        value = c.Single(nullable: false),
                        timestamp = c.DateTime(nullable: false),
                        ReadingContainer_ReadingContainerId = c.Int(),
                    })
                .PrimaryKey(t => t.ReadingId)
                .ForeignKey("dbo.ReadingContainers", t => t.ReadingContainer_ReadingContainerId)
                .Index(t => t.ReadingContainer_ReadingContainerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Readings", "ReadingContainer_ReadingContainerId", "dbo.ReadingContainers");
            DropForeignKey("dbo.Sensorcharacteristics", "CharacteristicContainer_CharacteristicContainerId", "dbo.CharacteristicContainers");
            DropForeignKey("dbo.Appartmentcharacteristics", "CharacteristicContainer_CharacteristicContainerId", "dbo.CharacteristicContainers");
            DropIndex("dbo.Readings", new[] { "ReadingContainer_ReadingContainerId" });
            DropIndex("dbo.Sensorcharacteristics", new[] { "CharacteristicContainer_CharacteristicContainerId" });
            DropIndex("dbo.Appartmentcharacteristics", new[] { "CharacteristicContainer_CharacteristicContainerId" });
            DropTable("dbo.Readings");
            DropTable("dbo.ReadingContainers");
            DropTable("dbo.Sensorcharacteristics");
            DropTable("dbo.Appartmentcharacteristics");
            DropTable("dbo.CharacteristicContainers");
        }
    }
}
