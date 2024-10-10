Declare @userid as int = 42;
WITH ThisWeek AS (
    SELECT
        [status],
        ISNULL(COUNT(r1.requestID),0) AS RequestCount
    FROM
        [dbo].[Request] r1
    WHERE
        [type] <> 'T' and
        [createdBy] = @userid and
        [createdDate] >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) -- Beginning of this week
        AND [createdDate] < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) -- Beginning of next week
    GROUP BY
        [status]
),
LastWeek AS (
    SELECT
        [status],
        ISNULL(COUNT(r2.requestID),0)AS RequestCountLastWeek
    FROM
        [dbo].[Request] r2
    WHERE
        [type] <> 'T' and
        [createdBy] = @userid and
        [createdDate] >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) - 1, 0) -- Beginning of last week
        AND [createdDate] < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) -- Beginning of this week
    GROUP BY
        [status]
)
SELECT
    COALESCE(tw.[status], 'Completed') AS [status],
    COALESCE(tw.RequestCount, 0) AS RequestCount,
    COALESCE(lw.RequestCountLastWeek, 0) AS RequestCountLastWeek,
    IIF(COALESCE(lw.RequestCountLastWeek, 0) = 0, 100, (COALESCE(tw.RequestCount, 0) - COALESCE(lw.RequestCountLastWeek, 0)) * 100.0 / (COALESCE(lw.RequestCountLastWeek, 0) + COALESCE(tw.RequestCount, 0))) AS PercentageChange
FROM
    (VALUES ('Completed'), ('Verification'), ('New'), ('Incomplete')) AS StatusList([status])
LEFT JOIN
    ThisWeek tw ON StatusList.[status] = tw.[status]
LEFT JOIN
    LastWeek lw ON StatusList.[status] = lw.[status];


DECLARE @userid AS INT = 42;

WITH StatusList AS (
    SELECT 'Completed' AS [status]
    UNION ALL
    SELECT 'Ongoing' -- Combined 'Published' and 'Verification'
    UNION ALL
    SELECT 'New'
    UNION ALL
    SELECT 'Incomplete'
),

ThisWeek AS (
    SELECT
        CASE WHEN [status] IN ('Published', 'Verification') THEN 'Ongoing' ELSE [status] END AS [status],
        COUNT(*) AS RequestCount
    FROM
        [dbo].[Request]
    WHERE
        [type] <> 'T' AND
        ([createdBy] = @userid OR [assignedTo] = @userid) AND
        [createdDate] >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) -- Beginning of this week
        AND [createdDate] < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) + 1, 0) -- Beginning of next week
    GROUP BY
        CASE WHEN [status] IN ('Published', 'Verification') THEN 'Ongoing' ELSE [status] END
),
LastWeek AS (
    SELECT
        CASE WHEN [status] IN ('Published', 'Verification') THEN 'Ongoing' ELSE [status] END AS [status],
        COUNT(*) AS RequestCountLastWeek
    FROM
        [dbo].[Request]
    WHERE
        [type] <> 'T' AND
        ([createdBy] = @userid OR [assignedTo] = @userid) AND
        [createdDate] >= DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()) - 1, 0) -- Beginning of last week
        AND [createdDate] < DATEADD(WEEK, DATEDIFF(WEEK, 0, GETDATE()), 0) -- Beginning of this week
    GROUP BY
        CASE WHEN [status] IN ('Published', 'Verification') THEN 'Ongoing' ELSE [status] END
)

SELECT
    sl.[status],
    COALESCE(tw.RequestCount, 0) AS RequestCount,
    COALESCE(lw.RequestCountLastWeek, 0) AS RequestCountLastWeek,
    IIF(COALESCE(lw.RequestCountLastWeek, 0) = 0, 100, (COALESCE(tw.RequestCount, 0) - COALESCE(lw.RequestCountLastWeek, 0)) * 100.0 / (COALESCE(lw.RequestCountLastWeek, 0) + COALESCE(tw.RequestCount, 0))) AS PercentageChange
FROM
    StatusList sl
LEFT JOIN
    ThisWeek tw ON sl.[status] = tw.[status]
LEFT JOIN
    LastWeek lw ON sl.[status] = lw.[status];
    Select * from request where status = 'completed'
    UPDATE [dbo].[Request] SET [status] = 'incomplete' WHERE status <> 'completed' and GETDATE() > [dueDate];