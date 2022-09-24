﻿
CREATE FUNCTION tSQLt.Private_GetFormattedErrorInfo()
RETURNS TABLE
AS
RETURN
  SELECT 'Message: ' + ISNULL(ERROR_MESSAGE(),'<NULL>') + ' | Procedure: ' + ISNULL(ERROR_PROCEDURE(),'<NULL>') + ISNULL(' (' + CAST(ERROR_LINE() AS NVARCHAR(MAX)) + ')','') + ' | Severity, State: ' + ISNULL(CAST(ERROR_SEVERITY() AS NVARCHAR(MAX)),'<NULL>') + ', ' + ISNULL(CAST(ERROR_STATE() AS NVARCHAR(MAX)), '<NULL>') + ' | Number: ' + ISNULL(CAST(ERROR_NUMBER() AS NVARCHAR(MAX)), '<NULL>') AS FormattedError;
