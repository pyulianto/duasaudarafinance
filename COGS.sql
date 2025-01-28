SELECT
	SUM(Amount)
FROM tblTransaction
WHERE TransItemId IN (
	SELECT
		id
	FROM tblTransactionItem
	WHERE IsIn=1

)
AND CONVERT(varchar(8),DateTrans,112) < '20250101'
AND CONVERT(varchar(8),DateTrans,112) >= '20240101'



SELECT
	Bulan,
	SUM(Amount) AS Amount
FROM (

	SELECT
		CONVERT(varchar(6),DateTrans,112) AS Bulan,
		Amount
	FROM tblTransaction
	WHERE TransItemId IN (
		SELECT
			id
		FROM tblTransactionItem
		WHERE IsIn=1

	)
	AND CONVERT(varchar(8),DateTrans,112) < '20250101'
	AND CONVERT(varchar(8),DateTrans,112) >= '20240101'

)tbl1
GROUP BY Bulan
ORDER BY Bulan
