namespace DataMigration
{
    internal static class SqlScripts
    {
        public static string ZeroDb()
        {
            return
                @"
                --
                DECLARE @SQL nvarchar(max) = '';

                -- check constraints
                SET @SQL = '';
                SELECT @SQL += 'ALTER TABLE ['+SCHEMA_NAME([schema_id])+'].['+OBJECT_NAME([parent_object_id])+'] DROP CONSTRAINT ['+name+'];
                ' FROM sys.check_constraints;
                EXEC (@SQL);

                -- foreign keys
                SET @SQL = '';
                SELECT @SQL += 'ALTER TABLE ['+SCHEMA_NAME([schema_id])+'].['+OBJECT_NAME(parent_object_id)+'] DROP CONSTRAINT ['+[name]+'];
                ' FROM sys.foreign_keys;
                EXEC (@SQL);

                -- stored procedures
                SET @SQL = '';
                SELECT @SQL += 'DROP PROCEDURE ['+SCHEMA_NAME([schema_id])+'].['+[name]+'];
                ' FROM sys.procedures WHERE ([type] IN ('P','X'));
                EXEC (@SQL);

                -- functions
                SET @SQL = '';
                
                SELECT @SQL += N' DROP FUNCTION ' + QUOTENAME(SCHEMA_NAME(schema_id)) + N'.' + QUOTENAME(name) 
                    FROM sys.objects WHERE type_desc LIKE '%FUNCTION%';
                EXEC (@SQL);

                -- views (except ones that ship from Microsoft)
                SET @SQL = '';
                SELECT @SQL += 'DROP VIEW ['+SCHEMA_NAME([schema_id])+'].['+[name]+'];
                ' FROM sys.views WHERE ([is_ms_shipped] = 0);
                EXEC (@SQL);

                -- turning off system versioned tables
                SET @SQL = '';
                SELECT @SQL += 'ALTER TABLE ['+SCHEMA_NAME([schema_id])+'].['+[name]+'] SET ( SYSTEM_VERSIONING = OFF)
                ' FROM sys.tables WHERE ([temporal_type] = 2);
                EXEC (@SQL);

                -- tables
                SET @SQL = '';
                SELECT @SQL += 'DROP TABLE ['+SCHEMA_NAME([schema_id])+'].['+[name]+'];
                ' FROM sys.tables;
                EXEC (@SQL);

                -- User Defined Types
                SET @SQL = '';
                SELECT @SQL += 'DROP TYPE ['+SCHEMA_NAME([schema_id])+'].['+[name]+'];
                ' FROM sys.types WHERE ([is_user_defined] = 1);
                EXEC (@SQL);
                ";
        }
    }
}