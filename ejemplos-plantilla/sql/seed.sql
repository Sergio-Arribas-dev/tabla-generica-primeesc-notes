-- Crear tabla Employees si no existe
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employees]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Employees] (
        [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
        [Username] NVARCHAR(100) NOT NULL,
        [Salary] DECIMAL(10, 2) NOT NULL,
        [BirthDate] DATETIME2 NULL,
        [EmploymentStatus] NVARCHAR(50) NOT NULL,
        [HasHouse] BIT NOT NULL,
        [StatusColorHex] VARCHAR(7) DEFAULT '#999999',
        [CreatedUtc] DATETIME2 DEFAULT GETUTCDATE()
    );

    -- Índices
    CREATE INDEX IX_Employees_Username ON [dbo].[Employees]([Username]);
    CREATE INDEX IX_Employees_EmploymentStatus ON [dbo].[Employees]([EmploymentStatus]);
    CREATE INDEX IX_Employees_BirthDate ON [dbo].[Employees]([BirthDate]);
END

-- Insertar datos de ejemplo si la tabla está vacía
IF (SELECT COUNT(*) FROM [dbo].[Employees]) = 0
BEGIN
    INSERT INTO [dbo].[Employees] ([Id], [Username], [Salary], [BirthDate], [EmploymentStatus], [HasHouse], [StatusColorHex])
    VALUES
        (NEWID(), 'sergio.arribas', 65000, '1988-03-15', 'Full-time', 1, '#16A34A'),
        (NEWID(), 'maria.garcia', 55000, '1992-07-22', 'Full-time', 0, '#16A34A'),
        (NEWID(), 'juan.fernandez', 45000, '1995-11-30', 'Part-time', 1, '#EAB308'),
        (NEWID(), 'laura.lopez', 72000, '1985-01-10', 'Full-time', 1, '#16A34A'),
        (NEWID(), 'carlos.rodriguez', 50000, '1998-05-20', 'Contract', 0, '#DC2626'),
        (NEWID(), 'ana.martinez', 58000, NULL, 'Full-time', 1, '#16A34A'),
        (NEWID(), 'pedro.sanchez', 42000, '2000-09-12', 'Part-time', 0, '#EAB308'),
        (NEWID(), 'elena.diaz', 68000, '1990-02-28', 'Full-time', 1, '#16A34A');
END
