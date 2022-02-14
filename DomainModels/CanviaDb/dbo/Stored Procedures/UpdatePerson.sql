-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdatePerson]
	-- Add the parameters for the stored procedure here
	@lastName nvarchar(MAX),
	@id int
AS
BEGIN
    -- Insert statements for procedure here
	update [Canvia].[dbo].[Person] SET Lastname = @lastName WHERE Id = @id
END