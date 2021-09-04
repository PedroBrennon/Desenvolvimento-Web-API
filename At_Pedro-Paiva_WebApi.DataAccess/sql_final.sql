-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/01/2018 09:37:31
-- Generated from EDMX file: D:\PEDRO\Visual Studio Projects\At_Pedro-Paiva_WebApi\At_Pedro-Paiva_WebApi.DataAccess\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [WebApiData];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AutorLivro_Autor]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AutorLivro] DROP CONSTRAINT [FK_AutorLivro_Autor];
GO
IF OBJECT_ID(N'[dbo].[FK_AutorLivro_Livro]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AutorLivro] DROP CONSTRAINT [FK_AutorLivro_Livro];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Autor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Autor];
GO
IF OBJECT_ID(N'[dbo].[Livro]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Livro];
GO
IF OBJECT_ID(N'[dbo].[ApplicationUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ApplicationUser];
GO
IF OBJECT_ID(N'[dbo].[AutorLivro]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AutorLivro];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Autor'
CREATE TABLE [dbo].[Autor] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nome] nvarchar(max)  NOT NULL,
    [Sobrenome] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [DtNasc] datetime  NOT NULL
);
GO

-- Creating table 'Livro'
CREATE TABLE [dbo].[Livro] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Titulo] nvarchar(max)  NOT NULL,
    [ISBN] nvarchar(max)  NOT NULL,
    [Ano] datetime  NOT NULL
);
GO

-- Creating table 'ApplicationUser'
CREATE TABLE [dbo].[ApplicationUser] (
    [Id] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AutorLivro'
CREATE TABLE [dbo].[AutorLivro] (
    [Autor_Id] int  NOT NULL,
    [Livro_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Autor'
ALTER TABLE [dbo].[Autor]
ADD CONSTRAINT [PK_Autor]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Livro'
ALTER TABLE [dbo].[Livro]
ADD CONSTRAINT [PK_Livro]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ApplicationUser'
ALTER TABLE [dbo].[ApplicationUser]
ADD CONSTRAINT [PK_ApplicationUser]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Autor_Id], [Livro_Id] in table 'AutorLivro'
ALTER TABLE [dbo].[AutorLivro]
ADD CONSTRAINT [PK_AutorLivro]
    PRIMARY KEY CLUSTERED ([Autor_Id], [Livro_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Autor_Id] in table 'AutorLivro'
ALTER TABLE [dbo].[AutorLivro]
ADD CONSTRAINT [FK_AutorLivro_Autor]
    FOREIGN KEY ([Autor_Id])
    REFERENCES [dbo].[Autor]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Livro_Id] in table 'AutorLivro'
ALTER TABLE [dbo].[AutorLivro]
ADD CONSTRAINT [FK_AutorLivro_Livro]
    FOREIGN KEY ([Livro_Id])
    REFERENCES [dbo].[Livro]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AutorLivro_Livro'
CREATE INDEX [IX_FK_AutorLivro_Livro]
ON [dbo].[AutorLivro]
    ([Livro_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------